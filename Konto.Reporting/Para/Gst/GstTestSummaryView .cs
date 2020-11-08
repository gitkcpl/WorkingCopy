using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Reports;
using Serilog;
using Syncfusion.Windows.Forms.PivotAnalysis.Serialization;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using System.Diagnostics;
using Syncfusion.PivotAnalysis.Base;
using Syncfusion.PivotConverter;
using Syncfusion.Windows.Forms;
using Syncfusion.XlsIO;

namespace Konto.Reporting.Para.Gst
{
    public partial class GstTestSummaryView : KontoForm
    {
        private List<GstSummaryCrossDto> _lst;
        public DateTime? _fromDate=null;
        public DateTime? _ToDate=null;
        SerializationOptions serailizationOptions = new SerializationOptions();
        DeserializationOptions deserailizationOptions = new DeserializationOptions();
        public GstTestSummaryView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.FormClosed += GstSummaryView_FormClosed;
            fromDateEdit.EditValue = KontoGlobals.DFromDate;
            toDateEdit.EditValue = KontoGlobals.DToDate;
            this.saveLayoutSimpleButton.Click += SaveLayoutSimpleButton_Click;
            this.Load += GstSummaryView_Load;
            this.excelSimpleButton.Click += ExcelSimpleButton_Click;

        }

        private void ExcelSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                var ExportAsPivotTable = (comboBoxEdit1.SelectedIndex == 0);


                var _file = string.Format(@"{0}.xlsx", DateTime.Now.Ticks);

                ExcelExport excelExport = new ExcelExport(pivotGridControl1, ExcelVersion.Excel2010);
                excelExport.ExportMode = (ExportAsPivotTable) ? ExportModes.PivotTable : ExportModes.Cell;
                excelExport.Export("ExportFile\\" + _file);

                if (MessageBox.Show(@"Export Success! Do you want to open the exported file?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Process.Start("ExportFile\\" + _file);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                Log.Error(ex.ToString(), "Pivot Export");
            }
           
        }

        private void SaveLayoutSimpleButton_Click(object sender, EventArgs e)
        {
            this.pivotGridControl1.Serialize("analysis\\gst_sum.xml", serialization());
        }

        private void GstSummaryView_Load(object sender, EventArgs e)
        {
            if (this._fromDate != null)
            {
                fromDateEdit.EditValue = _fromDate;
                toDateEdit.EditValue = _ToDate;
            }
            this.pivotGridControl1.ShowPivotTableFieldList = true;
            this.pivotGridControl1.TableControl.AllowRowPivotFiltering = true;
            this.pivotGridControl1.TableControl.FreezeHeaders = true;
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

                using (var db = new KontoContext())
                {
                    db.Database.CommandTimeout = 0;

                    _lst = db.Database.SqlQuery<GstSummaryCrossDto>("dbo.gst_summary_cross @fromdate={0},@todate={1}," +
                        "@compid={2}", fdate, tdate, KontoGlobals.CompanyId).ToList();

                    this.pivotGridControl1.ItemSource = _lst;

                }

                if(File.Exists("analysis\\gst_sum.xml"))
                {
                    this.pivotGridControl1.Deserialize("analysis\\gst_sum.xml", deserialization());
                }

                //using (var con = new SqlConnection(KontoGlobals.sqlConnectionString.ConnectionString))
                //{

                //    using (var cmd = new SqlCommand("dbo.gst_summary_cross", con))
                //    {
                //        cmd.CommandTimeout = 0;
                //        cmd.Parameters.Add("@fromdate", SqlDbType.Int).Value = fdate;
                //        cmd.Parameters.Add("@todate", SqlDbType.Int).Value = tdate;
                //        cmd.Parameters.Add("@compid", SqlDbType.Int).Value = KontoGlobals.CompanyId;


                //        cmd.CommandType = CommandType.StoredProcedure;

                //        con.Open();
                //        var DtCriterias = new DataTable();
                //        DtCriterias.Load(cmd.ExecuteReader());
                //        con.Close();


                //    }
                //}
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Gst Summary");
                MessageBox.Show(ex.ToString());
            }

            
        }

        private SerializationOptions serialization()
        {
            serailizationOptions.SerializeFiltering = false;
            serailizationOptions.SerializeSorting = true;
            serailizationOptions.SerializeGrouping = true;
            serailizationOptions.SerializeConditionalFormats = true;
            serailizationOptions.SerializePivotRows = true;
            serailizationOptions.SerializePivotColumns = true;
            serailizationOptions.SerializePivotCalculations = true;
            serailizationOptions.SerializeExpandCollapseState = true;
            return serailizationOptions;
        }

        private DeserializationOptions deserialization()
        {
            deserailizationOptions.DeserializeFiltering = false;
            deserailizationOptions.DeserializeGrouping = true;
            deserailizationOptions.DeserializeSorting = true;
            deserailizationOptions.DeserializeConditionalFormats = true;
            deserailizationOptions.DeserailizePivotRows = true;
            deserailizationOptions.DeserializePivotColumns = true;
            deserailizationOptions.DeserializePivotCalculations = true;
            deserailizationOptions.DeserializeExpandCollapseState = true;
            
            return deserailizationOptions;
        }
    }
}
