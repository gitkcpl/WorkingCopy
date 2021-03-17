using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Comp
{

    public partial class SelectCompanyView : KontoForm
    {
        private KontoContext db = new KontoContext();
        public CompModel Company { get; set; }
        public SelectCompanyView()
        {
            InitializeComponent();
            this.Load += SelectCompanyView_Load;
            lkpAction1.OkButtonClick += LkpAction1_OkButtonClick;
            
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as CompModel;
                if (dr != null)
                {
                    KontoGlobals.CompanyId = dr.Id;
                    KontoGlobals.PackageId = (int) dr.NobId;
                    this.Company = dr;
                    KontoUtils.Company = dr;
                    this.DialogResult = DialogResult.OK;
                }
            }
            else if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
           
        }
        private void SelectCompanyView_Load(object sender, EventArgs e)
        {
            var lst = db.Companies
                                        .Where(x=>!x.IsDeleted)
                                        .OrderBy(x => x.CompName).ToList();

            if (lst.Count == 1)
            {
                KontoGlobals.CompanyId = lst[0].Id;
                this.DialogResult = DialogResult.OK;
                this.Company = lst[0];
                KontoUtils.Company = this.Company;
                KontoGlobals.PackageId = (int)Company.NobId;
                this.Close();
            }
            bindingSource1.DataSource = lst;
        }

        private void LkpAction1_OkButtonClick(object sender, EventArgs e)
        {
            var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as CompModel;
            if (dr != null)
            {
                KontoGlobals.CompanyId = dr.Id;
                this.Company = dr;
                KontoUtils.Company = this.Company;
                KontoGlobals.PackageId = (int)dr.NobId;
                this.DialogResult = DialogResult.OK;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
