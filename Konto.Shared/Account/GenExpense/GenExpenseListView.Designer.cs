namespace Konto.Shared.Account.GenExpense
{
    partial class GenExpenseListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenExpenseListView));
            this.listDateRange1 = new Konto.Core.Shared.Libs.ListDateRange();
            this.panel1 = new System.Windows.Forms.Panel();
            this.attachSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customGridControl3 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView3 = new Konto.Core.Shared.Libs.CustomGridView();
            this.customGridControl2 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView2 = new Konto.Core.Shared.Libs.CustomGridView();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView2)).BeginInit();
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
            this.listDateRange1.Location = new System.Drawing.Point(477, 4);
            this.listDateRange1.Name = "listDateRange1";
            this.listDateRange1.SelectedItem = null;
            this.listDateRange1.Size = new System.Drawing.Size(514, 25);
            this.listDateRange1.TabIndex = 2;
            this.listDateRange1.ToDate = 0;
            this.listDateRange1.VoucherType = Konto.App.Shared.VoucherTypeEnum.GenExpense;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.attachSimpleButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 281);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 30);
            this.panel1.TabIndex = 15;
            // 
            // attachSimpleButton
            // 
            this.attachSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attachSimpleButton.Appearance.Options.UseFont = true;
            this.attachSimpleButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("attachSimpleButton.ImageOptions.Image")));
            this.attachSimpleButton.Location = new System.Drawing.Point(3, 3);
            this.attachSimpleButton.Name = "attachSimpleButton";
            this.attachSimpleButton.Size = new System.Drawing.Size(103, 24);
            this.attachSimpleButton.TabIndex = 7;
            this.attachSimpleButton.TabStop = false;
            this.attachSimpleButton.Text = "Attachment";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.customGridControl3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customGridControl2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 168);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(991, 113);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // customGridControl3
            // 
            this.customGridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl3.Location = new System.Drawing.Point(498, 3);
            this.customGridControl3.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.customGridControl3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl3.MainView = this.customGridView3;
            this.customGridControl3.Name = "customGridControl3";
            this.customGridControl3.Size = new System.Drawing.Size(490, 107);
            this.customGridControl3.TabIndex = 15;
            this.customGridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView3});
            // 
            // customGridView3
            // 
            this.customGridView3.Appearance.FooterPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView3.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.customGridView3.Appearance.FooterPanel.Options.UseFont = true;
            this.customGridView3.Appearance.FooterPanel.Options.UseForeColor = true;
            this.customGridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView3.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView3.Appearance.Row.Options.UseFont = true;
            this.customGridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridView3.GridControl = this.customGridControl3;
            this.customGridView3.Name = "customGridView3";
            this.customGridView3.OptionsBehavior.AllowIncrementalSearch = true;
            this.customGridView3.OptionsBehavior.Editable = false;
            this.customGridView3.OptionsCustomization.AllowRowSizing = true;
            this.customGridView3.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.customGridView3.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.customGridView3.OptionsLayout.Columns.StoreAllOptions = true;
            this.customGridView3.OptionsLayout.Columns.StoreAppearance = true;
            this.customGridView3.OptionsLayout.StoreAllOptions = true;
            this.customGridView3.OptionsLayout.StoreAppearance = true;
            this.customGridView3.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.customGridView3.OptionsView.ColumnAutoWidth = false;
            this.customGridView3.OptionsView.ShowGroupPanel = false;
            // 
            // customGridControl2
            // 
            this.customGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl2.Location = new System.Drawing.Point(3, 3);
            this.customGridControl2.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.customGridControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl2.MainView = this.customGridView2;
            this.customGridControl2.Name = "customGridControl2";
            this.customGridControl2.Size = new System.Drawing.Size(489, 107);
            this.customGridControl2.TabIndex = 14;
            this.customGridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView2});
            // 
            // customGridView2
            // 
            this.customGridView2.Appearance.FooterPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView2.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.customGridView2.Appearance.FooterPanel.Options.UseFont = true;
            this.customGridView2.Appearance.FooterPanel.Options.UseForeColor = true;
            this.customGridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView2.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView2.Appearance.Row.Options.UseFont = true;
            this.customGridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridView2.GridControl = this.customGridControl2;
            this.customGridView2.Name = "customGridView2";
            this.customGridView2.OptionsBehavior.AllowIncrementalSearch = true;
            this.customGridView2.OptionsBehavior.Editable = false;
            this.customGridView2.OptionsCustomization.AllowRowSizing = true;
            this.customGridView2.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.customGridView2.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.customGridView2.OptionsLayout.Columns.StoreAllOptions = true;
            this.customGridView2.OptionsLayout.Columns.StoreAppearance = true;
            this.customGridView2.OptionsLayout.StoreAllOptions = true;
            this.customGridView2.OptionsLayout.StoreAppearance = true;
            this.customGridView2.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.customGridView2.OptionsView.ColumnAutoWidth = false;
            this.customGridView2.OptionsView.ShowGroupPanel = false;
            // 
            // customGridControl1
            // 
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 35);
            this.customGridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(991, 133);
            this.customGridControl1.TabIndex = 17;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.customGridView1.Appearance.FooterPanel.Options.UseFont = true;
            this.customGridView1.Appearance.FooterPanel.Options.UseForeColor = true;
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
            this.customGridView1.OptionsLayout.StoreFormatRules = true;
            this.customGridView1.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.customGridView1.OptionsView.ColumnAutoWidth = false;
            this.customGridView1.OptionsView.ShowFooter = true;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // GenExpenseListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.KontoGrid = this.customGridControl1;
            this.KontoView = this.customGridView1;
            this.Name = "GenExpenseListView";
            this.Size = new System.Drawing.Size(1040, 311);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panelControl3, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.customGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Core.Shared.Libs.ListDateRange listDateRange1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton attachSimpleButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Core.Shared.Libs.CustomGridControl customGridControl3;
        private Core.Shared.Libs.CustomGridView customGridView3;
        private Core.Shared.Libs.CustomGridControl customGridControl2;
        private Core.Shared.Libs.CustomGridView customGridView2;
        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
    }
}
