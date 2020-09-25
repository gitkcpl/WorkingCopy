using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;

namespace Konto.Shared.Masters.Acc
{
    public partial class AccLookup : LookupBase
    {
        public VoucherTypeEnum VoucherType { get; set; }
        public bool FillParty { get; set; }
        public string Nature { get; set; }
        public string TaxType { get; set; }
        public int GroupId { get; set; }
        public int NewGroupId { get; set; }

        public event EventHandler ShownPopup;
        public AccLookupDto LookupDto { get; set; }
        public AccLookup AgentLookup { get; set; }
        public AccLookup TransportLookup { get; set; }
        public AccLookup()
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
        
        public void SetAcc(int id)
        {

            if (id == 0) return;
            using (KontoContext ctx = new KontoContext())
            {
                this.LookupDto = ctx.Database.SqlQuery<AccLookupDto>("EXEC dbo.acclookup @companyid={0},@yearid={1}, @accountid={2}",
                    KontoGlobals.CompanyId, KontoGlobals.YearId, id).FirstOrDefault();
                if(LookupDto!=null)
                {
                    this.SelectedText = LookupDto.AccName;
                    buttonEdit1.Text = LookupDto.AccName;
                    
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
            var frm = new AccLkpWindow
            {
                SelectedTex = this.SelectedText,
                SelectedValue = Convert.ToInt32(this.SelectedValue),
                FillParty  = this.FillParty,
                VoucherType = this.VoucherType,
                Nature = this.Nature,
                TaxType = this.TaxType,
                GroupId = this.GroupId,
                NewGroupId = this.NewGroupId,
                RefId = this.NewGroupId,
                Tag = MenuId.Account
            };
            frm.ShowDialog(this.ParentForm);
            if (frm.DialogResult == DialogResult.OK)
            {
                this.LookupDto = frm.customGridView1.GetRow(frm.customGridView1.FocusedRowHandle) as AccLookupDto;
                this.SelectedText = frm.SelectedTex;
                this.SelectedValue = frm.SelectedValue;
                this.PrimaryKey = frm.SelectedValue;
                this.buttonEdit1.Text = this.SelectedText;
                if (this.AgentLookup != null)
                {
                    this.AgentLookup.SelectedValue = this.LookupDto.AgentId;
                    this.AgentLookup.SelectedText = this.LookupDto.Agent;
                    this.AgentLookup.buttonEdit1.Text = this.LookupDto.Agent;
                }
                if(this.TransportLookup != null)
                {
                    this.TransportLookup.SelectedValue = this.LookupDto.TransportId;
                    this.TransportLookup.SelectedText = this.LookupDto.Transport;
                    this.TransportLookup.buttonEdit1.Text = this.LookupDto.Transport;
                }
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
            else if ( (keyData == (Keys.L | Keys.Shift)) && Convert.ToInt32(this.SelectedValue)>0)
            {
                var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.Ledger.LedgerMainView").Unwrap() as KontoForm;

                if ( _frm.GetType().GetProperty("AccId") != null)
                {
                    PropertyInfo groupid = _frm.GetType().GetProperty("AccId");
                    groupid.SetValue(_frm, this.SelectedValue);
                    _frm.ShowDialog();
                }
            }
            else if (keyData == Keys.Return)
            {
                if (Convert.ToInt32(this.SelectedValue) == 0)
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
