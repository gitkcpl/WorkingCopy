using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.UI;
using GrapeCity.ActiveReports.Document;
using GrapeCity.ActiveReports.ReportsCore.Configuration;
using GrapeCity.ActiveReports.Viewer.Win.Internal.Export;
using Konto.App.Shared;
using Konto.Core.Shared.Export;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Core.Shared.Frms
{
    public partial class KontoRepViewer : KontoForm
    {
        private ExportForm _exportForm;

        private ExportForm.ReportType? _reportType;
        private GrapeCity.ActiveReports.Document.PageDocument _doc;
        public string ToMailId { get; set; }
        public VoucherTypeEnum VoucherType { get; set; }

        public string AgentMailId { get; set; }
        
        public KontoRepViewer()
        {
            InitializeComponent();
            this.Load += KontoRepViewer_Load;
            viewer1.LoadCompleted += Viewer1_LoadCompleted;
            this.FormClosed += KontoRepViewer_FormClosed;
            this.sendMailButton.Click += SendMailButton_Click;
            this.pdfSimpleButton.Click += PdfSimpleButton_Click;
            this.excelSimpleButton.Click += ExcelSimpleButton_Click;
        }

        private void ExcelSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {


                var _file = string.Format(@"{0}.xlsx", DateTime.Now.Ticks);
                GrapeCity.ActiveReports.Export.Excel.Section.XlsExport PdfExport1 = new GrapeCity.ActiveReports.Export.Excel.Section.XlsExport();
                PdfExport1.FileFormat = GrapeCity.ActiveReports.Export.Excel.Section.FileFormat.Xlsx;
                PdfExport1.Export(this._doc, "ExportFile\\" + _file);
                if (MessageBox.Show("File Exported Successfully, Do You want to open File ?", "Export",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string windir = Environment.GetEnvironmentVariable("WINDIR");
                    Process prc = new Process();
                    prc.StartInfo.FileName = windir + @"\explorer.exe";
                    prc.StartInfo.Arguments = "ExportFile\\" + _file; ;
                    prc.Start();
                }
            }
            catch (Exception ex)
            {
               Serilog.Log.Error(ex, "excel Export");
                MessageBox.Show(ex.ToString());
            }
        }

       

        private void PdfSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {


                var _file = string.Format(@"{0}.pdf", DateTime.Now.Ticks);
                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport PdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                PdfExport1.Export(this._doc, "ExportFile\\" + _file);
                if (MessageBox.Show("File Exported Successfully, Do You want to open File ?", "Export",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string windir = Environment.GetEnvironmentVariable("WINDIR");
                    Process prc = new Process();
                    prc.StartInfo.FileName = windir + @"\explorer.exe";
                    prc.StartInfo.Arguments = "ExportFile\\" + _file;
                    prc.Start();
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "pdf Export");
                MessageBox.Show(ex.ToString());
            }
        }

       

        private void SendMailButton_Click(object sender, EventArgs e)
        {
            var frm = new SendEmailView();
            frm.ToEmailId = this.ToMailId;
            frm.VoucherType = this.VoucherType;
            frm.ReportDoc = _doc;
            frm.ShowDialog();
        }

        private void KontoRepViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        public KontoRepViewer(GrapeCity.ActiveReports.Document.PageDocument doc)
        {
            InitializeComponent();
            viewer1.LoadCompleted += Viewer1_LoadCompleted;
            this.FormClosed += KontoRepViewer_FormClosed;
            this.Load += KontoRepViewer_Load;
            this.sendMailButton.Click += SendMailButton_Click;
            this.pdfSimpleButton.Click += PdfSimpleButton_Click;
            this.excelSimpleButton.Click += ExcelSimpleButton_Click;
            _doc = doc;
            _doc.LocateDataSource += new GrapeCity.ActiveReports.LocateDataSourceEventHandler(Doc_LocateDataSource);
            
            viewer1.LoadDocument(doc);
            //viewer1.Sidebar.Visible = true;
            //viewer1.Sidebar.SelectedIndex = 3;
        }
        private void Doc_LocateDataSource(object sender, GrapeCity.ActiveReports.LocateDataSourceEventArgs args)
        {
            args.Report.PageReport.Report.DataSources[0].ConnectionProperties.ConnectString = KontoGlobals.sqlConnectionString.ConnectionString;
        }
        public void LoadDocument(FileInfo file)
        {
            try
            {
                //Icon ico = new Icon(GetType().Module.Assembly.GetManifestResourceStream("myLib.Resources.Export16x16.ico"));
                //viewer1.Toolbar.ToolStrip.ImageList
                //    = new ImageList();
                //viewer1.Toolbar.ToolStrip.ImageList.Images.Add("exporticon", ico);
                //ToolStrip ts = viewer1.Toolbar.ToolStrip;
                //ToolStripButton tsbExport = new ToolStripButton("Export Excel");
                //tsbExport.Image = viewer1.Toolbar.ToolStrip.ImageList.Images["exporticon"];
                //tsbExport.Click += new EventHandler(tsbExport_Click);
                //ts.Items.Add(new ToolStripSeparator());
                //ts.Items.Add(tsbExport);

                this._reportType = new ExportForm.ReportType?(ViewerHelper.DetermineReportType(file));
                //string str = (!ViewerHelper.isRdf(file) ? ViewerHelper.GetReportServerUri(file) : string.Empty);
                //if (string.IsNullOrEmpty(str))
                //{
                //    this.viewer1.LoadDocument(file.FullName);
                //}


                //this.SetReportName(this.openFileDialog.FileName);
            }
            catch (Exception exception)
            {
                this.viewer1.HandleError(exception);
            }
        }

        private void ExportDocument()
        {
            ExportForm.ReportType reportType;


            if (this._exportForm == null)
            {
                bool? configFlag = ConfigurationHelper.GetConfigFlag("UsePdfExportFilter");
                this._exportForm = new ExportForm((!configFlag.GetValueOrDefault() ? false : configFlag.HasValue));
            }
            ExportForm.ReportType? nullable = this._reportType;
            reportType = (nullable.HasValue ? nullable.GetValueOrDefault() : this.DetermineOpenedReportType());
            var vw = new ExportViewer(this.viewer1);


            this._exportForm.Text = "Export Report";

            this._exportForm.Show(this, new ExportViewer(this.viewer1), reportType);

            //ExportFormold exportForm = new ExportFormold( PageDocument);
            //exportForm.ShowDialog(this);
        }
        private void KontoRepViewer_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            Icon ico = new Icon(GetType().Module.Assembly.GetManifestResourceStream("Konto.Core.Shared.Resources.Export16x16.ico"));
            viewer1.Toolbar.ToolStrip.ImageList
                = new ImageList();
            viewer1.Toolbar.ToolStrip.ImageList.Images.Add("exporticon", ico);
            ToolStrip ts = viewer1.Toolbar.ToolStrip;
            ToolStripButton tsbExport = new ToolStripButton("Export");
            tsbExport.Image = viewer1.Toolbar.ToolStrip.ImageList.Images["exporticon"];
            tsbExport.Click += new EventHandler(tsbExport_Click);
            ts.Items.Add(new ToolStripSeparator());
            ts.Items.Add(tsbExport);

            ToolStripButton tsbDesigner = new ToolStripButton("Designer");
            tsbDesigner.Click += TsbDesigner_Click;
            ts.Items.Add(new ToolStripSeparator());
            ts.Items.Add(tsbDesigner);
            this.ActiveControl = viewer1;
        }
        private void TsbDesigner_Click(object sender, EventArgs e)
        {
            ////var frmd = new KontoArDesignerView();
            ////frmd.endUserDesigner1._reportName = this.;
            ////frmd.endUserDesigner1.reportDesigner.LoadReport(new FileInfo(rep.FileName));
            ////frmd.Text = "Konto Designer - " + rep.FileName;
            ////frmd.Show();
            ////GrapeCity.ActiveReports.Document.PageDocument reportDocument = new GrapeCity.ActiveReports.Document.PageDocument(_doc.PageReport);
            //var dlg = new SaveFileDialog();
            //if (dlg.ShowDialog() == DialogResult.Cancel) return;
            //var outputDirectory = new System.IO.DirectoryInfo(dlg.FileName);
            //outputDirectory.Create();

            //GrapeCity.ActiveReports.Export.Excel.Page.ExcelRenderingExtensionSettings excelSetting = new
            //    GrapeCity.ActiveReports.Export.Excel.Page.ExcelRenderingExtensionSettings();

            //excelSetting.FileFormat = GrapeCity.ActiveReports.Export.Excel.Page.FileFormat.Xlsx;
            //excelSetting.MultiSheet = false;
            //excelSetting.Pagination = false;
            //GrapeCity.ActiveReports.Extensibility.Rendering.ISettings setting = excelSetting;
            //// Set the rendering extension and render the report.
            //GrapeCity.ActiveReports.Export.Excel.Page.ExcelRenderingExtension
            //excelRenderingExtension = new GrapeCity.ActiveReports.Export.Excel.Page.ExcelRenderingExtension();
            //GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider outputProvider = new
            //GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider(outputDirectory,
            //System.IO.Path.GetFileNameWithoutExtension(outputDirectory.Name));
            //// Overwrite output file if it already exists.
            //outputProvider.OverwriteOutputFile = true;
            //_doc.Render(excelRenderingExtension, outputProvider,
            //setting.GetSettings());

           
            //string windir = Environment.GetEnvironmentVariable("WINDIR");
            //Process prc = new Process();
            //prc.StartInfo.FileName = windir + @"\explorer.exe";
            //prc.StartInfo.Arguments = outputDirectory.FullName + "\\" + outputDirectory.Name +  ".xlsx";
            //prc.Start();
        }
        private void tsbExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportDocument();
                //GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport PdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                //PdfExport1.Export(viewer1.Document, Application.StartupPath + "\\PDFExpt.pdf");

            }
            catch (Exception ex)
            {

            }
        }
        private void Viewer1_LoadCompleted(object sender, EventArgs e)
        {
            viewer1.Focus();
        }
        private ExportForm.ReportType DetermineOpenedReportType()
        {
            if (this.viewer1.Document != null)
            {
                return ExportForm.ReportType.Section;
            }
            if (this.viewer1.IsFplDocumentOpened())
            {
                return ExportForm.ReportType.PageFpl;
            }
            return ExportForm.ReportType.PageCpl;
        }

        private void KontoRepViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void KontoRepViewer_Shown(object sender, EventArgs e)
        {
            this.Focus();
            viewer1.Focus();
            //SendKeys.SendWait("{TAB}");
            //SendKeys.SendWait("{TAB}");
            //SendKeys.SendWait("{TAB}");
        }
    }
}
