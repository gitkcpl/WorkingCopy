using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.Enterprise.Data.DataEngine.Expressions;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Reports;
using Konto.Data.Models.Transaction;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Reporting.Para.OrdPara
{
    public partial class RepOrderView : KontoForm
    {
        public bool MultiView { get; set; }
       
        //public string ReportFilterType { get; set; }
        public RepOrderView()
        {
            InitializeComponent();
            this.Load += LedgerParaView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.FormClosed += OrdParaMainView_FormClosed;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;
            this.ledgerGridView.RowStyle += LedgerGridView_RowStyle;
            this.ledgerGridView.RowCellStyle += LedgerGridView_RowCellStyle;
            this.customSimpleButton.Click += CustomSimpleButton_Click;
            this.repGridView1.FocusedRowChanged += RepGridView1_FocusedRowChanged;
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
               new ComboBoxPairs("PurchaseOrder", "PurchaseOrder"),
                new ComboBoxPairs("SalesOrder", "SalesOrder"),
                new ComboBoxPairs("GreyOrder", "GreyOrder")
            };
         

            typeLookUpEdit.Properties.DataSource = cbp;

            this.FirstActiveControl = fDateEdit;


            typeLookUpEdit.EditValueChanged += TypeLookUpEdit_EditValueChanged;
        }

        private void RepGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
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


            using (var db = new KontoContext())
            {
                var cols = db.RepCols.Where(x => x.FileName == rw.FileName).OrderBy(x => x.UserOrder).ToList();
                colsGridControl.DataSource = cols;
            }
            groupOn1Lookupedit.Properties.DataSource = cbg;

            groupOn2LookUpEdit.Properties.DataSource = cbg;

            if (!string.IsNullOrEmpty(rw.LastGroup1))
                groupOn1Lookupedit.EditValue = rw.LastGroup1;
            else
                groupOn1Lookupedit.EditValue = "Month";

            if (!string.IsNullOrEmpty(rw.LastGroup2))
                groupOn2LookUpEdit.EditValue = rw.LastGroup2;
            else
                groupOn2LookUpEdit.EditValue = "None";
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
        private void OrdParaMainView_FormClosed(object sender, FormClosedEventArgs e)
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
                
                    var reptype = db.ReportTypes
                   .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == typeLookUpEdit.EditValue.ToString())
                   .ToList();
                    reportTypeModelBindingSource.DataSource = reptype;

                var rep = reptype.FirstOrDefault();
                var AccList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", rep.VoucherTypeId).ToList();

                ledgerGridControl.DataSource = AccList;
                if (typeLookUpEdit.EditValue.ToString() == "PurchaseOrder")
                {
                    voucherLookup1.VTypeId = VoucherTypeEnum.PurchaseOrder;
                }
                else
                {
                    voucherLookup1.VTypeId = VoucherTypeEnum.SalesOrder;
                }

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

        private void Show_X_Report()
        {

            var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));



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
            string _title =  rw.ReportName + " Register For "
                                          + fDateEdit.DateTime.ToString("dd/MM/yyyy") + " To " + tDateEdit.DateTime.ToString("dd/MM/yyyy");

            xrep.Parameters["comp_id"].Value = KontoGlobals.CompanyId;

            xrep.Parameters["year_id"].Value = KontoGlobals.YearId;

            xrep.Parameters["from_date"].Value = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));

            xrep.Parameters["to_date"].Value = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));

            int _ReportId = SetCheckedParameters(xrep);

            xrep.Parameters["report_id"].Value = _ReportId;

            if (!string.IsNullOrEmpty(branchLookUpEdit1.Text))
            {
                xrep.Parameters["branch_id"].Value = Convert.ToInt32(branchLookUpEdit1.EditValue);
                //_title = _title + " " + branchLookUpEdit1.Text;
            }

            xrep.Parameters["trans_id"].Value = Convert.ToInt32(accLookup1.SelectedValue);

            xrep.Parameters["report_title"].Value = _title;

            xrep.Parameters["group_on1"].Value = groupOn1Lookupedit.EditValue;


            xrep.Parameters["group_on2"].Value = groupOn2LookUpEdit.EditValue;

            xrep.Parameters["ord_status"].Value = challanTypeLookUpEdit.EditValue;
            xrep.Parameters["is_pending"].Value = checkEdit1.Checked;

            xrep.Parameters["vtype_id"].Value = rw.VoucherTypeId;

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
                xrep.Parameters["voucher_id"].Value = voucherLookup1.SelectedValue;


            SetCheckedParameters(xrep);

            colsGridView.UpdateCurrentRow();

            var cols = colsGridView.GetSelectedRows();
            List<int> show_cols = new List<int>();
            using (var db = new KontoContext())
            {
                for (int i = 0; i < colsGridView.RowCount - 1; i++)
                {
                    var row = colsGridView.GetRow(i) as RepColumn;
                    var col = db.RepCols.Find(row.Id);
                    col.Show = row.Show;
                }

                var rept = db.ReportTypes.Find(rw.Id);
                if (rept != null)
                {
                    rept.LastGroup1 = groupOn1Lookupedit.Text;
                    rept.LastGroup2 = groupOn2LookUpEdit.Text;
                }

                db.SaveChanges();
            }

            foreach (int r in cols)
            {
                var cl = colsGridView.GetRow(r) as RepColumn;
                show_cols.Add(cl.UserOrder);
            }

            xrep.Parameters["report_cols"].Value = show_cols.ToArray();

            if (radioGroup1.SelectedIndex == 0)
                xrep.Landscape = false;
            else
                xrep.Landscape = true;

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
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, subGroupGridView, "sub_group", _ReportId));

                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, categoryGridView, "category", _ReportId));
               
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, sizeGridView, "size", _ReportId));

               
                //party
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, ledgerGridView, "party", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, pgGridView1, "party_group", _ReportId));
                
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, agentGridView1, "agent", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, designGridView1, "design", _ReportId));


                _db.ReportParas.AddRange(_paraList);
                _db.SaveChanges();
            }



            return _ReportId;

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

                var tble = _pageReport.Report.Body.ReportItems["Table1"] as GrapeCity.ActiveReports.PageReportModel.Table;
                

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

                    if (itemGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["product"].CurrentValue = "Y";
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
                        doc.Parameters["design"].CurrentValue = "Y";
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
                        doc.Parameters["color"].CurrentValue = "Y";
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
                if (doc.Parameters["VoucherTypeID"] != null)
                    doc.Parameters["VoucherTypeID"].CurrentValue = rep.VoucherTypeId;

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

                if (!string.IsNullOrEmpty(groupOn1Lookupedit.Text))
                {
                    if (doc.Parameters["GroupOn"] != null)
                        doc.Parameters["GroupOn"].CurrentValue = groupOn1Lookupedit.EditValue;
                }

                if (!string.IsNullOrEmpty(groupOn2LookUpEdit.Text))
                {
                    if (doc.Parameters["gp2"] != null)
                        doc.Parameters["gp2"].CurrentValue = groupOn2LookUpEdit.EditValue;
                }

                if (doc.Parameters["OrdStatus"] != null)
                    doc.Parameters["OrdStatus"].CurrentValue = challanTypeLookUpEdit.EditValue;

                if (rep.VoucherTypeId == (int)VoucherTypeEnum.MillIssue && dr.Contains("Details.rdlx") )
                {
                    var vis = new Visibility();
                    vis.Hidden = "true";
                    tble.TableColumns[5].Visibility = vis;

                    var qtyTextbox = tble.TableGroups[0].Header.TableRows[1].TableCells[7].ReportItems["TextBox65"] as GrapeCity.ActiveReports.PageReportModel.TextBox;
                    if (qtyTextbox != null)
                        qtyTextbox.Value = "Meters";
                }

                var frm = new KontoRepViewer(doc);
                frm.Text = "Order Register Preview";
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
                    pg1.Text = "Order Register";
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
                Log.Error(ex, "Order report");
                MessageBox.Show(ex.ToString());
            }

        }
        private void LedgerParaView_Load(object sender, EventArgs e)
        {
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

                var itemlist = db.Products.Where(x => !x.IsDeleted && x.ItemType =="I")
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                .ToList();

                var designlist = db.Products.Where(x => !x.IsDeleted && x.ItemType =="D")
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                .ToList();


                itemGridControl.DataSource = itemlist;
                pgGridControl1.DataSource = pglist;
                designGridControl1.DataSource = designlist;
                agentGridControl1.DataSource = agentlist;
                divLookUpEdit.Properties.DataSource = divList;
                branchLookUpEdit1.Properties.DataSource = branchList;
                colorGridControl.DataSource = colorList;

                var grps = db.PGroups.OrderBy(x => x.GroupName)
                  .Select(x => new BaseLookupDto { DisplayText = x.GroupName, Id = x.Id }).ToList();

                groupGridControl.DataSource = grps;

                var subgrps = db.PSubGroups.OrderBy(x => x.SubName)
                    .Select(x => new BaseLookupDto { DisplayText = x.SubName, Id = x.Id }).ToList();

                subGroupGridControl.DataSource = subgrps;

                var szs = db.SizeModels.OrderBy(x => x.SizeName)
                 .Select(x => new BaseLookupDto { DisplayText = x.SizeName, Id = x.Id }).ToList();

                sizeGridControl.DataSource = szs;

                //category
                var cats = db.CategroyModels.OrderBy(x => x.CatName)
                    .Select(x => new BaseLookupDto { DisplayText = x.CatName, Id = x.Id }).ToList();

                categoryGridControl.DataSource = cats;

                //product types
                var ptyps = db.ProductTypes.OrderBy(x => x.TypeName)
                    .Select(x => new BaseLookupDto { DisplayText = x.TypeName, Id = x.Id }).ToList();

                typeGridControl.DataSource = ptyps;
            }
           


            List<ComboBoxPairs> sts = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("All", "All"),
                new ComboBoxPairs("PENDING", "PENDING"),
                new ComboBoxPairs("APPROVED", "APPROVED"),
                new ComboBoxPairs("CANCELED", "CANCELED"),
                new ComboBoxPairs("CLOSED", "CLOSED"),
                new ComboBoxPairs("REJECTED", "REJECTED"),
            };
            challanTypeLookUpEdit.Properties.DataSource = sts;
            if (string.IsNullOrEmpty(this.ReportFilterType))
                typeLookUpEdit.EditValue = "SalesOrder";
            else
                typeLookUpEdit.EditValue = this.ReportFilterType;
            challanTypeLookUpEdit.EditValue = "All";
        }

       
    }
}
