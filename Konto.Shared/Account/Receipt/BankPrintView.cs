using Konto.Core.Shared;
using System;
using System.Windows.Forms;

namespace Konto.Shared.Account.Receipt
{
    public partial class BankPrintView : KontoForm
    {
        public bool chkPrint = false;
        public bool chkFarwrd = false;
        public bool chkRec = false;
        public bool IsPayment = false;
        public string frmVouchers;
        public string toVouchers;

        public BankPrintView()
        {
            InitializeComponent();

            this.Load += BankPrintView_Load;
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;

            frmtextBox.Focus();
        }

        private void BankPrintView_Load(object sender, EventArgs e)
        {
            frmtextBox.Text = frmVouchers;
            TotextBox.Text = toVouchers;

            if (!IsPayment)
            {
                chkChequePrint.Visible = false;
                chkReceipt.CheckState = CheckState.Checked;
            }
            else
            {
                chkReceipt.Text = "Payment Voucher";
                chkChequePrint.CheckState = CheckState.Checked;
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            chkPrint = chkChequePrint.Checked;
            chkRec = chkReceipt.Checked;
            chkFarwrd = chkForwarding.Checked;
            frmVouchers = frmtextBox.Text;
            toVouchers = TotextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}