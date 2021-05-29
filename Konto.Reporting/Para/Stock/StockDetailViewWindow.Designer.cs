namespace Konto.Reporting.Para.Stock
{
    partial class StockDetailViewWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockDetailViewWindow));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.productLookup1 = new Konto.Shared.Masters.Item.ProductLookup();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colReportType = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colItemId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProduct = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colParticular = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBillNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colInwQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOutQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colInwPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOutPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colStockQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colStockPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cancelSimpleButton);
            this.panelControl1.Controls.Add(this.okSimpleButton);
            this.panelControl1.Controls.Add(this.autoLabel3);
            this.panelControl1.Controls.Add(this.productLookup1);
            this.panelControl1.Controls.Add(this.autoLabel2);
            this.panelControl1.Controls.Add(this.autoLabel1);
            this.panelControl1.Controls.Add(this.dateEdit2);
            this.panelControl1.Controls.Add(this.dateEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(800, 30);
            this.panelControl1.TabIndex = 0;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.cancelSimpleButton.Location = new System.Drawing.Point(714, 2);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(81, 23);
            this.cancelSimpleButton.TabIndex = 10;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.okSimpleButton.Location = new System.Drawing.Point(647, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(61, 24);
            this.okSimpleButton.TabIndex = 9;
            this.okSimpleButton.Text = "Ok";
            // 
            // autoLabel3
            // 
            this.autoLabel3.DX = -67;
            this.autoLabel3.DY = 3;
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.LabeledControl = this.productLookup1;
            this.autoLabel3.Location = new System.Drawing.Point(326, 6);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(63, 17);
            this.autoLabel3.TabIndex = 8;
            this.autoLabel3.Text = "Product :";
            // 
            // productLookup1
            // 
            this.productLookup1.GroupDto = null;
            this.productLookup1.Location = new System.Drawing.Point(393, 3);
            this.productLookup1.LookupTitle = null;
            this.productLookup1.Name = "productLookup1";
            this.productLookup1.PrimaryKey = null;
            this.productLookup1.PTypeId = Konto.App.Shared.ProductTypeEnum.NA;
            this.productLookup1.RequiredField = false;
            this.productLookup1.SelectedText = null;
            this.productLookup1.SelectedValue = null;
            this.productLookup1.Size = new System.Drawing.Size(248, 24);
            this.productLookup1.TabIndex = 7;
            this.productLookup1.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // autoLabel2
            // 
            this.autoLabel2.DX = -26;
            this.autoLabel2.DY = 3;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.LabeledControl = this.dateEdit2;
            this.autoLabel2.Location = new System.Drawing.Point(197, 6);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(22, 17);
            this.autoLabel2.TabIndex = 6;
            this.autoLabel2.Text = "To";
            // 
            // dateEdit2
            // 
            this.dateEdit2.EditValue = null;
            this.dateEdit2.EnterMoveNextControl = true;
            this.dateEdit2.Location = new System.Drawing.Point(223, 3);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEdit2.Properties.Appearance.Options.UseFont = true;
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Size = new System.Drawing.Size(95, 24);
            this.dateEdit2.TabIndex = 4;
            // 
            // autoLabel1
            // 
            this.autoLabel1.DX = -87;
            this.autoLabel1.DY = 3;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.LabeledControl = this.dateEdit1;
            this.autoLabel1.Location = new System.Drawing.Point(10, 6);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(83, 17);
            this.autoLabel1.TabIndex = 5;
            this.autoLabel1.Text = "From Period";
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.EnterMoveNextControl = true;
            this.dateEdit1.Location = new System.Drawing.Point(97, 3);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEdit1.Properties.Appearance.Options.UseFont = true;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Size = new System.Drawing.Size(95, 24);
            this.dateEdit1.TabIndex = 3;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 30);
            this.gridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(800, 420);
            this.gridControl1.TabIndex = 12;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Reports.StockDetDto);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupFooter.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.GroupFooter.Options.UseFont = true;
            this.gridView1.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupRow.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.GroupRow.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colReportType,
            this.colItemId,
            this.colProduct,
            this.colParticular,
            this.colVoucherNo,
            this.colBillNo,
            this.colVoucherName,
            this.colTransDate,
            this.colInwQty,
            this.colOutQty,
            this.colInwPcs,
            this.colOutPcs,
            this.colStockQty,
            this.colStockPcs,
            this.colQty,
            this.colPcs});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupFormat = "[#image]{1} {2}";
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
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colReportType
            // 
            this.colReportType.FieldName = "ReportType";
            this.colReportType.Name = "colReportType";
            // 
            // colItemId
            // 
            this.colItemId.FieldName = "ItemId";
            this.colItemId.Name = "colItemId";
            // 
            // colProduct
            // 
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            // 
            // colParticular
            // 
            this.colParticular.FieldName = "Particular";
            this.colParticular.Name = "colParticular";
            this.colParticular.Visible = true;
            this.colParticular.VisibleIndex = 4;
            this.colParticular.Width = 166;
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
            // 
            // colVoucherName
            // 
            this.colVoucherName.FieldName = "VoucherName";
            this.colVoucherName.Name = "colVoucherName";
            this.colVoucherName.Visible = true;
            this.colVoucherName.VisibleIndex = 2;
            this.colVoucherName.Width = 105;
            // 
            // colTransDate
            // 
            this.colTransDate.FieldName = "TransDate";
            this.colTransDate.Name = "colTransDate";
            this.colTransDate.Visible = true;
            this.colTransDate.VisibleIndex = 1;
            // 
            // colInwQty
            // 
            this.colInwQty.FieldName = "InwQty";
            this.colInwQty.Name = "colInwQty";
            this.colInwQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InwQty", "{0:0.##}")});
            this.colInwQty.Visible = true;
            this.colInwQty.VisibleIndex = 6;
            // 
            // colOutQty
            // 
            this.colOutQty.FieldName = "OutQty";
            this.colOutQty.Name = "colOutQty";
            this.colOutQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OutQty", "{0:0.##}")});
            this.colOutQty.Visible = true;
            this.colOutQty.VisibleIndex = 7;
            // 
            // colInwPcs
            // 
            this.colInwPcs.FieldName = "InwPcs";
            this.colInwPcs.Name = "colInwPcs";
            this.colInwPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InwPcs", "{0:0.##}")});
            this.colInwPcs.Visible = true;
            this.colInwPcs.VisibleIndex = 5;
            this.colInwPcs.Width = 81;
            // 
            // colOutPcs
            // 
            this.colOutPcs.FieldName = "OutPcs";
            this.colOutPcs.Name = "colOutPcs";
            this.colOutPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OutPcs", "{0:0.##}")});
            this.colOutPcs.Visible = true;
            this.colOutPcs.VisibleIndex = 8;
            this.colOutPcs.Width = 84;
            // 
            // colStockQty
            // 
            this.colStockQty.FieldName = "StockQty";
            this.colStockQty.Name = "colStockQty";
            this.colStockQty.Visible = true;
            this.colStockQty.VisibleIndex = 9;
            this.colStockQty.Width = 89;
            // 
            // colStockPcs
            // 
            this.colStockPcs.FieldName = "StockPcs";
            this.colStockPcs.Name = "colStockPcs";
            this.colStockPcs.Visible = true;
            this.colStockPcs.VisibleIndex = 10;
            // 
            // colQty
            // 
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            // 
            // colPcs
            // 
            this.colPcs.FieldName = "Pcs";
            this.colPcs.Name = "colPcs";
            // 
            // StockDetailViewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "StockDetailViewWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock Ledger";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Shared.Masters.Item.ProductLookup productLookup1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Core.Shared.Libs.CustomGridColumn colReportType;
        private Core.Shared.Libs.CustomGridColumn colItemId;
        private Core.Shared.Libs.CustomGridColumn colProduct;
        private Core.Shared.Libs.CustomGridColumn colParticular;
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colBillNo;
        private Core.Shared.Libs.CustomGridColumn colVoucherName;
        private Core.Shared.Libs.CustomGridColumn colTransDate;
        private Core.Shared.Libs.CustomGridColumn colInwQty;
        private Core.Shared.Libs.CustomGridColumn colOutQty;
        private Core.Shared.Libs.CustomGridColumn colInwPcs;
        private Core.Shared.Libs.CustomGridColumn colOutPcs;
        private Core.Shared.Libs.CustomGridColumn colStockQty;
        private Core.Shared.Libs.CustomGridColumn colStockPcs;
        private Core.Shared.Libs.CustomGridColumn colQty;
        private Core.Shared.Libs.CustomGridColumn colPcs;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
    }
}