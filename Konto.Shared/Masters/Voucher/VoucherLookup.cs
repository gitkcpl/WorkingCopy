using System;
using System.Windows.Forms;
using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using System.Linq;
using Konto.Data.Models.Masters.Dtos;

namespace Konto.Shared.Masters.Voucher
{
    public partial class VoucherLookup : LookupBase
    {
        public VoucherLookupDto GroupDto { get; set; }

        public VoucherTypeEnum VTypeId { get; set; }
        public VoucherLookup()
        {
            InitializeComponent();
            this.SelectedValueChanged += VoucherLookup_SelectedValueChanged;
        }

        private void VoucherLookup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.SelectedValue == null)
            {
                this.buttonEdit1.Text = string.Empty;
            }
            if(Convert.ToInt32(this.SelectedValue)==0)
            {
                this.GroupDto = null;
                return;
            }
          
        }
        public void SetDefault()
        {
            using (var db = new KontoContext())
            {
                

                var _vmodel = db.Vouchers.FirstOrDefault(x => x.VTypeId == (int)this.VTypeId);
                if (_vmodel != null)
                {
                    SetGroup(_vmodel.Id);
                    this.SelectedValue = _vmodel.Id;
                    
                }
            }
        }

        public void SetGroup(int id)
        {
          
            using (var _context = new KontoContext())
            {

                GroupDto = (from vc in _context.Vouchers
                             join st in _context.VchSetups on vc.Id equals st.VoucherId
                             where vc.Id == id & st.CompId== KontoGlobals.CompanyId
                             orderby vc.VoucherName
                             select new VoucherLookupDto()
                             {
                                 DisplayText = vc.VoucherName,
                                 VTypeId = (int)vc.VTypeId,
                                 SortName = vc.SortName,
                                 AccId = vc.BookAcId,
                                 Email = st.EmailAfterSave,
                                 FixBook = st.BookFix,
                                 Increment = st.Increment,
                                 InvoiceHeading = st.InvoiceHeading,
                                 LastSerial = st.Last_Serial,
                                 Mask = st.Serial_Mask,
                                 Max = st.Max_Value,
                                 PreFillZero = st.PreFillZero,
                                 PrintAfterSave = st.PrintAfterSave,
                                 RefVoucherId = vc.RefVoucherId,
                                 Reset = st.FyReset,
                                 Sms = st.SmsAfterSave,
                                 StartFrom = st.StartFrom,
                                 Width = st.VchWidth,
                                 Id = vc.Id,ManualSeries= st.ManualSeries
                             }
                    ).FirstOrDefault();
                if (this.GroupDto != null)
                {
                    this.SelectedText = GroupDto.DisplayText;
                    buttonEdit1.Text = GroupDto.DisplayText;
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
            var frm = new VoucherLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32(this.SelectedValue),
                VTypeId = (int)this.VTypeId,
                RefId = (int)this.VTypeId,
                Tag = MenuId.Voucher
            };
            frm.ShowDialog(this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.GroupDto = frm.customGridView1.GetRow(frm.customGridView1.FocusedRowHandle) as VoucherLookupDto;
                this.SelectedText = GroupDto.DisplayText;
                this.SelectedValue = frm.SelectedValue;
                this.PrimaryKey = frm.SelectedValue;
                this.buttonEdit1.Text = this.SelectedText;
                
            }
            this.Parent.SelectNextControl(this, true, true, true,false);
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
