using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Konto.Shared.Masters.PG
{
    public partial class PgLkpWindow : LookupForm
    {
        List<PgLookupDto> _modelList = new List<PgLookupDto>();
        public PgLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.Party_Group_Lookup_Layout;
            this.FormClassName = "Konto.Shared.Masters.PG.PgIndex";
            this.AsemblyName = "Konto.Shared";
        }
        public override void LoadData()
        {
            base.LoadData();

            using (var _db = new KontoContext())
            {
                _modelList = (from ar in _db.PartyGroups
                              where (!ar.IsDeleted && ar.IsActive)
                              orderby ar.GroupName
                              select new PgLookupDto
                              {
                                  DisplayText = ar.GroupName,
                                  Id = ar.Id,
                              }
                             ).ToList();
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
