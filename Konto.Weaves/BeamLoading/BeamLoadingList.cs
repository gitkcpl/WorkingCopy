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

namespace Konto.Weaves.BeamLoading
{
    public partial class BeamLoadingList : ListBaseView
    {
        private List<BeamProdDto> _modelList = new List<BeamProdDto>();
        public BeamLoadingList()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.BeamLoading_List; 
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<DivisionModel, DivListDto>());

            using (var _db = new KontoContext())
            {
                _modelList =
                  (from pd in _db.Prods
                   join v in _db.Vouchers on pd.VoucherId equals v.Id into vou_join
                   from vou in vou_join.DefaultIfEmpty()
                   join p in _db.Products on pd.ProductId equals p.Id into pro_join
                   from pro in pro_join.DefaultIfEmpty()
                   join c in _db.ColorModels on pd.ColorId equals c.Id into Color_join
                   from col in Color_join.DefaultIfEmpty()
                   join m in _db.MachineMasters on pd.MacId equals m.Id into mac_join
                   from m1 in mac_join.DefaultIfEmpty()
                   where pd.MacId > 0 && pd.ProdStatus == "LOADED"
                   && pd.IsActive == true && pd.IsDeleted == false
                   orderby pd.Id descending
                   select new BeamProdDto()
                   {
                       Id = pd.Id,
                       BoxProductId = pd.BoxProductId,
                       BoxRate = pd.BoxRate,
                       ColorId = pd.ColorId,
                       CompId = pd.CompId,
                       Cops = pd.Cops,
                       CopsRate = pd.CopsRate,
                       CopsWt = pd.CopsWt,
                       CurrQty = pd.CurrQty,
                        NetWt = pd.NetWt,
                       FinQty = pd.FinQty,
                       GradeId = pd.GradeId,
                       GrossWt = pd.GrossWt,
                       ProductName = pro.ProductName,
                       DivId = pd.DivId,
                       IsClose = pd.IsClose,
                       IssueRefId = pd.IssueRefId,
                       IssueRefVoucherId = pd.IssueRefVoucherId,
                       LoadingDate = pd.LoadingDate,
                       MachineName = m1.MachineName,
                       MacId = pd.MacId, 
                       PackId = pd.PackId,
                       Pallet = pd.Pallet,
                       Remark = pd.Remark,
                       SubGradeId = pd.SubGradeId,
                       TwistType = pd.TwistType,
                       YarnName = col.ColorName,
                       IsActive = pd.IsActive,
                       IsDeleted = pd.IsDeleted,
                       Ply = pd.Ply,
                       ProdStatus = pd.ProdStatus,
                       ProductId = pd.ProductId,
                       RefId = pd.RefId,                     
                       SrNo = pd.SrNo,                      
                       TareWt = pd.TareWt,
                       Tops = pd.Tops,
                       TransId = pd.TransId,                      
                       VoucherDate = pd.VoucherDate,
                       VoucherId = pd.VoucherId,
                       VoucherNo = pd.VoucherNo,                      
                       YearId = pd.YearId 
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
                    var model = db.Prods.Find(_id);
                    model.ProdStatus = "STOCK";
                    model.MacId = null;
                    model.IsClose = false;
                    var emps = db.Prod_Emps.Where(k => k.ProdId == _id).ToList();
                    foreach (var item in emps)
                    {
                        item.IsDeleted = true;
                    }
                    var trans = db.loadingTranModels.Where(k => k.ProdId == _id).ToList();
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

                Log.Error(ex, "div delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}