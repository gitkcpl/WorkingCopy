using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Syncfusion.Windows.Forms.Tools;
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
    public partial class ReportSetupMainView : KontoForm
    {
        public ReportSetupMainView()
        {
            InitializeComponent();
            this.Load += ReportSetupMainView_Load;
            this.FormClosed += ReportSetupMainView_FormClosed;
            Dictionary<int, string> statusEnums = Enum.GetValues(typeof(VoucherTypeEnum))
              .Cast<VoucherTypeEnum>().ToDictionary(x => (int)x, x => x.ToString());

            repositoryItemLookUpEdit1.ValueMember = "Key";
            repositoryItemLookUpEdit1.DisplayMember = "Value";
            repositoryItemLookUpEdit1.DataSource = statusEnums;
        }

        private void ReportSetupMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void ReportSetupMainView_Load(object sender, EventArgs e)
        {
            var lst = new List<ReportTypeModel>();
            using(var db = new KontoContext())
            {
                lst = db.ReportTypes.ToList();
            }
            bindingSource1.DataSource = lst;
        }
    }
}
