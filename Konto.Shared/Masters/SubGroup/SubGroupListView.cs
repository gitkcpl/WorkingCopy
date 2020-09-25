using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.Data.Models.Masters.Dtos;
using Konto.App.Shared;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Konto.Data.Models.Masters;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;

namespace Konto.Shared.Masters.SubGroup
{
    public partial class SubGroupListView : ListBaseView
    {
        private List<SubGroupListDto> _modelList = new List<SubGroupListDto>();
        public SubGroupListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Sub_Group_Master_List_Layout;

            this.Load += SubGroupListView_Load;
        }

        private void SubGroupListView_Load(object sender, EventArgs e)
        {
         //   this.RefreshGrid();
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<PSubGroupModel, SubGroupListDto>()
                .ForMember(dto => dto.GroupName, conf => conf.MapFrom(ol => ol.PGroup.GroupName)));
                

            using (var _db = new KontoContext())
            {
                _modelList = _db.PSubGroups.Where(x => !x.IsDeleted)
                            .OrderBy(x => x.SubName)
                            .ProjectTo<SubGroupListDto>(configuration).ToList();

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
                    var model = db.PSubGroups.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "group delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}
