using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data.Models.Admin.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Konto.Shared.Masters.LogIn
{
    public partial class SelectGroupWindow : KontoForm
    {
        public SelectGroupWindow()
        {
            InitializeComponent();
            this.Load += SelectGroupWindow_Load;
            this.lkpAction1.OkButtonClick += LkpAction1_OkButtonClick;
            this.lkpAction1.NewButtonClick += LkpAction1_NewButtonClick;
            this.Shown += SelectGroupWindow_Shown;
            this.Activated += SelectGroupWindow_Activated;
        }

        private void SelectGroupWindow_Activated(object sender, EventArgs e)
        {
            customGridControl1.Focus();
        }

        private void SelectGroupWindow_Shown(object sender, EventArgs e)
        {
            
            this.ActiveControl = customGridControl1;
            //customGridControl1.Focus();
            //customGridView1.SelectRow(0);
            this.Activate();
            customGridControl1.Focus();
            this.Activate();

        }

        private void LkpAction1_NewButtonClick(object sender, EventArgs e)
        {
            var frm = new GroupSetupMainView();
            frm.ShowDialog();
            string filePath = "DbGroupListFile.xml";
            DataSet ds = new DataSet();
            if (File.Exists(filePath))
            {
                ds.ReadXml(filePath);

                DataTable dt = ds.Tables["GroupData"];

                var GroupList = new List<DBGroupDTO>();
                DBGroupDTO grp = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grp = new DBGroupDTO();
                    grp.GroupName = dt.Rows[i]["GroupName"].ToString();
                    grp.DBName = dt.Rows[i]["DbName"].ToString();
                    GroupList.Add(grp);
                }
                if (GroupList.Count == 1)
                {
                    KontoGlobals.DbName = grp.DBName;
                    KontoGlobals.DbGroup = grp.GroupName;
                }
                bindingSource1.DataSource = GroupList;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as DBGroupDTO;
                if (dr != null)
                {
                    KontoGlobals.DbGroup = dr.GroupName;
                    KontoGlobals.DbName = dr.DBName;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return true;
                }
            }
            else if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }
        private void LkpAction1_OkButtonClick(object sender, EventArgs e)
        {
            var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as DBGroupDTO;
            if (dr != null)
            {
                KontoGlobals.DbGroup = dr.GroupName;
                KontoGlobals.DbName = dr.DBName;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }

        private void SelectGroupWindow_Load(object sender, EventArgs e)
        {

            string filePath = "DbGroupListFile.xml";
            DataSet ds = new DataSet();
            if (File.Exists(filePath))
            {
                ds.ReadXml(filePath);

                DataTable dt = ds.Tables["GroupData"];

                var GroupList = new List<DBGroupDTO>();
                DBGroupDTO grp = null ;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grp = new DBGroupDTO();
                    grp.GroupName = dt.Rows[i]["GroupName"].ToString();
                    grp.DBName = dt.Rows[i]["DbName"].ToString();
                    GroupList.Add(grp);
                }
                if(GroupList.Count == 1)
                {
                    KontoGlobals.DbName = grp.DBName;
                    KontoGlobals.DbGroup = grp.GroupName;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                bindingSource1.DataSource = GroupList;
            }
           
        }
    }
}
