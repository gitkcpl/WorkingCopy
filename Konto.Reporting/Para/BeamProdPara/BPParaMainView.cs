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

namespace Konto.Reporting.Para.BeamProdPara
{
    public partial class BPParaMainView : KontoForm
    {
        public bool MultiView { get; set; }
        //public string ReportFilterType { get; set; }
        public BPParaMainView()
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
               .Where(k => k.IsActive && !k.IsDeleted && k.ReportTypes == "BP")
               .ToList();

                if (reptype.Count == 0) return;
                reportTypeModelBindingSource.DataSource = reptype;

                var rep = reptype.FirstOrDefault();
            }
        }
        private void CustomSimpleButton_Click(object sender, EventArgs e)
        {
            
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

                    if (EmpGridView1.SelectedRowsCount > 0)
                    {
                        doc.Parameters["Emp"].CurrentValue = "Y";
                        foreach (var item in EmpGridView1.GetSelectedRows())
                        {
                            var _acc = EmpGridView1.GetRow(item) as BaseLookupDto;
                            ModelReport = new ReportParaModel();
                            ModelReport.ReportId = _ReportId;
                            ModelReport.ParameterName = "Emp";
                            ModelReport.ParameterValue = _acc.Id;
                            _paraList.Add(ModelReport);
                        }
                    }
                    else
                        doc.Parameters["Emp"].CurrentValue = "N";

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
                //if (!string.IsNullOrEmpty(MacLookUpEdit.Text))
                //{
                //    if (doc.Parameters["machineis"] != null)
                //        doc.Parameters["machineis"].CurrentValue = Convert.ToInt32(MacLookUpEdit.EditValue);
                //}
                if (!string.IsNullOrEmpty(BeamNoTextEdit.Text))
                {
                    if (doc.Parameters["BeamNo"] != null)
                        doc.Parameters["BeamNo"].CurrentValue = Convert.ToInt32(BeamNoTextEdit.EditValue);
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
                if (!string.IsNullOrEmpty(FilterLookUpEdit.Text))
                {
                    if (doc.Parameters["FilterType"] != null)
                        doc.Parameters["FilterType"].CurrentValue = FilterLookUpEdit.EditValue;
                }

                var frm = new KontoRepViewer(doc);
                frm.Text = "Beam Register Preview";
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
                    pg1.Text = "Beam Register";
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
                Log.Error(ex, "Beam Register");
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

                var emplist = db.Emps.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.EmpName)
                                .Select(x => new BaseLookupDto { DisplayText = x.EmpName, Id = x.Id })
                                .ToList();

                var machineList = db.MachineMasters.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.MachineName)
                                .Select(x => new BaseLookupDto { DisplayText = x.MachineName, Id = x.Id })
                                .ToList();
                 
                itemGridControl.DataSource = itemlist;
                EmpGridControl1.DataSource = emplist;
                MachineGridControl.DataSource = machineList;
                divLookUpEdit.Properties.DataSource = divList1;
                pGroupLookUpEdit.Properties.DataSource = pGroupList;
               //MacLookUpEdit.Properties.DataSource = machineList;
                branchLookUpEdit1.Properties.DataSource = branchList;
            }
            List<ComboBoxPairs> cbg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("None", "None"),
                new ComboBoxPairs("Party", "Party"),
                new ComboBoxPairs("Item", "Item"),
                new ComboBoxPairs("Agent", "Agent"),
                new ComboBoxPairs("Voucher", "Voucher"),
                new ComboBoxPairs("Division", "Division"),
                new ComboBoxPairs("Branch", "Branch"),
                new ComboBoxPairs("Party+Item", "Party+Item"),
                new ComboBoxPairs("Agent+Party", "Agent+Party"),
            };

            List<ComboBoxPairs> ftg = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("All", "All"),
                new ComboBoxPairs("PendingLoading", "PendingLoading"),
                new ComboBoxPairs("LoadedBeam", "LoadedBeam"),
                new ComboBoxPairs("CloseBeam", "CloseBeam")
            };
            groupOnLookUpEdit.Properties.DataSource = cbg;
            groupOnLookUpEdit.EditValue = "None";

            FilterLookUpEdit.Properties.DataSource = ftg;
            FilterLookUpEdit.EditValue = "None";
        }
        private void acGroupGridControl_Click(object sender, EventArgs e)
        {

        }
    }
}
