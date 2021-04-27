using DevExpress.XtraReports.UI;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using Konto.Core.Shared.Libs;

namespace Konto.Reporting.Para.Stock
{
    public partial class StockParaMainView : KontoForm
    {
        public StockParaMainView()
        {
            InitializeComponent();
            
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate;
            FillLookup();
            //this.branchLookUpEdit.EditValue = KontoGlobals.BranchId;
            //this.divLookUpEdit.EditValue = 1;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            repGridView1.FocusedRowChanged += RepGridView1_FocusedRowChanged;
            this.FormClosed += StockParaMainView_FormClosed;
            this.customSimpleButton.Click += CustomSimpleButton_Click;

            this.FirstActiveControl = fDateEdit;
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
        private void StockParaMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void RepGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var rep = repGridView1.GetRow(e.FocusedRowHandle) as ReportTypeModel;
            if (rep == null) return;
            titleTextEdit.Text = rep.ReportName + " For The Period " + fDateEdit.DateTime.ToString("dd-MM-yyyy") + " To " +  tDateEdit.DateTime.ToString("dd-MM-yyyy");
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            //ShowReport();
            Show_X_Report();
        }
        //private void ShowReport()
        //{
        //    if (repGridView1.RowCount == 0)
        //    {
        //        MessageBox.Show("No Report Exist from Preview..");
        //        return;
        //    }
        //    try
        //    {
        //        GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
        //        string dr = "";
        //        var rw = repGridView1.GetRow(repGridView1.FocusedRowHandle) as ReportTypeModel;
        //        if (rw == null)
        //            rw = repGridView1.GetRow(0) as ReportTypeModel;
        //        dr = rw.FileName;
        //        _pageReport.Load(new System.IO.FileInfo(dr));

        //        GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
        //        _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
        //        var _paraList = new List<ReportParaModel>();
        //        int _ReportId = 0;

        //        doc.Parameters["grade"].CurrentValue = "N";

        //        using (var _db = new KontoContext())
        //        {
        //            var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
        //            _ReportId = reportid + 1;

        //            ReportParaModel ModelReport;

        //            if (productGridView.SelectedRowsCount > 0)
        //            {
        //                var SelectedProduct = productGridView.GetSelectedRows();
        //                doc.Parameters["item"].CurrentValue = "Y";
        //                foreach (var item in SelectedProduct)
        //                {
        //                    var row = productGridView.GetRow(item) as BaseLookupDto;
        //                    ModelReport = new ReportParaModel();
        //                    ModelReport.ReportId = _ReportId;
        //                    ModelReport.ParameterName = "product";
        //                    ModelReport.ParameterValue = row.Id;
        //                    _paraList.Add(ModelReport);
        //                }

        //            }
        //            else
        //            {
        //                doc.Parameters["item"].CurrentValue = "N";
        //            }
        //            if (designGridView1.SelectedRowsCount > 0)
        //            {
        //                doc.Parameters["design"].CurrentValue = "Y";
        //                var SelectedDesign = designGridView1.GetSelectedRows();
        //                foreach (var item in SelectedDesign)
        //                {
        //                    ModelReport = new ReportParaModel();
        //                    ModelReport.ReportId = _ReportId;
        //                    var row = designGridView1.GetRow(item) as BaseLookupDto;
        //                    ModelReport.ParameterName = "design";
        //                    ModelReport.ParameterValue = row.Id;
        //                    _paraList.Add(ModelReport);
        //                }
        //            }
        //            else
        //            {
        //                doc.Parameters["design"].CurrentValue = "N";
        //            }

        //            if (colorGridView2.SelectedRowsCount > 0)
        //            {
        //                var SelectedColor = colorGridView2.GetSelectedRows();
        //                doc.Parameters["color"].CurrentValue = "Y";
        //                foreach (var item in SelectedColor)
        //                {
        //                    ModelReport = new ReportParaModel();
        //                    ModelReport.ReportId = _ReportId;
        //                    var row = colorGridView2.GetRow(item) as BaseLookupDto;
        //                    ModelReport.ParameterName = "color";
        //                    ModelReport.ParameterValue = row.Id;
        //                    _paraList.Add(ModelReport);
        //                }

        //            }
        //            else
        //            {
        //                doc.Parameters["color"].CurrentValue = "N";
        //            }
        //            _db.ReportParas.AddRange(_paraList);
        //            _db.SaveChanges();
        //        }

        //        doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
        //        doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
        //        doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
        //        doc.Parameters["todate"].CurrentValue = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
        //        doc.Parameters["reportid"].CurrentValue = _ReportId;

        //        //if(Convert.ToInt32(pTypeLookup1.SelectedValue) >0)
        //        // doc.Parameters["itemtype"].CurrentValue = Convert.ToInt32(pTypeLookup1.SelectedValue);
        //        //else
        //        //    doc.Parameters["itemtype"].CurrentValue = 0;

