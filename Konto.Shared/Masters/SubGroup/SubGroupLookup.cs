﻿using System;
using System.Windows.Forms;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;

namespace Konto.Shared.Masters.SubGroup
{
    public partial class SubGroupLookup : LookupBase
    {
        public SubGroupLookup()
        {
            InitializeComponent();
            this.SelectedValueChanged += SubGroupLookup_SelectedValueChanged;
        }

        private void SubGroupLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.SelectedValue == null)
            {
                this.buttonEdit1.Text = string.Empty;
            }
        }
        public void SetGroup()
        {
            using (var db = new KontoContext())
            {
                var id = Convert.ToInt32(this.SelectedValue);
                var model = db.PSubGroups.Find(id);
                if (model != null)
                {
                    this.SelectedText = model.SubName;
                    buttonEdit1.Text = model.SubName;
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
            var frm1 = this.ParentForm as KontoForm;

            var frm = new SubGroupLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32(this.SelectedValue),
                Tag = MenuId.Sub_Group

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
               // if (Convert.ToInt32(this.SelectedValue) == 0 && this.RequiredField)
               // {
                    ShowList();
                    return true;
               // }
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
