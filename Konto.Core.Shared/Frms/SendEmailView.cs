using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Export.Html;
using GrapeCity.ActiveReports.Document;
using Konto.App.Shared;
using Konto.Data;
using Konto.Data.Models.Masters;
using MailBee.Security;
using MailBee.SmtpMail;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Konto.Core.Shared.Frms
{
    public partial class SendEmailView : KontoForm
    {
        public string ToEmailId { get; set; }
        public string AgentMailId { get; set; }
        public VoucherTypeEnum VoucherType { get; set; }
        public PageDocument ReportDoc { get; set; }
        public string FileName { get; set; }
        private CompModel Comp;
        public SendEmailView()
        {
            InitializeComponent();
            this.Load += SendEmailView_Load;
            atch2ButtonEdit.ButtonClick += Atch2ButtonEdit_ButtonClick;
            atch3ButtonEdit.ButtonClick += Atch3ButtonEdit_ButtonClick;
            atch4ButtonEdit.ButtonClick += Atch4ButtonEdit_ButtonClick;
            atch1ButtonEdit.ButtonClick += Atch1ButtonEdit_ButtonClick;
            this.sendSimpleButton.Click += SendSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
        }

        private void Atch1ButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var _openFile = new XtraOpenFileDialog();
            if(_openFile.ShowDialog() == DialogResult.OK)
            {
                atch1ButtonEdit.Text = _openFile.FileName;
            }
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void Atch4ButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var _openFile = new XtraOpenFileDialog();
            if (_openFile.ShowDialog() == DialogResult.OK)
            {
                atch4ButtonEdit.Text = _openFile.FileName;
            }
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void Atch3ButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var _openFile = new XtraOpenFileDialog();
            if (_openFile.ShowDialog() == DialogResult.OK)
            {
                atch3ButtonEdit.Text = _openFile.FileName;
            }
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void SendSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(toMailTextEdit.Text))
                {
                    MessageBox.Show("Please Enter Valid EmailId");
                    return;
                }
                else if (string.IsNullOrEmpty(subjectTextEdit.Text))
                {
                    MessageBox.Show("Please Enter Valid Subject");
                    return;
                }
                else if (string.IsNullOrEmpty(termsRichEditControl.Text))
                {
                    MessageBox.Show("Please Enter Valid Message");
                    return;
                }

                // company object
                MailBee.Global.LicenseKey = "MN120-4FA35C0C24043460043189003820-98EF";
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription("Sending Mail...");
                Smtp mailer = new Smtp();
                SmtpServer server = new SmtpServer(Comp.EmailPass, Comp.SendFrom, Comp.SendPass);

                string strBody = string.Empty;
                server.SslMode = SslStartupMode.UseStartTls;
                server.Port = Convert.ToInt32(Comp.SortName); // Convert.ToInt32(Comp.);
                mailer.SmtpServers.Add(server);
                mailer.From.Email = Comp.SendFrom;
                mailer.Subject = subjectTextEdit.Text;
                
                HtmlDocumentExporterOptions options = new HtmlDocumentExporterOptions();
                options.ExportRootTag = ExportRootTag.Body;
                options.CssPropertiesExportType = CssPropertiesExportType.Inline;
                HtmlExporter exporter = new HtmlExporter(this.termsRichEditControl.Model, options);
                string stringHtml = exporter.Export();

                mailer.BodyHtmlText = stringHtml;

                if (!string.IsNullOrEmpty(atch1ButtonEdit.Text))
                    mailer.Message.Attachments.Add(atch1ButtonEdit.Text);
                if (!string.IsNullOrEmpty(atch2ButtonEdit.Text))
                    mailer.Message.Attachments.Add(atch2ButtonEdit.Text);
                if (!string.IsNullOrEmpty(atch3ButtonEdit.Text))
                    mailer.Message.Attachments.Add(atch3ButtonEdit.Text);
                if (!string.IsNullOrEmpty(atch4ButtonEdit.Text))
                    mailer.Message.Attachments.Add(atch4ButtonEdit.Text);

                string[] strparty = toMailTextEdit.Text.Split(';');
                foreach (var mail in strparty)
                {
                    if (!string.IsNullOrEmpty(mail))
                        mailer.Message.To.Add(mail);
                }

                if (!string.IsNullOrEmpty(ccMailTextEdit.Text))
                    mailer.Cc.Add(ccMailTextEdit.Text);

                if (!string.IsNullOrEmpty(bccMailTextEdit.Text))
                    mailer.Bcc.Add(bccMailTextEdit.Text);

                if (mailer.Send())
                {
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Email Send Successfully.....");
                    this.Close();
                    this.Dispose();
                }
                else
                {
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Email Not Sended Successfully");
                   
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                Serilog.Log.Error(ex, "Error While Sending Mail");
                MessageBox.Show(ex.ToString());
                
            }
           
        }

        private void Atch2ButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var _openFile = new XtraOpenFileDialog();
            _openFile.FileOk += _openFile_FileOk;
            _openFile.ShowDialog();
        }

        private void _openFile_FileOk(object sender, CancelEventArgs e)
        {
            var _file = (XtraOpenFileDialog)sender;
            atch2ButtonEdit.Text = _file.FileName;
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void SendEmailView_Load(object sender, EventArgs e)
        {
            this.toMailTextEdit.Text = this.ToEmailId;
            using(var db = new KontoContext())
            {
                Comp = db.Companies.Find(KontoGlobals.CompanyId);
                var _id = (int)this.VoucherType;
                var vch = db.VoucherTypes.Find(_id);
                
                if (vch == null) return;
                termsRichEditControl.HtmlText = vch.EmailBody;
                subjectTextEdit.Text = vch.EmailSub;
                ccMailTextEdit.Text = vch.OtherEmail;
            }

            if (this.ReportDoc != null)
            {
                this.FileName   = string.Format(@"{0}.pdf", DateTime.Now.Ticks);
                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport PdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                PdfExport1.Export(this.ReportDoc, "Mailfile\\" + this.FileName);
                atch1ButtonEdit.Text = "Mailfile\\" + this.FileName;
                //System.IO.DirectoryInfo outputDirectory = new System.IO.DirectoryInfo("ExportPDF");
                //outputDirectory.Create();

                //// Provide settings for your rendering output.
                //GrapeCity.ActiveReports.Export.Pdf.Page.Settings pdfSetting = new GrapeCity.ActiveReports.Export.Pdf.Page.Settings();
                //GrapeCity.ActiveReports.Extensibility.Rendering.ISettings setting = pdfSetting;

                ////Set the rendering extension and render the report.
                //GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension pdfRenderingExtension = new GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension();
                //GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider outputProvider = new GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider(outputDirectory, System.IO.Path.GetFileNameWithoutExtension(outputDirectory.Name));
                //ReportDoc.Render(pdfRenderingExtension, outputProvider, pdfSetting);
            }
        }
    }
}
