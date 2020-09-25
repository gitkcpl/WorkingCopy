using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Konto.Core.Shared.Frms;
using Konto.Data.Models.Masters.Dtos;
using Konto.App.Shared;
using Konto.Core.Shared.Libs;
using Syncfusion.Windows.Forms;
using Serilog;
using Konto.Data;

namespace Konto.Shared.Masters.Country
{
    public partial class CountryListView : ListBaseView
    {
        private List<CountryListViewDto> _modelList = new List<CountryListViewDto>();
        public CountryListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.CountryMasterList_Layout;
        }

        private void CountryListView_Load(object sender, EventArgs e)
        {
           // this.RefreshGrid();
        }

        public override void RefreshGrid()
        {
            base.RefreshGrid();
            using (var _db = new KontoContext())
            {
                _modelList = (from st in _db.Countries
                              where (!st.IsDeleted)
                              orderby st.CountryName
                              select new CountryListViewDto
                              {
                                  CountryName = st.CountryName,
                                  CreateDate = st.CreateDate,
                                  CreateUser = st.CreateUser,
                                  Id = st.Id,
                                  IsActive = st.IsActive,
                                  ModifyDate = st.ModifyDate,
                                  ModifyUser = st.ModifyUser
                              }
                             ).ToList();
            }

            customGridControl1.DataSource = _modelList;
            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            if (_modelList.Count == 0)
                listAction1.EditDeleteDisabled(false);
            else
                listAction1.EditDeleteDisabled(true);
        }

        public override void DeleteRec()
        {
            base.DeleteRec();

            if (customGridView1.FocusedRowHandle < 0) return;
            try
            {
                var _id = Convert.ToInt32(this.customGridView1.GetRowCellValue(customGridView1.FocusedRowHandle, "Id"));

                if (MessageBoxAdv.Show(KontoGlobals.DeleteBeforeMsg, "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                using (var db = new KontoContext())
                {
                    var model = db.Countries.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "state delete");
                MessageBoxAdv.Show(ex.ToString());
            }
        }
    }
}
