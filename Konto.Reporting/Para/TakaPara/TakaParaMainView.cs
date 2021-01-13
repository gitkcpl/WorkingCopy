using DevExpress.XtraGrid.Views.Grid;
using GrapeCity.ActiveReports.PageReportModel;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Reporting.Para.TakaPara
{
    public partial class TakaParaMainView : KontoForm
    {
        public bool MultiView { get; set; }
        //public string ReportFilterType { get; set; }
        public TakaParaMainView()
        {
            InitializeComponent();
            this.Load += LedgerParaView_Load;
            this.FormClosed += ChlParaMainView_FormClosed;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.fDateEdit.EditValue = KontoGlobals.DFromDate;
            this.tDateEdit.EditValue = KontoGlobals.DToDate; 
            this.customSimpleButton.Click += CustomSimpleButton_Click;
            SetReport();
        }

        private void SetReport()
        {
            using (var db = new KontoContext())
            {

                var reptype = db.ReportTypes
               .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == "TP")
               .ToList();

                if (reptype.Count == 0) return;
                reportTypeModelBindingSource.DataSource = reptype;

                var rep = reptype.FirstOrDefault();
            }
        }
        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Edit Selected Report", "Edit/New", MessageBoxButtons.YesNo) == DialogResult.No)
            //{
            //    var _db = new KontoContext();
            //    var frm = new CustomeRepWindow
            //    {
            //        SpParaList = new List<SpParaModel>(
            //                _db.SpParas.Where(k => k.SpName == "Challan_reg").ToList()),
            //        //    ReportType = typeLookUpEdit.EditValue.ToString(),
            //        SPName = "dbo.Challan_reg",
            //        FileName = "reg\\CustomReport",// + typeLookUpEdit.Text,
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
                    else
                        doc.Parameters["product"].CurrentValue = "N";

                    if (ColorGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["color"].CurrentValue = "Y";
                        foreach (var item in ColorGridView.GetSelectedRows())
                        {
                            var _acc = ColorGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "color";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    else
                        doc.Parameters["color"].CurrentValue = "N";

                    if (DesignGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["design"].CurrentValue = "Y";
                        foreach (var item in DesignGridView.GetSelectedRows())
                        {
                            var _acc = DesignGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "design";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    else
                        doc.Parameters["design"].CurrentValue = "N";

                    if (GradeGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["Grade"].CurrentValue = "Y";
                        foreach (var item in GradeGridView.GetSelectedRows())
                        {
                            var _acc = GradeGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "Grade";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    else
                        doc.Parameters["Grade"].CurrentValue = "N";
                     
                    if (MachineGridView.SelectedRowsCount > 0)
                    {
                        doc.Parameters["machine"].CurrentValue = "Y";
                        foreach (var item in MachineGridView.GetSelectedRows())
                        {
                            var _acc = MachineGridView.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "machine";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    else
                        doc.Parameters["machine"].CurrentValue = "N";

                    if (_paraList.Count > 0)
                    {
                        _db.ReportParas.AddRange(_paraList);
                        _db.SaveChanges();
                    }
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

                if (Convert.ToInt32(voucherLookup1.SelectedValue) > 0)
                {
                    if (doc.Parameters["voucherid"] != null)
                        doc.Parameters["voucherid"].CurrentValue = Convert.ToInt32(voucherLookup1.SelectedValue);
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
                        doc.Parameters["divid"].CurrentValue = Convert.ToInt32(divLookUpEdit.EditValue);
                } 
                if (!string.IsNullOrEmpty(pGroupLookUpEdit.Text))
                {
                    if (doc.Parameters["productgroup"] != null)
                        doc.Parameters["productgroup"].CurrentValue = Convert.ToInt32(pGroupLookUpEdit.EditValue);
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
                        doc.Parameters["GroupOn"].CurrentValue = groupOnLookUpEdit.EditValue;
                }
                if (doc.Parameters["PType"] != null)
                    doc.Parameters["PType"].CurrentValue = (int)ProductTypeEnum.GREY;

                var frm = new KontoRepViewer(doc);
                frm.Text = "Taka Register Preview";
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
                    pg1.Text = "Taka Register";
                    _tab.TabPages.Add(pg1);
                    _tab.SelectedTab = pg1;
                    frm.TopLevel = false;
                    frm.Parent = pg1;
                    frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
                    frm.Show();// = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Taka Register");
                MessageBox.Show(ex.ToString());
            }
        }
        private void LedgerParaView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            { 
                  var divList1 = db.Divisions.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.DivisionName)
                                .Select(x => new ComboBoxPairs { _Key = x.DivisionName, _Value = x.Id.ToString() })
                                .ToList();

                var branchList = db.Branches.Where(x => !x.IsDeleted)
                                    .OrderBy(x => x.BranchName)
                                    .Select(x => new ComboBoxPairs { _Key = x.BranchName, _Value = x.Id.ToString() })
                                    .ToList();
                var pGroupList = db.PGroups.Where(x => !x.IsDeleted)
                             .OrderBy(x => x.GroupName)
                             .Select(x => new ComboBoxPairs { _Key = x.GroupName, _Value = x.Id.ToString() })
                             .ToList();
                
                var itemlist = db.Products.Where(x => !x.IsDeleted && x.PTypeId == (int)ProductTypeEnum.GREY)
                                .OrderBy(x => x.ProductName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                .ToList();

                var colorlist = db.ColorModels.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.ColorName)
                                .Select(x => new BaseLookupDto { DisplayText = x.ColorName, Id = x.Id })
                                .ToList();

                var machineList = db.MachineMasters.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.MachineName)
                                .Select(x => new BaseLookupDto { DisplayText = x.MachineName, Id = x.Id })
                                .ToList();

                var designlist = db.Products.Where(x => !x.IsDeleted && x.ItemType == "D")
                                 .OrderBy(x => x.ProductName)
                                 .Select(x => new BaseLookupDto { DisplayText = x.ProductName, Id = x.Id })
                                 .ToList();

                var gradeList = db.Grades.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.GradeName)
                                .Select(x => new BaseLookupDto { DisplayText = x.GradeName, Id = x.Id })
                                .ToList();
                 
                itemGridControl.DataSource = itemlist;
                ColorGridControl.DataSource = colorlist;
                MachineGridControl.DataSource = machineList;
                DesignGridControl.DataSource = designlist;
                GradeGridControl.DataSource = gradeList;
                divLookUpEdit.Properties.DataSource = divList1;
                pGroupLookUpEdit.Properties.DataSource = pGroupList;
               //MacLookUpEdit.Properties.DataSource = machineList;
                branchLookUpEdit1.Properties.DataSource = branchList;
            }
            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"), 
                new ComboBoxPairs("Product", "Product"),
                new ComboBoxPairs("Color", "Color"),
                new ComboBoxPairs("Machine", "Machine"),
                new ComboBoxPairs("Design", "Design"),
                new ComboBoxPairs("Date", "Date")
            };

            groupOnLookUpEdit.Properties.DataSource = cbg;
            groupOnLookUpEdit.EditValue = "None";
             
        }
        private void acGroupGridControl_Click(object sender, EventArgs e)
        {

        }
    }
}
