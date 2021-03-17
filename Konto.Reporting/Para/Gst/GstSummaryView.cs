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
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Gst
{
    public partial class GstSummaryView : KontoForm
    {
        private List<GstSummaryCrossDto> _lst;
        public DateTime? _fromDate=null;
        public DateTime? _ToDate=null;
        public GstSummaryView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.FormClosed += GstSummaryView_FormClosed;
            fromDateEdit.EditValue = KontoGlobals.DFromDate;
            toDateEdit.EditValue = KontoGlobals.DToDate;
            c1FlexPivotPage1.FlexPivotChart.Hide();
            this.Load += GstSummaryView_Load;

            this.FirstActiveControl = fromDateEdit;

        }

        private void GstSummaryView_Load(object sender, EventArgs e)
        {
            if (this._fromDate != null)
            {
                fromDateEdit.EditValue = _fromDate;
                toDateEdit.EditValue = _ToDate;
            }
        }

        private void GstSummaryView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            

            try
            {
                var fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
                var tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));

                //using(var db = new KontoContext())
                //{
                //    db.Database.CommandTimeout = 0;

                //    _lst = db.Database.SqlQuery<GstSummaryCrossDto>("dbo.gst_summary_cross @fromdate={0},@todate={1}," +
                //        "@compid={2}", fdate, tdate, KontoGlobals.CompanyId).ToList();
                //    this.c1FlexPivotPage1.DataSource = _lst;

                //}
                using (var con = new SqlConnection(KontoGlobals.sqlConnectionString.ConnectionString))
                {

                    using (var cmd = new SqlCommand("dbo.gst_summary_cross", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@fromdate", SqlDbType.Int).Value = fdate;
                        cmd.Parameters.Add("@todate", SqlDbType.Int).Value = tdate;
                        cmd.Parameters.Add("@compid", SqlDbType.Int).Value = KontoGlobals.CompanyId;


                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        var DtCriterias = new DataTable();
                        DtCriterias.Load(cmd.ExecuteReader());
                        con.Close();
                        this.c1FlexPivotPage1.DataSource = DtCriterias;

                    }
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Gst Summary");
                MessageBox.Show(ex.ToString());
            }

            try
            {
                c1FlexPivotPage1.FlexPivotEngine.BeginUpdate();
                //set predefined view
                string xmlString = System.IO.File.ReadAllText("analysis\\gst_summ.olapx");
                // XmlDocument views = new XmlDocument();
                //  views.LoadXml(listDateRange1.SelectedItem.LayoutFile);
                //XmlNode nd = views.SelectSingleNode(string.Format("FlexPivotViews/C1FlexPivot[@id='{0}']", comboBox1.SelectedItem));
                
                c1FlexPivotPage1.FlexPivotPanel.ViewDefinition = xmlString;
                c1FlexPivotPage1.FlexPivotEngine.EndUpdate();
                c1FlexPivotPage1.FlexPivotGrid.Refresh();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    }
}
