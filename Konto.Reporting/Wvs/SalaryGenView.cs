using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Reports;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Wvs
{
    public partial class SalaryGenView : KontoForm
    {
        public SalaryGenView()
        {
            InitializeComponent();
            fromDateEdit.EditValue = KontoGlobals.DFromDate;
            toDateEdit.EditValue = KontoGlobals.DToDate;
            getSimpleButton.Click += GetSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            rateSimpleButton.Click += RateSimpleButton_Click;
            this.FormClosed += SalaryGenView_FormClosed;
        }

        private void SalaryGenView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void RateSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new UpdateJobRateView();

            if (frm.ShowDialog() == DialogResult.OK)
                getSimpleButton.PerformClick();
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void GetSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewComboBoxEdit.Text == "Details")
                {
                    DetailsLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    pivotLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else
                {
                    DetailsLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    pivotLayoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }


                    int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
                int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

                

                using (var db = new KontoContext())
                {
                    var list = db.Database.SqlQuery<PatiaRegDto>("dbo.SalaryGeneration @companyid={0},@empid={1},@fromdate={2}," +
                            "@todate={3},@YearId={4}", KontoGlobals.CompanyId, Convert.ToInt32(empLookup1.SelectedValue),
                             fdate, tdate, KontoGlobals.YearId).ToList();

                    if (viewComboBoxEdit.Text == "Details")
                        gridControl1.DataSource = list;
                    else
                        pivotGridControl1.DataSource = list;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex, "patia register");
            }
        }
    }
}
