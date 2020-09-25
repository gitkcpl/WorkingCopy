namespace Konto.Core.Shared.Frms
{
    partial class KontoRepViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KontoRepViewer));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sendMailButton = new DevExpress.XtraEditors.SimpleButton();
            this.viewer1 = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
            this.pdfSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.excelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.excelSimpleButton);
            this.panelControl1.Controls.Add(this.pdfSimpleButton);
            this.panelControl1.Controls.Add(this.sendMailButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 34);
            this.panelControl1.TabIndex = 1;
            // 
            // sendMailButton
            // 
            this.sendMailButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendMailButton.Appearance.Options.UseFont = true;
            this.sendMailButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sendMailButton.ImageOptions.SvgImage")));
            this.sendMailButton.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.sendMailButton.Location = new System.Drawing.Point(5, 2);
            this.sendMailButton.Name = "sendMailButton";
            this.sendMailButton.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.sendMailButton.Size = new System.Drawing.Size(93, 29);
            this.sendMailButton.TabIndex = 4;
            this.sendMailButton.Text = "Send Mail";
            this.sendMailButton.ToolTip = "Create New Record (Ins)";
            // 
            // viewer1
            // 
            this.viewer1.AllowSplitter = false;
            this.viewer1.AnnotationDropDownVisible = true;
            this.viewer1.CurrentPage = 0;
            this.viewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer1.Location = new System.Drawing.Point(0, 34);
            this.viewer1.Name = "viewer1";
            this.viewer1.PreviewPages = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.viewer1.Sidebar.ParametersPanel.ContextMenu = null;
            this.viewer1.Sidebar.ParametersPanel.Text = "Parameters";
            this.viewer1.Sidebar.ParametersPanel.Width = 200;
            // 
            // 
            // 
            this.viewer1.Sidebar.SearchPanel.ContextMenu = null;
            this.viewer1.Sidebar.SearchPanel.Text = "Search results";
            this.viewer1.Sidebar.SearchPanel.Width = 200;
            // 
            // 
            // 
            this.viewer1.Sidebar.ThumbnailsPanel.ContextMenu = null;
            this.viewer1.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
            this.viewer1.Sidebar.ThumbnailsPanel.Width = 200;
            this.viewer1.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
            // 
            // 
            // 
            this.viewer1.Sidebar.TocPanel.ContextMenu = null;
            this.viewer1.Sidebar.TocPanel.Expanded = true;
            this.viewer1.Sidebar.TocPanel.Text = "Document map";
            this.viewer1.Sidebar.TocPanel.Width = 200;
            this.viewer1.Sidebar.Width = 200;
            this.viewer1.Size = new System.Drawing.Size(800, 424);
            this.viewer1.TabIndex = 2;
            this.viewer1.ViewType = GrapeCity.Viewer.Common.Model.ViewType.Continuous;
            // 
            // pdfSimpleButton
            // 
            this.pdfSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pdfSimpleButton.Appearance.Options.UseFont = true;
            this.pdfSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage1")));
            this.pdfSimpleButton.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.pdfSimpleButton.Location = new System.Drawing.Point(210, 2);
            this.pdfSimpleButton.Name = "pdfSimpleButton";
            this.pdfSimpleButton.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.pdfSimpleButton.Size = new System.Drawing.Size(62, 29);
            this.pdfSimpleButton.TabIndex = 5;
            this.pdfSimpleButton.Text = "PDF";
            this.pdfSimpleButton.ToolTip = "Create New Record (Ins)";
            // 
            // excelSimpleButton
            // 
            this.excelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.excelSimpleButton.Appearance.Options.UseFont = true;
            this.excelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.excelSimpleButton.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.excelSimpleButton.Location = new System.Drawing.Point(276, 2);
            this.excelSimpleButton.Name = "excelSimpleButton";
            this.excelSimpleButton.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.excelSimpleButton.Size = new System.Drawing.Size(68, 29);
            this.excelSimpleButton.TabIndex = 6;
            this.excelSimpleButton.Text = "Excel";
            this.excelSimpleButton.ToolTip = "Create New Record (Ins)";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Enabled = false;
            this.simpleButton2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton2.ImageOptions.SvgImage")));
            this.simpleButton2.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.simpleButton2.Location = new System.Drawing.Point(103, 2);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.simpleButton2.Size = new System.Drawing.Size(103, 29);
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "WhatsApp";
            this.simpleButton2.ToolTip = "Create New Record (Ins)";
            // 
            // KontoRepViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionFont = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 458);
            this.Controls.Add(this.viewer1);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "KontoRepViewer";
            this.Text = "KontoRepViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.KontoRepViewer_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KontoRepViewer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private GrapeCity.ActiveReports.Viewer.Win.Viewer viewer1;
        private DevExpress.XtraEditors.SimpleButton sendMailButton;
        private DevExpress.XtraEditors.SimpleButton pdfSimpleButton;
        private DevExpress.XtraEditors.SimpleButton excelSimpleButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}