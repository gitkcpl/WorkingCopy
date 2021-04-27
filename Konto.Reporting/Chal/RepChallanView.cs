using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction;
using Konto.Reporting.CustomRep;
using Serilog;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using Konto.Core.Shared.Libs;

namespace Konto.Reporting.Chal
{
    public partial class RepChallanView : KontoForm
    {
        public bool MultiView { get; set; }
        //public string ReportFilterType { get; set; }
        public RepChallanView()
        {
            InitializeComponent();
            this.Load += LedgerParaView_Load;
            this.FormClosed += ChlParaMainView_FormClosed;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;
            this.ledgerGridView.RowStyle += LedgerGridView_RowStyle;
            this.ledgerGridView.RowCellStyle += LedgerGridView_RowCellStyle;
            this.customSimpleButton.Click += CustomSimpleButton_Click;

            this.repGridView1.FocusedRowChanged += RepGridView1_FocusedRowChanged;
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Outward", "SCHALLAN"),
                new ComboBoxPairs("Inward", "PCHALLAN"),
                new ComboBoxPairs("Mill Issue", "MillIssue"),
                new ComboBoxPairs("Job Issue", "JobIssue"),
                new ComboBoxPairs("Mill Receipt", "MillRec"),
                new ComboBoxPairs("Job Receipt", "JobRec"),
                new ComboBoxPairs("Mill Return", "MillRet"),
            };

            //typeLookUpEdit.Properties.DataSource = cbp;

            this.FirstActiveControl = fDateEdit;

            //typeLookUpEdit.EditValueChanged += TypeLookUpEdit_EditValueChanged;
        }

