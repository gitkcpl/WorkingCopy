using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.App.Shared;
using Konto.Data;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using Konto.Data.Models.Admin;
using Konto.Data.Models.Admin.Dtos;

namespace Konto.Shared.Security
{
    public partial class UserListView : ListBaseView
    {
        private List<UserListDto> _modelList = new List<UserListDto>();
        public UserListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.User_List_Layout;

            this.Load += UserListView_Load;
        }
        

        private void UserListView_Load(object sender, EventArgs e)
        {
           // this.RefreshGrid();
        }
        public override void RefreshGrid()
        {
            base.RefreshGrid();
           
            using (var _db = new KontoContext())
            {
                _modelList = (from st in _db.UserMasters
                             join ct in _db.Roles on st.RoleId equals ct.Id into ct_join
                             from ct in ct_join.DefaultIfEmpty()
                             where (!st.IsDeleted)
                             orderby st.UserName
                             select new UserListDto
                             {
                                 UserName = st.UserName,
                                 RoleName = ct.RoleName,
                                 CreateDate = st.CreateDate,
                                 CreateUser = st.CreateUser,
                                 Id = st.Id,
                                 IsActive = st.IsActive,
                                 ModifyDate = st.ModifyDate,
                                 ModifyUser = st.ModifyUser
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
                    var model = db.UserMasters.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "User delete");
                MessageBoxAdv.Show(this, "Error While Delete !!", "Exception ", ex.ToString());
            }
        }
    }
}
