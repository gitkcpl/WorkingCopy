namespace Konto.Trading.JobReceipt
{
    partial class PendingJrViewWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingJrViewWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.pendingMillReceiptSpBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRefId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colVoucherId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChlnDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssueQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssuePcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPendingQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPendingPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCurrPendingQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCurrPendingPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQty = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProduct = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColor = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDesignId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDesign = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCgstPer = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSgstPer = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIgst = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIgstPer = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colUnitId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colUnitName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsActive = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsDeleted = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsClear = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colJobcardno = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colJobId = new Konto.Core.Shared.Libs.CustomGridColumn();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 340);
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
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(865, 340);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // pendingMillReceiptSpBindingSource
            // 
            this.pendingMillReceiptSpBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.JobReceiptDTO);
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
            this.colChallanId,
            this.colTransId,
            this.colRefId,
            this.colVoucherId,
            this.colChlnDate,
            this.colChallanDate,
            this.colChallanNo,
            this.colIssueQty,
            this.colIssuePcs,
            this.colPendingQty,
            this.colPendingPcs,
            this.colCurrPendingQty,
            this.colCurrPendingPcs,
            this.colQty,
            this.colPcs,
            this.colProduct,
            this.colProductId,
            this.colColorId,
            this.colColor,
            this.colDesignId,
            this.colDesign,
            this.colRate,
            this.colCgst,
            this.colCgstPer,
            this.colSgst,
            this.colSgstPer,
            this.colIgst,
            this.colIgstPer,
            this.colUnitId,
            this.colUnitName,
            this.colIsActive,
            this.colIsDeleted,
            this.colIsClear,
            this.colJobcardno,
            this.colJobId});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
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
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colChallanId
            // 
            this.colChallanId.FieldName = "ChallanId";
            this.colChallanId.Name = "colChallanId";
            // 
            // colTransId
            // 
            this.colTransId.FieldName = "TransId";
            this.colTransId.Name = "colTransId";
            this.colTransId.OptionsColumn.AllowEdit = false;
            // 
            // colRefId
            // 
            this.colRefId.FieldName = "RefId";
            this.colRefId.Name = "colRefId";
            this.colRefId.OptionsColumn.AllowEdit = false;
            // 
            // colVoucherId
            // 
            this.colVoucherId.FieldName = "VoucherId";
            this.colVoucherId.Name = "colVoucherId";
            this.colVoucherId.OptionsColumn.AllowEdit = false;
            // 
            // colChlnDate
            // 
            this.colChlnDate.FieldName = "ChlnDate";
            this.colChlnDate.Name = "colChlnDate";
            this.colChlnDate.OptionsColumn.AllowEdit = false;
            this.colChlnDate.Width = 84;
            // 
            // colChallanDate
            // 
            this.colChallanDate.FieldName = "ChallanDate";
            this.colChallanDate.Name = "colChallanDate";
            this.colChallanDate.OptionsColumn.AllowEdit = false;
            this.colChallanDate.OptionsColumn.ReadOnly = true;
            this.colChallanDate.Visible = true;
            this.colChallanDate.VisibleIndex = 1;
            this.colChallanDate.Width = 95;
            // 
            // colChallanNo
            // 
            this.colChallanNo.FieldName = "ChallanNo";
            this.colChallanNo.Name = "colChallanNo";
            this.colChallanNo.OptionsColumn.AllowEdit = false;
            this.colChallanNo.Visible = true;
            this.colChallanNo.VisibleIndex = 0;
            this.colChallanNo.Width = 102;
            // 
            // colIssueQty
            // 
            this.colIssueQty.Caption = "Issue Mtrs";
            this.colIssueQty.FieldName = "IssueQty";
            this.colIssueQty.Name = "colIssueQty";
            this.colIssueQty.OptionsColumn.AllowEdit = false;
            this.colIssueQty.Visible = true;
            this.colIssueQty.VisibleIndex = 3;
            // 
            // colIssuePcs
            // 
            this.colIssuePcs.FieldName = "IssuePcs";
            this.colIssuePcs.Name = "colIssuePcs";
            this.colIssuePcs.OptionsColumn.AllowEdit = false;
            this.colIssuePcs.Visible = true;
            this.colIssuePcs.VisibleIndex = 4;
            // 
            // colPendingQty
            // 
            this.colPendingQty.Caption = "Pend Mtrs";
            this.colPendingQty.FieldName = "PendingQty";
            this.colPendingQty.Name = "colPendingQty";
            this.colPendingQty.OptionsColumn.AllowEdit = false;
            this.colPendingQty.Visible = true;
            this.colPendingQty.VisibleIndex = 5;
            this.colPendingQty.Width = 81;
            // 
            // colPendingPcs
            // 
            this.colPendingPcs.FieldName = "PendingPcs";
            this.colPendingPcs.Name = "colPendingPcs";
            this.colPendingPcs.OptionsColumn.AllowEdit = false;
            this.colPendingPcs.Visible = true;
            this.colPendingPcs.VisibleIndex = 6;
            this.colPendingPcs.Width = 90;
            // 
            // colCurrPendingQty
            // 
            this.colCurrPendingQty.Caption = "Cur Pend Mtrs";
            this.colCurrPendingQty.FieldName = "CurrPendingQty";
            this.colCurrPendingQty.Name = "colCurrPendingQty";
            this.colCurrPendingQty.Visible = true;
            this.colCurrPendingQty.VisibleIndex = 9;
            this.colCurrPendingQty.Width = 122;
            // 
            // colCurrPendingPcs
            // 
            this.colCurrPendingPcs.FieldName = "CurrPendingPcs";
            this.colCurrPendingPcs.Name = "colCurrPendingPcs";
            this.colCurrPendingPcs.OptionsColumn.AllowEdit = false;
            this.colCurrPendingPcs.Visible = true;
            this.colCurrPendingPcs.VisibleIndex = 10;
            this.colCurrPendingPcs.Width = 117;
            // 
            // colQty
            // 
            this.colQty.Caption = "Used Mtrs";
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 8;
            // 
            // colPcs
            // 
            this.colPcs.Caption = "Used Pcs";
            this.colPcs.FieldName = "Pcs";
            this.colPcs.Name = "colPcs";
            this.colPcs.Visible = true;
            this.colPcs.VisibleIndex = 7;
            // 
            // colProduct
            // 
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.OptionsColumn.AllowEdit = false;
            this.colProduct.Visible = true;
            this.colProduct.VisibleIndex = 2;
            this.colProduct.Width = 137;
            // 
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            this.colProductId.OptionsColumn.AllowEdit = false;
            // 
            // colColorId
            // 
            this.colColorId.FieldName = "ColorId";
            this.colColorId.Name = "colColorId";
            this.colColorId.OptionsColumn.AllowEdit = false;
            // 
            // colColor
            // 
            this.colColor.FieldName = "Color";
            this.colColor.Name = "colColor";
            this.colColor.OptionsColumn.AllowEdit = false;
            // 
            // colDesignId
            // 
            this.colDesignId.FieldName = "DesignId";
            this.colDesignId.Name = "colDesignId";
            this.colDesignId.OptionsColumn.AllowEdit = false;
            // 
            // colDesign
            // 
            this.colDesign.FieldName = "Design";
            this.colDesign.Name = "colDesign";
            this.colDesign.OptionsColumn.AllowEdit = false;
            this.colDesign.Visible = true;
            this.colDesign.VisibleIndex = 11;
            // 
            // colRate
            // 
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.OptionsColumn.AllowEdit = false;
            // 
            // colCgst
            // 
            this.colCgst.FieldName = "Cgst";
            this.colCgst.Name = "colCgst";
            this.colCgst.OptionsColumn.AllowEdit = false;
            // 
            // colCgstPer
            // 
            this.colCgstPer.FieldName = "CgstPer";
            this.colCgstPer.Name = "colCgstPer";
            this.colCgstPer.OptionsColumn.AllowEdit = false;
            // 
            // colSgst
            // 
            this.colSgst.FieldName = "Sgst";
            this.colSgst.Name = "colSgst";
            this.colSgst.OptionsColumn.AllowEdit = false;
            // 
            // colSgstPer
            // 
            this.colSgstPer.FieldName = "SgstPer";
            this.colSgstPer.Name = "colSgstPer";
            this.colSgstPer.OptionsColumn.AllowEdit = false;
            // 
            // colIgst
            // 
            this.colIgst.FieldName = "Igst";
            this.colIgst.Name = "colIgst";
            this.colIgst.OptionsColumn.AllowEdit = false;
            // 
            // colIgstPer
            // 
            this.colIgstPer.FieldName = "IgstPer";
            this.colIgstPer.Name = "colIgstPer";
            this.colIgstPer.OptionsColumn.AllowEdit = false;
            // 
            // colUnitId
            // 
            this.colUnitId.FieldName = "UnitId";
            this.colUnitId.Name = "colUnitId";
            this.colUnitId.OptionsColumn.AllowEdit = false;
            // 
            // colUnitName
            // 
            this.colUnitName.FieldName = "UnitName";
            this.colUnitName.Name = "colUnitName";
            this.colUnitName.OptionsColumn.AllowEdit = false;
            this.colUnitName.Visible = true;
            this.colUnitName.VisibleIndex = 12;
            // 
            // colIsActive
            // 
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.OptionsColumn.AllowEdit = false;
            // 
            // colIsDeleted
            // 
            this.colIsDeleted.FieldName = "IsDeleted";
            this.colIsDeleted.Name = "colIsDeleted";
            this.colIsDeleted.OptionsColumn.AllowEdit = false;
            // 
            // colIsClear
            // 
            this.colIsClear.FieldName = "IsClear";
            this.colIsClear.Name = "colIsClear";
            this.colIsClear.Visible = true;
            this.colIsClear.VisibleIndex = 13;
            // 
            // colJobcardno
            // 
            this.colJobcardno.FieldName = "Jobcardno";
            this.colJobcardno.Name = "colJobcardno";
            // 
            // colJobId
            // 
            this.colJobId.FieldName = "JobId";
            this.colJobId.Name = "colJobId";
            // 
            // PendingJrViewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 377);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PendingJrViewWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending Job Receipt";
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
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colChallanId;
        private Core.Shared.Libs.CustomGridColumn colTransId;
        private Core.Shared.Libs.CustomGridColumn colRefId;
        private Core.Shared.Libs.CustomGridColumn colVoucherId;
        private Core.Shared.Libs.CustomGridColumn colChlnDate;
        private Core.Shared.Libs.CustomGridColumn colChallanDate;
        private Core.Shared.Libs.CustomGridColumn colChallanNo;
        private Core.Shared.Libs.CustomGridColumn colIssueQty;
        private Core.Shared.Libs.CustomGridColumn colIssuePcs;
        private Core.Shared.Libs.CustomGridColumn colPendingQty;
        private Core.Shared.Libs.CustomGridColumn colPendingPcs;
        private Core.Shared.Libs.CustomGridColumn colCurrPendingQty;
        private Core.Shared.Libs.CustomGridColumn colCurrPendingPcs;
        private Core.Shared.Libs.CustomGridColumn colQty;
        private Core.Shared.Libs.CustomGridColumn colPcs;
        private Core.Shared.Libs.CustomGridColumn colProduct;
        private Core.Shared.Libs.CustomGridColumn colProductId;
        private Core.Shared.Libs.CustomGridColumn colColorId;
        private Core.Shared.Libs.CustomGridColumn colColor;
        private Core.Shared.Libs.CustomGridColumn colDesignId;
        private Core.Shared.Libs.CustomGridColumn colDesign;
        private Core.Shared.Libs.CustomGridColumn colRate;
        private Core.Shared.Libs.CustomGridColumn colCgst;
        private Core.Shared.Libs.CustomGridColumn colCgstPer;
        private Core.Shared.Libs.CustomGridColumn colSgst;
        private Core.Shared.Libs.CustomGridColumn colSgstPer;
        private Core.Shared.Libs.CustomGridColumn colIgst;
        private Core.Shared.Libs.CustomGridColumn colIgstPer;
        private Core.Shared.Libs.CustomGridColumn colUnitId;
        private Core.Shared.Libs.CustomGridColumn colUnitName;
        private Core.Shared.Libs.CustomGridColumn colIsActive;
        private Core.Shared.Libs.CustomGridColumn colIsDeleted;
        private Core.Shared.Libs.CustomGridColumn colIsClear;
        private Core.Shared.Libs.CustomGridColumn colJobcardno;
        private Core.Shared.Libs.CustomGridColumn colJobId;
        public System.Windows.Forms.BindingSource pendingMillReceiptSpBindingSource;
    }
}