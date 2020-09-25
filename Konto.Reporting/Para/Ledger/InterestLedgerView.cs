using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Ledger
{
    public partial class InterestLedgerView : KontoForm
    {
        public int ReportId { get; set; }
        public string Party { get; set; }
        public string AcGroup { get; set; }
        public int FromDate { get; set; }
        public int ToDate { get; set; }
        public int AccId { get; set; }
        List<LedgertransDto> Trans = new List<LedgertransDto>();
        public InterestLedgerView()
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.intPerSpinEdit.Value = 12;
            jvSimpleButton.Click += JvSimpleButton_Click;
        }

        private void JvSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new IntJournalView();
            frm.Rows = this.gridView1.GetSelectedRows();
            frm.IntGridView = this.gridView1;
            frm.Trans = this.Trans;
            frm.InterestPer = intPerSpinEdit.Value;
            frm.ShowDialog();
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            using(var db = new KontoContext())
            {
                 Trans = db.Database.SqlQuery<LedgertransDto>(
                "dbo.IntLedger_Reports @companyid={0},@fromdate={1},@todate={2},@party={3}" +
                ",@reportid={4},@yearid={5},@acgroup={6},@partyid={7},@IntPer={8},@Days={9},@TotalDays={10}",
                Convert.ToInt32(KontoGlobals.CompanyId), this.FromDate, this.ToDate,
                this.Party, this.ReportId, KontoGlobals.YearId, this.AcGroup, AccId, 
                    intPerSpinEdit.Value, dueDaysSpinEdit.Value, calculateOnComboBoxEdit.EditValue).ToList();

                detailsGridControl1.DataSource = Trans;
            }
            gridView1.ExpandAllGroups();
            gridView1.Focus();
        }

        private void gridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            var bal = view.GetGroupRowValue(e.RowHandle, colBal);
            var intbal = view.GetGroupRowValue(e.RowHandle, colIntAmt);
            info.GroupText = view.GetGroupRowValue(e.RowHandle, info.Column) + "  <color=#e3165b>" + bal + "</color > / <color=blue>"  + intbal + " </color>";
        }
    }
}
