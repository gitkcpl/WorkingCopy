using Konto.App.Shared;
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
    public partial class AddMenuView : KontoForm
    {
        public AddMenuView()
        {
            InitializeComponent();
            this.Load += AddMenuView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddMenuView_Load(object sender, EventArgs e)
        {
            var lst = new List<ErpModule>();
            using (var db = new KontoContext())
            {
                //lst =  db.ErpModules.Where(x => !x.IsDeleted).OrderBy(x => x.Id).ToList();
                lst = (from p in db.ErpModules
                       where !db.Menu_Packages.Any(mm=>(mm.MenuId== p.Id)) && !p.IsDeleted && p.Visible && p.IsActive
                       orderby p.Id
                       select p
                       ).ToList();

            }
            bindingSource1.DataSource = lst;
        }
    }
}
