using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.TakaProduction
{
    public partial class TakaProdList : ListBaseView
    {
        private List<BeamProdDto> _modelList = new List<BeamProdDto>();
        public TakaProdList()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.TakaProd_List; 
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
           
            using (var _db = new KontoContext())
            {
                 var _list = new List<BeamProdDto>();

                var spcol = _db.SpCollections.FirstOrDefault(k => k.Id ==
                            (int)SpCollectionEnum.TakaprodList);
                if (spcol == null)
                {
                    _list = (_db.Database.SqlQuery<BeamProdDto>(
                    "dbo.TakaprodList @CompanyId={0},@VoucherID={1},@FromDate={2},@ToDate={3}",
                    KontoGlobals.CompanyId, (int)VoucherTypeEnum.TakaProd,
                    KontoGlobals.FromDate, KontoGlobals.ToDate).ToList());
                }
                else
                {
                    _list = (_db.Database.SqlQuery<BeamProdDto>(
                     spcol.Name + " @CompanyId={0},@VoucherID={1},@FromDate={2},@ToDate={3}",
                   KontoGlobals.CompanyId, (int)VoucherTypeEnum.TakaProd,
                    KontoGlobals.FromDate, KontoGlobals.ToDate).ToList());
                }
                this._modelList = _list; 
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
                    model.IsDeleted = true;

                    var emps = db.Prod_Emps.Where(k => k.ProdId == _id).ToList();
                    foreach (var item in emps)
                    {
                        item.IsDeleted = true;
                    }

                    var wefts = db.prod_Wefts.Where(k => k.ProdId == _id).ToList();
                    foreach (var item in wefts)
                    {
                        item.IsDeleted = true;
                    }

                    var beams = db.TakaBeams.Where(k => k.ProdId == _id).ToList();
                    foreach (var item in beams)
                    {
                        item.IsDeleted = true;
                    }

                    var stk = db.StockTranses.Where(k => k.RefId == model.RowId && k.VoucherId == model.VoucherId).ToList();
                    db.StockTranses.RemoveRange(stk);

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Taka Production delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}