using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Security
{
    public partial class AddPermission : KontoForm
    {
        private List<PermissionsModel> SelectPermissionList { get; set; }
        public Int32[] SelectedRowHandles { get; set; }
        public int RoleId { get; set; }
        public string PType { get; set; }
        public AddPermission()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.SelectedRowHandles = this.customGridView1.GetSelectedRows();
            this.Close();
        }

        private void AddPermission_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {
                SelectPermissionList = db.Database
                                           .SqlQuery<PermissionsModel>("dbo.PermissionList @ptype={0},@roleid={1} ", PType,RoleId).ToList();
            }

            this.customGridControl1.DataSource = this.SelectPermissionList;
        }
    }
}
