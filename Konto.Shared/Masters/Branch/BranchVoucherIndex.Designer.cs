
namespace Konto.Shared.Masters.Branch
{
    partial class BranchVoucherIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchVoucherIndex));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.branchVoucherDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBranchId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colSaleVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.saleRepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colSaleReturnVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPurchaseVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colStockTransferVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colReceiptVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPaymentVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCrDrNoteVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchVoucherDtoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleRepositoryItemLookUpEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.okSimpleButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cancelSimpleButton, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 228);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(815, 37);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(634, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok";
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(726, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.cancelSimpleButton.TabIndex = 6;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // customGridControl1
            // 
            this.customGridControl1.DataSource = this.branchVoucherDtoBindingSource;
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 0);
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.saleRepositoryItemLookUpEdit});
            this.customGridControl1.Size = new System.Drawing.Size(815, 228);
            this.customGridControl1.TabIndex = 9;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // branchVoucherDtoBindingSource
            // 
            this.branchVoucherDtoBindingSource.DataSource = typeof(Konto.Data.Models.Masters.Dtos.BranchVoucherDto);
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.customGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.customGridView1.Appearance.Row.Options.UseFont = true;
            this.customGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colBranchId,
            this.colSaleVoucherId,
            this.colSaleReturnVoucherId,
            this.colPurchaseVoucherId,
            this.colStockTransferVoucherId,
            this.colReceiptVoucherId,
            this.colPaymentVoucherId,
            this.colCrDrNoteVoucherId});
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
            this.customGridView1.OptionsView.ColumnAutoWidth = false;
            this.customGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.customGridView1.OptionsView.ShowFooter = true;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colBranchId
            // 
            this.colBranchId.Caption = "Branch";
            this.colBranchId.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colBranchId.FieldName = "BranchId";
            this.colBranchId.Name = "colBranchId";
            this.colBranchId.Visible = true;
            this.colBranchId.VisibleIndex = 0;
            this.colBranchId.Width = 120;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.DisplayMember = "DisplayText";
            this.repositoryItemLookUpEdit1.ImmediatePopup = true;
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ShowFooter = false;
            this.repositoryItemLookUpEdit1.ShowHeader = false;
            this.repositoryItemLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemLookUpEdit1.ValueMember = "Id";
            // 
            // colSaleVoucherId
            // 
            this.colSaleVoucherId.Caption = "Sale Voucher";
            this.colSaleVoucherId.ColumnEdit = this.saleRepositoryItemLookUpEdit;
            this.colSaleVoucherId.FieldName = "SaleVoucherId";
            this.colSaleVoucherId.Name = "colSaleVoucherId";
            this.colSaleVoucherId.Visible = true;
            this.colSaleVoucherId.VisibleIndex = 1;
            this.colSaleVoucherId.Width = 119;
            // 
            // saleRepositoryItemLookUpEdit
            // 
            this.saleRepositoryItemLookUpEdit.AutoHeight = false;
            this.saleRepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.saleRepositoryItemLookUpEdit.DisplayMember = "DisplayText";
            this.saleRepositoryItemLookUpEdit.ImmediatePopup = true;
            this.saleRepositoryItemLookUpEdit.Name = "saleRepositoryItemLookUpEdit";
            this.saleRepositoryItemLookUpEdit.NullText = "";
            this.saleRepositoryItemLookUpEdit.ShowFooter = false;
            this.saleRepositoryItemLookUpEdit.ShowHeader = false;
            this.saleRepositoryItemLookUpEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.saleRepositoryItemLookUpEdit.ValueMember = "Id";
            // 
            // colSaleReturnVoucherId
            // 
            this.colSaleReturnVoucherId.Caption = "Sale Return";
            this.colSaleReturnVoucherId.ColumnEdit = this.saleRepositoryItemLookUpEdit;
            this.colSaleReturnVoucherId.FieldName = "SaleReturnVoucherId";
            this.colSaleReturnVoucherId.Name = "colSaleReturnVoucherId";
            this.colSaleReturnVoucherId.Visible = true;
            this.colSaleReturnVoucherId.VisibleIndex = 2;
            this.colSaleReturnVoucherId.Width = 125;
            // 
            // colPurchaseVoucherId
            // 
            this.colPurchaseVoucherId.Caption = "Purchase Voucher";
            this.colPurchaseVoucherId.ColumnEdit = this.saleRepositoryItemLookUpEdit;
            this.colPurchaseVoucherId.FieldName = "PurchaseVoucherId";
            this.colPurchaseVoucherId.Name = "colPurchaseVoucherId";
            this.colPurchaseVoucherId.Visible = true;
            this.colPurchaseVoucherId.VisibleIndex = 3;
            this.colPurchaseVoucherId.Width = 139;
            // 
            // colStockTransferVoucherId
            // 
            this.colStockTransferVoucherId.Caption = "Stock Transfer";
            this.colStockTransferVoucherId.ColumnEdit = this.saleRepositoryItemLookUpEdit;
            this.colStockTransferVoucherId.FieldName = "StockTransferVoucherId";
            this.colStockTransferVoucherId.Name = "colStockTransferVoucherId";
            this.colStockTransferVoucherId.Visible = true;
            this.colStockTransferVoucherId.VisibleIndex = 4;
            this.colStockTransferVoucherId.Width = 118;
            // 
            // colReceiptVoucherId
            // 
            this.colReceiptVoucherId.Caption = "Receipt Voucher";
            this.colReceiptVoucherId.ColumnEdit = this.saleRepositoryItemLookUpEdit;
            this.colReceiptVoucherId.FieldName = "ReceiptVoucherId";
            this.colReceiptVoucherId.Name = "colReceiptVoucherId";
            this.colReceiptVoucherId.Visible = true;
            this.colReceiptVoucherId.VisibleIndex = 5;
            this.colReceiptVoucherId.Width = 120;
            // 
            // colPaymentVoucherId
            // 
            this.colPaymentVoucherId.Caption = "Payment Voucher";
            this.colPaymentVoucherId.ColumnEdit = this.saleRepositoryItemLookUpEdit;
            this.colPaymentVoucherId.FieldName = "PaymentVoucherId";
            this.colPaymentVoucherId.Name = "colPaymentVoucherId";
            this.colPaymentVoucherId.Visible = true;
            this.colPaymentVoucherId.VisibleIndex = 6;
            this.colPaymentVoucherId.Width = 136;
            // 
            // colCrDrNoteVoucherId
            // 
            this.colCrDrNoteVoucherId.Caption = "CrdrNote Voucher";
            this.colCrDrNoteVoucherId.ColumnEdit = this.saleRepositoryItemLookUpEdit;
            this.colCrDrNoteVoucherId.FieldName = "CrDrNoteVoucherId";
            this.colCrDrNoteVoucherId.Name = "colCrDrNoteVoucherId";
            this.colCrDrNoteVoucherId.Visible = true;
            this.colCrDrNoteVoucherId.VisibleIndex = 7;
            this.colCrDrNoteVoucherId.Width = 135;
            // 
            // BranchVoucherIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 265);
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BranchVoucherIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Branch Voucher Setup";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchVoucherDtoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saleRepositoryItemLookUpEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private System.Windows.Forms.BindingSource branchVoucherDtoBindingSource;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colBranchId;
        private Core.Shared.Libs.CustomGridColumn colSaleVoucherId;
        private Core.Shared.Libs.CustomGridColumn colPurchaseVoucherId;
        private Core.Shared.Libs.CustomGridColumn colStockTransferVoucherId;
        private Core.Shared.Libs.CustomGridColumn colReceiptVoucherId;
        private Core.Shared.Libs.CustomGridColumn colPaymentVoucherId;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit saleRepositoryItemLookUpEdit;
        private Core.Shared.Libs.CustomGridColumn colSaleReturnVoucherId;
        private Core.Shared.Libs.CustomGridColumn colCrDrNoteVoucherId;
    }
}