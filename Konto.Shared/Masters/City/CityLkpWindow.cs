using Konto.App.Shared;
using Konto.Core.Shared.Frms;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
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

namespace Konto.Shared.Masters.City
{
    public partial class CityLkpWindow : LookupForm
    {
        List<CityLookupDto> _modelList = new List<CityLookupDto>();
        public CityLkpWindow()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.CityLookup_Layout;
            this.FormClassName = "Konto.Shared.Masters.City.CityIndex";
            this.AsemblyName = "Konto.Shared";
        }

       

        public override void LoadData()
        {
            base.LoadData();

            using (var _db = new KontoContext())
            {
                _modelList = (from st in _db.Cities
                              join ct in _db.States on st.StateId equals ct.Id into ct_join
                              from ct in ct_join.DefaultIfEmpty()
                              where (!st.IsDeleted && st.IsActive)
                              orderby st.CityName
                              select new CityLookupDto
                              {
                                  State = ct.StateName,
                                  DisplayText = st.CityName,
                                  Id = st.Id,
                              }
                             ).ToList();
            }

            customGridControl1.DataSource = _modelList;

            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;


        }

        private void CityLkpWindow_Shown(object sender, EventArgs e)
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
