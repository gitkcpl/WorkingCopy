using System;
using System.Windows.Forms;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;

namespace Konto.Shared.Masters.Haste
{
    public partial class HasteLookup : LookupBase
    {
        public HasteLookup()
        {
            InitializeComponent();
            //this.SelectedValueChanged += GroupLookup_SelectedValueChanged;
        }

       
        public void SetValue()
        {
            using (var db = new KontoContext())
            {
                var id = Convert.ToInt32(this.SelectedValue);
                var model = db.Hastes.Find(id);
                if (model != null)
                {
                    this.SelectedText = model.HasteName;
                    buttonEdit1.Text = model.HasteName;
                }
            }
        }
        public void SetEmpty()
        {
            this.SelectedValue = null;
            this.buttonEdit1.Text = string.Empty;
        }
        private void ShowList()
        {
            var frm = new HasteLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32(this.SelectedValue),
                Tag = MenuId.Emp
            };
            frm.ShowDialog(this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.SelectedText = frm.SelectedTex;
                this.PrimaryKey = frm.SelectedValue;
                this.buttonEdit1.Text = this.SelectedText;

            }
            this.Parent.SelectNextControl(this, true, true, true, false);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete && !this.RequiredField)
            {
                this.SelectedValue = null;
                this.buttonEdit1.Text = string.Empty;
                return true;

            }
            else if (keyData == Keys.Return)
            {
                if (Convert.ToInt32(this.SelectedValue) == 0 && this.RequiredField)
                {
                    ShowList();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

       
        private void buttonEdit1_Enter(object sender, EventArgs e)
        {
            this.buttonEdit1.SelectAll();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ShowList();
        }
    }
}
