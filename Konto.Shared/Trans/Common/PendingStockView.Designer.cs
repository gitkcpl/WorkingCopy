namespace Konto.Shared.Trans.Common
{
    partial class PendingStockView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingStockView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.detailStockDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRowId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSrNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colInwardNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colWeaver = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colYarnName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGradeId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRefId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCops = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCopsWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPly = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTops = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCopsRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBoxWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCartnWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGrossWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTareWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colNetWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDivId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCurrQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFinQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssueRefId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssueRefVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRemark = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPlyProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVTypeId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLotNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRefNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLastParty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLastVoucher = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailStockDtoBindingSource)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 396);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(801, 37);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(707, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(615, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.detailStockDtoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(801, 396);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // detailStockDtoBindingSource
            // 
            this.detailStockDtoBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.DetailStockDto);
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
            this.colRowId,
            this.colTransId,
            this.colVDate,
            this.colSrNo,
            this.colInwardNo,
            this.colWeaver,
            this.colProductId,
            this.colYarnName,
            this.colGradeId,
            this.colColorId,
            this.colColorName,
            this.colVoucherId,
            this.colVoucherDate,
            this.colVoucherNo,
            this.colRefId,
            this.colCops,
            this.colCopsWt,
            this.colRefNo,
            this.colLastVoucher,
            this.colLastParty,
            this.colPly,
            this.colTops,
            this.colCopsRate,
            this.colBoxWt,
            this.colCartnWt,
            this.colGrossWt,
            this.colTareWt,
            this.colNetWt,
            this.colQty,
            this.colDivId,
            this.colCurrQty,
            this.colFinQty,
            this.colIssueRefId,
            this.colIssueRefVoucherId,
            this.colRemark,
            this.colPlyProductId,
            this.colVTypeId,
            this.colLotNo});
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
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
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
            // colRowId
            // 
            this.colRowId.FieldName = "RowId";
            this.colRowId.Name = "colRowId";
            // 
            // colTransId
            // 
            this.colTransId.FieldName = "TransId";
            this.colTransId.Name = "colTransId";
            // 
            // colVDate
            // 
            this.colVDate.Caption = "Date";
            this.colVDate.FieldName = "VDate";
            this.colVDate.Name = "colVDate";
            this.colVDate.Visible = true;
            this.colVDate.VisibleIndex = 7;
            this.colVDate.Width = 102;
            // 
            // colSrNo
            // 
            this.colSrNo.FieldName = "SrNo";
            this.colSrNo.Name = "colSrNo";
            this.colSrNo.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom)});
            this.colSrNo.Visible = true;
            this.colSrNo.VisibleIndex = 1;
            this.colSrNo.Width = 54;
            // 
            // colInwardNo
            // 
            this.colInwardNo.Caption = "Lot/Inward No";
            this.colInwardNo.FieldName = "InwardNo";
            this.colInwardNo.Name = "colInwardNo";
            this.colInwardNo.Visible = true;
            this.colInwardNo.VisibleIndex = 6;
            this.colInwardNo.Width = 118;
            // 
            // colWeaver
            // 
            this.colWeaver.FieldName = "Weaver";
            this.colWeaver.Name = "colWeaver";
            this.colWeaver.Visible = true;
            this.colWeaver.VisibleIndex = 8;
            this.colWeaver.Width = 187;
            // 
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            // 
            // colYarnName
            // 
            this.colYarnName.Caption = "Quality";
            this.colYarnName.FieldName = "YarnName";
            this.colYarnName.Name = "colYarnName";
            this.colYarnName.Visible = true;
            this.colYarnName.VisibleIndex = 5;
            this.colYarnName.Width = 137;
            // 
            // colGradeId
            // 
            this.colGradeId.FieldName = "GradeId";
            this.colGradeId.Name = "colGradeId";
            // 
            // colColorId
            // 
            this.colColorId.FieldName = "ColorId";
            this.colColorId.Name = "colColorId";
            // 
            // colColorName
            // 
            this.colColorName.FieldName = "ColorName";
            this.colColorName.Name = "colColorName";
            this.colColorName.Visible = true;
            this.colColorName.VisibleIndex = 10;
            this.colColorName.Width = 92;
            // 
            // colVoucherId
            // 
            this.colVoucherId.FieldName = "VoucherId";
            this.colVoucherId.Name = "colVoucherId";
            this.colVoucherId.Width = 90;
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.FieldName = "VoucherDate";
            this.colVoucherDate.Name = "colVoucherDate";
            this.colVoucherDate.Width = 115;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.Caption = "TakaNo";
            this.colVoucherNo.FieldName = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 2;
            this.colVoucherNo.Width = 70;
            // 
            // colRefId
            // 
            this.colRefId.FieldName = "RefId";
            this.colRefId.Name = "colRefId";
            // 
            // colCops
            // 
            this.colCops.FieldName = "Cops";
            this.colCops.Name = "colCops";
            // 
            // colCopsWt
            // 
            this.colCopsWt.FieldName = "CopsWt";
            this.colCopsWt.Name = "colCopsWt";
            // 
            // colPly
            // 
            this.colPly.FieldName = "Ply";
            this.colPly.Name = "colPly";
            // 
            // colTops
            // 
            this.colTops.FieldName = "Tops";
            this.colTops.Name = "colTops";
            // 
            // colCopsRate
            // 
            this.colCopsRate.FieldName = "CopsRate";
            this.colCopsRate.Name = "colCopsRate";
            // 
            // colBoxWt
            // 
            this.colBoxWt.FieldName = "BoxWt";
            this.colBoxWt.Name = "colBoxWt";
            // 
            // colCartnWt
            // 
            this.colCartnWt.FieldName = "CartnWt";
            this.colCartnWt.Name = "colCartnWt";
            // 
            // colGrossWt
            // 
            this.colGrossWt.FieldName = "GrossWt";
            this.colGrossWt.Name = "colGrossWt";
            this.colGrossWt.Visible = true;
            this.colGrossWt.VisibleIndex = 9;
            this.colGrossWt.Width = 79;
            // 
            // colTareWt
            // 
            this.colTareWt.FieldName = "TareWt";
            this.colTareWt.Name = "colTareWt";
            // 
            // colNetWt
            // 
            this.colNetWt.Caption = "Grey Meters";
            this.colNetWt.DisplayFormat.FormatString = "F";
            this.colNetWt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNetWt.FieldName = "NetWt";
            this.colNetWt.Name = "colNetWt";
            this.colNetWt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NetWt", "{0:0.##}")});
            this.colNetWt.Visible = true;
            this.colNetWt.VisibleIndex = 4;
            this.colNetWt.Width = 83;
            // 
            // colQty
            // 
            this.colQty.Caption = "Avbl Mtrs";
            this.colQty.DisplayFormat.FormatString = "F";
            this.colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:F}", "")});
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 3;
            this.colQty.Width = 79;
            // 
            // colDivId
            // 
            this.colDivId.FieldName = "DivId";
            this.colDivId.Name = "colDivId";
            // 
            // colCurrQty
            // 
            this.colCurrQty.FieldName = "CurrQty";
            this.colCurrQty.Name = "colCurrQty";
            // 
            // colFinQty
            // 
            this.colFinQty.FieldName = "FinQty";
            this.colFinQty.Name = "colFinQty";
            // 
            // colIssueRefId
            // 
            this.colIssueRefId.FieldName = "IssueRefId";
            this.colIssueRefId.Name = "colIssueRefId";
            this.colIssueRefId.Width = 113;
            // 
            // colIssueRefVoucherId
            // 
            this.colIssueRefVoucherId.FieldName = "IssueRefVoucherId";
            this.colIssueRefVoucherId.Name = "colIssueRefVoucherId";
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Width = 136;
            // 
            // colPlyProductId
            // 
            this.colPlyProductId.FieldName = "PlyProductId";
            this.colPlyProductId.Name = "colPlyProductId";
            // 
            // colVTypeId
            // 
            this.colVTypeId.FieldName = "VTypeId";
            this.colVTypeId.Name = "colVTypeId";
            // 
            // colLotNo
            // 
            this.colLotNo.FieldName = "LotNo";
            this.colLotNo.Name = "colLotNo";
            this.colLotNo.Width = 81;
            // 
            // colRefNo
            // 
            this.colRefNo.FieldName = "RefNo";
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 11;
            this.colRefNo.Width = 64;
            // 
            // colLastParty
            // 
            this.colLastParty.FieldName = "LastParty";
            this.colLastParty.Name = "colLastParty";
            this.colLastParty.Visible = true;
            this.colLastParty.VisibleIndex = 13;
            // 
            // colLastVoucher
            // 
            this.colLastVoucher.FieldName = "LastVoucher";
            this.colLastVoucher.Name = "colLastVoucher";
            this.colLastVoucher.Visible = true;
            this.colLastVoucher.VisibleIndex = 12;
            this.colLastVoucher.Width = 96;
            // 
            // PendingStockView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(801, 433);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PendingStockView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Pending Stock";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailStockDtoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private System.Windows.Forms.BindingSource detailStockDtoBindingSource;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colRowId;
        private Core.Shared.Libs.CustomGridColumn colTransId;
        private Core.Shared.Libs.CustomGridColumn colSrNo;
        private Core.Shared.Libs.CustomGridColumn colInwardNo;
        private Core.Shared.Libs.CustomGridColumn colWeaver;
        private Core.Shared.Libs.CustomGridColumn colProductId;
        private Core.Shared.Libs.CustomGridColumn colYarnName;
        private Core.Shared.Libs.CustomGridColumn colGradeId;
        private Core.Shared.Libs.CustomGridColumn colColorId;
        private Core.Shared.Libs.CustomGridColumn colVoucherId;
        private Core.Shared.Libs.CustomGridColumn colVoucherDate;
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colRefId;
        private Core.Shared.Libs.CustomGridColumn colCops;
        private Core.Shared.Libs.CustomGridColumn colCopsWt;
        private Core.Shared.Libs.CustomGridColumn colPly;
        private Core.Shared.Libs.CustomGridColumn colTops;
        private Core.Shared.Libs.CustomGridColumn colCopsRate;
        private Core.Shared.Libs.CustomGridColumn colBoxWt;
        private Core.Shared.Libs.CustomGridColumn colCartnWt;
        private Core.Shared.Libs.CustomGridColumn colGrossWt;
        private Core.Shared.Libs.CustomGridColumn colTareWt;
        private Core.Shared.Libs.CustomGridColumn colNetWt;
        private Core.Shared.Libs.CustomGridColumn colQty;
        private Core.Shared.Libs.CustomGridColumn colDivId;
        private Core.Shared.Libs.CustomGridColumn colCurrQty;
        private Core.Shared.Libs.CustomGridColumn colFinQty;
        private Core.Shared.Libs.CustomGridColumn colIssueRefId;
        private Core.Shared.Libs.CustomGridColumn colIssueRefVoucherId;
        private Core.Shared.Libs.CustomGridColumn colRemark;
        private Core.Shared.Libs.CustomGridColumn colPlyProductId;
        private Core.Shared.Libs.CustomGridColumn colVTypeId;
        private Core.Shared.Libs.CustomGridColumn colLotNo;
        private Core.Shared.Libs.CustomGridColumn colVDate;
        private Core.Shared.Libs.CustomGridColumn colColorName;
        private Core.Shared.Libs.CustomGridColumn colRefNo;
        private Core.Shared.Libs.CustomGridColumn colLastVoucher;
        private Core.Shared.Libs.CustomGridColumn colLastParty;
    }
}