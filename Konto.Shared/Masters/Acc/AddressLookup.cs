using System;
using System.Linq;
using System.Windows.Forms;
using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;

namespace Konto.Shared.Masters.Acc
{
    public partial class AddressLookup : LookupBase
    {
        public AccLookup DelvAccLookup { get; set; }
       
        public event EventHandler ShownPopup;
        public AddressLookupDto LookupDto { get; set; }
        public AddressLookup()
        {
            InitializeComponent();
            this.SelectedValueChanged += AccLookup_SelectedValueChanged;
        }

        private void AccLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.SelectedValue == null)
            {
                this.buttonEdit1.Text = string.Empty;
            }
            if(Convert.ToInt32(this.SelectedValue)==0)
            {
                this.LookupDto = null;
                return;
            }
          
        }
        
        public void SetValue(int id)
        {
            

            using (KontoContext ctx = new KontoContext())
            {
                var spcol1 = ctx.SpCollections.FirstOrDefault(k => k.Id ==
                                  (int)SpCollectionEnum.DeliveryAddressList);

                if (spcol1 == null)
                {
                    this.LookupDto =
                        ctx.Database.SqlQuery<AddressLookupDto>(
                        "dbo.DeliveryAddressList @addressid={0}",
                         id).FirstOrDefault();
                }
                else
                {
                    this.LookupDto = ctx.Database.SqlQuery<AddressLookupDto>(
                     spcol1.Name + " @addressid={0}",
                      id).FirstOrDefault();
                }
              
                if (LookupDto!=null)
                {
                    this.SelectedText = LookupDto.Address;
                    buttonEdit1.Text = LookupDto.Address;
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
            var frm = new AddressLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32(this.SelectedValue),
                DelvAccId = Convert.ToInt32(DelvAccLookup.SelectedValue),
                Tag = MenuId.Account
            };
            frm.ShowDialog(this.ParentForm);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.LookupDto = frm.customGridView1.GetRow(frm.customGridView1.FocusedRowHandle) as AddressLookupDto;
                this.SelectedText = frm.SelectedTex;
                this.SelectedValue = frm.SelectedValue;
                this.PrimaryKey = frm.SelectedValue;
                this.buttonEdit1.Text = this.SelectedText;
                
            }
            this.Parent.SelectNextControl(this, true, true, true, false);
            this.ShownPopup?.Invoke(this, new EventArgs());
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
