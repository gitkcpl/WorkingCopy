using AutoMapper;
using AutoMapper.QueryableExtensions;
using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            this.GridLayoutFileName = KontoFileLayout.Acc_Lookup_List_Layout;
            this.FormClassName = "Konto.Shared.Masters.Acc.AccIndex";
            this.AsemblyName = "Konto.Shared";
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
