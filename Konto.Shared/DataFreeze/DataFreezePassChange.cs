using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
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

namespace Konto.Shared.DataFreeze
{
    public partial class DataFreezePassChange : KontoForm
    {
        public DataFreezePassChange()
        {
            InitializeComponent();
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            var _db = new KontoContext();
         
            string enc1 = KontoUtils.Encrypt(oldpasswordTextEdit.Text.Trim(), "sblw-3hn8-sqoy19");
            var Ulist = _db.UserMasters.FirstOrDefault(k => k.UserName.ToUpper() == KontoGlobals.UserName.ToUpper()
               && k.UserPass == enc1);

            if (Ulist != null)
            {
                if (NewpasswordTextEdit.Text== ConfirmpasswordTextEdit.Text)
                {
                    string enc = KontoUtils.Encrypt(NewpasswordTextEdit.Text.Trim(), "sblw-3hn8-sqoy19");
                    _db.Database.ExecuteSqlCommand("update UserMaster set UserPass = '" + enc + "' where id=" + KontoGlobals.UserId);
                    MessageBox.Show("Your Password Updated Successfully..!!");
                    KontoGlobals.Pass = NewpasswordTextEdit.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your Confirm Password does not match with new password..!!");
                }
            }
            else
            {
                MessageBox.Show("Please Enter Valid Old Password..!!");
            }
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
