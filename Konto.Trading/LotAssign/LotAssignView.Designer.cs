namespace Konto.Trading.LotAssign
{
    partial class LotAssignView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotAssignView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.lotAssignDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colBalId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIssueNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChlnDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colMillName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLotNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colQuality = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPcs = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colMeter = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lotAssignDtoBindingSource)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 300);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(806, 37);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(712, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 31);
            this.cancelSimpleButton.TabIndex = 1;
            this.cancelSimpleButton.Text = "Cancel";
            this.cancelSimpleButton.Click += new System.EventHandler(this.cancelSimpleButton_Click);
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(620, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.lotAssignDtoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(806, 300);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // lotAssignDtoBindingSource
            // 
            this.lotAssignDtoBindingSource.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.LotAssignDto);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBalId,
            this.colIssueNo,
            this.colChlnDate,
            this.colChallanDate,
            this.colMillName,
            this.colLotNo,
            this.colQuality,
            this.colPcs,
            this.colMeter});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
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
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colBalId
            // 
            this.colBalId.FieldName = "BalId";
            this.colBalId.Name = "colBalId";
            // 
            // colIssueNo
            // 
            this.colIssueNo.FieldName = "IssueNo";
            this.colIssueNo.Name = "colIssueNo";
            this.colIssueNo.OptionsColumn.AllowEdit = false;
            this.colIssueNo.Visible = true;
            this.colIssueNo.VisibleIndex = 1;
            this.colIssueNo.Width = 107;
            // 
            // colChlnDate
            // 
            this.colChlnDate.FieldName = "ChlnDate";
            this.colChlnDate.Name = "colChlnDate";
            // 
            // colChallanDate
            // 
            this.colChallanDate.FieldName = "ChallanDate";
            this.colChallanDate.Name = "colChallanDate";
            this.colChallanDate.OptionsColumn.AllowEdit = false;
            this.colChallanDate.OptionsColumn.ReadOnly = true;
            this.colChallanDate.Visible = true;
            this.colChallanDate.VisibleIndex = 2;
            this.colChallanDate.Width = 120;
            // 
            // colMillName
            // 
            this.colMillName.FieldName = "MillName";
            this.colMillName.Name = "colMillName";
            this.colMillName.OptionsColumn.AllowEdit = false;
            this.colMillName.Visible = true;
            this.colMillName.VisibleIndex = 3;
            this.colMillName.Width = 137;
            // 
            // colLotNo
            // 
            this.colLotNo.FieldName = "LotNo";
            this.colLotNo.Name = "colLotNo";
            this.colLotNo.Visible = true;
            this.colLotNo.VisibleIndex = 0;
            this.colLotNo.Width = 106;
            // 
            // colQuality
            // 
            this.colQuality.FieldName = "Quality";
            this.colQuality.Name = "colQuality";
            this.colQuality.OptionsColumn.AllowEdit = false;
            this.colQuality.Visible = true;
            this.colQuality.VisibleIndex = 4;
            this.colQuality.Width = 113;
            // 
            // colPcs
            // 
            this.colPcs.FieldName = "Pcs";
            this.colPcs.Name = "colPcs";
            this.colPcs.OptionsColumn.AllowEdit = false;
            this.colPcs.Visible = true;
            this.colPcs.VisibleIndex = 5;
            // 
            // colMeter
            // 
            this.colMeter.FieldName = "Meter";
            this.colMeter.Name = "colMeter";
            this.colMeter.OptionsColumn.AllowEdit = false;
            this.colMeter.Visible = true;
            this.colMeter.VisibleIndex = 6;
            this.colMeter.Width = 86;
            // 
            // LotAssignView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(806, 337);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LotAssignView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lot Allotment";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lotAssignDtoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private System.Windows.Forms.BindingSource lotAssignDtoBindingSource;
        private Core.Shared.Libs.CustomGridColumn colBalId;
        private Core.Shared.Libs.CustomGridColumn colIssueNo;
        private Core.Shared.Libs.CustomGridColumn colChlnDate;
        private Core.Shared.Libs.CustomGridColumn colChallanDate;
        private Core.Shared.Libs.CustomGridColumn colMillName;
        private Core.Shared.Libs.CustomGridColumn colLotNo;
        private Core.Shared.Libs.CustomGridColumn colQuality;
        private Core.Shared.Libs.CustomGridColumn colPcs;
        private Core.Shared.Libs.CustomGridColumn colMeter;
    }
}