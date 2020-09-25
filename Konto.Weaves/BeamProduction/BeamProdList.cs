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

namespace Konto.Weaves.BeamProduction
{
    public partial class BeamProdList : ListBaseView
    {
        private List<BeamProdDto> _modelList = new List<BeamProdDto>();
        public BeamProdList()
        {
            InitializeComponent();
           // this.GridLayoutFileName = KontoFileLayout.DivMaster_List_Layout; 
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<DivisionModel, DivListDto>());

            using (var _db = new KontoContext())
            {
                var spcol = _db.SpCollections.FirstOrDefault(k => k.Id ==
                       (int)SpCollectionEnum.BeamprodList);
                if (spcol == null)
                {
                    _modelList =  (_db.Database.SqlQuery<BeamProdDto>(
                     "dbo.BeamprodList @CompanyId={0},@VTypeId={1},@FromDate={2},@ToDate={3}",
                     KontoGlobals.CompanyId, (int)VoucherTypeEnum.BeamProd,
                     KontoGlobals.FromDate, KontoGlobals.ToDate).ToList());
                }
                else
                {
                    _modelList = _db.Database.SqlQuery<BeamProdDto>(
                     spcol.Name + " @CompanyId={0},@VTypeId={1},@FromDate={2},@ToDate={3}",
                   KontoGlobals.CompanyId, (int)VoucherTypeEnum.BeamProd,
                     KontoGlobals.FromDate, KontoGlobals.ToDate).ToList();
                }
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