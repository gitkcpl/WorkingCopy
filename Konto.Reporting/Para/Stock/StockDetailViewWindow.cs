using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Reports;
using Konto.Shared.Trans.GRN;
using Konto.Shared.Trans.JobIssue;
using Konto.Shared.Trans.PInvoice;
using Konto.Shared.Trans.PReturn;
using Konto.Shared.Trans.SalesChallan;
using Konto.Shared.Trans.SInvoice;
using Konto.Shared.Trans.SReturn;
using Konto.Shared.Trans.StockJournal;
using Konto.Shared.Trans.StoreIssue;
using Konto.Shared.Trans.StoreIssueReturn;
using Konto.Trading.GP;
using Konto.Trading.JobReceipt;
using Konto.Trading.MillIssue;
using Konto.Trading.MillReceipt;
using Konto.Trading.TakaWiseJobReceipt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Stock
{
    public partial class StockDetailViewWindow : KontoForm
    {
        public DateTime? _FromDate { get; set; }
        public DateTime? _ToDate { get; set; }
        public int ProductId { get; set; }
        public int BranchId { get; set; }
        public string _item { get; set; }
        public StockDetailViewWindow()
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.Load += StockDetailViewWindow_Load;
            this.gridControl1.ProcessGridKey += GridControl1_ProcessGridKey;
        }

        private void GridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (gridView1.FocusedRowHandle < 1) return;
            var dr = gridView1.GetRow(gridView1.FocusedRowHandle) as StockDetDto;
            ShowZoom(dr);
        }
        private void ShowZoom(StockDetDto err)
        {
            var db = new KontoContext();
           
            var vw = new KontoMetroForm();
            if (err.VTypeId == (int)VoucherTypeEnum.SaleInvoice)
            {
                var bll = db.Bills.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new SInvoiceIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.PurchaseInvoice)
            {
                var bll = db.Bills.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new PInvoiceIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.SaleReturn)
            {
                var bll = db.Bills.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new SReturnIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.PurchaseReturn)
            {
                var bll = db.Bills.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new PReturnIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.GrayPurchaseChallan)
            {
                var bll = db.Bills.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new GPIndex();
                vw.EditKey = bll.Id;
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.MillReceipt)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new MrvIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.JobReceipt)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new JrIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.Inward)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new GRNIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.SalesChallan)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new ScIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.JobIssue)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new JobIssueIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.TakaWiseJobReceipt)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new TakaWiseJobReceiptIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.MillIssue)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new MillIssueIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.StoreIssue)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new StoreIssueIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.StoreIssueReturn)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new SIReturnIndex();
                vw.EditKey = bll.Id;
            }
            else if (err.VTypeId == (int)VoucherTypeEnum.StockJournal)
            {
                var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
                if (bll == null) return;
                vw = new SJIndex();
                vw.EditKey = bll.Id;
            }

            //else if (err.VTypeId == (int)VoucherTypeEnum.MillReturn)
            //{
            //    var bll = db.Challans.FirstOrDefault(x => x.RowId == err.MasterRefId);
            //    if (bll == null) return;
            //    vw = new MillReturnIn();
            //    vw.EditKey = bll.Id;
            //}

            vw.OpenForLookup = true;
           
            vw.ShowDialog();
        }
        private void StockDetailViewWindow_Load(object sender, EventArgs e)
        {
            if (this._FromDate!= null)
            {
                dateEdit1.EditValue = this._FromDate;
                dateEdit2.EditValue = this._ToDate;
                this.productLookup1.SelectedValue = this.ProductId;
                this.productLookup1.SetGroup(this.ProductId);
                okSimpleButton.PerformClick();
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            var fdate = Convert.ToInt32(dateEdit1.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(dateEdit2.DateTime.ToString("yyyyMMdd"));
            using (var _db = new KontoContext())
            {
                _db.Database.CommandTimeout = 0;

                var Trans = _db.Database.SqlQuery<StockDetDto>(
                    "dbo.StockDetails @CompanyId={0},@ItemId={1},@YearId={2},@FromDate={3},@ToDate={4},@BranchId={5}," +
                    "@item={6}",
                    KontoGlobals.CompanyId, Convert.ToInt32(productLookup1.SelectedValue),
                    KontoGlobals.YearId, fdate, tdate, this.BranchId,_item).ToList();

                gridControl1.DataSource = Trans;
                gridView1.Focus();
            }

        }
    }
}
