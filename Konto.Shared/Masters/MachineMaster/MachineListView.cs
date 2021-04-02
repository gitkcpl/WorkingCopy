using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.Data.Models.Masters.Dtos;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;

namespace Konto.Shared.Masters.MachineMaster
{
    public partial class MachineListView : ListBaseView
    {
        private List<MachineListDto> _modelList = new List<MachineListDto>();
        public MachineListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.CityMasterList_Layout;
        }

        private void CityListView_Load(object sender, EventArgs e)
        {
            //this.RefreshGrid();
        }

        public override void RefreshGrid()
        {
            base.RefreshGrid();
            using (var _db = new KontoContext())
            {
                _modelList = (from mm in _db.MachineMasters
                              join d in _db.Divisions on mm.DivId equals d.Id into ct_join
                              from d in ct_join.DefaultIfEmpty()
                              where (!mm.IsDeleted)
                              orderby mm.MachineName
                              select new MachineListDto
                              {
                                 Id=mm.Id,
                                 CompanyID=mm.CompanyID,
                                 CreateDate=mm.CreateDate,
                                 CreateUser=mm.CreateUser,
                                 DivId=mm.DivId,
                                 DivisionName=d.DivisionName,
                                 IsActive=mm.IsActive,
                                 IsDeleted=mm.IsDeleted,
                                 MachineName=mm.MachineName,
                                 ModifyDate=mm.ModifyDate,
                                 ModifyUser=mm.ModifyUser,
                                 Remark=mm.Remark
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
                    var model = db.MachineMasters.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "city delete");
                //MessageBoxAdv.Show()
            }
        }

        public override void ImportExcel()
        {
            base.ImportExcel();
            var _exp = new MacImport();
            _exp.ShowDialog();
        }
    }
}
