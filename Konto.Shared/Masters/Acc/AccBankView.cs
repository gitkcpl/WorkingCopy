using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using Serilog;
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

namespace Konto.Shared.Masters.Acc
{
    public partial class AccBankView : KontoForm
    {
        public int AccId { get; set; }
        public AccBankView()
        {
            InitializeComponent();
            this.Load += AccBankView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(nameTextBox.Text.Trim()))
            {
                MessageBox.Show("Invalid Bank Name");
                nameTextBox.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(acnoTextBoxExt.Text))
            {
                MessageBox.Show("Invalid Account No.");
                acnoTextBoxExt.Focus();
                return;
            }
            try
            {
                using (var db = new KontoContext())
                {
                    var bank = db.AccBanks.FirstOrDefault(x => x.AccId == this.AccId);
                    if (bank == null)
                        bank = new AccBankModel();
                    bank.BankName = nameTextBox.Text.Trim();
                    bank.BranchName = branchTextBoxExt.Text.Trim();
                    bank.IfsCode = ifscTextBoxExt.Text.Trim();
                    bank.AccountNo = acnoTextBoxExt.Text.Trim();

                    if (bank.Id == 0)
                    {
                        bank.AccId = this.AccId;
                        bank.RowId = Guid.NewGuid();
                        db.AccBanks.Add(bank);
                    }
                    db.SaveChanges();

                }
                MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Bank Save");
                MessageBoxAdv.Show(this, "Error While Save !!", "Exception ", ex.ToString());
            }
           


        }

        private void AccBankView_Load(object sender, EventArgs e)
        {
            using(var db = new KontoContext())
            {
                var bank = db.AccBanks.FirstOrDefault(x => x.AccId == this.AccId);
                if (bank == null) return;
                nameTextBox.Text = bank.BankName;
                branchTextBoxExt.Text = bank.BranchName;
                ifscTextBoxExt.Text = bank.IfsCode;
                acnoTextBoxExt.Text = bank.AccountNo;
            }
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
