using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Branch
{
    public partial class BranchVoucherIndex : KontoForm
    {
        public BranchVoucherIndex()
        {
            InitializeComponent();
            this.Load += BranchVoucherIndex_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            var lsts = this.branchVoucherDtoBindingSource.DataSource as List<BranchVoucherDto>;
            using (var db = new KontoContext())
            {
                foreach (var item in lsts)
                {
                    var vb = new BranchVoucher();
                    if (item.Id != 0)
                        vb = db.BranchVouchers.Find(item.Id);
                    vb.BranchId = item.BranchId;
                    vb.SaleVoucherId = item.SaleVoucherId;
                    vb.PurchaseVoucherId = item.PurchaseVoucherId;
                    vb.StockTransferVoucherId = item.StockTransferVoucherId;
                    vb.ReceiptVoucherId = item.ReceiptVoucherId;
                    vb.PaymentVoucherId = item.PaymentVoucherId;
                    if (vb.Id == 0)
                        db.BranchVouchers.Add(vb);

                }
                db.SaveChanges();
                MessageBox.Show("Updated !!");
                this.Close();
            }
        }

        private void BranchVoucherIndex_Load(object sender, EventArgs e)
        {
            using(var db = new KontoContext())
            {
                var brs = (from p in db.Branches
                           where !p.IsDeleted
                           orderby p.BranchName
                           select new BaseLookupDto
                           {
                               Id = p.Id,
                               DisplayText = p.BranchName
                           }).ToList();

                var vrs = (from p in db.Vouchers
                           where !p.IsDeleted //&& p.VTypeId == (int)vtypeid
                           orderby p.VoucherName
                           select new BaseLookupDto
                           {
                               Id = p.Id,
                               DisplayText = p.VoucherName
                           }).ToList();
                saleRepositoryItemLookUpEdit.DataSource = vrs;

                repositoryItemLookUpEdit1.DataSource = brs;

                var lts = (from p in db.BranchVouchers
                           select new BranchVoucherDto
                           {
                               Id = p.Id,
                               BranchId = p.BranchId,
                               PaymentVoucherId = p.PaymentVoucherId,
                               PurchaseVoucherId = p.PurchaseVoucherId,
                               ReceiptVoucherId = p.ReceiptVoucherId,
                               SaleVoucherId = p.SaleVoucherId,
                               StockTransferVoucherId = p.StockTransferVoucherId
                           }).ToList();

                branchVoucherDtoBindingSource.DataSource = lts;
                customGridControl1.RefreshDataSource();
            }
        }
    }
}
