using DevExpress.Data.ODataLinq.Helpers;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Masters.FinYear
{
    public partial class SelectYearView : KontoForm
    {
        private KontoContext db = new KontoContext();
        public FinYearModel FinYear { get; set; }
        public SelectYearView()
        {
            InitializeComponent();
            this.Load += SelectYearView_Load;
            this.lkpAction1.OkButtonClick += LkpAction1_OkButtonClick;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as FinYearModel;
                if (dr != null)
                {
                    KontoGlobals.YearId = dr.Id;
                    KontoGlobals.FromDate = (int) dr.FromDate;
                    KontoGlobals.ToDate = (int) dr.ToDate;
                    KontoGlobals.DFromDate = dr.FDate;
                    KontoGlobals.DToDate = dr.TDate;
                    this.FinYear = dr;
                    this.DialogResult = DialogResult.OK;
                }
            }
            else if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void LkpAction1_OkButtonClick(object sender, EventArgs e)
        {
            var dr = customGridView1.GetRow(customGridView1.FocusedRowHandle) as FinYearModel;
            if (dr != null)
            {
                KontoGlobals.YearId = dr.Id;
                KontoGlobals.FromDate = (int)dr.FromDate;
                KontoGlobals.ToDate = (int)dr.ToDate;
                KontoGlobals.DFromDate = dr.FDate;
                KontoGlobals.DToDate = dr.TDate;
                this.FinYear = dr;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void SelectYearView_Load(object sender, EventArgs e)
        {
            var lst = db.FinYears.Where(x => !x.IsDeleted).OrderByDescending(x => x.FromDate).ToList();
            if (lst.Count == 1)
            {
                KontoGlobals.YearId = lst[0].Id;
                KontoGlobals.YearId = lst[0].Id;
                KontoGlobals.FromDate = (int)lst[0].FromDate;
                KontoGlobals.ToDate = (int)lst[0].ToDate;
                KontoGlobals.DFromDate = lst[0].FDate;
                KontoGlobals.DToDate = lst[0].TDate;
                this.FinYear = lst[0];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            bindingSource1.DataSource = lst;
        }
    }
}
