using Konto.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Reconcile
{
    public partial class BankOpBal : KontoForm
    {
        public decimal OpBal { get; set; }
        public BankOpBal()
        {
            InitializeComponent();
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.okSimpleButton.Click += OkSimpleButton_Click;
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.OpBal = textEdit1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
