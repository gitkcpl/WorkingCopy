using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Masters.Dtos;
using Konto.Core.Shared.Libs;
using Serilog;
using Syncfusion.Windows.Forms;
using System.Windows.Forms;
using Konto.App.Shared;

namespace Konto.Shared.Masters.State
{
    public partial class StateListView : ListBaseView
    {
       
        private List<StateListViewDto> _stateList = new List<StateListViewDto>();
        public StateListView(object _mid)
        {
            InitializeComponent();
            this.listAction1.ModuleId = Convert.ToInt32(_mid);
            this.GridLayoutFileName = KontoFileLayout.StateMasterList_Layout;
        }

       

        public StateListView()
        {
            InitializeComponent();
            this.GridLayoutFileName = KontoFileLayout.StateMasterList_Layout;
        }

        private void StateListView_Load(object sender, EventArgs e)
        {
           // this.RefreshGrid();
            
            //customGridView1.po

        }

        public override void RefreshGrid()
        {
            base.RefreshGrid();
            using (var _db = new KontoContext())
            {
                _stateList = (from st in _db.States
                              join ct in _db.Countries on st.CountryId equals ct.Id into ct_join
                              from ct in ct_join.DefaultIfEmpty()
                              where (!st.IsDeleted)
                              orderby st.StateName
                              select new StateListViewDto
                              {
                                  StateName = st.StateName,
                                  Country = ct.CountryName,
                                  CountryId = st.CountryId,
                                  CreateDate = st.CreateDate,
                                  CreateUser = st.CreateUser,
                                  GstCode = st.GstCode,
                                  Id = st.Id,
                                  IsActive = st.IsActive,
                                  ModifyDate = st.ModifyDate,
                                  ModifyUser = st.ModifyUser
                              }
                             ).ToList();
            }

            customGridControl1.DataSource = _stateList;
            if (string.IsNullOrEmpty(this.GridLayoutFileName) || this.KontoView == null) return;

            KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, this.KontoView);

            this.ActiveControl = customGridControl1;
            if (_stateList.Count == 0)
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
                    var model = db.States.Find(_id);
                    model.IsDeleted = true;
                    db.SaveChanges();
                    customGridView1.DeleteRow(customGridView1.FocusedRowHandle);
                    MessageBoxAdv.Show(KontoGlobals.DeleteAfterMsg, "Delete !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "state delete");
                //MessageBoxAdv.Show()
            }
        }
    }
}
