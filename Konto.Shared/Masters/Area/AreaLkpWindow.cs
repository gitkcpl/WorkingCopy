using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Masters.Area
{
    public partial class AreaLkpWindow : LookupForm
    {
        List<AreaLookupDto> _modelList = new List<AreaLookupDto>();
        public AreaLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.AreaLookup_Layout;
            this.FormClassName = "Konto.Shared.Masters.Area.AreaIndex";
            this.AsemblyName = "Konto.Shared";
        }
        public override void LoadData()
        {
            base.LoadData();

            using (var _db = new KontoContext())
            {
                _modelList = (from ar in _db.Areas
                              join ct in _db.Cities on ar.CityId equals ct.Id into ct_join
                              from ct in ct_join.DefaultIfEmpty()
                              join st in _db.States on ct.StateId equals st.Id into st_join
                              from st in st_join.DefaultIfEmpty()
                              where (!st.IsDeleted && st.IsActive)
                              orderby ar.AreaName
                              select new AreaLookupDto
                              {
                                  State = st.StateName,
                                  DisplayText = ar.AreaName,
                                  City = ct.CityName,
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
