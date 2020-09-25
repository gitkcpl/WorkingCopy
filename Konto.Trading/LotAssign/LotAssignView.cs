using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Konto.Trading.LotAssign
{
    public partial class LotAssignView : KontoForm
    {
        private List<LotAssignDto> UpdatedList = new List<LotAssignDto>();
        public LotAssignView()
        {
            InitializeComponent();
            this.Load += LotAssignView_Load;
            this.gridView1.RowUpdated += GridView1_RowUpdated;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.FormClosed += LotAssignView_FormClosed;
        }

        private void LotAssignView_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (KontoContext context = new KontoContext())
                {
                    foreach (var item in UpdatedList)
                    {
                        var mod = context.ChallanTranses.Find(item.BalId);
                        mod.LotNo = item.LotNo;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lot Assign");
                MessageBox.Show(ex.ToString());
                
            }
            MessageBox.Show("Record Updated !!");
        }

        private void GridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var rw = e.Row as LotAssignDto;
            UpdatedList.Add(rw);
        }

        private void LotAssignView_Load(object sender, EventArgs e)
        {
            using (var db = new KontoContext())
            {
                var _lotList = 
                    db.Database.SqlQuery<LotAssignDto>("dbo.PendingLotSp  @companyid={0},@yearid={1}"
                    , KontoGlobals.CompanyId, KontoGlobals.YearId).ToList();

                this.lotAssignDtoBindingSource.DataSource = _lotList;
            }
            this.ActiveControl = this.gridControl1;
        }

        private void cancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
