﻿namespace Konto.Shared.Trans.GRN
{
    partial class GRNListView
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
            this.listDateRange1 = new Konto.Core.Shared.Libs.ListDateRange();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.rcptGridControl = new Konto.Core.Shared.Libs.CustomGridControl();
            this.rcptGridView = new Konto.Core.Shared.Libs.CustomGridView();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcptGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcptGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.listDateRange1);
            this.panelControl2.Size = new System.Drawing.Size(1040, 35);
            this.panelControl2.Controls.SetChildIndex(this.listAction1, 0);
            this.panelControl2.Controls.SetChildIndex(this.listDateRange1, 0);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(991, 35);
            // 
            // listDateRange1
            // 
            this.listDateRange1.FromDate = 0;
            this.listDateRange1.IsAnalysis = false;
            this.listDateRange1.KontoGrid = this.customGridControl1;
            this.listDateRange1.Location = new System.Drawing.Point(477, 4);
            this.listDateRange1.Name = "listDateRange1";
            this.listDateRange1.SelectedItem = null;
            this.listDateRange1.Size = new System.Drawing.Size(514, 25);
            this.listDateRange1.TabIndex = 2;
            this.listDateRange1.ToDate = 0;
            this.listDateRange1.VoucherType = Konto.App.Shared.VoucherTypeEnum.Inward;
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 284);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(991, 27);
            this.panelControl1.TabIndex = 4;
            // 
            // rcptGridControl
            // 
            this.rcptGridControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rcptGridControl.Location = new System.Drawing.Point(0, 140);
            this.rcptGridControl.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.rcptGridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.rcptGridControl.MainView = this.rcptGridView;
            this.rcptGridControl.Name = "rcptGridControl";
            this.rcptGridControl.Size = new System.Drawing.Size(991, 144);
            this.rcptGridControl.TabIndex = 13;
            this.rcptGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rcptGridView});
            this.rcptGridControl.Visible = false;
            // 
            // rcptGridView
            // 
            this.rcptGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcptGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.rcptGridView.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcptGridView.Appearance.Row.Options.UseFont = true;
            this.rcptGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.rcptGridView.GridControl = this.rcptGridControl;
            this.rcptGridView.Name = "rcptGridView";
            this.rcptGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.rcptGridView.OptionsBehavior.Editable = false;
            this.rcptGridView.OptionsCustomization.AllowRowSizing = true;
            this.rcptGridView.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.rcptGridView.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.rcptGridView.OptionsLayout.Columns.StoreAllOptions = true;
            this.rcptGridView.OptionsLayout.Columns.StoreAppearance = true;
            this.rcptGridView.OptionsLayout.StoreAllOptions = true;
            this.rcptGridView.OptionsLayout.StoreAppearance = true;
            this.rcptGridView.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.rcptGridView.OptionsView.ColumnAutoWidth = false;
            this.rcptGridView.OptionsView.ShowFooter = true;
            this.rcptGridView.OptionsView.ShowGroupPanel = false;
            // 
            // customGridControl1
            // 
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 35);
            this.customGridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(991, 105);
            this.customGridControl1.TabIndex = 14;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.Row.Options.UseFont = true;
            this.customGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridView1.GridControl = this.customGridControl1;
            this.customGridView1.Name = "customGridView1";
            this.customGridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.customGridView1.OptionsBehavior.Editable = false;
            this.customGridView1.OptionsCustomization.AllowRowSizing = true;
            this.customGridView1.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.customGridView1.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.customGridView1.OptionsLayout.Columns.StoreAllOptions = true;
            this.customGridView1.OptionsLayout.Columns.StoreAppearance = true;
            this.customGridView1.OptionsLayout.StoreAllOptions = true;
            this.customGridView1.OptionsLayout.StoreAppearance = true;
            this.customGridView1.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.customGridView1.OptionsView.ColumnAutoWidth = false;
            this.customGridView1.OptionsView.ShowFooter = true;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // GRNListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.rcptGridControl);
            this.Controls.Add(this.panelControl1);
            this.KontoGrid = this.customGridControl1;
            this.KontoView = this.customGridView1;
            this.Name = "GRNListView";
            this.Size = new System.Drawing.Size(1040, 311);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panelControl3, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.rcptGridControl, 0);
            this.Controls.SetChildIndex(this.customGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcptGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcptGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Core.Shared.Libs.ListDateRange listDateRange1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private Core.Shared.Libs.CustomGridControl rcptGridControl;
        private Core.Shared.Libs.CustomGridView rcptGridView;
    }
}
