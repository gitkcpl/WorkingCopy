namespace Konto.Weaves.BeamLoading
{
    partial class PendingBeamLoadingView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingBeamLoadingView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.pendingOrderDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSrNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCopsWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPallet = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGrossWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCops = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPly = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTops = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTareWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colNetWt = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCurrQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFinQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colWeaver = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colExtra1 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProdOutId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCartnWt = new Konto.Core.Shared.Libs.CustomGridColumn();
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
            this.pendingOrderDtoBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.BeamProdDto);
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
            this.colSrNo,
            this.colVoucherNo,
            this.colVDate,
            this.colCopsWt,
            this.colPallet,
            this.colCartnWt,
            this.colCops,
            this.colPly,
            this.colTops,
            this.colGrossWt,
            this.colTareWt,
            this.colNetWt,
            this.colQty,
            this.colCurrQty,
            this.colFinQty,
            this.colChallanNo,
            this.colWeaver,
            this.colExtra1,
            this.colVoucherId,
            this.colProdOutId});
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
            this.colId.Caption = "Id";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colSrNo
            // 
            this.colSrNo.FieldName = "SrNo";
            this.colSrNo.Name = "colSrNo";
            this.colSrNo.Visible = true;
            this.colSrNo.VisibleIndex = 0;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.Caption = "BeamNo";
            this.colVoucherNo.FieldName = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 1;
            this.colVoucherNo.Width = 85;
            // 
            // colVDate
            // 
            this.colVDate.Caption = "Date";
            this.colVDate.FieldName = "VDate";
            this.colVDate.Name = "colVDate";
            this.colVDate.Visible = true;
            this.colVDate.VisibleIndex = 2;
            this.colVDate.Width = 124;
            // 
            // colCopsWt
            // 
            this.colCopsWt.Caption = "Mtr/Taka";
            this.colCopsWt.FieldName = "CopsWt";
            this.colCopsWt.Name = "colCopsWt";
            this.colCopsWt.Visible = true;
            this.colCopsWt.VisibleIndex = 3;
            // 
            // colPallet
            // 
            this.colPallet.Caption = "Denier";
            this.colPallet.FieldName = "Pallet";
            this.colPallet.Name = "colPallet";
            this.colPallet.Visible = true;
            this.colPallet.VisibleIndex = 4;
            // 
            // colGrossWt
            // 
            this.colGrossWt.Caption = "Width";
            this.colGrossWt.FieldName = "GrossWt";
            this.colGrossWt.Name = "colGrossWt";
            this.colGrossWt.Visible = true;
            this.colGrossWt.VisibleIndex = 9;
            // 
            // colCops
            // 
            this.colCops.Caption = "No Of Taka";
            this.colCops.FieldName = "Cops";
            this.colCops.Name = "colCops";
            this.colCops.Visible = true;
            this.colCops.VisibleIndex = 6;
            this.colCops.Width = 105;
            // 
            // colPly
            // 
            this.colPly.Caption = "Length";
            this.colPly.FieldName = "Ply";
            this.colPly.Name = "colPly";
            this.colPly.Visible = true;
            this.colPly.VisibleIndex = 7;
            // 
            // colTops
            // 
            this.colTops.Caption = "Ends";
            this.colTops.FieldName = "Tops";
            this.colTops.Name = "colTops";
            this.colTops.Visible = true;
            this.colTops.VisibleIndex = 8;
            // 
            // colTareWt
            // 
            this.colTareWt.Caption = "Pick";
            this.colTareWt.FieldName = "TareWt";
            this.colTareWt.Name = "colTareWt";
            this.colTareWt.Visible = true;
            this.colTareWt.VisibleIndex = 10;
            // 
            // colNetWt
            // 
            this.colNetWt.Caption = "NetWt.";
            this.colNetWt.FieldName = "NetWt";
            this.colNetWt.Name = "colNetWt";
            this.colNetWt.Visible = true;
            this.colNetWt.VisibleIndex = 11;
            // 
            // colQty
            // 
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 12;
            // 
            // colCurrQty
            // 
            this.colCurrQty.FieldName = "CurrQty";
            this.colCurrQty.Name = "colCurrQty";
            this.colCurrQty.Visible = true;
            this.colCurrQty.VisibleIndex = 13;
            // 
            // colFinQty
            // 
            this.colFinQty.FieldName = "FinQty";
            this.colFinQty.Name = "colFinQty";
            this.colFinQty.Visible = true;
            this.colFinQty.VisibleIndex = 14;
            // 
            // colChallanNo
            // 
            this.colChallanNo.FieldName = "ChallanNo";
            this.colChallanNo.Name = "colChallanNo";
            this.colChallanNo.Visible = true;
            this.colChallanNo.VisibleIndex = 15;
            // 
            // colWeaver
            // 
            this.colWeaver.FieldName = "Weaver";
            this.colWeaver.Name = "colWeaver";
            this.colWeaver.Visible = true;
            this.colWeaver.VisibleIndex = 16;
            // 
            // colExtra1
            // 
            this.colExtra1.FieldName = "Extra1";
            this.colExtra1.Name = "colExtra1";
            this.colExtra1.Visible = true;
            this.colExtra1.VisibleIndex = 17;
            // 
            // colVoucherId
            // 
            this.colVoucherId.FieldName = "VoucherId";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colProdOutId
            // 
            this.colProdOutId.FieldName = "ProdOutId";
            this.colProdOutId.Name = "colProdOutId";
            // 
            // colCartnWt
            // 
            this.colCartnWt.Caption = "Wastage%";
            this.colCartnWt.FieldName = "CartnWt";
            this.colCartnWt.Name = "colCartnWt";
            this.colCartnWt.Visible = true;
            this.colCartnWt.VisibleIndex = 5;
            // 
            // PendingBeamLoadingView
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
            this.Name = "PendingBeamLoadingView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Beam";
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
        private Core.Shared.Libs.CustomGridColumn colSrNo;
        private Core.Shared.Libs.CustomGridColumn colVoucherNo;
        private Core.Shared.Libs.CustomGridColumn colGrossWt;
        private Core.Shared.Libs.CustomGridColumn colCops;
        private Core.Shared.Libs.CustomGridColumn colPly;
        private Core.Shared.Libs.CustomGridColumn colTops;
        private Core.Shared.Libs.CustomGridColumn colTareWt;
        private Core.Shared.Libs.CustomGridColumn colNetWt;
        private Core.Shared.Libs.CustomGridColumn colCurrQty;
        private Core.Shared.Libs.CustomGridColumn colChallanNo;
        private Core.Shared.Libs.CustomGridColumn colWeaver;
        private Core.Shared.Libs.CustomGridColumn colQty;
        private Core.Shared.Libs.CustomGridColumn colFinQty;
        private Core.Shared.Libs.CustomGridColumn colExtra1;
        private Core.Shared.Libs.CustomGridColumn colVoucherId;
        private Core.Shared.Libs.CustomGridColumn colProdOutId;
        private Core.Shared.Libs.CustomGridColumn colVDate;
        private Core.Shared.Libs.CustomGridColumn colCopsWt;
        private Core.Shared.Libs.CustomGridColumn colPallet;
        private Core.Shared.Libs.CustomGridColumn colCartnWt;
    }
}