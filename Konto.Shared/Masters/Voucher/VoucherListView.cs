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

namespace Konto.Shared.Masters.Voucher
{
    public partial class VoucherListView : ListBaseView
    {
        private List<VoucherListDto> _modelList = new List<VoucherListDto>();
        public VoucherListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Voucher_List_Layout;

            this.Load += SizeListView_Load;
        }

        private void SizeListView_Load(object sender, EventArgs e)
        {
            //this.RefreshGrid();
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<VoucherModel, VoucherListDto>()
                .ForMember(dto => dto.TypeName, conf => conf.MapFrom(ol => ol.VoucherType.TypeName)));

            using (var _db = new KontoContext())
            {
                _modelList = _db.Vouchers.Where(x => !x.IsDeleted)
                            .OrderBy(x => x.VoucherName)
                            .ProjectTo<VoucherListDto>(configuration).ToList();

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
                    var model = db.Vouchers.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "voucher delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }

        private void serialSimpleButton_Click(object sender, EventArgs e)
        {
            if (customGridView1.FocusedRowHandle < 0) return;
            var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));
            var frm = new SerialSetupView();
            frm._Id = _id;
            frm.ShowDialog();
        }
    }
}