        //        if (!string.IsNullOrEmpty(branchLookUpEdit.Text))
        //        {
        //            doc.Parameters["branchid"].CurrentValue = branchLookUpEdit.EditValue; 
        //        }

        //        if (!string.IsNullOrEmpty(divLookUpEdit.Text))
        //        {
        //            if (doc.Parameters["divid"] != null)
        //                doc.Parameters["divid"].CurrentValue = divLookUpEdit.EditValue;
        //        }
        //        //}
        //        //if (Convert.ToInt32(groupLookup1.SelectedValue)!=0)
        //        //{
        //        //    doc.Parameters["itemgroupid"].CurrentValue = Convert.ToInt32(groupLookup1.SelectedValue);
        //        //}

        //        if (!string.IsNullOrEmpty(titleTextEdit.Text))
        //        {
        //            doc.Parameters["report_title"].CurrentValue = titleTextEdit.Text ;
        //        }
        //        if (!string.IsNullOrEmpty(footerTextEdit.Text))
        //        {
        //            doc.Parameters["report_footer"].CurrentValue = footerTextEdit.Text;
        //        }

        //        var _tab = this.ParentForm.Parent.Parent as TabControlAdv;
        //        if (_tab == null) return;
        //        var frm = new KontoRepViewer(doc);
        //        frm.Text = "Stock Print View";

        //        var pg1 = new TabPageAdv();
        //        pg1.Text = "Stock Print";
        //        _tab.TabPages.Add(pg1);
               
        //        frm.TopLevel = false;
        //        frm.Parent = pg1;
        //        _tab.SelectedTab = pg1;
        //        //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
        //        frm.Show();// = true;
        //    }
        //    catch (Exception ex)
        //    {

        //        Log.Error(ex, "Stock Report");
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

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
                MessageBox.Show(@"No Report Exist from Preview..");
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

            if (!string.IsNullOrEmpty(branchLookUpEdit.Text))
            {
                xrep.Parameters["branch_id"].Value = Convert.ToInt32(branchLookUpEdit.EditValue);
                _title = _title + " " + branchLookUpEdit.Text;
            }

            if (!string.IsNullOrEmpty(divLookUpEdit.Text))
            {

                xrep.Parameters["div_id"].Value = Convert.ToInt32(divLookUpEdit.EditValue);
                _title = _title + " " + divLookUpEdit.Text;
            }

            xrep.Parameters["report_title"].Value = _title;

            xrep.Parameters["group_on1"].Value = groupOnLookUpEdit.EditValue;
            
            
            xrep.Parameters["group_on2"].Value = groupOn2LookUpEdit.EditValue;


            xrep.Parameters["pcs_required"].Value = pcsCheckEdit.Checked;
            xrep.Parameters["qty_required"].Value = qtyCheckEdit.Checked;
            xrep.Parameters["code_required"].Value = codeCheckEdit.Checked;
            
            xrep.Parameters["value_required"].Value = valueCheckEdit.Checked;

            string _filter = string.Empty;

            if (!pcsCheckEdit.Checked || !qtyCheckEdit.Checked)
                xrep.Landscape = false;
            else
                xrep.Landscape = true;

            if (!pcsCheckEdit.Checked && !qtyCheckEdit.Checked) // if both false by user
            {
                xrep.Landscape = false;
                xrep.Parameters["qty_required"].Value = true;
                _filter = "StockQty > 0";
            }
            if(pcsCheckEdit.Checked && !qtyCheckEdit.Checked)
                _filter = "StockPcs > 0";

            if (comboBoxEdit1.SelectedIndex == 0)
            {
                _filter = qtyCheckEdit.Checked ? "StockQty > 0" : "StockPcs > 0";
            }
            else if (comboBoxEdit1.SelectedIndex == 1)
            {
                _filter = qtyCheckEdit.Checked ? "StockQty < 0" : "StockPcs < 0";
            }
            else if (comboBoxEdit1.SelectedIndex == 2)
            {
                _filter = qtyCheckEdit.Checked ? "StockQty = 0" : "StockPcs = 0";
            }
            else if (comboBoxEdit1.SelectedIndex == 3)
            {
                _filter = qtyCheckEdit.Checked ? "StockQty <= 0" : "StockPcs <= 0";
            }
            else
            {
                _filter = string.Empty;
            }

            xrep.FilterString = _filter;

            var sqlDataSource = (xrep.DataSource as SqlDataSource);
            sqlDataSource.ConnectionParameters = new CustomStringConnectionParameters(KontoGlobals.sqlConnectionString.ConnectionString);
            sqlDataSource.ConnectionOptions.DbCommandTimeout = 0;



            var frm = new RepXViewer();
            frm.Text = _title;
            frm.RepSource = xrep;
            frm.Show();


        }

