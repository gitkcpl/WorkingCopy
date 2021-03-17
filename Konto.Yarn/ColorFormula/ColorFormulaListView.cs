using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Yarn.ColorFormula
{
    public partial class ColorFormulaListView : ListBaseView
    {
        private List<RcpuiDto> _modelList = new List<RcpuiDto>();
        public ColorFormulaListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.ColorFormula_List;
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();

            using (var _db = new KontoContext())
            {
                this._modelList = (from b in _db.RCPUIs
                                   join voucher in _db.Vouchers on b.VoucherId equals voucher.Id into join_voucher
                                   from voucher in join_voucher.DefaultIfEmpty()
                                   join vouchertype in _db.VoucherTypes on voucher.VTypeId equals vouchertype.Id into join_vouchertype
                                   from vouchertype in join_vouchertype.DefaultIfEmpty()
                                   orderby b.Id
                                   where b.CompId == KontoGlobals.CompanyId && b.YearId == KontoGlobals.YearId && !b.IsDeleted
                                   //&& (b.VoucherDate >= KontoGlobals.FromDate && b.VoucherDate <= KontoGlobals.ToDate)
                                   && voucher.VTypeId == (int)VoucherTypeEnum.ColorRecipe
                                   select new RcpuiDto()
                                   {
                                       BranchId = b.BranchId,
                                       CompId = b.CompId,
                                       Id = b.Id,
                                       Description = b.Description,
                                       Qty = b.Qty,
                                       ProductId = b.ProductId,
                                       Remark = b.Remark,
                                       ColorId = b.ColorId,
                                       VoucherDate = b.VoucherDate,
                                       VoucherId = b.VoucherId,
                                       VoucherNo = b.VoucherNo,
                                       YearId = b.YearId
                                   }).ToList();
            }

            customGridControl1.DataSource = _modelList;
            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            if (_modelList.Count == 0)
                listAction1.EditDeleteDisabled(false);
            else
                listAction1.EditDeleteDisabled(true);
        }
        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var model = db.RCPUIs.Find(_id);
                    model.IsDeleted = true;

                    var trans = db.RcpuiTrans.Where(k => k.RCPUIId == _id).ToList();
                    foreach (var item in trans)
                    {
                        item.IsDeleted = true;
                    }

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Color Formula delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}