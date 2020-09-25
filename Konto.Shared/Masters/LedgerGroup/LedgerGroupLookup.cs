using System;
using System.Windows.Forms;
using AutoMapper;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;

namespace Konto.Shared.Masters.LedgerGroup
{
    public partial class LedgerGroupLookup : LookupBase
    {
        public LedgerGroupLookupDto GroupDto { get; set; }
        public LedgerGroupLookup()
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
            if(Convert.ToInt32(this.SelectedValue)==0)
            {
                this.GroupDto = null;
                return;
            }
          
        }
        
        public void SetGroup(int id)
        {
            var configuration = new MapperConfiguration(cfg =>
            cfg.CreateMap<AcGroupModel, LedgerGroupLookupDto>());

            using (var db = new KontoContext())
            {
                
                var model = db.AcGroupModels.Find(id);
                if (model != null)
                {
                    GroupDto = new LedgerGroupLookupDto();
                    var mapper = new Mapper(configuration);
                    mapper.Map<AcGroupModel, LedgerGroupLookupDto>(model, GroupDto);

                    this.SelectedText = model.GroupName;
                    buttonEdit1.Text = model.GroupName;
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
            var frm = new LedgerGroupLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32(this.SelectedValue),
                Tag = MenuId.LedgerGroup
            };
            frm.ShowDialog(this.Parent.Parent.Parent);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.GroupDto = frm.customGridView1.GetRow(frm.customGridView1.FocusedRowHandle) as LedgerGroupLookupDto;
                this.SelectedText = GroupDto.DisplayText;
                this.SelectedValue = frm.SelectedValue;
                this.PrimaryKey = frm.SelectedValue;
                this.buttonEdit1.Text = this.SelectedText;
                
            }
           //this.Parent.SelectNextControl(this, true, true, true,false);
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
