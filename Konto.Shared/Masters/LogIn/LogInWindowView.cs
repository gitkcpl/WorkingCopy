using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Admin;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.LogIn
{
    public partial class LogInWindowView : KontoForm
    {
        public LogInWindowView()
        {
            InitializeComponent();
            this.loginSimpleButton.Click += LoginSimpleButton_Click;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Keysoft");
            if (key != null)
            {
                userNameTextEdit.Text = key.GetValue("Name").ToString();
            }
            else
            {
                userNameTextEdit.Text = "Keysoft";
            }
            // this.Load += LogInWindowView_Load;
           // this.ActiveControl = userNameTextEdit;
            this.Shown += LogInWindowView_Shown;
           
        }

      

        private void LogInWindowView_Shown(object sender, EventArgs e)
        {

            // userNameTextEdit.Select();
            // userNameTextEdit.Focus();
            // Application.DoEvents();
            this.Focus();

            this.BeginInvoke((MethodInvoker)delegate { userNameTextEdit.Focus(); });
        }

        private void LogInWindowView_Load(object sender, EventArgs e)
        {
            userNameTextEdit.Select();
            this.ActiveControl = userNameTextEdit;

            
         //   userNameTextEdit.Focus();
        }

        public bool IsStartup { get; set; }
        private void LoginSimpleButton_Click(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {
                
                    string enc = KontoUtils.Encrypt(passwordTextEdit.Text.Trim(), "sblw-3hn8-sqoy19");
              //  string enc1 = KontoUtils.Decrypt("rbCxG9xU4Gs=", "sblw-3hn8-sqoy19");
                var usr = db.UserMasters.Include("BranchFk").Include("Role").FirstOrDefault(k => k.UserName.ToUpper() == userNameTextEdit.Text.ToUpper()
                                                                             && k.UserPass == enc);

                    if (usr != null)
                    {
                        KontoGlobals.UserId = usr.Id;
                        KontoGlobals.UserName = usr.UserName;
                        //var usr = db.UserMasters.Include("BranchFk").FirstOrDefault(k => k.Id == KontoGlobals.UserId);
                        KontoGlobals.UserRoleId = usr.RoleId;

                        KontoGlobals.BranchId = usr.BranchId ?? 0;
                            

                        if(usr.Role!=null)
                            KontoGlobals.isSysAdm = usr.Role.IsSysAdmin;

                        KontoGlobals.EmpId = usr.EmpId ?? 0;

                        RBAC.UserRight = new RBACUser(usr.UserName);
                        //UserName save in registry
                        Microsoft.Win32.RegistryKey keysoftRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Keysoft");
                        keysoftRegistryKey.SetValue("Name", usr.UserName);
                        keysoftRegistryKey.Close();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid UserName or Password !!!");
                         userNameTextEdit.Focus();
                    }
                
            }
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            if (this.IsStartup)
            {
                Application.Exit();
                return;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }
    }
}
