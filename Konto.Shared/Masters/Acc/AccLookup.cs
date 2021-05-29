using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
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
        public LedgerGroupEnum NewGroupId { get; set; }

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
        
        public void WalkInCustomer()
        {
            using (KontoContext ctx = new KontoContext())
            {
                var cust = (from ac in ctx.Accs
                            join bal in ctx.AccBals on ac.Id equals bal.AccId
                            where bal.CompId == KontoGlobals.CompanyId && bal.YearId == KontoGlobals.YearId
                            && bal.MobileNo == "0000000000"
                            select new
                            {
                                ac.AccName,
                                ac.Id,
                                ac.GstIn,
                            }
                           ).FirstOrDefault();
                if (cust != null)
                {
                    this.LookupDto = ctx.Database.SqlQuery<AccLookupDto>("EXEC dbo.acclookup @companyid={0},@yearid={1}, @accountid={2}",
                        KontoGlobals.CompanyId, KontoGlobals.YearId, cust.Id).FirstOrDefault();
                    if (LookupDto != null)
                    {
                        this.SelectedText = LookupDto.AccName;
                        buttonEdit1.Text = LookupDto.AccName;
                        this.SelectedValue = cust.Id;

                    }
                }
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
                    //this.SelectedValue = id;
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
                NewGroupId = (int) this.NewGroupId,
                RefId = (int) this.NewGroupId,
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
            else if ((keyData == (Keys.O | Keys.Shift)) && Convert.ToInt32(this.SelectedValue) > 0)
            {
                Show_Outs();
            }
            else if (keyData == Keys.Return)
            {
                if (Convert.ToInt32(this.SelectedValue) == 0)
                {
                    ShowList();
                    if (Convert.ToInt32(this.SelectedValue) == 0)
                        return false;

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
            if (Convert.ToInt32(this.SelectedValue) == 0)
                this.Focus();
        }

        private void Show_Outs()
        {
          if(this.LookupDto==null) return;
            GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
            _pageReport.Load(new System.IO.FileInfo("outs\\outs_ar.rdlx"));
            _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
            GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
            doc.Parameters["partyid"].CurrentValue = this.LookupDto.Id;
            doc.Parameters["paid"].CurrentValue = "UNPAID";
            doc.Parameters["branchid"].CurrentValue = 0;
            doc.Parameters["companyid"].CurrentValue = KontoGlobals.CompanyId;

            doc.Parameters["yearid"].CurrentValue = KontoGlobals.YearId;
            doc.Parameters["fromdate"].CurrentValue = 20000401;
            doc.Parameters["todate"].CurrentValue = KontoGlobals.ToDate;
            doc.Parameters["payfromdate"].CurrentValue = 20000401;
            doc.Parameters["paytodate"].CurrentValue = KontoGlobals.ToDate;

            using (var db = new KontoContext())
            {
                var grp = db.AcGroupModels.Find(this.LookupDto.GroupId);
                if (grp != null && grp.Extra1 != null)
                    doc.Parameters["nature"].CurrentValue = grp.Extra1;
                else
                    doc.Parameters["nature"].CurrentValue = "R";
            }

            doc.Parameters["report_title"].CurrentValue = "Oustanding Report";

            var frm = new KontoRepViewer(doc);
            frm.Text = "Oustanding Print Preview";
            frm.ShowDialog();
        }
    }
}
