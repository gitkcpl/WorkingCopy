using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
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
            this.branchLookUpEdit.EditValue = KontoGlobals.BranchId;
            this.divLookUpEdit.EditValue = 1;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            repGridView1.FocusedRowChanged += RepGridView1_FocusedRowChanged;
            this.FormClosed += StockParaMainView_FormClosed;
            this.customSimpleButton.Click += CustomSimpleButton_Click;
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
            ShowReport();
        }
        private void ShowReport()
        {
            if (repGridView1.RowCount == 0)
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

                GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
                _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
                var _paraList = new List<ReportParaModel>();
                int _ReportId = 0;

                doc.Parameters["grade"].CurrentValue = "N";

                using (var _db = new KontoContext())
                {
                    var reportid = _db.ReportParas.DefaultIfEmpty().Max(k => k == null ? 0 : k.ReportId);
                    _ReportId = reportid + 1;

                    ReportParaModel ModelReport;

                    if (productGridView.SelectedRowsCount > 0)
                    {
                        var SelectedProduct = productGridView.GetSelectedRows();
                        doc.Parameters["item"].CurrentValue = "Y";
                        foreach (var item in SelectedProduct)
                        {
                            var row = productGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "product";
                            ModelReport.ParameterValue = row.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["item"].CurrentValue = "N";
                    }
                    if (designGridView1.SelectedRowsCount > 0)
                    {
                        doc.Parameters["design"].CurrentValue = "Y";
                        var SelectedDesign = designGridView1.GetSelectedRows();
                        foreach (var item in SelectedDesign)
                        {
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            var row = designGridView1.GetRow(item) as BaseLookupDto;
                            ModelReport.ParameterName = "design";
                            ModelReport.ParameterValue = row.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    else
                    {
                        doc.Parameters["design"].CurrentValue = "N";
                    }

                    if (colorGridView2.SelectedRowsCount > 0)
                    {
                        var SelectedColor = colorGridView2.GetSelectedRows();
                        doc.Parameters["color"].CurrentValue = "Y";
                        foreach (var item in SelectedColor)
                        {
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            var row = colorGridView2.GetRow(item) as BaseLookupDto;
                            ModelReport.ParameterName = "color";
                            ModelReport.ParameterValue = row.Id;
                            _paraList.Add(ModelReport);
                        }

                    }
                    else
                    {
                        doc.Parameters["color"].CurrentValue = "N";
                    }
                    _db.ReportParas.AddRange(_paraList);
                    _db.SaveChanges();
                }

                doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;
                doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
                doc.Parameters["fromdate"].CurrentValue = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
                doc.Parameters["todate"].CurrentValue = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
                doc.Parameters["reportid"].CurrentValue = _ReportId;

                if(Convert.ToInt32(pTypeLookup1.SelectedValue) >0)
                 doc.Parameters["itemtype"].CurrentValue = Convert.ToInt32(pTypeLookup1.SelectedValue);
                else
                    doc.Parameters["itemtype"].CurrentValue = 0;

                if (!string.IsNullOrEmpty(branchLookUpEdit.Text))
                {
                    doc.Parameters["branchid"].CurrentValue = branchLookUpEdit.EditValue; 
                }
                if (!string.IsNullOrEmpty(divLookUpEdit.Text))
                {
                    if (doc.Parameters["divid"] != null)
                        doc.Parameters["divid"].CurrentValue = divLookUpEdit.EditValue;
                }
                if (Convert.ToInt32(groupLookup1.SelectedValue)!=0)
                {
                    doc.Parameters["itemgroupid"].CurrentValue = Convert.ToInt32(groupLookup1.SelectedValue);
                }

                if (!string.IsNullOrEmpty(titleTextEdit.Text))
                {
                    doc.Parameters["report_title"].CurrentValue = titleTextEdit.Text ;
                }
                if (!string.IsNullOrEmpty(footerTextEdit.Text))
                {
                    doc.Parameters["report_footer"].CurrentValue = footerTextEdit.Text;
                }

                var _tab = this.ParentForm.Parent.Parent as TabControlAdv;
                if (_tab == null) return;
                var frm = new KontoRepViewer(doc);
                frm.Text = "Stock Print View";

                var pg1 = new TabPageAdv();
                pg1.Text = "Stock Print";
                _tab.TabPages.Add(pg1);
                _tab.SelectedTab = pg1;
                frm.TopLevel = false;
                frm.Parent = pg1;
                frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                frm.Show();// = true;
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Stock Report");
                MessageBox.Show(ex.ToString());
            }
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

                branchLookUpEdit.Properties.DataSource = branches;
                divLookUpEdit.Properties.DataSource = divs;
                repGridControl1.DataSource = reptype;
                productGridControl.DataSource = products;
                designGridControl1.DataSource = designs;
                colorGridControl2.DataSource = colors;

            }
        }


    }
}
