namespace Konto.Shared.Masters.Area
{
    partial class AreaLkpWindow
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
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(285, 33);
            // 
            // customGridControl1
            // 
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 33);
            this.customGridControl1.LookAndFeel.SkinName = "Office 2019 Colorful";
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(267, 250);
            this.customGridControl1.TabIndex = 3;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.customGridView1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.customGridView1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.customGridView1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.Row.Options.UseFont = true;
            this.customGridView1.GridControl = this.customGridControl1;
            this.customGridView1.Name = "customGridView1";
            this.customGridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.customGridView1.OptionsBehavior.Editable = false;
            this.customGridView1.OptionsBehavior.ReadOnly = true;
            this.customGridView1.OptionsCustomization.AllowColumnMoving = false;
            this.customGridView1.OptionsCustomization.AllowGroup = false;
            this.customGridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.customGridView1.OptionsCustomization.AllowRowSizing = true;
            this.customGridView1.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.customGridView1.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.customGridView1.OptionsView.ShowAutoFilterRow = true;
            this.customGridView1.OptionsView.ShowColumnHeaders = false;
            this.customGridView1.OptionsView.ShowDetailButtons = false;
            this.customGridView1.OptionsView.ShowFooter = true;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            this.customGridView1.OptionsView.ShowIndicator = false;
            this.customGridView1.RowHeight = 30;
            // 
            // AreaLkpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 309);
            this.Controls.Add(this.customGridControl1);
            this.KontoView = this.customGridView1;
            this.Name = "AreaLkpWindow";
            this.Text = "Select A Area";
            this.Shown += new System.EventHandler(this.AreaLkpWindow_Shown);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.customGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
    }
}