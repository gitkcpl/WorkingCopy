using DevExpress.Data.WcfLinq.Helpers;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Branch
{
    public partial class SelectBranchWindow : KontoForm
    {
        private KontoContext db = new KontoContext();
        public BranchModel Branch { get; set; }
        public SelectBranchWindow()
        {
            InitializeComponent();
            this.Load += SelectBranchWindow_Load;
            this.lkpAction1.OkButtonClick += LkpAction1_OkButtonClick;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as BranchModel;
                if (dr != null)
                {
                    KontoGlobals.BranchId = dr.Id;
                    this.Branch = dr;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else if(keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void LkpAction1_OkButtonClick(object sender, EventArgs e)
        {

            var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as BranchModel;
            if (dr != null)
            {
                KontoGlobals.BranchId = dr.Id;
                this.Branch = dr;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void SelectBranchWindow_Load(object sender, EventArgs e)
        {
            var lst = db.Branches.Where(x => !x.IsDeleted)
                .OrderBy(x => x.BranchName).ToList();
            if (lst.Count == 1)
            {
                KontoGlobals.BranchId = lst[0].Id;
                this.Branch = lst[0];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            bindingSource1.DataSource = lst;

        }

        private void customGridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
