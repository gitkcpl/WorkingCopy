using System;
using System.Windows.Forms;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using System.Linq;
using Konto.Data.Models.Masters.Dtos;

namespace Konto.Shared.Masters.Process
{
    public partial class ProcessLookup : LookupBase
    {
        public ProcessLookupDto LookupDto { get; set; }
        public ProcessLookup()
        {
            InitializeComponent();
            this.SelectedValueChanged += GroupLookup_SelectedValueChanged;
        }

        private void GroupLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.SelectedValue == null)
            {
                this.buttonEdit1.Text = string.Empty;
            }
        }
        public void SetValue()
        {
            using (var db = new KontoContext())
            {
                var id = Convert.ToInt32(this.SelectedValue);
                var model = (from ar in db.Process
                             join tx in db.TaxMasters on ar.TaxId equals tx.Id
                             where (!ar.IsDeleted && ar.IsActive && ar.Id == id)
                             orderby ar.ProcessName
                             select new ProcessLookupDto
                             {
                                 DisplayText = ar.ProcessName,
                                 Id = ar.Id,
                                 Sgst = tx.Sgst,
                                 Cgst = tx.Cgst,
                                 Igst = tx.Igst
                             }).FirstOrDefault();
                if (model != null)
                {
                    this.LookupDto = model;
                    this.SelectedText = model.DisplayText;
                    buttonEdit1.Text = model.DisplayText;
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
          

            var frm = new ProcessLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32(this.SelectedValue),
                Tag = MenuId.Product_Group

            };

            frm.ShowDialog(this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.LookupDto = frm.customGridView1.GetRow(frm.customGridView1.FocusedRowHandle) as ProcessLookupDto;
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