        private void RepGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            List<BaseLookupDto> cols = new List<BaseLookupDto>();
            if (repGridView1.FocusedRowHandle == 0)
            {
                cols.Add(new BaseLookupDto(){DisplayText = "Date",Id=0});
                cols.Add(new BaseLookupDto() { DisplayText = "VoucherNo", Id = 1 });
                cols.Add(new BaseLookupDto() { DisplayText = "ChallanNo", Id = 2 });
                cols.Add(new BaseLookupDto() { DisplayText = "OrderNo", Id = 3 });
                cols.Add(new BaseLookupDto() { DisplayText = "OrderDate", Id = 4 });
                cols.Add(new BaseLookupDto() { DisplayText = "Party", Id = 5 });
                cols.Add(new BaseLookupDto() { DisplayText = "Pcs", Id = 6 });
                cols.Add(new BaseLookupDto() { DisplayText = "Qty", Id =7 });
                cols.Add(new BaseLookupDto() { DisplayText = "Transport", Id = 8 });
                cols.Add(new BaseLookupDto() { DisplayText = "Agent", Id = 9 });
                cols.Add(new BaseLookupDto() { DisplayText = "Remark", Id = 10 });
                
                colsGridControl.DataSource = cols;

                colsGridView.SelectRows(0,2);
                colsGridView.SelectRows(5, 7);
            }
            else if (repGridView1.FocusedRowHandle == 1)
            {
                cols.Add(new BaseLookupDto() { DisplayText = "Date", Id = 0 });
                cols.Add(new BaseLookupDto() { DisplayText = "VoucherNo", Id = 1 });
                cols.Add(new BaseLookupDto() { DisplayText = "ChallanNo", Id = 2 });
                cols.Add(new BaseLookupDto() { DisplayText = "OrderNo", Id = 3 });
                cols.Add(new BaseLookupDto() { DisplayText = "OrderDate", Id = 4 });
                cols.Add(new BaseLookupDto() { DisplayText = "Party", Id = 5 });
                cols.Add(new BaseLookupDto() { DisplayText = "Item", Id = 6 });
                cols.Add(new BaseLookupDto() { DisplayText = "Color", Id = 7 });
                cols.Add(new BaseLookupDto() { DisplayText = "Design", Id = 8 });
                cols.Add(new BaseLookupDto() { DisplayText = "Grade", Id = 9 });
                cols.Add(new BaseLookupDto() { DisplayText = "Lot/Batch", Id = 10 });
                cols.Add(new BaseLookupDto() { DisplayText = "Cut/Cops", Id = 11 });
                cols.Add(new BaseLookupDto() { DisplayText = "Pcs/Box", Id = 12 });
                cols.Add(new BaseLookupDto() { DisplayText = "Qty", Id = 13 });
                cols.Add(new BaseLookupDto() { DisplayText = "Rate", Id = 14 });
                cols.Add(new BaseLookupDto() { DisplayText = "Transport", Id = 15 });
                cols.Add(new BaseLookupDto() { DisplayText = "Agent", Id = 16 });
                cols.Add(new BaseLookupDto() { DisplayText = "Remark", Id = 17 });
                colsGridControl.DataSource = cols;

                colsGridView.SelectRows(0, 2);
                colsGridView.SelectRows(5, 6);
                colsGridView.SelectRows(12, 14);
            }


        }
        private void Show_X_Report()
        {
            var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            int divid = 0;
            int branchid = 0;

            if (fdate < KontoGlobals.FromDate || fdate > KontoGlobals.ToDate)
            {
                MessageBox.Show("From date out of financial date");
                fDateEdit.Focus();
                return;

            }

            if (tdate > KontoGlobals.ToDate || tdate < KontoGlobals.FromDate)
            {
                MessageBox.Show(@"To date out of financial date");
                tDateEdit.Focus();
                return;
            }
            if (repGridView1.RowCount == 0)
            {
                MessageBox.Show(@"No Report Exist for Preview..");
                return;
            }

            var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            string dr = "";

            if (rw == null)
                rw = repGridView1.GetRow(0) as ReportTypeModel;
            dr = rw.FileName;
            string objectToInstantiate = dr + ",Konto.Reporting";
            var objectType = Type.GetType(objectToInstantiate);

            var xrep = Activator.CreateInstance(objectType) as XtraReport;
            if (xrep == null)
            {
                MessageBox.Show(@"Report Does not exist");
                return;
            }
            string _title = rw.ReportName + " Register For "
                                          + fDateEdit.DateTime.ToString("dd/MM/yyyy") + " To " + tDateEdit.DateTime.ToString("dd/MM/yyyy");

            xrep.Parameters["comp_id"].Value = KontoGlobals.CompanyId;

            //xrep.Parameters["year_id"].Value = KontoGlobals.YearId;

            xrep.Parameters["from_date"].Value = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));

            xrep.Parameters["to_date"].Value = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            int _ReportId = SetCheckedParameters(xrep);

            xrep.Parameters["report_id"].Value = _ReportId;

            if (!string.IsNullOrEmpty(branchLookUpEdit1.Text))
            {
                xrep.Parameters["branch_id"].Value = Convert.ToInt32(branchLookUpEdit1.EditValue);
                //_title = _title + " " + branchLookUpEdit1.Text;
            }

            if (!string.IsNullOrEmpty(divLookUpEdit.Text))
            {

                xrep.Parameters["div_id"].Value = Convert.ToInt32(divLookUpEdit.EditValue);
               // _title = _title + " " + divLookUpEdit.Text;
            }

            xrep.Parameters["report_title"].Value = _title;

            xrep.Parameters["group_on1"].Value = gp1LookupEdit.EditValue;


            xrep.Parameters["group_on2"].Value = gp2lookUpEdit.EditValue;

            if (!string.IsNullOrEmpty(challanTypeLookUpEdit.Text))
            {
                xrep.Parameters["challan_type"].Value = challanTypeLookUpEdit.EditValue;
            }

            xrep.Parameters["vtype_id"].Value = VoucherTypeEnum.Inward;

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
                xrep.Parameters["voucher_id"].Value = voucherLookup1.SelectedValue;

            if (Convert.ToInt32(gradeLookup1.SelectedValue) > 0)
                xrep.Parameters["voucher_id"].Value = gradeLookup1.SelectedValue;


            
            var cols = colsGridView.GetSelectedRows();
            List<int> show_cols = new List<int>();
            foreach (int r in cols)
            {
                var cl = colsGridView.GetRow(r) as BaseLookupDto;
                show_cols.Add(cl.Id);
            }

            xrep.Parameters["report_cols"].Value = show_cols.ToArray();

            if (radioGroup1.SelectedIndex == 0)
                xrep.Landscape = false;
            else
                xrep.Landscape = true;
            //string _filter = string.Empty;


            //xrep.FilterString = _filter;

            var sqlDataSource = (xrep.DataSource as SqlDataSource);
            sqlDataSource.ConnectionParameters = new CustomStringConnectionParameters(KontoGlobals.sqlConnectionString.ConnectionString);
            sqlDataSource.ConnectionOptions.DbCommandTimeout = 0;



            var frm = new RepXViewer();
            frm.Text = _title;
            frm.RepSource = xrep;
            frm.Show();


        }

        private int SetCheckedParameters(XtraReport xrep)
        {
            int _ReportId = 0;
            var _paraList = new List<ReportParaModel>();
            using (var _db = new KontoContext())
            {
                var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                _ReportId = reportid + 1;

                // product
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, itemGridView, "item", _ReportId));

                // product types
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, typeGridView, "p_type", _ReportId));

                //group
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, groupGridView, "item_group", _ReportId));

                
                //color
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, colorGridView, "color", _ReportId));


                //party
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, ledgerGridView, "party", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, pgGridView1, "party_group", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, agentGridView1, "Agent", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, designGridView1, "Design", _ReportId));
                

                _db.ReportParas.AddRange(_paraList);
                _db.SaveChanges();
            }



            return _ReportId;

        }
        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {
            //if(MessageBox.Show("Edit Selected Report","Edit/New",MessageBoxButtons.YesNo) == DialogResult.No)
            //{
            //    var _db = new KontoContext();
            //    var frm = new CustomeRepWindow
            //    {
                    
            //        SpParaList = new List<SpParaModel>(
            //                _db.SpParas.Where(k => k.SpName == "Challan_reg").ToList()),
            //        ReportType = typeLookUpEdit.EditValue.ToString(),
            //        SPName = "dbo.Challan_reg",
            //        FileName = "reg\\CustomReport" + typeLookUpEdit.Text,
            //        VTypeId = (int)VoucherTypeEnum.Inward

            //    };
            //    frm.ShowDialog();
            //    return;
            //}
            //if (repGridView1.RowCount == 0) return;
            //var rep = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            //if (rep == null)
            //    rep = repGridView1.GetRow(0) as ReportTypeModel;
            //var frmd = new KontoArDesignerView();
            //frmd.endUserDesigner1._reportName = rep.FileName;
            //frmd.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(rep.FileName));
            //frmd.Text = "Konto Designer - " + rep.FileName;
            //frmd.Show();
        }
        private void ChlParaMainView_FormClosed(object sender, FormClosedEventArgs e)
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
               
                   // var reptype = db.ReportTypes
                   //.Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == typeLookUpEdit.EditValue.ToString())
                   //.ToList();

              //  if (reptype.Count == 0) return;
                  //  reportTypeModelBindingSource.DataSource = reptype;

              //  var rep = reptype.FirstOrDefault();
                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", 0).ToList();

                ledgerGridControl.DataSource = AccList;
                var TransTypeList = new List<TransType>();
                //if (typeLookUpEdit.EditValue.ToString()=="MillIssue")
                //{
                //    TransTypeList = db.transTypes.
                //    Where(k => k.IsActive && !k.IsDeleted &&
                //    (k.Category.ToUpper() == "MILLISSUE" || k.Category == null))
                //    .ToList();

                //}
                challanTypeLookUpEdit.Properties.DataSource = TransTypeList;

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

            Show_X_Report();

        }

        private void ShowReport()
        {

            if(repGridView1.RowCount ==0)
            {
                MessageBox.Show("No Report Exist from Preview..");
                return;
            }

            var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
            string dr = "";

            if (rw == null)
                rw = repGridView1.GetRow(0) as ReportTypeModel;
            dr = rw.FileName;


            if (dr.Substring(dr.Length-3,3) == "mrt")
            {
                ShowStimulSoft(dr);
                return;
            }
            try
            {
                GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
            

                _pageReport.Load(new System.IO.FileInfo(dr));

               // _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;

                var tble = _pageReport.Report.Body.ReportItems["Table1"] as GrapeCity.ActiveReports.PageReportModel.Table;
                //if (checkEdit1.CheckState == CheckState.Checked)
                //{
                   
                //    if (tble == null) return;
                //    var grp = tble.TableGroups[0] as TableGroup;
                //    grp.Grouping.PageBreakAtEnd = true;
                //}


               // _pageReport.Document.LocateDataSource += new GrapeCity.ActiveReports.LocateDataSourceEventHandler(Doc_LocateDataSource);
                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
                //doc.LocateDataSource += Doc_LocateDataSource;
                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
               
                int _ReportId = 0;

              _ReportId=  SetCheckedParameters(doc,null);

 
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

                if (!string.IsNullOrEmpty(gp1LookupEdit.Text))
                {
                    if (doc.Parameters["GroupOn"] != null)
                        doc.Parameters["GroupOn"].CurrentValue = gp1LookupEdit.EditValue;
                }
                if (!string.IsNullOrEmpty(challanTypeLookUpEdit.Text))
                {
                    if (doc.Parameters["ChallanType"] != null)
                        doc.Parameters["ChallanType"].CurrentValue = challanTypeLookUpEdit.EditValue;
                }

                if (rep.VoucherTypeId == (int)VoucherTypeEnum.MillIssue && dr.Contains("Details.rdlx") && !dr.Contains("Pending"))
                {
                    var vis = new Visibility();
                    vis.Hidden = "true";
                    tble.TableColumns[5].Visibility = vis;

                    var qtyTextbox = tble.TableGroups[0].Header.TableRows[1].TableCells[7].ReportItems["TextBox65"] as GrapeCity.ActiveReports.PageReportModel.TextBox;
                    if (qtyTextbox != null)
                        qtyTextbox.Value = "Meters";
                }
                var frm = new KontoRepViewer(doc);
                frm.Text = "Challan Register Preview";
                if (this.ParentForm.Parent == null || this.ParentForm.Parent.Parent == null)
                {

                    frm.WindowState = FormWindowState.Maximized;
                    frm.MinimizeBox = true;
                    frm.MaximizeBox = true;
                    frm.Show();

                }
                else
                {
                    var _tab = this.ParentForm.Parent.Parent as TabControlAdv;
                    if (_tab == null) return;

                    var pg1 = new TabPageAdv();
                    pg1.Text = "Challan Register";
                    _tab.TabPages.Add(pg1);
                  
                    frm.TopLevel = false;
                    frm.Parent = pg1;
                    _tab.SelectedTab = pg1;
                    //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                    frm.Show();// = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "ledger report");
                MessageBox.Show(ex.ToString());
            }

        }

        private int SetCheckedParameters(GrapeCity.ActiveReports.Document.PageDocument doc,
                Stimulsoft.Report.StiReport _rep)
        {
            int _ReportId = 0;
            var _paraList = new List<ReportParaModel>();
            using (var _db = new KontoContext())
            {
                var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
              _ReportId = reportid + 1;

                ReportParaModel ModelReport;

                if (ledgerGridView.SelectedRowsCount > 0)
                {
                    if (doc != null)
                        doc.Parameters["party"].CurrentValue = "Y";
                    else
                      _rep["party"] = "Y";

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

                if (itemGridView.SelectedRowsCount > 0)
                {
                    if (doc != null)
                        doc.Parameters["item"].CurrentValue = "Y";
                    else
                        _rep["item"] = "Y";
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

                if (designGridView1.SelectedRowsCount > 0)
                {
                    if (doc != null)
                        doc.Parameters["design"].CurrentValue = "Y";
                    else
                        _rep["design"] = "Y";

                    foreach (var item in designGridView1.GetSelectedRows())
                    {
                        var _acc = designGridView1.GetRow(item) as BaseLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "design";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }
                if (colorGridView.SelectedRowsCount > 0)
                {
                    if (doc != null)
                        doc.Parameters["color"].CurrentValue = "Y";
                    else
                        _rep["color"] = "Y";

                    foreach (var item in colorGridView.GetSelectedRows())
                    {
                        var _acc = colorGridView.GetRow(item) as BaseLookupDto;
                        ModelReport = new ReportParaModel();
                        ModelReport.ReportId = _ReportId;
                        ModelReport.ParameterName = "color";
                        ModelReport.ParameterValue = _acc.Id;
                        _paraList.Add(ModelReport);
                    }
                }

                _db.ReportParas.AddRange(_paraList);
                _db.SaveChanges();
            }
            return _ReportId;

        }


       private void ShowStimulSoft(string _filename)
        {
            try
            {

                StiReport _rep = new StiReport();
                _rep.Load(_filename);

                ((StiSqlDatabase)_rep.Dictionary.Databases["cnn"]).ConnectionString = KontoGlobals.sqlConnectionString.ConnectionString;

                _rep.Compile();

                int _ReportId= SetCheckedParameters(null,_rep);

                _rep["companyid"] = KontoGlobals.CompanyId;

                _rep["yearid"] = KontoGlobals.YearId;

                 _rep["fromdate"]= Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));

                  _rep["todate"] = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

                  _rep["reportid"] = _ReportId;

                if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
                {
                     _rep["voucherid"] = Convert.ToInt32(voucherLookup1.SelectedValue);
                }

                

                var reptype = reportTypeModelBindingSource.DataSource as List<ReportTypeModel>;

                var rep = reptype.FirstOrDefault();
                
                 _rep["vtypeid"] = rep.VoucherTypeId;

                if (!string.IsNullOrEmpty(branchLookUpEdit1.Text))
                {
                     _rep["branchid"] = Convert.ToInt32(branchLookUpEdit1.EditValue);
                }

                if (!string.IsNullOrEmpty(divLookUpEdit.Text))
                {
                    
                       _rep["divid"] = Convert.ToInt32(divLookUpEdit.EditValue);
                }

                if (!string.IsNullOrEmpty(titleTextEdit.Text))
                {
                    
                    _rep["report_title"] = titleTextEdit.Text;
                }
                if (!string.IsNullOrEmpty(footerTextEdit.Text))
                {
                    
                       _rep["report_footer"] = footerTextEdit.Text;
                }

                if (!string.IsNullOrEmpty(gp1LookupEdit.Text))
                {
                   
                        _rep["GroupOn"]= gp1LookupEdit.EditValue;
                }
                if (!string.IsNullOrEmpty(challanTypeLookUpEdit.Text))
                {
                   _rep["ChallanType"] = challanTypeLookUpEdit.EditValue;
                }

             
                StiOptions.Viewer.ViewerTitle = "Issue Vs Receipt";
                _rep.ShowWithRibbonGUI();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void LedgerParaView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {

                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                    , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", VoucherTypeEnum.Inward).ToList();

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
              
                var divList = db.Divisions.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.DivisionName)
                                .Select(x => new BaseLookupDto { DisplayText = x.DivisionName, Id = x.Id })
                                .ToList();

                var branchList = db.Branches.Where(x => !x.IsDeleted)
                                    .OrderBy(x => x.BranchName)
                                    .Select(x => new BaseLookupDto { DisplayText = x.BranchName, Id = x.Id })
                                    .ToList();

                var colorList = db.ColorModels.Where(x => !x.IsDeleted)
                                    .OrderBy(x => x.ColorName)
                                    .Select(x => new BaseLookupDto { DisplayText = x.ColorName, Id = x.Id })
                                    .ToList();

                var itemlist = db.Products.Where(x => !x.IsDeleted && x.PTypeId == (int)ProductTypeEnum.GREY)
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                .ToList();

                var designlist = db.Products.Where(x => !x.IsDeleted && x.ItemType =="D")
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                .ToList();

                var groups = db.PGroups.Where(x => !x.IsDeleted)
                    .OrderBy(x => x.GroupName)
                    .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id })
                    .ToList();

                var types = db.ProductTypes.Where(x => !x.IsDeleted)
                    .OrderBy(x => x.TypeName)
                    .Select(x => new BaseLookupDto { DisplayText = x.TypeName, Id = x.Id })
                    .ToList();


                var reptype = db.ReportTypes
               .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes =="PCHALLAN")
               .ToList();

                
                 reportTypeModelBindingSource.DataSource = reptype;

                

                var TransTypeList = new List<TransType>();
                
                    TransTypeList = db.transTypes.
                    Where(k => k.IsActive && !k.IsDeleted && k.Category=="Inward"
                   )
                    .ToList();

                

                challanTypeLookUpEdit.Properties.DataSource = TransTypeList;

                pgGridControl1.DataSource = pglist;
                agentGridControl1.DataSource = agentlist;

                ledgerGridControl.DataSource = AccList;

                divLookUpEdit.Properties.DataSource = divList;
                branchLookUpEdit1.Properties.DataSource = branchList;

                itemGridControl.DataSource = itemlist;
                designGridControl1.DataSource = designlist;
                colorGridControl.DataSource = colorList;
                groupGridControl1.DataSource = groups;
                typeGridControl.DataSource = types;
                
            }
            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"),
                new ComboBoxPairs("Party", "Party"),
                new ComboBoxPairs("Agent", "Agent"),
                new ComboBoxPairs("PartyGroup", "PartyGroup"),
                new ComboBoxPairs("Voucher", "Voucher"),
                new ComboBoxPairs("Division", "Division"),
                new ComboBoxPairs("Branch", "Branch"),
            };

            gp1LookupEdit.Properties.DataSource = cbg;
            gp1LookupEdit.EditValue = "None";
            gp2lookUpEdit.Properties.DataSource = cbg;
            gp2lookUpEdit.EditValue = "None";


            //if (!string.IsNullOrEmpty(ReportFilterType))
            //    typeLookUpEdit.EditValue = this.ReportFilterType;
            //else
            //{
            //    if(this.Tag.ToString() == MenuId.Grn_Report.ToString())
            //    typeLookUpEdit.EditValue = "PCHALLAN";
            //    else if (this.Tag.ToString() == MenuId.Sales_Challan_Report.ToString())
            //        typeLookUpEdit.EditValue = "SCHALLAN";
            //    else if (this.Tag.ToString() == MenuId.Grey_Issue_To_Mill_Report.ToString())
            //        typeLookUpEdit.EditValue = "MillIssue";
            //    else if (this.Tag.ToString() == MenuId.Job_Issue.ToString())
            //        typeLookUpEdit.EditValue = "JobIssue";
            //    else if (this.Tag.ToString() == MenuId.Job_Receipt.ToString())
            //        typeLookUpEdit.EditValue = "JobRec";
            //}
        }

        private void acGroupGridControl_Click(object sender, EventArgs e)
        {
            
        }

        private void customGridControl2_Click(object sender, EventArgs e)
        {

        }
    }
}
