using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Reports;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Outs
{
    public partial class OutsMainView : KontoForm
    {
        private string  DetailsLayoutFile { get; set; }
        private string SummaryLayoutFile { get; set; }
        private string AgeingSummaryLayoutFile { get; set; }
        private string AgeingDetailsLayoutFile { get; set; }

        private string Range1 { get; set; }
        private string Range2 { get; set; }
        private string Range3 { get; set; }
        private string Range4 { get; set; }
        private string Range5 { get; set; }

        private List<OutsDto> Trans { get; set; }
        public OutsMainView()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormClosed += OutsMainView_FormClosed;
            FillLookup();
            
            viewLookUpEdit.EditValue = "Summary";
            typeLookUpEdit.EditValue = "Receivable";
            this.ActiveControl = typeLookUpEdit;
            fDateEdit.DateTime = KontoGlobals.DFromDate;
            tDateEdit.DateTime = KontoGlobals.DToDate;
            pFDateEdit.DateTime = KontoGlobals.DFromDate;
            pTDateEdit.DateTime = KontoGlobals.DToDate;

            DetailsLayoutFile = KontoFileLayout.Outs_Details_View;
            SummaryLayoutFile = KontoFileLayout.Outs_Summary_View;
            AgeingSummaryLayoutFile = KontoFileLayout.Outs_Ageing_Summary_View;
            AgeingDetailsLayoutFile = KontoFileLayout.Outs_Ageing_Details_View;

            tabControlAdv1.SelectedIndexChanged += TabControlAdv1_SelectedIndexChanged;

            this.FirstActiveControl = fDateEdit;

        }

        private void TabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPageAdv2.Controls.Count > 0) return;
            var frm = new OutsParaView();
            frm.TopLevel = false;
            frm.Parent = tabPageAdv2;
            frm.Location = new Point(tabPageAdv2.Location.X + tabPageAdv2.Width / 2 - frm.Width / 2, tabPageAdv2.Location.Y + tabPageAdv2.Height / 2 - frm.Height / 2);
            frm.Show();
        }

        private void OutsMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void FillLookup()
        {
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Details", "Details"),
                new ComboBoxPairs("Summary", "Summary"),
                new ComboBoxPairs("Ageing Summary", "Ageing Summary"),
            };
            viewLookUpEdit.Properties.DataSource = cbp;

            List<ComboBoxPairs> cbpp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Receivable", "Receivable"),
                new ComboBoxPairs("Payable", "Payable"),

            };
            typeLookUpEdit.Properties.DataSource = cbpp;
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription("Generating..");
                var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
                var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
                var pfdate = Convert.ToInt32(pFDateEdit.DateTime.ToString("yyyyMMdd"));
                var ptdate = Convert.ToInt32(pTDateEdit.DateTime.ToString("yyyyMMdd"));

                var group = "";
                if (typeLookUpEdit.EditValue.ToString() == "Receivable")
                    group = "R";
                else
                    group = "P";
                var _db = new KontoContext();
                _db.Database.CommandTimeout = 0;
                if (viewLookUpEdit.EditValue.ToString() == "Summary")
                {
                    Trans = _db.Database.SqlQuery<OutsDto>(
                     "dbo.OutstandingReport @CompanyId={0},@nature={1},@fromdate={2},@todate={3},@payfromdate={4}," +
                     "@paytodate={5},@YearId={6}", Convert.ToInt32(KontoGlobals.CompanyId),
                     group, fdate, tdate, pfdate, ptdate, KontoGlobals.YearId).ToList();

                    if (Trans.Count == 0)
                    {
                        MessageBox.Show("Record Not Found");
                        return;
                    }

                    var list = Trans.GroupBy(grp => new { grp.Account, grp.AccountId, grp.Agent, grp.Bal, grp.MobileNo }).Select(x => new OutSummaryDTO()
                    {
                        Account = x.Key.Account,
                        AccountId = x.Key.AccountId,
                        BillAmt = x.Sum(k => k.BillAmt),
                        ReturnAmt = x.Sum(k => k.ReturnAmt),
                        AdjustAmt = x.Sum(k => k.AdjustAmt),
                        PendingAmt = x.Sum(k => k.PendingAmt),
                        TdsAmt = x.Sum(k => k.TdsAmt),
                        Days = (int)x.Average(k => k.Days),
                        Agent = x.Key.Agent,
                        MobileNo = x.Key.MobileNo,
                        Bal = x.Key.Bal,
                        OutsDetails = Trans.Where(p => p.AccountId == x.Key.AccountId).ToList()
                    }).ToList();
                    bindingSource1.DataSource = list;
                    this.gridView1.Columns.Clear();
                    this.gridControl1.DataSource = this.bindingSource1;
                    this.gridView1.PopulateColumns();
                    KontoUtils.RestoreLayoutGrid(this.SummaryLayoutFile, this.gridView1);
                    mainLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    detaisLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    detailsGridView1.Columns.Clear();
                    bindingSource2.DataSource = bindingSource1;
                    bindingSource2.DataMember = "OutsDetails";
                    detailsGridControl1.DataSource = bindingSource2;
                    detailsGridView1.PopulateColumns();
                    KontoUtils.RestoreLayoutGrid(this.DetailsLayoutFile, this.detailsGridView1);
                }
                else if (viewLookUpEdit.EditValue.ToString() == "Details")
                {
                    Trans = _db.Database.SqlQuery<OutsDto>(
                     "dbo.OutstandingReport @CompanyId={0},@nature={1},@fromdate={2},@todate={3},@payfromdate={4}," +
                     "@paytodate={5},@YearId={6}", Convert.ToInt32(KontoGlobals.CompanyId),
                     group, fdate, tdate, pfdate, ptdate, KontoGlobals.YearId).ToList();

                    if (Trans.Count == 0)
                    {
                        MessageBox.Show("Record Not Found");
                        return;
                    }

                    bindingSource2.DataSource = Trans;
                    bindingSource2.DataMember = null;
                    mainLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    this.detailsGridView1.Columns.Clear();
                    this.detailsGridControl1.DataSource = this.bindingSource2;
                    detailsGridView1.PopulateColumns();
                    KontoUtils.RestoreLayoutGrid(this.DetailsLayoutFile, this.detailsGridView1);
                    detaisLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                }
                else
                {
                    var frm = new RangeParaView();
                    if (frm.ShowDialog() != DialogResult.OK) return;
                    Range1 = frm.Range1 + " To " + frm.Range2 + " Days";
                    Range2 = (frm.Range2 + 1) + " To " + frm.Range3 + " Days";
                    Range3 = (frm.Range3 + 1) + " To " + frm.Range4 + " Days";
                    Range4 = (frm.Range4 + 1) + " To " + frm.Range5 + " Days";
                    Range5 = " > " + frm.Range5 + " Days";

                    var Tran = _db.Database.SqlQuery<OutsAgeingFifoDto>(
                        "dbo.Outs_Ageing_Fifo @CompanyId={0},@nature={1},@fromdate={2},@todate={3},@payfromdate={4},@paytodate={5}," +
                        "@range1={6},@range2={7},@range3={8},@range4={9},@range5={10}",
                        Convert.ToInt32(KontoGlobals.CompanyId), group, fdate, tdate, pfdate, ptdate,
                        frm.Range1, frm.Range2, frm.Range3, frm.Range4,frm.Range5).ToList();
                    if (Tran.Count == 0)
                    {
                        MessageBox.Show("Record Not Found");
                        return;
                    }
                    var list = Tran.GroupBy(k => new { k.Account, k.AccountId, k.Bal, k.PanNo }).Select(x => new OutAgingSummaryDto()
                    {
                        Account = x.Key.Account,
                        AccountId = x.Key.AccountId,
                        PanNo = x.Key.PanNo,
                        Bal = x.Key.Bal,
                        Pending = x.Sum(p => p.Pending),
                        Range1Value = x.Sum(p => p.Range1Value),
                        Range2Value = x.Sum(p => p.Range2Value),
                        Range3Value = x.Sum(p => p.Range3Value),
                        Range4Value = x.Sum(p => p.Range4Value),
                        AboveRangeValue = x.Sum(p => p.AboveRangeValue),
                        FifoDetails = Tran.Where(p => p.AccountId == x.Key.AccountId).ToList()

                    }).ToList();


                    bindingSource1.DataSource = list;
                    bindingSource2.DataSource = bindingSource1;
                    bindingSource2.DataMember = "FifoDetails";

                    this.gridView1.Columns.Clear();
                    this.gridControl1.DataSource = this.bindingSource1;
                    this.gridView1.PopulateColumns();
                    KontoUtils.RestoreLayoutGrid(this.AgeingSummaryLayoutFile, this.gridView1);
                    mainLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;


                    gridView1.Columns["Range1Value"].Caption = Range1;
                    gridView1.Columns["Range2Value"].Caption = Range2;
                    gridView1.Columns["Range3Value"].Caption = Range3;
                    gridView1.Columns["Range4Value"].Caption = Range4;
                    gridView1.Columns["AboveRangeValue"].Caption = Range5;

                    detaisLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    detailsGridView1.Columns.Clear();
                    detailsGridControl1.DataSource = bindingSource2;
                    detailsGridView1.PopulateColumns();
                    KontoUtils.RestoreLayoutGrid(this.AgeingDetailsLayoutFile, this.detailsGridView1);

                    detailsGridView1.Columns["Range1Value"].Caption = Range1;
                    detailsGridView1.Columns["Range2Value"].Caption = Range2;
                    detailsGridView1.Columns["Range3Value"].Caption = Range3;
                    detailsGridView1.Columns["Range4Value"].Caption = Range4;
                    detailsGridView1.Columns["AboveRangeValue"].Caption = Range5;

                }
                splashScreenManager1.CloseWaitForm();
                gridControl1.Focus();
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.ToString());
                Log.Error(ex, "Outs Para");
               
            }
           
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F2 | Keys.Shift))
            {
                var frm = new GridPropertView();
                if (this.gridView1.IsFocusedView)
                {
                    frm.gridControl1.DataSource = this.gridControl1.DataSource;
                    frm.gridView1.Assign(this.gridView1, false);
                    if (frm.ShowDialog() != DialogResult.OK) return true; ;
                    this.gridView1.Assign(frm.gridView1, false);

                    if (viewLookUpEdit.EditValue.ToString() == "Summary")
                        KontoUtils.SaveLayoutGrid(this.SummaryLayoutFile, this.gridView1);
                    else if (viewLookUpEdit.EditValue.ToString() == "Details")
                    {
                        KontoUtils.SaveLayoutGrid(this.DetailsLayoutFile, this.gridView1);
                    }
                    else
                        KontoUtils.SaveLayoutGrid(this.AgeingSummaryLayoutFile, this.gridView1);
                }
                else if(this.detailsGridView1.IsFocusedView)
                {

                    frm.gridControl1.DataSource = this.detailsGridControl1.DataSource;
                    frm.gridView1.Assign(this.detailsGridView1, false);
                    if (frm.ShowDialog() != DialogResult.OK) return true; ;
                    this.detailsGridView1.Assign(frm.gridView1, false);
                    if (viewLookUpEdit.EditValue.ToString() == "Details")
                        KontoUtils.SaveLayoutGrid(this.DetailsLayoutFile, this.detailsGridView1);
                    else
                        KontoUtils.SaveLayoutGrid(this.AgeingDetailsLayoutFile, this.detailsGridView1);
                }
                return true;
              
            }
            else if(keyData == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }
    }
}
