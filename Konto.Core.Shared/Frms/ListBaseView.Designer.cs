namespace Konto.Core.Shared.Frms
{
    partial class ListBaseView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Konto.Core.Shared.Frms.WaitForm1), true, true, typeof(System.Windows.Forms.UserControl), true);
            this.listAction1 = new Konto.Core.Shared.Libs.ListAction();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // listAction1
            // 
            this.listAction1.Location = new System.Drawing.Point(3, 2);
            this.listAction1.ModuleId = 0;
            this.listAction1.Name = "listAction1";
            this.listAction1.Size = new System.Drawing.Size(491, 29);
            this.listAction1.TabIndex = 1;
            this.listAction1.NewButtonClick += new System.EventHandler(this.listAction1_NewButtonClick);
            this.listAction1.EditButtonClick += new System.EventHandler(this.listAction1_EditButtonClick);
            this.listAction1.DeleteButtonClick += new System.EventHandler(this.listAction1_DeleteButtonClick);
            this.listAction1.CancleRecordClick += new System.EventHandler(this.listAction1_CancleRecordClick);
            this.listAction1.PrintButtonClick += new System.EventHandler(this.listAction1_PrintButtonClick);
            this.listAction1.RefreshButtonClick += new System.EventHandler(this.listAction1_RefreshButtonClick);
            this.listAction1.GridSettingsButtonClick += new System.EventHandler(this.listAction1_GridSettingsButtonClick);
            this.listAction1.ColumnSettingsButtonClick += new System.EventHandler(this.listAction1_ColumnSettingsButtonClick);
            this.listAction1.CancelSettingsButtonClick += new System.EventHandler(this.listAction1_CancelSettingsButtonClick);
            this.listAction1.ResetSettingsButtonClick += new System.EventHandler(this.listAction1_ResetSettingsButtonClick);
            this.listAction1.SaveSettingsButtonClick += new System.EventHandler(this.listAction1_SaveSettingsButtonClick);
            this.listAction1.ExcelButtonClick += new System.EventHandler(this.listAction1_ExcelButtonClick);
            this.listAction1.ImportButtonClick += new System.EventHandler(this.listAction1_ImportButtonClick);
            this.listAction1.WordButtonClick += new System.EventHandler(this.listAction1_WordButtonClick);
            this.listAction1.PdfButtonClick += new System.EventHandler(this.listAction1_PdfButtonClick);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.listAction1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(695, 35);
            this.panelControl2.TabIndex = 2;
            // 
            // panelControl3
            // 
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(646, 35);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(49, 276);
            this.panelControl3.TabIndex = 3;
            this.panelControl3.Visible = false;
            // 
            // ListBaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Name = "ListBaseView";
            this.Size = new System.Drawing.Size(695, 311);
            this.Load += new System.EventHandler(this.ListBaseView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        public DevExpress.XtraEditors.PanelControl panelControl2;
        public DevExpress.XtraEditors.PanelControl panelControl3;
        public Libs.ListAction listAction1;
    }
}
