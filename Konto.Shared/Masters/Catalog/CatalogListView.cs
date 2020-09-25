using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.Data.Models.Masters.Dtos;
using Konto.App.Shared;
using AutoMapper;
using Konto.Data.Models.Masters;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using AutoMapper.QueryableExtensions;

namespace Konto.Shared.Masters.Catalog
{
    public partial class CatalogListView : ListBaseView
    {
        private List<CatalogListDto> _modelList = new List<CatalogListDto>();
        public CatalogListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Catalogue_Master_List_Layout;

            this.Load += CatalogListView_Load;
        }

        private void CatalogListView_Load(object sender, EventArgs e)
        {
           /// this.RefreshGrid();
        }
        public override void RefreshGrid()
        {
            try
            {
                base.RefreshGrid();
                var configuration = new MapperConfiguration(cfg =>
                    cfg.CreateMap<CatalogModel, CatalogListDto>());

                using (var _db = new KontoContext())
                {
                    _modelList = _db.Catalogs.Where(x => !x.IsDeleted)
                                .OrderBy(x => x.CatalogName)
                                .ProjectTo<CatalogListDto>(configuration).ToList();

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
            catch (Exception ex)
            {
                Log.Error(ex, "catalog list");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
            
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    this.ActiveControl = customGridControl1;
                    return;
                }
                using (var db = new KontoContext())
                {
                    var model = db.Catalogs.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ActiveControl = customGridControl1;
                }
                
            }
            catch (Exception ex)
            {

                Log.Error(ex, "catalog delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}
