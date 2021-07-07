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
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using Konto.Core.Shared.Libs;
using Konto.Data.Models.Reports;

namespace Konto.Reporting.Para.BillPara
{
    public partial class RepSalesRegView : KontoForm
    {
        public bool MultiView { get; set; }
        
        public RepSalesRegView()
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
            
           // this.repGridView1.RowClick += RepGridView1_RowClick;
            this.repGridView1.FocusedRowChanged += RepGridView1_FocusedRowChanged;
            this.tabbedControlGroup2.SelectedPageChanged += TabbedControlGroup2_SelectedPageChanged;
            

            this.FirstActiveControl = fDateEdit;
            
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
            groupOnLookUpEdit.Properties.DataSource = cbg;

            groupOn2lookUpEdit.Properties.DataSource = cbg;

            if (!string.IsNullOrEmpty(rw.LastGroup1))
                groupOnLookUpEdit.EditValue = rw.LastGroup1;
            else
                groupOnLookUpEdit.EditValue = "Month";
            if (!string.IsNullOrEmpty(rw.LastGroup2))
                groupOn2lookUpEdit.EditValue = rw.LastGroup2;
            else
                groupOn2lookUpEdit.EditValue = "None";

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

            xrep.Parameters["trans_id"].Value = Convert.ToInt32(accLookup1.SelectedValue);

            xrep.Parameters["report_title"].Value = _title;

            xrep.Parameters["group_on1"].Value = groupOnLookUpEdit.EditValue;


            xrep.Parameters["group_on2"].Value = groupOn2lookUpEdit.EditValue;

           xrep.Parameters["vtype_id"].Value = KontoGlobals.PackageId == (int) PackageType.POS ? VoucherTypeEnum.Pos_Invoice : VoucherTypeEnum.SaleInvoice;

            if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
                xrep.Parameters["voucher_id"].Value = voucherLookup1.SelectedValue;


            SetCheckedParameters(xrep);

            colsGridView.UpdateCurrentRow();

