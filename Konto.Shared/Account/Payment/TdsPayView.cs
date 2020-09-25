using Konto.Core.Shared;
using Konto.Data.Models.Transaction.Dtos;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Account.Payment
{
    public partial class TdsPayView : KontoForm
    {
        public BankTransDto Trans { get; set; }
        

        public TdsPayView()
        {
            InitializeComponent();
            this.Load += TdsPayView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.tdsPerTextEdit.EditValueChanged += TdsPerTextEdit_EditValueChanged;
            this.grossSpinEdit.EditValueChanged += TdsAmtTextEdit_EditValueChanged;
        }

        private void TdsAmtTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            tdsAmtTextEdit.Value = decimal.Round(grossSpinEdit.Value * tdsPerTextEdit.Value / 100, 0);
        }

        private void TdsPerTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            tdsAmtTextEdit.Value = decimal.Round(grossSpinEdit.Value * tdsPerTextEdit.Value / 100, 0);
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
             if (Convert.ToInt32(tdsAccLookup.SelectedValue) == 0 && tdsAmtTextEdit.Value > 0)
            {
                MessageBoxAdv.Show(this, "Tds Account Must be Selected", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tdsAccLookup.Focus();
                return;
            }
            Trans.TdsAcId = Convert.ToInt32(tdsAccLookup.SelectedValue);
            Trans.TdsPer = tdsPerTextEdit.Value;
            Trans.TdsAmt = tdsAmtTextEdit.Value;
            Trans.Total = grossSpinEdit.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TdsPayView_Load(object sender, EventArgs e)
        {
            if (Trans == null) return;
            if (Trans.TdsAcId > 0)
            {
                tdsAccLookup.SelectedValue = Trans.TdsAcId;
                tdsAccLookup.SetAcc(Trans.TdsAcId);
            }
            tdsPerTextEdit.Value = Trans.TdsPer;
            grossSpinEdit.Value = Trans.Total;
            tdsAmtTextEdit.Value = Trans.TdsAmt;
            if(tdsAmtTextEdit.Value==0)
                tdsAmtTextEdit.Value = decimal.Round(Trans.Total * Trans.TdsPer / 100, 0);
        }
    }
}
