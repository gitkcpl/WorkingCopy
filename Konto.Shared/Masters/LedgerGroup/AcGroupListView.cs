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
using Aspose.Cells;

namespace Konto.Shared.Masters.LedgerGroup
{
    public partial class AcGroupListView : ListBaseView
    {
        private List<AcGroupListDto> _modelList = new List<AcGroupListDto>();
        public AcGroupListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.AcGroup_Master_List_Layout;

            this.Load += AcGroupListView_Load;
            this.excelSimpleButton.Click += ExcelSimpleButton_Click;
        }

        private void ExcelSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new AcGroupImport();
            frm.ShowDialog();
        }

        private void AcGroupListView_Load(object sender, EventArgs e)
        {
           // this.RefreshGrid();
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<AcGroupModel, AcGroupListDto>());

            using (var _db = new KontoContext())
            {
                _modelList = _db.AcGroupModels.Where(x => !x.IsDeleted)
                            .OrderBy(x => x.GroupName)
                            .ProjectTo<AcGroupListDto>(configuration).ToList();

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
                    var model = db.AcGroupModels.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "ledger group delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }

        private void grpSimpleButton_Click(object sender, EventArgs e)
        {
            if (customGridView1.FocusedRowHandle < 0) return;
            var frm = new GroupSettings();
            frm.GroupId = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, customGridView1.Columns["Id"]));
            frm.ShowDialog(this);
        }
    }
}
