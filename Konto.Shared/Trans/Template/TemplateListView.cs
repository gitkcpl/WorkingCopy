using AutoMapper;
using AutoMapper.QueryableExtensions;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Trans.Template
{
    public partial class TemplateListView : ListBaseView
    {
        private List<TempModelDto> _modelList = new List<TempModelDto>();

        public TemplateListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Template_Master_List_Layout;

            this.Load += TemplateListView_Load;
        }

        private void TemplateListView_Load(object sender, EventArgs e)
        {
            // this.RefreshGrid();
        }

        public override void RefreshGrid()
        {
            try
            {
                base.RefreshGrid();
                var configuration = new MapperConfiguration(cfg =>
                    cfg.CreateMap<TemplateModel, TempModelDto>());

                using (var _db = new KontoContext())
                {
                    //_modelList = _db.Templates.Where(x => !x.IsDeleted)
                    //            .OrderBy(x => x.Descriptions)
                    //            .ProjectTo<TempModelDto>(configuration).ToList();
                    _modelList = _db.Database.SqlQuery<TempModelDto>(
                                    "dbo.TemplateList").ToList();
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
                Log.Error(ex, "Template refresh Grid");
                MessageBoxAdv.Show(this, "Error While Refresh !!", "Exception ", ex.ToString());
            }
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
                    var model = db.Templates.Find(_id);
                    model.IsDeleted = true;

                    var trans = db.Templatetrans.Where(k => k.TemplateId == model.Id).ToList();
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

                Log.Error(ex, "Template delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }

    }
}
