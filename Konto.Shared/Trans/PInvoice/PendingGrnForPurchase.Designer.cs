namespace Konto.Shared.Trans.PInvoice
{
    partial class PendingGrnForPurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingGrnForPurchase));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colChallanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChlnDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBillNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRcdDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransportId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTotalQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colInwPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colInwQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTotalPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colNetTotal = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDocNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDocDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVehicleNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRemark = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 274);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(846, 37);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(752, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(660, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(846, 274);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.PendingChallanOnInvoiceDto);
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
            this.colChallanNo,
            this.colChallanDate,
            this.colChlnDate,
            this.colBillNo,
            this.colRcdDate,
            this.colTransportId,
            this.colTotalQty,
            this.colInwPcs,
            this.colInwQty,
            this.colTotalPcs,
            this.colNetTotal,
            this.colId,
            this.colVoucherId,
            this.colDocNo,
            this.colDocDate,
            this.colVehicleNo,
            this.colRemark});
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
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 35;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colChallanNo
            // 
            this.colChallanNo.FieldName = "ChallanNo";
            this.colChallanNo.Name = "colChallanNo";
            this.colChallanNo.Visible = true;
            this.colChallanNo.VisibleIndex = 1;
            this.colChallanNo.Width = 101;
            // 
            // colChallanDate
            // 
            this.colChallanDate.FieldName = "ChallanDate";
            this.colChallanDate.Name = "colChallanDate";
            this.colChallanDate.Visible = true;
            this.colChallanDate.VisibleIndex = 2;
            this.colChallanDate.Width = 107;
            // 
            // colChlnDate
            // 
            this.colChlnDate.FieldName = "ChlnDate";
            this.colChlnDate.Name = "colChlnDate";
            // 
            // colBillNo
            // 
            this.colBillNo.FieldName = "BillNo";
            this.colBillNo.Name = "colBillNo";
            // 
            // colRcdDate
            // 
            this.colRcdDate.FieldName = "RcdDate";
            this.colRcdDate.Name = "colRcdDate";
            this.colRcdDate.Visible = true;
            this.colRcdDate.VisibleIndex = 3;
            this.colRcdDate.Width = 96;
            // 
            // colTransportId
            // 
            this.colTransportId.FieldName = "TransportId";
            this.colTransportId.Name = "colTransportId";
            this.colTransportId.Width = 90;
            // 
            // colTotalQty
            // 
            this.colTotalQty.FieldName = "TotalQty";
            this.colTotalQty.Name = "colTotalQty";
            this.colTotalQty.Visible = true;
            this.colTotalQty.VisibleIndex = 4;
            this.colTotalQty.Width = 94;
            // 
            // colInwPcs
            // 
            this.colInwPcs.FieldName = "InwPcs";
            this.colInwPcs.Name = "colInwPcs";
            // 
            // colInwQty
            // 
            this.colInwQty.FieldName = "InwQty";
            this.colInwQty.Name = "colInwQty";
            // 
            // colTotalPcs
            // 
            this.colTotalPcs.FieldName = "TotalPcs";
            this.colTotalPcs.Name = "colTotalPcs";
            this.colTotalPcs.Visible = true;
            this.colTotalPcs.VisibleIndex = 5;
            this.colTotalPcs.Width = 91;
            // 
            // colNetTotal
            // 
            this.colNetTotal.FieldName = "NetTotal";
            this.colNetTotal.Name = "colNetTotal";
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
            // colDocNo
            // 
            this.colDocNo.FieldName = "DocNo";
            this.colDocNo.Name = "colDocNo";
            // 
            // colDocDate
            // 
            this.colDocDate.FieldName = "DocDate";
            this.colDocDate.Name = "colDocDate";
            // 
            // colVehicleNo
            // 
            this.colVehicleNo.FieldName = "VehicleNo";
            this.colVehicleNo.Name = "colVehicleNo";
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 6;
            this.colRemark.Width = 265;
            // 
            // PendingGrnForPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(846, 311);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PendingGrnForPurchase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending Purchase Grn/Challlan";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Core.Shared.Libs.CustomGridColumn colChallanNo;
        private Core.Shared.Libs.CustomGridColumn colChallanDate;
        private Core.Shared.Libs.CustomGridColumn colChlnDate;
        private Core.Shared.Libs.CustomGridColumn colBillNo;
        private Core.Shared.Libs.CustomGridColumn colRcdDate;
        private Core.Shared.Libs.CustomGridColumn colTransportId;
        private Core.Shared.Libs.CustomGridColumn colTotalQty;
        private Core.Shared.Libs.CustomGridColumn colInwPcs;
        private Core.Shared.Libs.CustomGridColumn colInwQty;
        private Core.Shared.Libs.CustomGridColumn colTotalPcs;
        private Core.Shared.Libs.CustomGridColumn colNetTotal;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colVoucherId;
        private Core.Shared.Libs.CustomGridColumn colDocNo;
        private Core.Shared.Libs.CustomGridColumn colDocDate;
        private Core.Shared.Libs.CustomGridColumn colVehicleNo;
        private Core.Shared.Libs.CustomGridColumn colRemark;
    }
}