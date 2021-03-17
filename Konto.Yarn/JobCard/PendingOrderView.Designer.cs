namespace Konto.Yarn.JobCard
{
    partial class PendingOrderView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingOrderView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.pendingOrderDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDelvAccId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAddress1 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProduct = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTotalQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colNetTotal = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colrate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTotal = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRemarks = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pendingOrderDtoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 328);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(858, 37);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(764, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 31);
            this.cancelSimpleButton.TabIndex = 6;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(672, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // pendingOrderDtoBindingSource
            // 
            this.pendingOrderDtoBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.PendingOrderDto);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.pendingOrderDtoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(858, 328);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
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
            this.colVoucherNo,
            this.colVoucherDate,
            this.colDelvAccId,
            this.colAddress1,
            this.colProductId,
            this.colProduct,
            this.colColorId,
            this.colColorName,
            this.colTotalQty,
            this.colNetTotal,
            this.colrate,
            this.colTotal,
            this.colRemarks});
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
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.Caption = "Order No";
            this.colVoucherNo.FieldName = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 0;
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.Caption = "Order Date";
            this.colVoucherDate.FieldName = "VoucherDate";
            this.colVoucherDate.Name = "colVoucherDate";
            this.colVoucherDate.Visible = true;
            this.colVoucherDate.VisibleIndex = 1;
            this.colVoucherDate.Width = 106;
            // 
            // colDelvAccId
            // 
            this.colDelvAccId.FieldName = "DelvAccId";
            this.colDelvAccId.Name = "colDelvAccId";
            // 
            // colAddress1
            // 
            this.colAddress1.Caption = "Party";
            this.colAddress1.FieldName = "Address1";
            this.colAddress1.Name = "colAddress1";
            this.colAddress1.Visible = true;
            this.colAddress1.VisibleIndex = 2;
            this.colAddress1.Width = 140;
            // 
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            // 
            // colProduct
            // 
            this.colProduct.Caption = "Item";
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.Visible = true;
            this.colProduct.VisibleIndex = 3;
            this.colProduct.Width = 137;
            // 
            // colColorId
            // 
            this.colColorId.FieldName = "ColorId";
            this.colColorId.Name = "colColorId";
            // 
            // colColorName
            // 
            this.colColorName.Caption = "Shade";
            this.colColorName.FieldName = "ColorName";
            this.colColorName.Name = "colColorName";
            this.colColorName.Visible = true;
            this.colColorName.VisibleIndex = 4;
            this.colColorName.Width = 144;
            // 
            // colTotalQty
            // 
            this.colTotalQty.Caption = "Qty";
            this.colTotalQty.FieldName = "TotalQty";
            this.colTotalQty.Name = "colTotalQty";
            this.colTotalQty.Visible = true;
            this.colTotalQty.VisibleIndex = 5;
            // 
            // colNetTotal
            // 
            this.colNetTotal.Caption = "Balance";
            this.colNetTotal.FieldName = "NetTotal";
            this.colNetTotal.Name = "colNetTotal";
            this.colNetTotal.Visible = true;
            this.colNetTotal.VisibleIndex = 6;
            // 
            // colrate
            // 
            this.colrate.Caption = "Rate";
            this.colrate.FieldName = "rate";
            this.colrate.Name = "colrate";
            this.colrate.Visible = true;
            this.colrate.VisibleIndex = 7;
            // 
            // colTotal
            // 
            this.colTotal.Caption = "Total";
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 8;
            // 
            // colRemarks
            // 
            this.colRemarks.Caption = "Remarks";
            this.colRemarks.FieldName = "Remarks";
            this.colRemarks.Name = "colRemarks";
            this.colRemarks.Visible = true;
            this.colRemarks.VisibleIndex = 9;
            // 
            // PendingOrderView
            // 
            this.AcceptButton = this.okSimpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionFont = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(858, 365);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = true;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "PendingOrderView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Order";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pendingOrderDtoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private System.Windows.Forms.BindingSource pendingOrderDtoBindingSource;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colVoucherDate;
        private Core.Shared.Libs.CustomGridColumn colDelvAccId;
        private Core.Shared.Libs.CustomGridColumn colAddress1;
        private Core.Shared.Libs.CustomGridColumn colProductId;
        private Core.Shared.Libs.CustomGridColumn colProduct;
        private Core.Shared.Libs.CustomGridColumn colColorId;
        private Core.Shared.Libs.CustomGridColumn colColorName;
        private Core.Shared.Libs.CustomGridColumn colTotalQty;
        private Core.Shared.Libs.CustomGridColumn colNetTotal;
        private Core.Shared.Libs.CustomGridColumn colrate;
        private Core.Shared.Libs.CustomGridColumn colTotal;
        private Core.Shared.Libs.CustomGridColumn colRemarks;
    }
}