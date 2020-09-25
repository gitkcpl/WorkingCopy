namespace Konto.Shared.Trans.JobIssue
{
    partial class JobIssueBarCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobIssueBarCode));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.barcodeNoTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colBarcode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProduct = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colWeaver = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColor = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLotNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRefId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.ProducttextEdit = new DevExpress.XtraEditors.TextEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.ChangeQtysimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.QtyEdit = new DevExpress.XtraEditors.TextEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barcodeNoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProducttextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QtyEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cancelSimpleButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.okSimpleButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 401);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(790, 37);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(696, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 31);
            this.cancelSimpleButton.TabIndex = 1;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(604, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok [F3]";
            this.okSimpleButton.Click += new System.EventHandler(this.okSimpleButton_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ProducttextEdit);
            this.panelControl1.Controls.Add(this.autoLabel3);
            this.panelControl1.Controls.Add(this.ChangeQtysimpleButton);
            this.panelControl1.Controls.Add(this.autoLabel2);
            this.panelControl1.Controls.Add(this.QtyEdit);
            this.panelControl1.Controls.Add(this.autoLabel1);
            this.panelControl1.Controls.Add(this.barcodeNoTextEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(790, 32);
            this.panelControl1.TabIndex = 3;
            // 
            // autoLabel1
            // 
            this.autoLabel1.DX = -64;
            this.autoLabel1.DY = 3;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.LabeledControl = this.barcodeNoTextEdit;
            this.autoLabel1.Location = new System.Drawing.Point(24, 6);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(60, 17);
            this.autoLabel1.TabIndex = 8;
            this.autoLabel1.Text = "Barcode:";
            // 
            // barcodeNoTextEdit
            // 
            this.barcodeNoTextEdit.Location = new System.Drawing.Point(88, 3);
            this.barcodeNoTextEdit.Name = "barcodeNoTextEdit";
            this.barcodeNoTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barcodeNoTextEdit.Properties.Appearance.Options.UseFont = true;
            this.barcodeNoTextEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.barcodeNoTextEdit.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.barcodeNoTextEdit.Properties.MaxLength = 50;
            this.barcodeNoTextEdit.Size = new System.Drawing.Size(132, 24);
            this.barcodeNoTextEdit.TabIndex = 7;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 32);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(790, 369);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Transaction.TradingDto.JobIssueBarcodeDto);
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
            this.colBarcode,
            this.colQty,
            this.colProduct,
            this.colVoucherNo,
            this.colChallanNo,
            this.colWeaver,
            this.colColor,
            this.colLotNo,
            this.colVDate,
            this.colId,
            this.colTransId,
            this.colRefId,
            this.colProductId,
            this.colColorId,
            this.colVoucherDate});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
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
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridView1.OptionsSelection.CheckBoxSelectorField = "IsSelected";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colBarcode
            // 
            this.colBarcode.FieldName = "Barcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Barcode", "{0}")});
            this.colBarcode.Visible = true;
            this.colBarcode.VisibleIndex = 0;
            this.colBarcode.Width = 122;
            // 
            // colQty
            // 
            this.colQty.Caption = "Qty/Mtrs";
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:0.##}")});
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 1;
            this.colQty.Width = 76;
            // 
            // colProduct
            // 
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.Visible = true;
            this.colProduct.VisibleIndex = 2;
            this.colProduct.Width = 124;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.FieldName = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 4;
            this.colVoucherNo.Width = 102;
            // 
            // colChallanNo
            // 
            this.colChallanNo.FieldName = "ChallanNo";
            this.colChallanNo.Name = "colChallanNo";
            this.colChallanNo.Visible = true;
            this.colChallanNo.VisibleIndex = 5;
            this.colChallanNo.Width = 94;
            // 
            // colWeaver
            // 
            this.colWeaver.FieldName = "Weaver";
            this.colWeaver.Name = "colWeaver";
            this.colWeaver.Visible = true;
            this.colWeaver.VisibleIndex = 6;
            this.colWeaver.Width = 96;
            // 
            // colColor
            // 
            this.colColor.FieldName = "Color";
            this.colColor.Name = "colColor";
            this.colColor.Visible = true;
            this.colColor.VisibleIndex = 7;
            // 
            // colLotNo
            // 
            this.colLotNo.FieldName = "LotNo";
            this.colLotNo.Name = "colLotNo";
            this.colLotNo.Visible = true;
            this.colLotNo.VisibleIndex = 3;
            this.colLotNo.Width = 79;
            // 
            // colVDate
            // 
            this.colVDate.FieldName = "VDate";
            this.colVDate.Name = "colVDate";
            this.colVDate.OptionsColumn.ReadOnly = true;
            this.colVDate.Visible = true;
            this.colVDate.VisibleIndex = 8;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colTransId
            // 
            this.colTransId.FieldName = "TransId";
            this.colTransId.Name = "colTransId";
            // 
            // colRefId
            // 
            this.colRefId.FieldName = "RefId";
            this.colRefId.Name = "colRefId";
            // 
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            // 
            // colColorId
            // 
            this.colColorId.FieldName = "ColorId";
            this.colColorId.Name = "colColorId";
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.FieldName = "VoucherDate";
            this.colVoucherDate.Name = "colVoucherDate";
            // 
            // ProducttextEdit
            // 
            this.ProducttextEdit.Location = new System.Drawing.Point(291, 4);
            this.ProducttextEdit.Name = "ProducttextEdit";
            this.ProducttextEdit.Properties.AllowFocused = false;
            this.ProducttextEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProducttextEdit.Properties.Appearance.Options.UseFont = true;
            this.ProducttextEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ProducttextEdit.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.ProducttextEdit.Properties.MaxLength = 50;
            this.ProducttextEdit.Properties.ReadOnly = true;
            this.ProducttextEdit.Size = new System.Drawing.Size(132, 24);
            this.ProducttextEdit.TabIndex = 19;
            // 
            // autoLabel3
            // 
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.Location = new System.Drawing.Point(225, 6);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(59, 17);
            this.autoLabel3.TabIndex = 18;
            this.autoLabel3.Text = "Product:";
            // 
            // ChangeQtysimpleButton
            // 
            this.ChangeQtysimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeQtysimpleButton.Appearance.Options.UseFont = true;
            this.ChangeQtysimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ChangeQtysimpleButton.ImageOptions.SvgImage")));
            this.ChangeQtysimpleButton.Location = new System.Drawing.Point(668, 1);
            this.ChangeQtysimpleButton.Name = "ChangeQtysimpleButton";
            this.ChangeQtysimpleButton.Size = new System.Drawing.Size(94, 27);
            this.ChangeQtysimpleButton.TabIndex = 16;
            this.ChangeQtysimpleButton.Text = "OK";
            // 
            // autoLabel2
            // 
            this.autoLabel2.DX = -37;
            this.autoLabel2.DY = 3;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.LabeledControl = this.QtyEdit;
            this.autoLabel2.Location = new System.Drawing.Point(473, 6);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(33, 17);
            this.autoLabel2.TabIndex = 17;
            this.autoLabel2.Text = "Qty:";
            // 
            // QtyEdit
            // 
            this.QtyEdit.EnterMoveNextControl = true;
            this.QtyEdit.Location = new System.Drawing.Point(510, 3);
            this.QtyEdit.Name = "QtyEdit";
            this.QtyEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QtyEdit.Properties.Appearance.Options.UseFont = true;
            this.QtyEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.QtyEdit.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.QtyEdit.Properties.MaxLength = 50;
            this.QtyEdit.Size = new System.Drawing.Size(132, 24);
            this.QtyEdit.TabIndex = 15;
            // 
            // JobIssueBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(790, 438);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.Name = "JobIssueBarCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Job Issue Barcode";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barcodeNoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProducttextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QtyEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private Core.Shared.Libs.CustomGridColumn colBarcode;
        private Core.Shared.Libs.CustomGridColumn colQty;
        private Core.Shared.Libs.CustomGridColumn colProduct;
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colChallanNo;
        private Core.Shared.Libs.CustomGridColumn colWeaver;
        private Core.Shared.Libs.CustomGridColumn colColor;
        private Core.Shared.Libs.CustomGridColumn colLotNo;
        private Core.Shared.Libs.CustomGridColumn colVDate;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colTransId;
        private Core.Shared.Libs.CustomGridColumn colRefId;
        private Core.Shared.Libs.CustomGridColumn colProductId;
        private Core.Shared.Libs.CustomGridColumn colColorId;
        private Core.Shared.Libs.CustomGridColumn colVoucherDate;
        private DevExpress.XtraEditors.TextEdit barcodeNoTextEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        public System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.TextEdit ProducttextEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        public DevExpress.XtraEditors.SimpleButton ChangeQtysimpleButton;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private DevExpress.XtraEditors.TextEdit QtyEdit;
    }
}