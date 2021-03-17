namespace Konto.Shared.Trans.Po
{
    partial class PoListView
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
            this.components = new System.ComponentModel.Container();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.listDateRange1 = new Konto.Core.Shared.Libs.ListDateRange();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.grnAgainstOrderDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVTypeId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBillNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grnAgainstOrderDtoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.panelControl3.Size = new System.Drawing.Size(49, 80);
            // 
            // customGridControl1
            // 
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 35);
            this.customGridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(991, 80);
            this.customGridControl1.TabIndex = 11;
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
            // listDateRange1
            // 
            this.listDateRange1.FromDate = 0;
            this.listDateRange1.IsAnalysis = false;
            this.listDateRange1.KontoGrid = null;
            this.listDateRange1.Location = new System.Drawing.Point(477, 4);
            this.listDateRange1.Name = "listDateRange1";
            this.listDateRange1.SelectedItem = null;
            this.listDateRange1.Size = new System.Drawing.Size(514, 25);
            this.listDateRange1.TabIndex = 2;
            this.listDateRange1.ToDate = 0;
            this.listDateRange1.VoucherType = Konto.App.Shared.VoucherTypeEnum.PurchaseOrder;
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.FloatOnDblClick = false;
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel1.ID = new System.Guid("0d1e6fa9-9e30-4a07-a126-cce2b99565ba");
            this.dockPanel1.Location = new System.Drawing.Point(0, 115);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.AllowFloating = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 196);
            this.dockPanel1.Size = new System.Drawing.Size(1040, 196);
            this.dockPanel1.Text = "Receive Details";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gridControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 27);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1034, 166);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.grnAgainstOrderDtoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1034, 166);
            this.gridControl1.TabIndex = 14;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // grnAgainstOrderDtoBindingSource
            // 
            this.grnAgainstOrderDtoBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.GrnAgainstOrderDto);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colVTypeId,
            this.colDate,
            this.colChallanNo,
            this.colVoucherNo,
            this.colBillNo,
            this.colVoucherDate,
            this.colProductName,
            this.colPcs,
            this.colQty,
            this.colRate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.gridView1.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.gridView1.OptionsLayout.Columns.StoreAllOptions = true;
            this.gridView1.OptionsLayout.Columns.StoreAppearance = true;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAppearance = true;
            this.gridView1.OptionsNavigation.UseTabKey = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colVTypeId
            // 
            this.colVTypeId.FieldName = "VTypeId";
            this.colVTypeId.Name = "colVTypeId";
            // 
            // colChallanNo
            // 
            this.colChallanNo.FieldName = "ChallanNo";
            this.colChallanNo.Name = "colChallanNo";
            this.colChallanNo.Visible = true;
            this.colChallanNo.VisibleIndex = 1;
            this.colChallanNo.Width = 109;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.FieldName = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 0;
            this.colVoucherNo.Width = 96;
            // 
            // colBillNo
            // 
            this.colBillNo.FieldName = "BillNo";
            this.colBillNo.Name = "colBillNo";
            this.colBillNo.Visible = true;
            this.colBillNo.VisibleIndex = 3;
            this.colBillNo.Width = 112;
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.FieldName = "VoucherDate";
            this.colVoucherDate.Name = "colVoucherDate";
            this.colVoucherDate.Width = 102;
            // 
            // colProductName
            // 
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 4;
            this.colProductName.Width = 271;
            // 
            // colPcs
            // 
            this.colPcs.FieldName = "Pcs";
            this.colPcs.Name = "colPcs";
            this.colPcs.Visible = true;
            this.colPcs.VisibleIndex = 5;
            this.colPcs.Width = 80;
            // 
            // colQty
            // 
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 6;
            this.colQty.Width = 97;
            // 
            // colRate
            // 
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 7;
            this.colRate.Width = 106;
            // 
            // colDate
            // 
            this.colDate.FieldName = "Date";
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 2;
            this.colDate.Width = 102;
            // 
            // PoListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.dockPanel1);
            this.KontoGrid = this.customGridControl1;
            this.KontoView = this.customGridView1;
            this.Name = "PoListView";
            this.Size = new System.Drawing.Size(1040, 311);
            this.Controls.SetChildIndex(this.dockPanel1, 0);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panelControl3, 0);
            this.Controls.SetChildIndex(this.customGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grnAgainstOrderDtoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private Core.Shared.Libs.ListDateRange listDateRange1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private System.Windows.Forms.BindingSource grnAgainstOrderDtoBindingSource;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colVTypeId;
        private Core.Shared.Libs.CustomGridColumn colChallanNo;
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colBillNo;
        private Core.Shared.Libs.CustomGridColumn colVoucherDate;
        private Core.Shared.Libs.CustomGridColumn colProductName;
        private Core.Shared.Libs.CustomGridColumn colPcs;
        private Core.Shared.Libs.CustomGridColumn colQty;
        private Core.Shared.Libs.CustomGridColumn colRate;
        private Core.Shared.Libs.CustomGridColumn colDate;
    }
}