        private void FillLookup()
        {
            using(var db = new KontoContext())
            {
                var divs = (from p in db.Divisions
                           where !p.IsDeleted
                           orderby p.DivisionName
                           select new BaseLookupDto { DisplayText = p.DivisionName, Id = p.Id }).ToList();

                var branches = (from p in db.Branches
                           where !p.IsDeleted
                           orderby p.BranchName
                           select new BaseLookupDto { DisplayText = p.BranchName, Id = p.Id }).ToList();

                var reptype = db.ReportTypes
                   .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == "Stock")
                   .ToList();

                var products = (from p in db.Products
                                where !p.IsDeleted && p.ItemType == "I"
                                orderby p.ProductName
                                select new BaseLookupDto { DisplayText = p.ProductName, Id = p.Id }).ToList();

                var designs = (from p in db.Products
                                where !p.IsDeleted && p.ItemType == "D"
                                orderby p.ProductName
                                select new BaseLookupDto { DisplayText = p.ProductName, Id = p.Id }).ToList();

                var colors = (from p in db.ColorModels
                               where !p.IsDeleted
                               orderby p.ColorName
                               select new BaseLookupDto { DisplayText = p.ColorName, Id = p.Id }).ToList();

                var grps = (from p in db.PGroups
                    where !p.IsDeleted
                    orderby p.GroupName
                    select new BaseLookupDto { DisplayText = p.GroupName, Id = p.Id }).ToList();

                var subgrps = (from p in db.PSubGroups
                    where !p.IsDeleted
                    orderby p.SubName
                    select new BaseLookupDto { DisplayText = p.SubName, Id = p.Id }).ToList();

                var brands = (from p in db.Brands
                    where !p.IsDeleted
                    orderby p.BrandName
                    select new BaseLookupDto { DisplayText = p.BrandName, Id = p.Id }).ToList();

                var cats = (from p in db.CategroyModels
                    where !p.IsDeleted
                    orderby p.CatName
                    select new BaseLookupDto { DisplayText = p.CatName, Id = p.Id }).ToList();

                var sizes = (from p in db.SizeModels
                    where !p.IsDeleted
                    orderby p.SizeName
                    select new BaseLookupDto { DisplayText = p.SizeName, Id = p.Id }).ToList();

                var types = (from p in db.ProductTypes
                    where !p.IsDeleted
                    orderby p.TypeName
                    select new BaseLookupDto { DisplayText = p.TypeName, Id = p.Id }).ToList();


                var vendors = (from ac in db.Accs
                        join pd in db.AccBals on ac.Id equals pd.AccId into join_pd
                        from pd in join_pd.DefaultIfEmpty()
                        where pd.CompId == KontoGlobals.CompanyId && pd.YearId == KontoGlobals.YearId
                                                                  && pd.GroupId == 21 && !ac.IsDeleted
                        orderby ac.AccName
                        select new BaseLookupDto { DisplayText = ac.AccName, Id = ac.Id }
                    ).ToList();

                branchLookUpEdit.Properties.DataSource = branches;
                divLookUpEdit.Properties.DataSource = divs;
                repGridControl1.DataSource = reptype;
                productGridControl.DataSource = products;
                designGridControl1.DataSource = designs;
                colorGridControl2.DataSource = colors;
                groupGridControl1.DataSource = grps;
                subGroupGridControl2.DataSource = subgrps;
                brandGridControl3.DataSource = brands;
                categroyGridControl4.DataSource = cats;
                sizeGridControl1.DataSource = sizes;
                vendorGridControl5.DataSource = vendors;
                ptypeGridControl1.DataSource = types;
            }

            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"),
                
                new ComboBoxPairs("Type", "Type"),
                new ComboBoxPairs("Group", "Group"),
                new ComboBoxPairs("SubGroup", "SubGroup"),
                new ComboBoxPairs("Brand", "Brand"),
                new ComboBoxPairs("Category", "Category"),
                new ComboBoxPairs("Color", "Color"),
                new ComboBoxPairs("Size", "Size"),
                new ComboBoxPairs("Vendor", "Vendor"),


            };
            groupOnLookUpEdit.Properties.DataSource = cbg;
            groupOnLookUpEdit.EditValue = "None";

            groupOn2LookUpEdit.Properties.DataSource = cbg;
            groupOn2LookUpEdit.EditValue = "None";
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
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep,productGridView,"item",_ReportId));

                // product types
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, ptypeGridView1, "p_type", _ReportId));

                //group
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, groupGridView1, "group", _ReportId));

                //sub group
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, subGroupGridView2, "sub_group", _ReportId));

                //brand
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, brandGridView3, "brand", _ReportId));

                //color
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, colorGridView2, "color", _ReportId));

                
                //category
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, categroyGridView4, "cat", _ReportId));

                //size
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, sizeGridView1, "size", _ReportId));

                //vendor
                //brand
                _paraList.AddRange(KontoUtils.SetReportParameter(xrep, vendorGridView5, "party", _ReportId));




                _db.ReportParas.AddRange(_paraList);
                _db.SaveChanges();
            }

           
            
            return _ReportId;

        }

    }
}
