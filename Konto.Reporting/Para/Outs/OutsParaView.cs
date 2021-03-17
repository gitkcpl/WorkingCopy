using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports.PageReportModel;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Outs
{
    public partial class OutsParaView : KontoForm
    {
        public bool MultiView { get; set; }
        
        public OutsParaView()
        {
            InitializeComponent();
            this.Load += LedgerParaView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;
            this.pFDateEdit.EditValue = KontoGlobals.DFromDate;
            this.pTDateEdit.EditValue = KontoGlobals.DToDate;
            this.ledgerGridView.RowStyle += LedgerGridView_RowStyle;
            this.ledgerGridView.RowCellStyle += LedgerGridView_RowCellStyle;
            this.customSimpleButton.Click += CustomSimpleButton_Click;

            List<ComboBoxPairs> cbpp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Receivable", "Receivable"),
                new ComboBoxPairs("Payable", "Payable"),

            };
            typeLookUpEdit.Properties.DataSource = cbpp;

            this.FirstActiveControl = fDateEdit;

            typeLookUpEdit.EditValueChanged += TypeLookUpEdit_EditValueChanged;
        }
        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {
            if (repGridView1.RowCount == 0) return;
            var rep = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            if (rep == null)
                rep = repGridView1.GetRow(0) as ReportTypeModel;
            var frmd = new KontoArDesignerView();
            frmd.endUserDesigner1._reportName = rep.FileName;
            frmd.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(rep.FileName));
            frmd.Text = "Konto Designer - " + rep.FileName;
            frmd.Show();
        }
        private void TypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;
                string vtyp ="N" ;

                if (typeLookUpEdit.EditValue.ToString() == "Receivable")
                    vtyp = "R";
                else
                    vtyp = "P";

                    var reptype = db.ReportTypes
                   .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == typeLookUpEdit.EditValue.ToString())
                   .ToList();
                    reportTypeModelBindingSource.DataSource = reptype;

                var bookList = new List<BaseLookupDto>();
                if (typeLookUpEdit.EditValue.ToString() == "Receivable")
                {
                    bookList = (from ac in db.Accs
                                join pd in db.AccBals on ac.Id equals pd.AccId into join_pd
                                from pd in join_pd.DefaultIfEmpty()
                                join ag in db.AcGroupModels on pd.GroupId equals ag.Id
                                where pd.CompId == KontoGlobals.CompanyId && pd.YearId == KontoGlobals.YearId
                                && ag.Nature == "TRADING INCOME" && !ac.IsDeleted
                                orderby ac.AccName
                                select new BaseLookupDto { DisplayText = ac.AccName, Id = ac.Id }
                                 ).ToList();
                }
                else
                {
                    bookList = (from ac in db.Accs
                                join pd in db.AccBals on ac.Id equals pd.AccId into join_pd
                                from pd in join_pd.DefaultIfEmpty()
                                join ag in db.AcGroupModels on pd.GroupId equals ag.Id
                                where pd.CompId == KontoGlobals.CompanyId && pd.YearId == KontoGlobals.YearId
                                && (ag.Nature == "TRADING EXPENSE" || ag.Nature == "EXPENSE") && !ac.IsDeleted
                                orderby ac.AccName
                                select new BaseLookupDto { DisplayText = ac.AccName, Id = ac.Id }
                                 ).ToList();
                }

                    
                booksGridControl.DataSource = bookList;

                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4}," +
                    "@fillparty={5},@vouchertypeid={6},@outstype={7}"
                      , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", 0, vtyp).ToList();

                ledgerGridControl.DataSource = AccList;
            }
        }

        private void LedgerGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (e.RowHandle == gridView.FocusedRowHandle && e.Column == gridView.FocusedColumn)
            {
                e.Appearance.BackColor = gridView.Appearance.FocusedCell.BackColor;
            }
        }

        private void LedgerGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(view.PaintAppearance.SelectedRow);
            else
                e.Appearance.Assign(view.PaintAppearance.Row);
            e.HighPriority = true;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (this.MultiView)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            ShowReport();

        }
        private void ShowReport()
        {

            if(repGridView1.RowCount ==0)
            {
                MessageBox.Show("No Report Exist from Preview..");
                return;
            }
            try
            {
                GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
                string dr = "";

                var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
                if (rw == null)
                    rw = repGridView1.GetRow(0) as ReportTypeModel;
                dr = rw.FileName;


                _pageReport.Load(new System.IO.FileInfo(dr));

                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                if (checkEdit1.CheckState == CheckState.Checked)
                {
                    var tble = _pageReport.Report.Body.ReportItems["Table1"] as GrapeCity.ActiveReports.PageReportModel.Table;
                    if (tble == null) return;
                    var grp = tble.TableGroups[0] as TableGroup;
                    grp.Grouping.PageBreakAtEnd = true;
                }

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                var _paraList = new List<ReportParaModel>();
                int _ReportId = 0;
                using (var _db = new KontoContext())
                {
                    var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                    _ReportId = reportid + 1;

                    ReportParaModel ModelReport;

                    if (ledgerGridView.SelectedRowsCount > 1)
                    {
                        doc.Parameters["party"].CurrentValue = "Y";
                        foreach (var item in ledgerGridView.GetSelectedRows())
                        {
                            var _acc = ledgerGridView.GetRow(item) as AccLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "party";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    else
                    {
                        doc.Parameters["party"].CurrentValue = "N";
                        if (ledgerGridView.GetSelectedRows().Count() == 1)
                        {
                            var _acc = ledgerGridView.GetRow(ledgerGridView.GetSelectedRows()[0]) as AccLookupDto;
                            doc.Parameters["partyid"].CurrentValue = _acc.Id;
                        }
                    }

                    //ledger Group
                    if (acGroupGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["acgroup"].CurrentValue = "Y";
                        foreach (var item in acGroupGridView.GetSelectedRows())
                        {
                            var _grp = acGroupGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "acgroup";
                            ModelReport.ParameterValue = _grp.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["acgroup"].CurrentValue = "N";
                    }

                    // agent
                    if (agentGridView1.SelectedRowsCount > 0)
                    {
                        doc.Parameters["agent"].CurrentValue = "Y";
                        var SelectedAgent = agentGridView1.GetSelectedRows();
                        foreach (var item in SelectedAgent)
                        {
                            var agp = agentGridView1.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "agent";
                            ModelReport.ParameterValue = agp.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["agent"].CurrentValue = "N";
                    }

                    //party group
                    if (pgGridView1.SelectedRowsCount > 0)
                    {
                        doc.Parameters["partygroup"].CurrentValue = "Y";
                        var SelectedPg = pgGridView1.GetSelectedRows();
                        foreach (var item in SelectedPg)
                        {
                            var agp = pgGridView1.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "pgroup";
                            ModelReport.ParameterValue = agp.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["partygroup"].CurrentValue = "N";
                    }

                    //salesman
                    if (empGridView1.SelectedRowsCount > 0)
                    {
                        doc.Parameters["emp"].CurrentValue = "Y";
                        var SelectedEmp = empGridView1.GetSelectedRows();
                        foreach (var item in SelectedEmp)
                        {
                            var agp = empGridView1.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "emp";
                            ModelReport.ParameterValue = agp.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["emp"].CurrentValue = "N";
                    }

                    //books
                    if (booksGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["book"].CurrentValue = "Y";
                        var Selectedbooks = booksGridView.GetSelectedRows();
                        foreach (var item in Selectedbooks)
                        {
                            var agp = booksGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "book";
                            ModelReport.ParameterValue = agp.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["book"].CurrentValue = "N";
                    }

                    //city
                    if (cityGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["city"].CurrentValue = "Y";
                        var Selectedcity = cityGridView.GetSelectedRows();
                        foreach (var item in Selectedcity)
                        {
                            var agp = cityGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "city";
                            ModelReport.ParameterValue = agp.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["city"].CurrentValue = "N";
                    }

                    //area
                    if (areaGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["area"].CurrentValue = "Y";
                        var SelectedArea = areaGridView.GetSelectedRows();
                        foreach (var item in SelectedArea)
                        {
                            var agp = areaGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "area";
                            ModelReport.ParameterValue = agp.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["area"].CurrentValue = "N";
                    }

                    _db.ReportParas.AddRange(_paraList);
                    _db.SaveChanges();
                }

                if(!string.IsNullOrEmpty(daysTextEdit1.Text))
                {
                    doc.Parameters["duedays"].CurrentValue = daysTextEdit1.Text;
                }
                if (!string.IsNullOrEmpty(groupOnLookUpEdit.Text))
                {

                    if (doc.Parameters["GroupOn"] != null)
                    {
                        doc.Parameters["GroupOn"].CurrentValue = groupOnLookUpEdit.EditValue;
                    }
                }
                doc.Parameters["paid"].CurrentValue = paidLookUpEdit.EditValue.ToString();
                //doc.Parameters["cityid"].CurrentValue = Convert.ToInt32(cityLookup1.SelectedValue);
               // doc.Parameters["areaid"].CurrentValue = Convert.ToInt32(areaLookup1.SelectedValue);
                doc.Parameters["branchid"].CurrentValue = 0;
                doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
                doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
                doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(this.fDateEdit.DateTime.ToString("yyyyMMdd"));
                doc.Parameters["todate"].CurrentValue = Convert.ToInt32(this.tDateEdit.DateTime.ToString("yyyyMMdd"));

                if(doc.Parameters.Contains("payfromdate"))
                    doc.Parameters["payfromdate"].CurrentValue = Convert.ToInt32(this.pFDateEdit.DateTime.ToString("yyyyMMdd"));

                if (doc.Parameters.Contains("payfromdate"))
                    doc.Parameters["paytodate"].CurrentValue = Convert.ToInt32(this.pTDateEdit.DateTime.ToString("yyyyMMdd"));

                if (typeLookUpEdit.EditValue.ToString() == "Receivable")
                {
                    doc.Parameters["nature"].CurrentValue = "R";
                }
                else
                {
                    doc.Parameters["nature"].CurrentValue = "P";
                }

                doc.Parameters["reportid"].CurrentValue = _ReportId;
                doc.Parameters["report_title"].CurrentValue = rw.ReportName + " For The Period " + this.fDateEdit.DateTime.ToShortDateString() + " To " +
                                                              this.tDateEdit.DateTime.ToShortDateString();

                if (doc.Parameters.Contains("range1")) // add parameter for ageing report
                {

                }
             //   doc.Parameters["keycon"].CurrentValue = KontoGlobals.sqlConnectionString.ConnectionString;
                var _tab = this.ParentForm.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Oustanding Print Preview";

                var pg1 = new TabPageAdv();
                pg1.Text = "Oustanding Print";
                _tab.TabPages.Add(pg1);
                
                frm.TopLevel = false;
                frm.Parent = pg1;
                _tab.SelectedTab = pg1;
                //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "ledger report");
                MessageBox.Show(ex.ToString());
            }

        }
        private void LedgerParaView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;
                
                var GroupTrans = db.AcGroupModels
                    .OrderBy(x => x.GroupName)
                    .Where(x=>!x.IsDeleted)
                    .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id }).ToList();

                var agentlist = (from ac in db.Accs
                                 join pd in db.AccBals on ac.Id equals pd.AccId into join_pd
                                 from pd in join_pd.DefaultIfEmpty()
                                 where pd.CompId == KontoGlobals.CompanyId && pd.YearId == KontoGlobals.YearId
                                 && pd.GroupId == 31 && !ac.IsDeleted
                                 orderby ac.AccName
                                 select new BaseLookupDto { DisplayText = ac.AccName, Id = ac.Id }
                                 ).ToList();
                var pglist = db.PartyGroups.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.GroupName)
                                .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id })
                                .ToList();

                var emplist = db.Emps.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.EmpName)
                                .Select(x => new BaseLookupDto { DisplayText = x.EmpName, Id = x.Id })
                                .ToList();

                var citylist = db.Cities.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.CityName)
                                .Select(x => new BaseLookupDto { DisplayText = x.CityName, Id = x.Id })
                                .ToList();

                var arealist = db.Areas.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.AreaName)
                                .Select(x => new BaseLookupDto { DisplayText = x.AreaName, Id = x.Id })
                                .ToList();

             
               
                acGroupGridControl.DataSource = GroupTrans;
                pgGridControl1.DataSource = pglist;
                empGridControl1.DataSource = emplist;
                agentGridControl1.DataSource = agentlist;
                cityGridControl.DataSource = citylist;
                areaGridControl.DataSource = arealist;
            }

            List<ComboBoxPairs> cbo = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("ALL", "ALL"),
                new ComboBoxPairs("Unpaid", "UNPAID"),
                new ComboBoxPairs("Paid", "PAID"),

            };
            paidLookUpEdit.Properties.DataSource = cbo;

            paidLookUpEdit.EditValue = "UNPAID";

            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"),
                new ComboBoxPairs("Group", "Group"),
                new ComboBoxPairs("Agent", "Agent"),

            };

            groupOnLookUpEdit.Properties.DataSource = cbg;
            groupOnLookUpEdit.EditValue = "None";

            typeLookUpEdit.EditValue = "Receivable";
        }

        private void areaGridControl_Click(object sender, EventArgs e)
        {

        }
    }
}
