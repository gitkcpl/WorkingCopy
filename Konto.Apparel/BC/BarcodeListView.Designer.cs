﻿namespace Konto.Apparel.BC
{
    partial class BarcodeListView
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
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.listDateRange1 = new Konto.Core.Shared.Libs.ListDateRange();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.listDateRange1);
            this.panelControl2.Size = new System.Drawing.Size(999, 35);
            this.panelControl2.Controls.SetChildIndex(this.listAction1, 0);
            this.panelControl2.Controls.SetChildIndex(this.listDateRange1, 0);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(950, 35);
            // 
            // listAction1
            // 
            this.listAction1.Size = new System.Drawing.Size(474, 29);
            // 
            // customGridControl1
            // 
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 35);
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(950, 276);
            this.customGridControl1.TabIndex = 7;
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
            this.customGridView1.OptionsView.ColumnAutoWidth = false;
            this.customGridView1.OptionsView.ShowFooter = true;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // listDateRange1
            // 
            this.listDateRange1.FromDate = 0;
            this.listDateRange1.IsAnalysis = false;
            this.listDateRange1.KontoGrid = this.customGridControl1;
            this.listDateRange1.Location = new System.Drawing.Point(483, 5);
            this.listDateRange1.Name = "listDateRange1";
            this.listDateRange1.SelectedItem = null;
            this.listDateRange1.Size = new System.Drawing.Size(505, 25);
            this.listDateRange1.TabIndex = 4;
            this.listDateRange1.ToDate = 0;
            this.listDateRange1.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // BarcodeListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customGridControl1);
            this.KontoGrid = this.customGridControl1;
            this.KontoView = this.customGridView1;
            this.Name = "BarcodeListView";
            this.Size = new System.Drawing.Size(999, 311);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panelControl3, 0);
            this.Controls.SetChildIndex(this.customGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private Core.Shared.Libs.ListDateRange listDateRange1;
    }
}
