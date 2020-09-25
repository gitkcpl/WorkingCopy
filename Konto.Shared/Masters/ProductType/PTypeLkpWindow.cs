using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Konto.Shared.Masters.ProductType
{
    public partial class PTypeLkpWindow : LookupForm
    {
        List<PTypeLookupDto> _modelList = new List<PTypeLookupDto>();
        public PTypeLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.ProductType_lookup_Layout;
            this.FormClassName = "Konto.Shared.Masters.ProductType.PTypeIndex";
            this.AsemblyName = "Konto.Shared";
        }
        public override void LoadData()
        {
            base.LoadData();

            using (var _db = new KontoContext())
            {
                _modelList = (from st in _db.ProductTypes
                              where (!st.IsDeleted)
                              orderby st.TypeName
                              select new PTypeLookupDto
                              {
                                  
                                  DisplayText = st.TypeName,
                                  Id = st.Id,
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
