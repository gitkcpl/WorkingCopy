using DevExpress.Data.WcfLinq.Helpers;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Voucher
{
    public partial class SerialSetupView : KontoForm
    {
        public int _Id { get; set; }
        public SerialSetupView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
           using(var db = new KontoContext())
            {
                var seri = db.LastSerials.FirstOrDefault(x => x.CompId == KontoGlobals.CompanyId &&
                        x.YearId == KontoGlobals.YearId && x.VoucherId == _Id && x.BranchId == KontoGlobals.BranchId);
                if (seri != null)
                {
                    seri.Last_Serial = spinEdit1.Value.ToString("F0");
                    db.SaveChanges();
                    MessageBox.Show("Serial Updated..");
                }
            }
        }
    }
}
