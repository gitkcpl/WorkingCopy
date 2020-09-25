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
    public partial class DataFreezePass : KontoForm
    {
        public DataFreezePass()
        {
            InitializeComponent();
        }

        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            var _db = new KontoContext();
            var sp = new SysParaModel();
            KontoGlobals.Pass = passwordTextEdit.Text;
            string enc = KontoUtils.Encrypt(passwordTextEdit.Text.Trim(), "sblw-3hn8-sqoy19");
            var Ulist = _db.UserMasters.FirstOrDefault(k => k.UserName.ToUpper() == KontoGlobals.UserName.ToUpper()
               && k.UserPass == enc);
             
            if (Ulist != null)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.No;
                //KontoUtils.CloseWindowOrUserControl(this);
            }
        }
    }
}
