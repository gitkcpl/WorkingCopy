using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace Konto.Shared.Masters.LedgerGroup
{
    public partial class LedgerGroupLkpWindow : LookupForm
    {
        List<LedgerGroupLookupDto> _modelList = new List<LedgerGroupLookupDto>();
        public LedgerGroupLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Ledger_Group_Lookup_Layout;
            this.FormClassName = "Konto.Shared.Masters.LedgerGroup.AcGroupIndex";
            this.AsemblyName = "Konto.Shared";
        }
        public override void LoadData()
        {
            base.LoadData();

            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<AcGroupModel, LedgerGroupLookupDto>()
                .ForMember(x => x.DisplayText, p => p.MapFrom(x => x.GroupName))
                );

            using (var _db = new KontoContext())
            {
                _modelList = _db.AcGroupModels.Where(x => !x.IsDeleted && x.IsActive)
                              .OrderBy(x=>x.GroupName)
                             .ProjectTo<LedgerGroupLookupDto>(configuration).ToList();
                             
            }

            customGridControl1.DataSource = _modelList;

            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;


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
    }
}
