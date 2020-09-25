using System;
using System.Windows.Forms;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;

namespace Konto.Shared.Masters.City
{
    public partial class CityLookup : LookupBase
    {
        public CityLookup()
        {
            InitializeComponent();
            this.SelectedValueChanged += CityLookup_SelectedValueChanged;
        }

        private void CityLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if(this.SelectedValue == null)
            {
                this.buttonEdit1.Text = string.Empty;
            }
        }
        public void SetCity()
        {
            using (var db = new KontoContext())
            {
                var id = Convert.ToInt32(this.SelectedValue);
                var model = db.Cities.Find(id);
                if (model != null)
                {
                    this.SelectedText = model.CityName;
                    buttonEdit1.Text = model.CityName;
                }
            }
        }
        public void SetEmpty()
        {
            this.SelectedValue = null;
            this.buttonEdit1.Text = string.Empty;
        }
      

        private void buttonEdit1_Enter(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.SelectedValue) !=0)
            {
                this.buttonEdit1.SelectAll();
                return;
            }
            ShowList();
        }

        private void ShowList()
        {
            var frm = new CityLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32( this.SelectedValue),
                Tag = MenuId.City
            };
            frm.ShowDialog(this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.SelectedText = frm.SelectedTex;
                this.PrimaryKey = frm.SelectedValue;
                this.buttonEdit1.Text = this.SelectedText;
              
            }
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (keyData == Keys.Delete && !this.RequiredField)
            {
                this.SelectedValue = null;
                this.buttonEdit1.Text = string.Empty;
                return true;

            }
            //else if (keyData == Keys.Return)
            //{
            //    if (!this.RequiredField || !string.IsNullOrEmpty(this.SelectedText))
            //        this.ProcessDialogKey(Keys.Tab);
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ShowList();
        }
    }
}
