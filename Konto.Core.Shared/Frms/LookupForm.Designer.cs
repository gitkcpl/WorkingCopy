namespace Konto.Core.Shared.Frms
{
    partial class LookupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupForm));
            this.statusBarAdv1 = new Syncfusion.Windows.Forms.Tools.StatusBarAdv();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lkpAction1 = new Konto.Core.Shared.Libs.lkpAction();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBarAdv1
            // 
            this.statusBarAdv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdv1.BeforeTouchSize = new System.Drawing.Size(279, 26);
            this.statusBarAdv1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.statusBarAdv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBarAdv1.CustomLayoutBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.statusBarAdv1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarAdv1.ForeColor = System.Drawing.Color.White;
            this.statusBarAdv1.Location = new System.Drawing.Point(0, 289);
            this.statusBarAdv1.Name = "statusBarAdv1";
            this.statusBarAdv1.Padding = new System.Windows.Forms.Padding(3);
            this.statusBarAdv1.Size = new System.Drawing.Size(279, 26);
            this.statusBarAdv1.Spacing = new System.Drawing.Size(2, 2);
            this.statusBarAdv1.TabIndex = 0;
            this.statusBarAdv1.ThemeName = "Metro";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lkpAction1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(279, 33);
            this.panelControl1.TabIndex = 1;
            // 
            // lkpAction1
            // 
            this.lkpAction1.Location = new System.Drawing.Point(1, 2);
            this.lkpAction1.Name = "lkpAction1";
            this.lkpAction1.Size = new System.Drawing.Size(278, 29);
            this.lkpAction1.TabIndex = 0;
            this.lkpAction1.NewButtonClick += new System.EventHandler(this.lkpAction1_NewButtonClick);
            this.lkpAction1.EditButtonClick += new System.EventHandler(this.lkpAction1_EditButtonClick);
            this.lkpAction1.OkButtonClick += new System.EventHandler(this.lkpAction1_OkButtonClick);
            this.lkpAction1.CancelButtonClick += new System.EventHandler(this.lkpAction1_CancelButtonClick);
            this.lkpAction1.GridSettingsButtonClick += new System.EventHandler(this.lkpAction1_GridSettingsButtonClick);
            this.lkpAction1.ColumnSettingsButtonClick += new System.EventHandler(this.lkpAction1_ColumnSettingsButtonClick);
            this.lkpAction1.CancelSettingsButtonClick += new System.EventHandler(this.lkpAction1_CancelSettingsButtonClick);
            this.lkpAction1.ResetSettingsButtonClick += new System.EventHandler(this.lkpAction1_ResetSettingsButtonClick);
            this.lkpAction1.SaveSettingsButtonClick += new System.EventHandler(this.lkpAction1_SaveSettingsButtonClick);
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(261, 33);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(18, 256);
            this.panelControl2.TabIndex = 2;
            this.panelControl2.Visible = false;
            // 
            // LookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BorderThickness = 2;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionFont = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(279, 315);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.statusBarAdv1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.Name = "LookupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LookupForm";
            this.Load += new System.EventHandler(this.LookupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.StatusBarAdv statusBarAdv1;
        public DevExpress.XtraEditors.PanelControl panelControl1;
        private Libs.lkpAction lkpAction1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}