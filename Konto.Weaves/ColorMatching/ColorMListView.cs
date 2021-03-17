using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.ColorMatching
{
    public partial class ColorMListView : ListBaseView
    {
        private List<ColorMathingListDto> _modelList = new List<ColorMathingListDto>();
        public ColorMListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.ColorMaching_List;
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<DivisionModel, DivListDto>());

            using (var _db = new KontoContext())
            {
                var spcol = _db.SpCollections.FirstOrDefault(k => k.Id == (int)SpCollectionEnum.ColorMatchList);
                if (spcol == null)
                {
                    _modelList = new List<ColorMathingListDto>(_db.Database.SqlQuery<ColorMathingListDto>("dbo.ColorMatchList @CompanyId={0}",
                    KontoGlobals.CompanyId).ToList());
                }
                else
                {
                    _modelList = new List<ColorMathingListDto>(_db.Database.SqlQuery<ColorMathingListDto>(spcol.Name + " @CompanyId={0}",
                            KontoGlobals.CompanyId).ToList());
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
                    var Model  = db.WeftItems.Find(_id);
                    var _wefts = db.WeftItems.Where(k => k.IsDeleted == false && k.TypeId == Model.TypeId ).ToList();

                    foreach (var item in _wefts)
                    {
                        var col = db.jobCardTrans.FirstOrDefault(k => k.ItemId == item.ItemId && k.Ply == item.MColorId && k.IsDeleted == false);
                        if (col != null)
                        {
                            MessageBox.Show("Record Can't Delete. This Matching is Used in JobCard");
                            return;
                        }
                    }

                    foreach (var item in _wefts)
                    {
                        item.IsDeleted = true; 
                    }
                    //foreach (var item in _warps)
                    //{
                    //    item.IsDeleted = true; 
                    //}

                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Color Matching delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}