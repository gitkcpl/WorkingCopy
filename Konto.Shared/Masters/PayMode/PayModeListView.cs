using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.Data.Models.Masters.Dtos;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;

namespace Konto.Shared.Masters.PayMode
{
    public partial class PayModeListView : ListBaseView
    {
        private List<HasteListDto> _modelList = new List<HasteListDto>();
        public PayModeListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Pay_Mode_Master_List;
        }

        public override void RefreshGrid()
        {
            try
            {
                base.RefreshGrid();
                
                using (var _db = new KontoContext())
                {
                    _modelList = (from p in _db.Hastes
                                  join ac in _db.Accs on p.AccId equals ac.Id
                                  orderby p.HasteName
                                  where !p.IsDeleted && p.MType == "PM"
                                  select new HasteListDto
                                  {
                                      HasteName = p.HasteName,
                                      Remark = p.Remark,
                                      AccName = ac.AccName,
                                      IsActive = ac.IsActive,
                                      CreateDate = p.CreateDate,
                                      CreateUser = p.CreateUser,
                                      ModifyDate = p.ModifyDate,
                                      ModifyUser = p.ModifyUser,
                                      PanNo = p.PanNo,Id= p.Id
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
            catch (Exception ex)
            {

                Log.Error(ex, "misc refresh Grid");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
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
                    var model = db.Hastes.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "pay mode delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}
