namespace Konto.Shared.Trans.ST
{
    partial class StockRecvListView
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
            this.listDateRange1 = new Konto.Core.Shared.Libs.ListDateRange();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.transferReceiveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colReceiveDateTime = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBarCode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colReceivedBy = new Konto.Core.Shared.Libs.CustomGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferReceiveBindingSource)).BeginInit();
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
            // listAction1
            // 
            this.listAction1.Size = new System.Drawing.Size(468, 29);
            // 
            // listDateRange1
            // 
            this.listDateRange1.DfDate = new System.DateTime(((long)(0)));
            this.listDateRange1.FromDate = 0;
            this.listDateRange1.IsAnalysis = false;
            this.listDateRange1.KontoGrid = null;
            this.listDateRange1.Location = new System.Drawing.Point(477, 4);
            this.listDateRange1.Name = "listDateRange1";
            this.listDateRange1.ReportTypeNotRequired = true;
            this.listDateRange1.SelectedItem = null;
            this.listDateRange1.Size = new System.Drawing.Size(514, 25);
            this.listDateRange1.TabIndex = 2;
            this.listDateRange1.TfDate = new System.DateTime(((long)(0)));
            this.listDateRange1.ToDate = 0;
            this.listDateRange1.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 279);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(991, 32);
            this.panelControl1.TabIndex = 4;
            // 
            // customGridControl1
            // 
            this.customGridControl1.DataSource = this.transferReceiveBindingSource;
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 35);
            this.customGridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(991, 244);
            this.customGridControl1.TabIndex = 12;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // transferReceiveBindingSource
            // 
            this.transferReceiveBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.TransferReceive);
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.Row.Options.UseFont = true;
            this.customGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colReceiveDateTime,
            this.colVoucherNo,
            this.colBarCode,
            this.colProductName,
            this.colColorName,
            this.colQty,
            this.colPcs,
            this.colReceivedBy});
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
            this.customGridView1.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.customGridView1.OptionsView.ColumnAutoWidth = false;
            this.customGridView1.OptionsView.ShowAutoFilterRow = true;
            this.customGridView1.OptionsView.ShowFooter = true;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colReceiveDateTime
            // 
            this.colReceiveDateTime.Caption = "Receive Date";
            this.colReceiveDateTime.FieldName = "ReceiveDateTime";
            this.colReceiveDateTime.Name = "colReceiveDateTime";
            this.colReceiveDateTime.Visible = true;
            this.colReceiveDateTime.VisibleIndex = 0;
            this.colReceiveDateTime.Width = 93;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.FieldName = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 1;
            this.colVoucherNo.Width = 90;
            // 
            // colBarCode
            // 
            this.colBarCode.FieldName = "BarCode";
            this.colBarCode.Name = "colBarCode";
            this.colBarCode.Visible = true;
            this.colBarCode.VisibleIndex = 2;
            this.colBarCode.Width = 83;
            // 
            // colProductName
            // 
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 3;
            this.colProductName.Width = 165;
            // 
            // colColorName
            // 
            this.colColorName.FieldName = "ColorName";
            this.colColorName.Name = "colColorName";
            this.colColorName.Visible = true;
            this.colColorName.VisibleIndex = 4;
            this.colColorName.Width = 119;
            // 
            // colQty
            // 
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:0.##}")});
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 5;
            // 
            // colPcs
            // 
            this.colPcs.FieldName = "Pcs";
            this.colPcs.Name = "colPcs";
            this.colPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Pcs", "{0:0.##}")});
            this.colPcs.Visible = true;
            this.colPcs.VisibleIndex = 6;
            // 
            // colReceivedBy
            // 
            this.colReceivedBy.FieldName = "ReceivedBy";
            this.colReceivedBy.Name = "colReceivedBy";
            this.colReceivedBy.Visible = true;
            this.colReceivedBy.VisibleIndex = 7;
            this.colReceivedBy.Width = 132;
            // 
            // StockRecvListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.panelControl1);
            this.KontoGrid = this.customGridControl1;
            this.KontoView = this.customGridView1;
            this.Name = "StockRecvListView";
            this.Size = new System.Drawing.Size(1040, 311);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panelControl3, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.customGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferReceiveBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Core.Shared.Libs.ListDateRange listDateRange1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private System.Windows.Forms.BindingSource transferReceiveBindingSource;
        private Core.Shared.Libs.CustomGridColumn colReceiveDateTime;
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colBarCode;
        private Core.Shared.Libs.CustomGridColumn colProductName;
        private Core.Shared.Libs.CustomGridColumn colColorName;
        private Core.Shared.Libs.CustomGridColumn colQty;
        private Core.Shared.Libs.CustomGridColumn colPcs;
        private Core.Shared.Libs.CustomGridColumn colReceivedBy;
    }
}
