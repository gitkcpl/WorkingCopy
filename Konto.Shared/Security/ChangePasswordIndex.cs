using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Admin;
using Syncfusion.Windows.Forms;
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
    public partial class ChangePasswordIndex : KontoForm
    {
        public ChangePasswordIndex()
        {
            InitializeComponent();
            this.FormClosed += ChangePasswordIndex_FormClosed;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ChangePasswordIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(passwordTextEdit.Text))
                {
                    MessageBoxAdv.Show(this, "Current Password is required!!", "Invalid data!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    passwordTextEdit.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(NewPasswordTextEdit.Text))
                {
                    MessageBoxAdv.Show(this, "New Password is required!!", "Invalid data!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    NewPasswordTextEdit.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ConfirmTextEdit.Text))
                {
                    MessageBoxAdv.Show(this, "Confirm Password is required!!", "Invalid data!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ConfirmTextEdit.Focus();
                    return;
                }
                if (NewPasswordTextEdit.Text != ConfirmTextEdit.Text)
                {
                    MessageBoxAdv.Show(this, "Password and confirm password must be same!!!", "Not Match!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    NewPasswordTextEdit.Text = string.Empty;
                    ConfirmTextEdit.Text = string.Empty;
                    NewPasswordTextEdit.Focus();
                    return;
                }
                using (var db = new KontoContext())
                {
                    string enc = KontoUtils.Encrypt(passwordTextEdit.Text.Trim(), "sblw-3hn8-sqoy19");
                    UserMasterModel model = new UserMasterModel();
                    model = db.UserMasters.FirstOrDefault(x => x.UserName.ToUpper() == KontoGlobals.UserName.Trim().ToUpper()
                            && x.UserPass == enc && !x.IsDeleted);

                    if (model == null)
                    {
                        MessageBoxAdv.Show(this, "Invalid current password", "Invalid data!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        passwordTextEdit.Focus();
                        return;
                    }
                    else
                    {
                        model.UserPass = KontoUtils.Encrypt(NewPasswordTextEdit.Text.Trim(), "sblw-3hn8-sqoy19");
                        db.SaveChanges();

                        MessageBoxAdv.Show(this, KontoGlobals.SaveMessage, "Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxAdv.Show(this, ex.ToString(), "Not Saved !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
