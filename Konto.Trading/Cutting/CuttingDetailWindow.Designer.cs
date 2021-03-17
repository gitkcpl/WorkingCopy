namespace Konto.Trading.Cutting
{
    partial class CuttingDetailWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CuttingDetailWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChallanId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGradeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gradeRepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDesignId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colColorId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productRepositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colColorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colorRepositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDesignNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.designRepositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colPcs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiscId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lotNoRepositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.challanNoRrepositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradeRepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productRepositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRepositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.designRepositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lotNoRepositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.challanNoRrepositoryItemButtonEdit)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 325);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(858, 37);
            this.tableLayoutPanel1.TabIndex = 12;
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
            this.cancelSimpleButton.TabIndex = 1;
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
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Transaction.Dtos.CuttingTransDto);
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
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.gradeRepositoryItemLookUpEdit,
            this.productRepositoryItemButtonEdit,
            this.colorRepositoryItemButtonEdit,
            this.designRepositoryItemButtonEdit,
            this.lotNoRepositoryItemButtonEdit,
            this.repositoryItemTextEdit1,
            this.challanNoRrepositoryItemButtonEdit});
            this.gridControl1.Size = new System.Drawing.Size(858, 325);
            this.gridControl1.TabIndex = 0;
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
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colChallanId,
            this.colProductId,
            this.colGradeId,
            this.colDesignId,
            this.colColorId,
            this.colProductName,
            this.colColorName,
            this.colDesignNo,
            this.colPcs,
            this.colQty,
            this.colRemark,
            this.colMiscId,
            this.gridColumn1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 30;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsLayout.Columns.StoreAllOptions = true;
            this.gridView1.OptionsLayout.Columns.StoreAppearance = true;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAppearance = true;
            this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridView1.OptionsNavigation.UseTabKey = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
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
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            // 
            // colGradeId
            // 
            this.colGradeId.Caption = "Grade";
            this.colGradeId.ColumnEdit = this.gradeRepositoryItemLookUpEdit;
            this.colGradeId.FieldName = "GradeId";
            this.colGradeId.Name = "colGradeId";
            this.colGradeId.Visible = true;
            this.colGradeId.VisibleIndex = 1;
            this.colGradeId.Width = 73;
            // 
            // gradeRepositoryItemLookUpEdit
            // 
            this.gradeRepositoryItemLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.gradeRepositoryItemLookUpEdit.AutoHeight = false;
            this.gradeRepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gradeRepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Name1")});
            this.gradeRepositoryItemLookUpEdit.DisplayMember = "DisplayText";
            this.gradeRepositoryItemLookUpEdit.ImmediatePopup = true;
            this.gradeRepositoryItemLookUpEdit.Name = "gradeRepositoryItemLookUpEdit";
            this.gradeRepositoryItemLookUpEdit.NullText = "";
            this.gradeRepositoryItemLookUpEdit.ShowHeader = false;
            this.gradeRepositoryItemLookUpEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.gradeRepositoryItemLookUpEdit.ValueMember = "Id";
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
            // colProductName
            // 
            this.colProductName.Caption = "Finish Quality";
            this.colProductName.ColumnEdit = this.productRepositoryItemButtonEdit;
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowMove = false;
            this.colProductName.OptionsColumn.AllowShowHide = false;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 0;
            this.colProductName.Width = 176;
            // 
            // productRepositoryItemButtonEdit
            // 
            this.productRepositoryItemButtonEdit.AutoHeight = false;
            this.productRepositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.productRepositoryItemButtonEdit.Name = "productRepositoryItemButtonEdit";
            // 
            // colColorName
            // 
            this.colColorName.ColumnEdit = this.colorRepositoryItemButtonEdit;
            this.colColorName.FieldName = "ColorName";
            this.colColorName.Name = "colColorName";
            this.colColorName.OptionsColumn.ReadOnly = true;
            this.colColorName.Visible = true;
            this.colColorName.VisibleIndex = 2;
            this.colColorName.Width = 101;
            // 
            // colorRepositoryItemButtonEdit
            // 
            this.colorRepositoryItemButtonEdit.AutoHeight = false;
            this.colorRepositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.colorRepositoryItemButtonEdit.Name = "colorRepositoryItemButtonEdit";
            // 
            // colDesignNo
            // 
            this.colDesignNo.ColumnEdit = this.designRepositoryItemButtonEdit;
            this.colDesignNo.FieldName = "DesignNo";
            this.colDesignNo.Name = "colDesignNo";
            this.colDesignNo.OptionsColumn.ReadOnly = true;
            this.colDesignNo.Visible = true;
            this.colDesignNo.VisibleIndex = 3;
            this.colDesignNo.Width = 85;
            // 
            // designRepositoryItemButtonEdit
            // 
            this.designRepositoryItemButtonEdit.AutoHeight = false;
            this.designRepositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.designRepositoryItemButtonEdit.Name = "designRepositoryItemButtonEdit";
            // 
            // colPcs
            // 
            this.colPcs.Caption = "Pcs";
            this.colPcs.FieldName = "Pcs";
            this.colPcs.Name = "colPcs";
            this.colPcs.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Pcs", "{0:F}")});
            this.colPcs.Visible = true;
            this.colPcs.VisibleIndex = 5;
            this.colPcs.Width = 63;
            // 
            // colQty
            // 
            this.colQty.Caption = "Qty";
            this.colQty.ColumnEdit = this.repositoryItemTextEdit1;
            this.colQty.DisplayFormat.FormatString = "F";
            this.colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.OptionsColumn.AllowEdit = false;
            this.colQty.OptionsColumn.AllowFocus = false;
            this.colQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:F}")});
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 6;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "N2";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 7;
            this.colRemark.Width = 233;
            // 
            // colMiscId
            // 
            this.colMiscId.FieldName = "MiscId";
            this.colMiscId.Name = "colMiscId";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Cut";
            this.gridColumn1.FieldName = "Cops";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // lotNoRepositoryItemButtonEdit
            // 
            this.lotNoRepositoryItemButtonEdit.AutoHeight = false;
            this.lotNoRepositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.lotNoRepositoryItemButtonEdit.Name = "lotNoRepositoryItemButtonEdit";
            // 
            // challanNoRrepositoryItemButtonEdit
            // 
            this.challanNoRrepositoryItemButtonEdit.AutoHeight = false;
            this.challanNoRrepositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.challanNoRrepositoryItemButtonEdit.Name = "challanNoRrepositoryItemButtonEdit";
            // 
            // CuttingDetailWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(858, 362);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CuttingDetailWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cutting Details";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradeRepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productRepositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRepositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.designRepositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lotNoRepositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.challanNoRrepositoryItemButtonEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colChallanId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colDesignId;
        private DevExpress.XtraGrid.Columns.GridColumn colColorId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit productRepositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colColorName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit colorRepositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colDesignNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit designRepositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colPcs;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colMiscId;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit gradeRepositoryItemLookUpEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit lotNoRepositoryItemButtonEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit challanNoRrepositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colGradeId;
    }
}