            var cols = colsGridView.GetSelectedRows();
            List<int> show_cols = new List<int>();
            using (var db = new KontoContext())
            {
                for (int i = 0; i < colsGridView.RowCount-1; i++)
                {
                    var row = colsGridView.GetRow(i) as RepColumn;
                    var col = db.RepCols.Find(row.Id);
                    col.Show = row.Show;
                }

                var rept = db.ReportTypes.Find(rw.Id);
                if (rept != null)
                {
                    rept.LastGroup1 = groupOnLookUpEdit.Text;
                    rept.LastGroup2 = groupOn2lookUpEdit.Text;
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
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, brandGridView, "brand", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, sizeGridView, "size", _ReportId));

                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, processGridView, "process", _ReportId));

                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, vendorsGridView, "vendor", _ReportId));

                //party
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, ledgerGridView, "party", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, pgGridView1, "party_group", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, bookGridView1, "book", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, agentGridView1, "agent", _ReportId));
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, designGridView1, "design", _ReportId));


                _db.ReportParas.AddRange(_paraList);
                _db.SaveChanges();
            }



            return _ReportId;

        }
    
        private void LedgerParaView_Load(object sender, EventArgs e)
        {
            if (!KontoGlobals.isSysAdm) customSimpleButton.Visible = false;
            using (var db = new KontoContext())
            {

                db.Database.CommandTimeout = 0;
                var reptype = db.ReportTypes
                    .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == "Sale")
                    .ToList();
                reportTypeModelBindingSource.DataSource = reptype;

                var rep = reptype.FirstOrDefault();

                if (KontoGlobals.PackageId == (int) PackageType.POS)
                {
                    var AccList = db.Database.SqlQuery<AccLookupDto>(
                        "dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", (int)VoucherTypeEnum.Pos_Invoice).ToList();
                    ledgerGridControl.DataSource = AccList;
                }
                else
                {
                    var AccList = db.Database.SqlQuery<AccLookupDto>(
                        "dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", (int)VoucherTypeEnum.SaleInvoice).ToList();
                    ledgerGridControl.DataSource = AccList;
                }

                


                var vendors =  db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                    , (int)LedgerGroupEnum.SUNDRY_CREDITORS, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL", "Y", 0).ToList();

                if (KontoGlobals.PackageId == (int) PackageType.POS)
                {
                    var booklist = db.Database.SqlQuery<AccLookupDto>(
                        "dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL",
                        "N", (int)VoucherTypeEnum.Pos_Invoice).ToList();
                    bookGridControl1.DataSource = booklist;
                }
                else
                {
                    var booklist = db.Database.SqlQuery<AccLookupDto>(
                        "dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                        , 0, KontoGlobals.CompanyId, KontoGlobals.YearId, "N", "ALL",
                        "N", (int)VoucherTypeEnum.SaleInvoice).ToList();
                    bookGridControl1.DataSource = booklist;
                }

                
                vendorsGridControl.DataSource = vendors;
                
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



                var branchList = db.Branches.Where(x => !x.IsDeleted)
                                    .OrderBy(x => x.BranchName)
                                    .Select(x => new BaseLookupDto { DisplayText = x.BranchName, Id = x.Id })
                                    .ToList();

                var itemlist = db.Products.Where(x => !x.IsDeleted && x.ItemType =="I")
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id,Barcode = x.BarCode })
                                .ToList();
               
                itemGridControl.DataSource = itemlist;
                pgGridControl1.DataSource = pglist;
               
                agentGridControl1.DataSource = agentlist;
                branchLookUpEdit1.Properties.DataSource = branchList;

                var desist = db.Products.Where(x => !x.IsDeleted && x.ItemType == "D")
                    .OrderBy(x => x.ProductName)
                    .Select(x => new BaseLookupDto {DisplayText = x.ProductName, Id = x.Id, Barcode = x.BarCode})
                    .ToList();

                designGridControl1.DataSource = desist;

                var grps = db.PGroups.OrderBy(x => x.GroupName)
                    .Select(x => new BaseLookupDto {DisplayText = x.GroupName, Id = x.Id}).ToList();

                groupGridControl.DataSource = grps;

                var subgrps = db.PSubGroups.OrderBy(x => x.SubName)
                    .Select(x => new BaseLookupDto { DisplayText = x.SubName, Id = x.Id }).ToList();

                subGroupGridControl.DataSource = subgrps;

                var brands = db.Brands.OrderBy(x => x.BrandName)
                    .Select(x => new BaseLookupDto { DisplayText = x.BrandName, Id = x.Id }).ToList();

                brandGridControl.DataSource = brands;

                //colors
                var clrs = db.ColorModels.OrderBy(x => x.ColorName)
                    .Select(x => new BaseLookupDto { DisplayText = x.ColorName, Id = x.Id }).ToList();

                colorGridControl.DataSource = clrs;

                //size
                var szs = db.SizeModels.OrderBy(x => x.SizeName)
                    .Select(x => new BaseLookupDto { DisplayText = x.SizeName, Id = x.Id }).ToList();

                sizeGridControl.DataSource = szs;

                //category
                var cats = db.CategroyModels.OrderBy(x => x.CatName)
                    .Select(x => new BaseLookupDto { DisplayText = x.CatName, Id = x.Id }).ToList();

                categoryGridControl.DataSource = cats;

                //job type
                var jts = db.Process.OrderBy(x => x.ProcessName)
                    .Select(x => new BaseLookupDto { DisplayText = x.ProcessName, Id = x.Id }).ToList();

                processGridControl.DataSource = jts;

                //product types
                var ptyps = db.ProductTypes.OrderBy(x=> x.TypeName)
                    .Select(x=> new BaseLookupDto { DisplayText = x.TypeName, Id = x.Id }).ToList();

                typeGridControl.DataSource = ptyps;

            }
          

            //List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            //{
            //    new ComboBoxPairs("None", "None"),
            //    new ComboBoxPairs("Date", "Date"),
            //    new ComboBoxPairs("Month", "Month"),
            //    new ComboBoxPairs("Qtr", "Qtr"),
            //    new ComboBoxPairs("Branch", "Branch"),
            //    new ComboBoxPairs("Party", "Party"),
            //    new ComboBoxPairs("Voucher", "Voucher"),
            //    new ComboBoxPairs("Book", "Books"),
            //    new ComboBoxPairs("Process", "Process"),
            //    new ComboBoxPairs("PartyGroup", "PartyGroup"),
            //    new ComboBoxPairs("Agent", "Agent"),
            //    new ComboBoxPairs("Product", "Product"),
            //    new ComboBoxPairs("Design", "Design"),
            //    new ComboBoxPairs("Category", "Category"),
            //    new ComboBoxPairs("Brand", "Brand"),
            //    new ComboBoxPairs("Color", "Color"),
            //    new ComboBoxPairs("Size", "Size"),
            //    new ComboBoxPairs("Group", "Group"),
            //    new ComboBoxPairs("SubGroup", "SubGroup"),
            //    new ComboBoxPairs("Vendor", "Vendor"),
            //};

           


        }

        private void customGridControl1_Click(object sender, EventArgs e)
        {

        }

        private void RepSalesRegView_Load(object sender, EventArgs e)
        {

        }
    }
}
