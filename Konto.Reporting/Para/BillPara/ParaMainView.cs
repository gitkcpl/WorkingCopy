using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Reporting.CustomRep;
using Serilog;
using Syncfusion.Linq;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Reporting.Para.BillPara
{
    public partial class ParaMainView : KontoForm
    {
        public bool MultiView { get; set; }
        
        public ParaMainView()
        {
            InitializeComponent();
            this.Load += LedgerParaView_Load;
            this.FormClosed += ParaMainView_FormClosed;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;
            this.ledgerGridView.RowStyle += LedgerGridView_RowStyle;
            this.ledgerGridView.RowCellStyle += LedgerGridView_RowCellStyle;
            this.customSimpleButton.Click += CustomSimpleButton_Click;
           // this.repGridView1.RowClick += RepGridView1_RowClick;
            this.repGridView1.FocusedRowChanged += RepGridView1_FocusedRowChanged;
            this.tabbedControlGroup2.SelectedPageChanged += TabbedControlGroup2_SelectedPageChanged;
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Sales", "SALE"),
                new ComboBoxPairs("Purchase", "PURCHASE"),
                new ComboBoxPairs("DrCrNote", "DEBIT"),
                new ComboBoxPairs("Sales Return", "SRETURN"),
                new ComboBoxPairs("Purchase Return", "PRETURN"),
                new ComboBoxPairs("General Expense", "GEXPENSE"),
                new ComboBoxPairs("Rcm Entry", "RCM"),
                 new ComboBoxPairs("Gray Purchase", "GPURCHASE"),

            };
            typeLookUpEdit.Properties.DataSource = cbp;

            this.FirstActiveControl = fDateEdit;

            typeLookUpEdit.EditValueChanged += TypeLookUpEdit_EditValueChanged;
        }

        private void RepGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var rw = repGridView1.GetRow(e.FocusedRowHandle) as ReportTypeModel;
            if (rw == null)
                rw = repGridView1.GetRow(0) as ReportTypeModel;
            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>();
            if (rw.Remarks != null)
            {
                var _groupons = rw.Remarks.Split('-');

                foreach (var item in _groupons)
                {
                    cbg.Add(new ComboBoxPairs(item, item));
                }
            }
            groupOnLookUpEdit.Properties.DataSource = cbg;
            groupOnLookUpEdit.EditValue = "None";

            groupOn2LookUpEdit.Properties.DataSource = cbg;
            groupOn2LookUpEdit.EditValue = "None";

        }

        private void TabbedControlGroup2_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            if (e.Page.Name != "layoutControlGroup3") return;

                var ds = groupOnLookUpEdit.Properties.DataSource as List<ComboBoxPairs>;
                if (ds != null && ds.Count > 0) return;
                var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
                if (rw == null)
                    rw = repGridView1.GetRow(0) as ReportTypeModel;
                List<ComboBoxPairs> cbg = new List<ComboBoxPairs>();
                if (rw.Remarks != null)
                {
                    var _groupons = rw.Remarks.Split('-');

                    foreach (var item in _groupons)
                    {
                        cbg.Add(new ComboBoxPairs(item, item));
                    }
                }
                groupOnLookUpEdit.Properties.DataSource = cbg;
                groupOnLookUpEdit.EditValue = "None";
            
        }

        private void RepGridView1_RowClick(object sender, RowClickEventArgs e)
        {
            var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            if (rw == null)
                rw = repGridView1.GetRow(0) as ReportTypeModel;
            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>();
            if (rw.Remarks != null)
            {
                var _groupons = rw.Remarks.Split('-');
              
                foreach (var item in _groupons)
                {
                    cbg.Add(new ComboBoxPairs(item, item));
                }
            }
            groupOnLookUpEdit.Properties.DataSource = cbg;
            groupOnLookUpEdit.EditValue = "None";

            //List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            //{
            //    new ComboBoxPairs("None", "None"),
            //    new ComboBoxPairs("Book", "Book"),
            //    new ComboBoxPairs("Party", "Party"),
            //    new ComboBoxPairs("Agent", "Agent"),
            //    new ComboBoxPairs("Item", "Item"),
            //    new ComboBoxPairs("Party+Item", "Party+Item"),
            //    new ComboBoxPairs("Agent+Party", "Agent+Party"),
            //    new ComboBoxPairs("Book+Party", "Book+Party"),
            //    new ComboBoxPairs("Party+Book", "Party+Book"),

            //};
        }

        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Edit Selected Report", "Edit/New", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                var _db = new KontoContext();
                var frm = new CustomeRepWindow
                {

                    SpParaList = new List<SpParaModel>(
                            _db.SpParas.Where(k => k.SpName == "sales_reg").ToList()),
                    ReportType = typeLookUpEdit.EditValue.ToString(),
                    SPName = "dbo.sales_reg",
                    FileName = "reg\\CustomReport" + typeLookUpEdit.Text,
                    VTypeId = (int)VoucherTypeEnum.Inward

                };
                frm.ShowDialog();
                return;
            }
            if (repGridView1.RowCount == 0) return;
            var rep = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            if (rep == null)
                rep = repGridView1.GetRow(0) as ReportTypeModel;
            var frmd = new KontoArDesignerView();
            frmd.endUserDesigner1._reportName = rep.FileName;
            frmd.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(rep.FileName));
            frmd.Text = "Konto Designer - " + rep.FileName;
            frmd.Show();
            //if (repGridView1.RowCount==0) return;
            //var rep = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            //if (rep == null)
            //    rep = repGridView1.GetRow(0) as ReportTypeModel;
            //var frmd = new KontoArDesignerView();
            //frmd.endUserDesigner1._reportName = rep.FileName;
            //frmd.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(rep.FileName));
            //frmd.Text = "Konto Designer - " + rep.FileName;
            //frmd.Show();
        }

        private void ParaMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void TypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;
                    var reptype = db.ReportTypes
                   .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == typeLookUpEdit.EditValue.ToString())
                   .ToList();
                    reportTypeModelBindingSource.DataSource = reptype;

                var rep = reptype.FirstOrDefault();
                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", rep.VoucherTypeId).ToList();


                var booklist = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "N", rep.VoucherTypeId).ToList();

                ledgerGridControl.DataSource = AccList;
                bookGridControl1.DataSource = booklist;
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
                
                string dr = "";

                var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
                if (rw == null)
                    rw = repGridView1.GetRow(0) as ReportTypeModel;
                dr = rw.FileName;

                
                titleTextEdit.Text = typeLookUpEdit.Text  + " Reg " +  rw.ReportName + " For " + fDateEdit.DateTime.ToString("dd-MM-yyyy") + " To " +
                    tDateEdit.DateTime.ToString("dd-MM-yyyy");

                GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
                _pageReport.Load(new System.IO.FileInfo(dr));

                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                var tble = _pageReport.Report.Body.ReportItems["Table1"] as GrapeCity.ActiveReports.PageReportModel.Table;
                if (checkEdit1.CheckState == CheckState.Checked)
                {
                   
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
                   

                    if (ledgerGridView.SelectedRowsCount > 0)
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

                    if(bookGridView1.SelectedRowsCount > 0)
                    {
                        doc.Parameters["book"].CurrentValue = "Y";
                        foreach (var item in bookGridView1.GetSelectedRows())
                        {
                            var _acc = bookGridView1.GetRow(item) as AccLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "book";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    if (itemGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["item"].CurrentValue = "Y";
                        foreach (var item in itemGridView.GetSelectedRows())
                        {
                            var _acc = itemGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "product";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }

                    _db.ReportParas.AddRange(_paraList);
                    _db.SaveChanges();
                }

 
                if (doc.Parameters["companyid"] != null)
                    doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;

                if (doc.Parameters["yearid"] != null)
                    doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;

                if (doc.Parameters["fromdate"] != null)
                    doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));

                if (doc.Parameters["todate"] != null)
                    doc.Parameters["todate"].CurrentValue = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

                if (doc.Parameters["reportid"] != null)
                    doc.Parameters["reportid"].CurrentValue = _ReportId;

                if (Convert.ToInt32(voucherLookup1.SelectedValue)>0)
                {
                    if (doc.Parameters["voucherid"] != null)
                        doc.Parameters["voucherid"].CurrentValue = Convert.ToInt32(voucherLookup1.SelectedValue) ;
                }
               
                doc.Parameters["keycon"].CurrentValue = KontoGlobals.sqlConnectionString.ConnectionString;

                var reptype = reportTypeModelBindingSource.DataSource as List<ReportTypeModel>;

                var rep = reptype.FirstOrDefault();
                if (doc.Parameters["vtypeid"] != null)
                    doc.Parameters["vtypeid"].CurrentValue = rep.VoucherTypeId;

                if (!string.IsNullOrEmpty(branchLookUpEdit1.Text))
                {
                    if (doc.Parameters["branchid"] != null)
                        doc.Parameters["branchid"].CurrentValue = Convert.ToInt32(branchLookUpEdit1.EditValue);
                }

                if (!string.IsNullOrEmpty(divLookUpEdit.Text))
                {
                    if (doc.Parameters["divid"] != null)
                        doc.Parameters["divid"].CurrentValue = Convert.ToInt32(divLookUpEdit.EditValue) ;
                }

                if (!string.IsNullOrEmpty(titleTextEdit.Text))
                {
                    if (doc.Parameters["report_title"] != null)
                        doc.Parameters["report_title"].CurrentValue = titleTextEdit.Text;
                }
                if (!string.IsNullOrEmpty(footerTextEdit.Text))
                {
                    if (doc.Parameters["report_footer"] != null)
                        doc.Parameters["report_footer"].CurrentValue = footerTextEdit.Text;
                }

                if (!string.IsNullOrEmpty(groupOnLookUpEdit.Text))
                {

                    if (doc.Parameters["GroupOn"] != null)
                    {
                        doc.Parameters["GroupOn"].CurrentValue = groupOnLookUpEdit.EditValue;
                    }
                }

                if (!string.IsNullOrEmpty(groupOn2LookUpEdit.Text))
                {

                    if (doc.Parameters["gp2"] != null)
                    {
                        doc.Parameters["gp2"].CurrentValue = groupOn2LookUpEdit.EditValue;
                    }
                }

                if (rep.VoucherTypeId == (int)VoucherTypeEnum.GrayPurchaseInvoice && dr.Contains("Details.rdlx") )
                {
                    var vis = new Visibility();
                    vis.Hidden = "true";
                    tble.TableColumns[4].Visibility = vis; 
                    tble.TableColumns[5].Visibility = vis;

                    var qtyTextbox = tble.TableGroups[0].Header.TableRows[1].TableCells[7].ReportItems["TextBox69"] as GrapeCity.ActiveReports.PageReportModel.TextBox;
                    if (qtyTextbox != null)
                        qtyTextbox.Value = "Meters";
                }

                    var _tab = this.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Register Print Preview";

                var pg1 = new TabPageAdv();
                pg1.Text = "Register Print";
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
            if (!KontoGlobals.isSysAdm) customSimpleButton.Visible = false;
            using (var db = new KontoContext())
            {
                

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

                //var divList = db.Divisions.Where(x => !x.IsDeleted)
                //                .OrderBy(x => x.DivisionName)
                //                .Select(x => new BaseLookupDto { DisplayText = x.DivisionName, Id = x.Id })
                //                .ToList();

                var divList = (from p in db.CostHeads
                    where p.IsActive && !p.IsDeleted
                    select new BaseLookupDto()
                    {
                        DisplayText = p.HeadName,
                        Id = p.Id
                    }).ToList();

                var branchList = db.Branches.Where(x => !x.IsDeleted)
                                    .OrderBy(x => x.BranchName)
                                    .Select(x => new BaseLookupDto { DisplayText = x.BranchName, Id = x.Id })
                                    .ToList();

                var itemlist = db.Products.Where(x => !x.IsDeleted && x.ItemType =="I")
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                .ToList();
               
                itemGridControl.DataSource = itemlist;
                pgGridControl1.DataSource = pglist;
               
                agentGridControl1.DataSource = agentlist;
                divLookUpEdit.Properties.DataSource = divList;
                branchLookUpEdit1.Properties.DataSource = branchList;
               
            }
           

            if(!string.IsNullOrEmpty(ReportFilterType))
                typeLookUpEdit.EditValue = this.ReportFilterType;
            else if(this.Tag.ToString() == MenuId.Sales_Register.ToString())
                typeLookUpEdit.EditValue = "SALE";
            else if (this.Tag.ToString() == MenuId.Purchase_Register.ToString())
                typeLookUpEdit.EditValue = "PURCHASE";
            else if (this.Tag.ToString() == MenuId.Purchase_Return_Register.ToString())
                typeLookUpEdit.EditValue = "PRETURN";
            else if (this.Tag.ToString() == MenuId.Sales_Return_Register.ToString())
                typeLookUpEdit.EditValue = "SRETURN";
            else if (this.Tag.ToString() == MenuId.Grey_Purchase.ToString())
                typeLookUpEdit.EditValue = "GPURCHASE";
            else if (this.Tag.ToString() == MenuId.Crdr_Register.ToString())
                typeLookUpEdit.EditValue = "DEBIT";

        }

        private void acGroupGridControl_Click(object sender, EventArgs e)
        {

        }
    }
}
