namespace Konto.Core.Shared.Frms
{
    partial class KontoMetroForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KontoMetroForm));
            this.tabControlAdv1 = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.tabPageAdv1 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.createdLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.modifyLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.navAction1 = new Konto.Core.Shared.Libs.NavAction();
            this.tabPageAdv2 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.tabPageAdv3 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.tabPageAdv4 = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.helpStatusBarAdvPanel = new Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel();
            this.msgStatusBarAdvPanel = new Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel();
            this.statusBarAdv1 = new Syncfusion.Windows.Forms.Tools.StatusBarAdv();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Konto.Core.Shared.Frms.WaitForm1), true, true, true);
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.helpStatusBarAdvPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.msgStatusBarAdvPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).BeginInit();
            this.statusBarAdv1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.ActiveTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabControlAdv1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(543, 279);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv1);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv2);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv3);
            this.tabControlAdv1.Controls.Add(this.tabPageAdv4);
            this.tabControlAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAdv1.FocusOnTabClick = false;
            this.tabControlAdv1.Location = new System.Drawing.Point(0, 0);
            this.tabControlAdv1.Name = "tabControlAdv1";
            this.tabControlAdv1.Size = new System.Drawing.Size(543, 279);
            this.tabControlAdv1.TabIndex = 4;
            this.tabControlAdv1.TabStop = false;
            this.tabControlAdv1.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererOffice2016Colorful);
            this.tabControlAdv1.ThemeName = "TabRendererOffice2016Colorful";
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            this.tabControlAdv1.SelectedIndexChanged += new System.EventHandler(this.tabControlAdv1_SelectedIndexChanged);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPageAdv1.Controls.Add(this.tableLayoutPanel1);
            this.tabPageAdv1.Controls.Add(this.panelControl1);
            this.tabPageAdv1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabPageAdv1.Image = null;
            this.tabPageAdv1.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv1.Location = new System.Drawing.Point(2, 1);
            this.tabPageAdv1.Name = "tabPageAdv1";
            this.tabPageAdv1.ShowCloseButton = true;
            this.tabPageAdv1.Size = new System.Drawing.Size(518, 276);
            this.tabPageAdv1.TabIndex = 1;
            this.tabPageAdv1.Text = "Main Page";
            this.tabPageAdv1.ThemesEnabled = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.Controls.Add(this.okSimpleButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cancelSimpleButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.createdLabelControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.modifyLabelControl, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 238);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 38);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(327, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 32);
            this.okSimpleButton.TabIndex = 4;
            this.okSimpleButton.Text = "Ok [F10]";
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseBackColor = true;
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(424, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 32);
            this.cancelSimpleButton.TabIndex = 5;
            this.cancelSimpleButton.Text = "Cancel";
            this.cancelSimpleButton.Click += new System.EventHandler(this.cancelSimpleButton_Click);
            // 
            // createdLabelControl
            // 
            this.createdLabelControl.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createdLabelControl.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.createdLabelControl.Appearance.Options.UseFont = true;
            this.createdLabelControl.Appearance.Options.UseForeColor = true;
            this.createdLabelControl.Location = new System.Drawing.Point(3, 3);
            this.createdLabelControl.Name = "createdLabelControl";
            this.createdLabelControl.Size = new System.Drawing.Size(69, 17);
            this.createdLabelControl.TabIndex = 6;
            this.createdLabelControl.Text = "Created By:";
            // 
            // modifyLabelControl
            // 
            this.modifyLabelControl.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifyLabelControl.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.modifyLabelControl.Appearance.Options.UseFont = true;
            this.modifyLabelControl.Appearance.Options.UseForeColor = true;
            this.modifyLabelControl.Location = new System.Drawing.Point(165, 3);
            this.modifyLabelControl.Name = "modifyLabelControl";
            this.modifyLabelControl.Size = new System.Drawing.Size(72, 17);
            this.modifyLabelControl.TabIndex = 7;
            this.modifyLabelControl.Text = "Modifed By:";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.navAction1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(518, 35);
            this.panelControl1.TabIndex = 6;
            // 
            // navAction1
            // 
            this.navAction1.Location = new System.Drawing.Point(1, 2);
            this.navAction1.ModuleId = 0;
            this.navAction1.Name = "navAction1";
            this.navAction1.RecPos = 0;
            this.navAction1.Size = new System.Drawing.Size(512, 29);
            this.navAction1.TabIndex = 0;
            this.navAction1.TabStop = false;
            this.navAction1.TotalRecord = 0;
            this.navAction1.NewButtonClick += new System.EventHandler(this.navAction1_NewButtonClick);
            this.navAction1.FilterButtonClick += new System.EventHandler(this.navAction1_FilterButtonClick);
            this.navAction1.ListButtonClick += new System.EventHandler(this.navAction1_ListButtonClick);
            this.navAction1.FirstButtonClick += new System.EventHandler(this.navAction1_FirstButtonClick);
            this.navAction1.NextButtonClick += new System.EventHandler(this.navAction1_NextButtonClick);
            this.navAction1.PrevButtonClick += new System.EventHandler(this.navAction1_PrevButtonClick);
            this.navAction1.LastButtonClick += new System.EventHandler(this.navAction1_LastButtonClick);
            this.navAction1.SettingButtonClick += new System.EventHandler(this.navAction1_SettingButtonClick);
            // 
            // tabPageAdv2
            // 
            this.tabPageAdv2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPageAdv2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabPageAdv2.Image = null;
            this.tabPageAdv2.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv2.Location = new System.Drawing.Point(2, 1);
            this.tabPageAdv2.Name = "tabPageAdv2";
            this.tabPageAdv2.ShowCloseButton = true;
            this.tabPageAdv2.Size = new System.Drawing.Size(518, 276);
            this.tabPageAdv2.TabIndex = 2;
            this.tabPageAdv2.Text = "List Page";
            this.tabPageAdv2.ThemesEnabled = false;
            // 
            // tabPageAdv3
            // 
            this.tabPageAdv3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPageAdv3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabPageAdv3.Image = null;
            this.tabPageAdv3.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv3.Location = new System.Drawing.Point(2, 1);
            this.tabPageAdv3.Name = "tabPageAdv3";
            this.tabPageAdv3.ShowCloseButton = true;
            this.tabPageAdv3.Size = new System.Drawing.Size(518, 276);
            this.tabPageAdv3.TabIndex = 3;
            this.tabPageAdv3.Text = "Analysis";
            this.tabPageAdv3.ThemesEnabled = false;
            // 
            // tabPageAdv4
            // 
            this.tabPageAdv4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPageAdv4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabPageAdv4.Image = null;
            this.tabPageAdv4.ImageSize = new System.Drawing.Size(16, 16);
            this.tabPageAdv4.Location = new System.Drawing.Point(2, 1);
            this.tabPageAdv4.Name = "tabPageAdv4";
            this.tabPageAdv4.ShowCloseButton = true;
            this.tabPageAdv4.Size = new System.Drawing.Size(518, 276);
            this.tabPageAdv4.TabIndex = 4;
            this.tabPageAdv4.Text = "Reports";
            this.tabPageAdv4.ThemesEnabled = false;
            // 
            // helpStatusBarAdvPanel
            // 
            this.helpStatusBarAdvPanel.AutoSize = true;
            this.helpStatusBarAdvPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.helpStatusBarAdvPanel.BeforeTouchSize = new System.Drawing.Size(221, 20);
            this.helpStatusBarAdvPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.helpStatusBarAdvPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpStatusBarAdvPanel.ForeColor = System.Drawing.Color.White;
            this.helpStatusBarAdvPanel.Location = new System.Drawing.Point(240, 2);
            this.helpStatusBarAdvPanel.Margin = new System.Windows.Forms.Padding(0);
            this.helpStatusBarAdvPanel.Name = "helpStatusBarAdvPanel";
            this.helpStatusBarAdvPanel.Size = new System.Drawing.Size(221, 20);
            this.helpStatusBarAdvPanel.TabIndex = 1;
            this.helpStatusBarAdvPanel.Text = null;
            // 
            // msgStatusBarAdvPanel
            // 
            this.msgStatusBarAdvPanel.AutoSize = true;
            this.msgStatusBarAdvPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.msgStatusBarAdvPanel.BeforeTouchSize = new System.Drawing.Size(238, 20);
            this.msgStatusBarAdvPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.msgStatusBarAdvPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msgStatusBarAdvPanel.CanApplyTheme = false;
            this.msgStatusBarAdvPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgStatusBarAdvPanel.ForeColor = System.Drawing.Color.White;
            this.msgStatusBarAdvPanel.Location = new System.Drawing.Point(0, 2);
            this.msgStatusBarAdvPanel.Margin = new System.Windows.Forms.Padding(0);
            this.msgStatusBarAdvPanel.Name = "msgStatusBarAdvPanel";
            this.msgStatusBarAdvPanel.Size = new System.Drawing.Size(238, 20);
            this.msgStatusBarAdvPanel.TabIndex = 0;
            this.msgStatusBarAdvPanel.Text = null;
            this.msgStatusBarAdvPanel.ThemeStyle.BackColor = System.Drawing.Color.Empty;
            // 
            // statusBarAdv1
            // 
            this.statusBarAdv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdv1.BeforeTouchSize = new System.Drawing.Size(543, 26);
            this.statusBarAdv1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBarAdv1.Controls.Add(this.msgStatusBarAdvPanel);
            this.statusBarAdv1.Controls.Add(this.helpStatusBarAdvPanel);
            this.statusBarAdv1.CustomLayoutBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.statusBarAdv1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarAdv1.ForeColor = System.Drawing.Color.White;
            this.statusBarAdv1.Location = new System.Drawing.Point(0, 279);
            this.statusBarAdv1.Name = "statusBarAdv1";
            this.statusBarAdv1.Padding = new System.Windows.Forms.Padding(3);
            this.statusBarAdv1.Size = new System.Drawing.Size(543, 26);
            this.statusBarAdv1.Spacing = new System.Drawing.Size(2, 2);
            this.statusBarAdv1.Style = Syncfusion.Windows.Forms.Tools.StatusbarStyle.Metro;
            this.statusBarAdv1.TabIndex = 3;
            this.statusBarAdv1.ThemeName = "Metro";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // alertControl1
            // 
            this.alertControl1.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alertControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.alertControl1.AppearanceCaption.Options.UseFont = true;
            this.alertControl1.AppearanceCaption.Options.UseForeColor = true;
            this.alertControl1.AppearanceText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alertControl1.AppearanceText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.alertControl1.AppearanceText.Options.UseFont = true;
            this.alertControl1.AppearanceText.Options.UseForeColor = true;
            this.alertControl1.AutoFormDelay = 3000;
            this.alertControl1.FormLocation = DevExpress.XtraBars.Alerter.AlertFormLocation.BottomLeft;
            this.alertControl1.LookAndFeel.SkinName = "Office 2019 Colorful";
            this.alertControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.alertControl1.ShowPinButton = false;
            // 
            // KontoMetroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarHeight = 30;
            this.CaptionFont = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.ClientSize = new System.Drawing.Size(543, 305);
            this.Controls.Add(this.tabControlAdv1);
            this.Controls.Add(this.statusBarAdv1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "KontoMetroForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KontoMetroForm_FormClosed);
            this.Load += new System.EventHandler(this.KontoMetroForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.helpStatusBarAdvPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.msgStatusBarAdvPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).EndInit();
            this.statusBarAdv1.ResumeLayout(false);
            this.statusBarAdv1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public Syncfusion.Windows.Forms.Tools.TabControlAdv tabControlAdv1;
        public Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv1;
        public DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        public Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv2;
        public Libs.NavAction navAction1;
        private Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel helpStatusBarAdvPanel;
        private Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel msgStatusBarAdvPanel;
        private Syncfusion.Windows.Forms.Tools.StatusBarAdv statusBarAdv1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.LabelControl modifyLabelControl;
        public DevExpress.XtraEditors.LabelControl createdLabelControl;
        public Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv3;
        public Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageAdv4;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
    }
}