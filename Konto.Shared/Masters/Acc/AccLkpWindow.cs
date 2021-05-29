using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Konto.Core.Shared;

namespace Konto.Shared.Masters.Acc
{
    public partial class AccLkpWindow : LookupForm
    {
        List<AccLookupDto> _modelList = new List<AccLookupDto>();
        public VoucherTypeEnum VoucherType { get; set; }
        public bool FillParty { get; set; }
        public string Nature { get; set; }
        public string TaxType { get; set; }
        public int GroupId { get; set; }

        public int NewGroupId { get; set; }
        public AccLkpWindow()
        {
            InitializeComponent();
            customGridControl1.ProcessGridKey += CustomGridControl1_ProcessGridKey;
            this.ledgerSimpleButton.Click += LedgerSimpleButton_Click;
            this.outsSimpleButton.Click += OutsSimpleButton_Click;
            this.GridLayoutFileName = KontoFileLayout.Acc_Lookup_List_Layout;
            this.FormClassName ="Konto.Shared.Masters.Acc.AccIndex";
            this.AsemblyName = "Konto.Shared";
        }

        private void OutsSimpleButton_Click(object sender, EventArgs e)
        {
            var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as AccLookupDto;
            if (dr == null) return;
            GrapeCity.ActiveReports.PageReport _pageReport = new GrapeCity.ActiveReports.PageReport();
            _pageReport.Load(new System.IO.FileInfo("outs\\outs_ar.rdlx"));
            _pageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
            GrapeCity.ActiveReports.Document.PageDocument doc = new GrapeCity.ActiveReports.Document.PageDocument(_pageReport);
            doc.Parameters["partyid"].CurrentValue = dr.Id;
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
                var grp = db.AcGroupModels.Find(dr.GroupId);
                if(grp!=null && grp.Extra1!=null)
                    doc.Parameters["nature"].CurrentValue = grp.Extra1;
                else
                    doc.Parameters["nature"].CurrentValue = "R";
            }

            doc.Parameters["report_title"].CurrentValue = "Oustanding Report";
            
            var frm = new KontoRepViewer(doc);
            frm.Text = "Oustanding Print Preview";
            frm.ShowDialog();
        }

        private void CustomGridControl1_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.L)
            {
                customGridView1.OptionsBehavior.AllowIncrementalSearch = false;
                ledgerSimpleButton.PerformClick();
                customGridView1.OptionsBehavior.AllowIncrementalSearch = true;
            }
            else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.O)
            {
                customGridView1.OptionsBehavior.AllowIncrementalSearch = false;
                outsSimpleButton.PerformClick();
                customGridView1.OptionsBehavior.AllowIncrementalSearch = true;
            }
        }

        private void LedgerSimpleButton_Click(object sender, EventArgs e)
        {
            var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as AccLookupDto;
            if(dr==null) return;
            var _frm = Activator.CreateInstance("Konto.Reporting", "Konto.Reporting.Para.Ledger.LedgerMainView").Unwrap() as KontoForm;

            if (_frm.GetType().GetProperty("AccId") != null)
            {
                PropertyInfo groupid = _frm.GetType().GetProperty("AccId");
                groupid.SetValue(_frm,dr.Id);
                _frm.ShowDialog();
            }
        }

        public override void LoadData()
        {
            base.LoadData();

            //var configuration = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<AcGroupModel, LedgerGroupLookupDto>()
            //    .ForMember(x => x.DisplayText, p => p.MapFrom(x => x.GroupName))
            //    );

            //using (var _db = new KontoContext())
            //{
            //    _modelList = _db.AcGroupModels.Where(x => !x.IsDeleted && x.IsActive)
            //                  .OrderBy(x=>x.GroupName)
            //                 .ProjectTo<LedgerGroupLookupDto>(configuration).ToList();

            //}

            if (string.IsNullOrEmpty(TaxType))
                this.TaxType = "N";

            if (string.IsNullOrEmpty(this.Nature))
            {
                Nature = "ALL";
            }
            var _fillparty = "Y";

            if (!FillParty)
                _fillparty = "N";
            using (KontoContext db = new KontoContext())
            {
                if (this.VoucherType != VoucherTypeEnum.None)
                {
                    if (_fillparty == "Y")
                    {
                        var partyExist = db.VoucherParties.FirstOrDefault(k => k.VoucherTypeId == (int)VoucherType);
                        if (partyExist == null)
                        {
                            this.VoucherType = 0;
                        }
                    }
                    else
                    {
                        var bookExist = db.VoucherBooks.FirstOrDefault(k => k.VoucherTypeId == (int)VoucherType);
                        if (bookExist == null)
                        {
                            this.VoucherType = 0;
                        }
                    }
                }
                db.Database.CommandTimeout = 0;

                _modelList = db.Database.SqlQuery<AccLookupDto>("dbo.acclookup @groupid={0},@companyid={1},@yearid={2},@taxtype={3},@nature={4},@fillparty={5},@vouchertypeid={6}"
                    , this.GroupId, KontoGlobals.CompanyId, KontoGlobals.YearId, this.TaxType, this.Nature, _fillparty, (int)this.VoucherType).ToList();
            }

            customGridControl1.DataSource = _modelList;

            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            this.customGridView1.FocusedColumn = this.customGridView1.Columns[1];

        }

        private void AreaLkpWindow_Shown(object sender, EventArgs e)
        {
            if (this.SelectedValue <= 0) return;
            var item = _modelList.FirstOrDefault(x => x.Id == this.SelectedValue);
            var index = _modelList.IndexOf(item);
            if (index >= 0)
            {
                customGridView1.FocusedRowHandle = index;
            }
        }

        private void customGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var vw = sender as GridView;
            if (vw.FocusedRowHandle < 0) return;
            var row = vw.GetRow(vw.FocusedRowHandle) as AccLookupDto;
            gstinLabel.Text = row.GSTIN;
            grouplabelControl.Text = row.GroupName;
            partyGrouplabel.Text = row.PartyGroup;
            agentlabelControl.Text = row.Agent;
        }
    }
}
