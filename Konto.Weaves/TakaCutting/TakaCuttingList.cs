using AutoMapper;
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
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.TakaCutting
{
    public partial class TakaCuttingList : ListBaseView
    {
        private List<TakaCuttingListDto> _modelList = new List<TakaCuttingListDto>();
        public TakaCuttingList()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.TakaCutting_List;
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<DivisionModel, DivListDto>());

            using (var _db = new KontoContext())
            {
                int vid = (int)VoucherTypeEnum.TakaCutting;
                _modelList =
                   (from pout in _db.ProdOuts
                    join pd in _db.Prods on pout.ProdId equals pd.Id into pd_join
                    from pd in pd_join.DefaultIfEmpty()
                    join v in _db.Vouchers on pout.VoucherId equals v.Id into vou_join
                    from vou in vou_join.DefaultIfEmpty()
                    join pro in _db.Products on pd.ProductId equals pro.Id into pro_join
                    from pro in pro_join.DefaultIfEmpty()
                    join c in _db.ColorModels on pd.ColorId equals c.Id into Color_join
                    from col in Color_join.DefaultIfEmpty()
                    where vou.VTypeId == vid // && pout.VoucherId == pd.IssueRefVoucherId
                  && pd.IsActive && !pd.IsDeleted && pout.IsActive && !pout.IsDeleted
                  orderby pout.Id descending
                    select new TakaCuttingListDto()
                    {
                        Id = pout.Id,
                        ColorName = col.ColorName,
                        PoNo = pout.Remark,
                        ProductName = pro.ProductName,
                        Qty = pd.NetWt,
                        VDate = pd.VoucherDate,
                        TakaNo = pout.VoucherNo
                    }
                 ).ToList();
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
                    var ModelOut = db.ProdOuts.Find(_id);
                    var Trans = db.Prods.Where(k => k.IssueRefId == _id && k.IssueRefVoucherId == ModelOut.VoucherId && k.IsDeleted == false).ToList();
                    foreach (var item in Trans)
                    {
                        item.IsDeleted = true;
                    }

                    //Stock Trans
                    var stk = db.StockTranses.Where(k => k.MasterRefId == ModelOut.RowId).ToList();
                    if (stk != null)
                        db.StockTranses.RemoveRange(stk);

                    ModelOut.IsDeleted = true;

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Taka Cutting delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}