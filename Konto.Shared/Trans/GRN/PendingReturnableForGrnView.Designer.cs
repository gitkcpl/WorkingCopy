namespace Konto.Shared.Trans.GRN
{
    partial class PendingReturnableForGrnView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingReturnableForGrnView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.pendingOrderDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssueDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssuePcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssueQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPendingQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPendingPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colParty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCut = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDesignId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGradeId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProduct = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTotal = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSgstAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCgstAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIgstAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFreightRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFreight = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOtherAdd = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colOtherLess = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCess = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCessAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDisc = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDiscAmt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colUomId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colNetTotal = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLotNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colrate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRcptQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRemark = new Konto.Core.Shared.Libs.CustomGridColumn();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(815, 37);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(721, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(629, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // pendingOrderDtoBindingSource
            // 
            this.pendingOrderDtoBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.PendingReturnableDto);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.pendingOrderDtoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(815, 328);
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
            this.colVoucherNo,
            this.colIssueDate,
            this.colVoucherId,
            this.colIssuePcs,
            this.colIssueQty,
            this.colPendingQty,
            this.colPendingPcs,
            this.colRemark,
            this.colParty,
            this.colColorId,
            this.colCut,
            this.colDesignId,
            this.colGradeId,
            this.colProduct,
            this.colProductId,
            this.colTotal,
            this.colSgst,
            this.colSgstAmt,
            this.colCgst,
            this.colCgstAmt,
            this.colIgst,
            this.colIgstAmt,
            this.colFreightRate,
            this.colFreight,
            this.colOtherAdd,
            this.colOtherLess,
            this.colCess,
            this.colCessAmt,
            this.colDisc,
            this.colDiscAmt,
            this.colUomId,
            this.colPcs,
            this.colNetTotal,
            this.colLotNo,
            this.colrate,
            this.colTransId,
            this.colId,
            this.colRcptQty});
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
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
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
            this.colVoucherNo.VisibleIndex = 1;
            this.colVoucherNo.Width = 97;
            // 
            // colIssueDate
            // 
            this.colIssueDate.FieldName = "IssueDate";
            this.colIssueDate.Name = "colIssueDate";
            this.colIssueDate.Visible = true;
            this.colIssueDate.VisibleIndex = 2;
            this.colIssueDate.Width = 84;
            // 
            // colVoucherId
            // 
            this.colVoucherId.FieldName = "VoucherId";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colIssuePcs
            // 
            this.colIssuePcs.FieldName = "IssuePcs";
            this.colIssuePcs.Name = "colIssuePcs";
            this.colIssuePcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IssuePcs", "{0:F0}")});
            this.colIssuePcs.Visible = true;
            this.colIssuePcs.VisibleIndex = 4;
            // 
            // colIssueQty
            // 
            this.colIssueQty.FieldName = "IssueQty";
            this.colIssueQty.Name = "colIssueQty";
            this.colIssueQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IssueQty", "{0:F}")});
            this.colIssueQty.Visible = true;
            this.colIssueQty.VisibleIndex = 5;
            this.colIssueQty.Width = 78;
            // 
            // colPendingQty
            // 
            this.colPendingQty.FieldName = "PendingQty";
            this.colPendingQty.Name = "colPendingQty";
            this.colPendingQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PendingQty", "{0:F}")});
            this.colPendingQty.Visible = true;
            this.colPendingQty.VisibleIndex = 6;
            this.colPendingQty.Width = 95;
            // 
            // colPendingPcs
            // 
            this.colPendingPcs.FieldName = "PendingPcs";
            this.colPendingPcs.Name = "colPendingPcs";
            this.colPendingPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PendingPcs", "{0:F0}")});
            this.colPendingPcs.Visible = true;
            this.colPendingPcs.VisibleIndex = 7;
            this.colPendingPcs.Width = 97;
            // 
            // colParty
            // 
            this.colParty.FieldName = "Party";
            this.colParty.Name = "colParty";
            // 
            // colColorId
            // 
            this.colColorId.FieldName = "ColorId";
            this.colColorId.Name = "colColorId";
            // 
            // colCut
            // 
            this.colCut.FieldName = "Cut";
            this.colCut.Name = "colCut";
            // 
            // colDesignId
            // 
            this.colDesignId.FieldName = "DesignId";
            this.colDesignId.Name = "colDesignId";
            // 
            // colGradeId
            // 
            this.colGradeId.FieldName = "GradeId";
            this.colGradeId.Name = "colGradeId";
            // 
            // colProduct
            // 
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.Visible = true;
            this.colProduct.VisibleIndex = 3;
            this.colProduct.Width = 157;
            // 
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            // 
            // colTotal
            // 
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
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
            // colIgst
            // 
            this.colIgst.FieldName = "Igst";
            this.colIgst.Name = "colIgst";
            // 
            // colIgstAmt
            // 
            this.colIgstAmt.FieldName = "IgstAmt";
            this.colIgstAmt.Name = "colIgstAmt";
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
            // colOtherAdd
            // 
            this.colOtherAdd.FieldName = "OtherAdd";
            this.colOtherAdd.Name = "colOtherAdd";
            // 
            // colOtherLess
            // 
            this.colOtherLess.FieldName = "OtherLess";
            this.colOtherLess.Name = "colOtherLess";
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
            // colUomId
            // 
            this.colUomId.FieldName = "UomId";
            this.colUomId.Name = "colUomId";
            // 
            // colPcs
            // 
            this.colPcs.FieldName = "Pcs";
            this.colPcs.Name = "colPcs";
            // 
            // colNetTotal
            // 
            this.colNetTotal.FieldName = "NetTotal";
            this.colNetTotal.Name = "colNetTotal";
            // 
            // colLotNo
            // 
            this.colLotNo.FieldName = "LotNo";
            this.colLotNo.Name = "colLotNo";
            this.colLotNo.Visible = true;
            this.colLotNo.VisibleIndex = 9;
            this.colLotNo.Width = 96;
            // 
            // colrate
            // 
            this.colrate.FieldName = "rate";
            this.colrate.Name = "colrate";
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
            // colRcptQty
            // 
            this.colRcptQty.FieldName = "RcptQty";
            this.colRcptQty.Name = "colRcptQty";
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 8;
            this.colRemark.Width = 144;
            // 
            // PendingReturnableForGrnView
            // 
            this.AcceptButton = this.okSimpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionFont = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(815, 365);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = true;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "PendingReturnableForGrnView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending Returnable";
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
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colIssueDate;
        private Core.Shared.Libs.CustomGridColumn colVoucherId;
        private Core.Shared.Libs.CustomGridColumn colIssuePcs;
        private Core.Shared.Libs.CustomGridColumn colIssueQty;
        private Core.Shared.Libs.CustomGridColumn colPendingQty;
        private Core.Shared.Libs.CustomGridColumn colPendingPcs;
        private Core.Shared.Libs.CustomGridColumn colParty;
        private Core.Shared.Libs.CustomGridColumn colColorId;
        private Core.Shared.Libs.CustomGridColumn colCut;
        private Core.Shared.Libs.CustomGridColumn colDesignId;
        private Core.Shared.Libs.CustomGridColumn colGradeId;
        private Core.Shared.Libs.CustomGridColumn colProduct;
        private Core.Shared.Libs.CustomGridColumn colProductId;
        private Core.Shared.Libs.CustomGridColumn colTotal;
        private Core.Shared.Libs.CustomGridColumn colSgst;
        private Core.Shared.Libs.CustomGridColumn colSgstAmt;
        private Core.Shared.Libs.CustomGridColumn colCgst;
        private Core.Shared.Libs.CustomGridColumn colCgstAmt;
        private Core.Shared.Libs.CustomGridColumn colIgst;
        private Core.Shared.Libs.CustomGridColumn colIgstAmt;
        private Core.Shared.Libs.CustomGridColumn colFreightRate;
        private Core.Shared.Libs.CustomGridColumn colFreight;
        private Core.Shared.Libs.CustomGridColumn colOtherAdd;
        private Core.Shared.Libs.CustomGridColumn colOtherLess;
        private Core.Shared.Libs.CustomGridColumn colCess;
        private Core.Shared.Libs.CustomGridColumn colCessAmt;
        private Core.Shared.Libs.CustomGridColumn colDisc;
        private Core.Shared.Libs.CustomGridColumn colDiscAmt;
        private Core.Shared.Libs.CustomGridColumn colUomId;
        private Core.Shared.Libs.CustomGridColumn colPcs;
        private Core.Shared.Libs.CustomGridColumn colNetTotal;
        private Core.Shared.Libs.CustomGridColumn colLotNo;
        private Core.Shared.Libs.CustomGridColumn colrate;
        private Core.Shared.Libs.CustomGridColumn colTransId;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colRcptQty;
        private Core.Shared.Libs.CustomGridColumn colRemark;
    }
}