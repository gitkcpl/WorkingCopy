namespace Konto.Trading.TakaWiseJobReceipt
{
    partial class PendingJobIssueView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingJobIssueView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.pendingMillReceiptSpBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRefNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLotNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProduct = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDesignId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssueQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssuePcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPendingQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPendingPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colReceiptQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFinishQuality = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colReceiptPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTotal = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDisc = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDiscAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFreightRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFreight = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCgstAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIgstAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCess = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCessAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSgstAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colUomId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pendingMillReceiptSpBindingSource)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 260);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(865, 37);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(771, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(679, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.pendingMillReceiptSpBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(865, 260);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // pendingMillReceiptSpBindingSource
            // 
            this.pendingMillReceiptSpBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.PendingMillReceiptSp);
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
            this.colVoucherNo,
            this.colVoucherDate,
            this.colRefNo,
            this.colLotNo,
            this.colChallanNo,
            this.colProduct,
            this.colColorName,
            this.colProductId,
            this.colDesignId,
            this.colColorId,
            this.colIssueQty,
            this.colIssuePcs,
            this.colPendingQty,
            this.colPendingPcs,
            this.colReceiptQty,
            this.colFinishQuality,
            this.colReceiptPcs,
            this.colRate,
            this.colTotal,
            this.colDisc,
            this.colDiscAmt,
            this.colFreightRate,
            this.colFreight,
            this.colCgst,
            this.colCgstAmt,
            this.colIgstAmt,
            this.colIgst,
            this.colCess,
            this.colCessAmt,
            this.colSgst,
            this.colSgstAmt,
            this.colUomId,
            this.colTransId,
            this.colId,
            this.colVoucherId});
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
            this.gridView1.OptionsNavigation.UseTabKey = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.FieldName = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 0;
            this.colVoucherNo.Width = 93;
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.FieldName = "VoucherDate";
            this.colVoucherDate.Name = "colVoucherDate";
            this.colVoucherDate.Visible = true;
            this.colVoucherDate.VisibleIndex = 1;
            this.colVoucherDate.Width = 100;
            // 
            // colRefNo
            // 
            this.colRefNo.FieldName = "RefNo";
            this.colRefNo.Name = "colRefNo";
            // 
            // colLotNo
            // 
            this.colLotNo.FieldName = "LotNo";
            this.colLotNo.Name = "colLotNo";
            this.colLotNo.Visible = true;
            this.colLotNo.VisibleIndex = 2;
            // 
            // colChallanNo
            // 
            this.colChallanNo.FieldName = "ChallanNo";
            this.colChallanNo.Name = "colChallanNo";
            // 
            // colProduct
            // 
            this.colProduct.FieldName = "GreyQuality";
            this.colProduct.Name = "colProduct";
            this.colProduct.Visible = true;
            this.colProduct.VisibleIndex = 3;
            this.colProduct.Width = 109;
            // 
            // colColorName
            // 
            this.colColorName.FieldName = "ColorName";
            this.colColorName.Name = "colColorName";
            this.colColorName.Visible = true;
            this.colColorName.VisibleIndex = 11;
            this.colColorName.Width = 126;
            // 
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            // 
            // colDesignId
            // 
            this.colDesignId.FieldName = "DesignId";
            this.colDesignId.Name = "colDesignId";
            // 
            // colColorId
            // 
            this.colColorId.FieldName = "ColorId";
            this.colColorId.Name = "colColorId";
            // 
            // colIssueQty
            // 
            this.colIssueQty.Caption = "Grey Mtrs";
            this.colIssueQty.DisplayFormat.FormatString = "F";
            this.colIssueQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIssueQty.FieldName = "IssueQty";
            this.colIssueQty.Name = "colIssueQty";
            this.colIssueQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IssueQty", "{0:F}")});
            this.colIssueQty.Visible = true;
            this.colIssueQty.VisibleIndex = 4;
            // 
            // colIssuePcs
            // 
            this.colIssuePcs.Caption = "Grey Pcs";
            this.colIssuePcs.DisplayFormat.FormatString = "N";
            this.colIssuePcs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIssuePcs.FieldName = "IssuePcs";
            this.colIssuePcs.Name = "colIssuePcs";
            this.colIssuePcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IssuePcs", "{0:0.##}")});
            this.colIssuePcs.Visible = true;
            this.colIssuePcs.VisibleIndex = 5;
            // 
            // colPendingQty
            // 
            this.colPendingQty.Caption = "Pend Mtrs";
            this.colPendingQty.DisplayFormat.FormatString = "F";
            this.colPendingQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPendingQty.FieldName = "PendingQty";
            this.colPendingQty.Name = "colPendingQty";
            this.colPendingQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PendingQty", "{0:F}")});
            this.colPendingQty.Visible = true;
            this.colPendingQty.VisibleIndex = 6;
            this.colPendingQty.Width = 105;
            // 
            // colPendingPcs
            // 
            this.colPendingPcs.Caption = "Pend Pcs";
            this.colPendingPcs.DisplayFormat.FormatString = "N";
            this.colPendingPcs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPendingPcs.FieldName = "PendingPcs";
            this.colPendingPcs.Name = "colPendingPcs";
            this.colPendingPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PendingPcs", "{0:N}")});
            this.colPendingPcs.Visible = true;
            this.colPendingPcs.VisibleIndex = 7;
            this.colPendingPcs.Width = 100;
            // 
            // colReceiptQty
            // 
            this.colReceiptQty.Caption = "Rcpt Mtrs";
            this.colReceiptQty.DisplayFormat.FormatString = "F";
            this.colReceiptQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colReceiptQty.FieldName = "ReceiptQty";
            this.colReceiptQty.Name = "colReceiptQty";
            this.colReceiptQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ReceiptQty", "{0:F}")});
            this.colReceiptQty.Visible = true;
            this.colReceiptQty.VisibleIndex = 8;
            this.colReceiptQty.Width = 97;
            // 
            // colFinishQuality
            // 
            this.colFinishQuality.FieldName = "FinishQuality";
            this.colFinishQuality.Name = "colFinishQuality";
            this.colFinishQuality.Visible = true;
            this.colFinishQuality.VisibleIndex = 10;
            this.colFinishQuality.Width = 132;
            // 
            // colReceiptPcs
            // 
            this.colReceiptPcs.Caption = "Rcpt Pcs";
            this.colReceiptPcs.DisplayFormat.FormatString = "N";
            this.colReceiptPcs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colReceiptPcs.FieldName = "ReceiptPcs";
            this.colReceiptPcs.Name = "colReceiptPcs";
            this.colReceiptPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ReceiptPcs", "SUM={0:N}")});
            this.colReceiptPcs.Visible = true;
            this.colReceiptPcs.VisibleIndex = 9;
            this.colReceiptPcs.Width = 85;
            // 
            // colRate
            // 
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            // 
            // colDisc
            // 
            this.colDisc.FieldName = "Disc";
            this.colDisc.Name = "colDisc";
            // 
            // colDiscAmt
            // 
            this.colDiscAmt.FieldName = "DiscAmt";
            this.colDiscAmt.Name = "colDiscAmt";
            // 
            // colFreightRate
            // 
            this.colFreightRate.FieldName = "FreightRate";
            this.colFreightRate.Name = "colFreightRate";
            // 
            // colFreight
            // 
            this.colFreight.FieldName = "Freight";
            this.colFreight.Name = "colFreight";
            // 
            // colCgst
            // 
            this.colCgst.FieldName = "Cgst";
            this.colCgst.Name = "colCgst";
            // 
            // colCgstAmt
            // 
            this.colCgstAmt.FieldName = "CgstAmt";
            this.colCgstAmt.Name = "colCgstAmt";
            // 
            // colIgstAmt
            // 
            this.colIgstAmt.FieldName = "IgstAmt";
            this.colIgstAmt.Name = "colIgstAmt";
            // 
            // colIgst
            // 
            this.colIgst.FieldName = "Igst";
            this.colIgst.Name = "colIgst";
            // 
            // colCess
            // 
            this.colCess.FieldName = "Cess";
            this.colCess.Name = "colCess";
            // 
            // colCessAmt
            // 
            this.colCessAmt.FieldName = "CessAmt";
            this.colCessAmt.Name = "colCessAmt";
            // 
            // colSgst
            // 
            this.colSgst.FieldName = "Sgst";
            this.colSgst.Name = "colSgst";
            // 
            // colSgstAmt
            // 
            this.colSgstAmt.FieldName = "SgstAmt";
            this.colSgstAmt.Name = "colSgstAmt";
            // 
            // colUomId
            // 
            this.colUomId.FieldName = "UomId";
            this.colUomId.Name = "colUomId";
            // 
            // colTransId
            // 
            this.colTransId.FieldName = "TransId";
            this.colTransId.Name = "colTransId";
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colVoucherId
            // 
            this.colVoucherId.FieldName = "VoucherId";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // PendingMillIssueView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 297);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PendingMillIssueView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending Grey/Mill Challan";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pendingMillReceiptSpBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private System.Windows.Forms.BindingSource pendingMillReceiptSpBindingSource;
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colVoucherDate;
        private Core.Shared.Libs.CustomGridColumn colRefNo;
        private Core.Shared.Libs.CustomGridColumn colLotNo;
        private Core.Shared.Libs.CustomGridColumn colChallanNo;
        private Core.Shared.Libs.CustomGridColumn colProduct;
        private Core.Shared.Libs.CustomGridColumn colColorName;
        private Core.Shared.Libs.CustomGridColumn colProductId;
        private Core.Shared.Libs.CustomGridColumn colDesignId;
        private Core.Shared.Libs.CustomGridColumn colColorId;
        private Core.Shared.Libs.CustomGridColumn colIssueQty;
        private Core.Shared.Libs.CustomGridColumn colIssuePcs;
        private Core.Shared.Libs.CustomGridColumn colPendingQty;
        private Core.Shared.Libs.CustomGridColumn colPendingPcs;
        private Core.Shared.Libs.CustomGridColumn colReceiptQty;
        private Core.Shared.Libs.CustomGridColumn colReceiptPcs;
        private Core.Shared.Libs.CustomGridColumn colRate;
        private Core.Shared.Libs.CustomGridColumn colTotal;
        private Core.Shared.Libs.CustomGridColumn colDisc;
        private Core.Shared.Libs.CustomGridColumn colDiscAmt;
        private Core.Shared.Libs.CustomGridColumn colFreightRate;
        private Core.Shared.Libs.CustomGridColumn colFreight;
        private Core.Shared.Libs.CustomGridColumn colCgst;
        private Core.Shared.Libs.CustomGridColumn colCgstAmt;
        private Core.Shared.Libs.CustomGridColumn colIgstAmt;
        private Core.Shared.Libs.CustomGridColumn colIgst;
        private Core.Shared.Libs.CustomGridColumn colCess;
        private Core.Shared.Libs.CustomGridColumn colCessAmt;
        private Core.Shared.Libs.CustomGridColumn colSgst;
        private Core.Shared.Libs.CustomGridColumn colSgstAmt;
        private Core.Shared.Libs.CustomGridColumn colUomId;
        private Core.Shared.Libs.CustomGridColumn colTransId;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colVoucherId;
        private Core.Shared.Libs.CustomGridColumn colFinishQuality;
    }
}