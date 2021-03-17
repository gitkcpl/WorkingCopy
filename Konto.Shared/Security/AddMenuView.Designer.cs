
namespace Konto.Shared.Security
{
    partial class AddMenuView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMenuView));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colParentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModuleDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinkButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShortCutKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPackageId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDefaultReport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDefaultLayout = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTableName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssemblyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMainAssembly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colListAssembly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMDI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVisible = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIconPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckRight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVisibleOnDashBoard = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVisibleOnSideBar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSeprator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOffline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExtra1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExtra2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImageIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRowId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDeleted = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModifyDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModifyUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIpAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colorRepositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gradeRepositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRepositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradeRepositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
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
            this.colorRepositoryItemButtonEdit,
            this.gradeRepositoryItemButtonEdit,
            this.repositoryItemLookUpEdit1});
            this.gridControl1.Size = new System.Drawing.Size(767, 353);
            this.gridControl1.TabIndex = 11;
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
            this.colParentId,
            this.colModuleDesc,
            this.colOrderIndex,
            this.colLinkButton,
            this.colShortCutKey,
            this.colPackageId,
            this.colDefaultReport,
            this.colDefaultLayout,
            this.colTableName,
            this.colAssemblyName,
            this.colMainAssembly,
            this.colListAssembly,
            this.colMDI,
            this.colTitle,
            this.colVisible,
            this.colIconPath,
            this.colCheckRight,
            this.colVisibleOnDashBoard,
            this.colVisibleOnSideBar,
            this.colIsSeprator,
            this.colOffline,
            this.colExtra1,
            this.colExtra2,
            this.colImageIndex,
            this.colId,
            this.colRowId,
            this.colIsActive,
            this.colIsDeleted,
            this.colCreateDate,
            this.colModifyDate,
            this.colCreateUser,
            this.colModifyUser,
            this.colIpAddress});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(601, 292, 252, 306);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsLayout.Columns.StoreAllOptions = true;
            this.gridView1.OptionsLayout.Columns.StoreAppearance = true;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAppearance = true;
            this.gridView1.OptionsNavigation.UseTabKey = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 25;
            // 
            // colParentId
            // 
            this.colParentId.FieldName = "ParentId";
            this.colParentId.Name = "colParentId";
            this.colParentId.Visible = true;
            this.colParentId.VisibleIndex = 5;
            // 
            // colModuleDesc
            // 
            this.colModuleDesc.FieldName = "ModuleDesc";
            this.colModuleDesc.Name = "colModuleDesc";
            this.colModuleDesc.Visible = true;
            this.colModuleDesc.VisibleIndex = 1;
            this.colModuleDesc.Width = 116;
            // 
            // colOrderIndex
            // 
            this.colOrderIndex.FieldName = "OrderIndex";
            this.colOrderIndex.Name = "colOrderIndex";
            this.colOrderIndex.Visible = true;
            this.colOrderIndex.VisibleIndex = 2;
            this.colOrderIndex.Width = 103;
            // 
            // colLinkButton
            // 
            this.colLinkButton.FieldName = "LinkButton";
            this.colLinkButton.Name = "colLinkButton";
            this.colLinkButton.Width = 99;
            // 
            // colShortCutKey
            // 
            this.colShortCutKey.FieldName = "ShortCutKey";
            this.colShortCutKey.Name = "colShortCutKey";
            this.colShortCutKey.Visible = true;
            this.colShortCutKey.VisibleIndex = 3;
            this.colShortCutKey.Width = 111;
            // 
            // colPackageId
            // 
            this.colPackageId.FieldName = "PackageId";
            this.colPackageId.Name = "colPackageId";
            this.colPackageId.Visible = true;
            this.colPackageId.VisibleIndex = 6;
            // 
            // colDefaultReport
            // 
            this.colDefaultReport.FieldName = "DefaultReport";
            this.colDefaultReport.Name = "colDefaultReport";
            this.colDefaultReport.Width = 99;
            // 
            // colDefaultLayout
            // 
            this.colDefaultLayout.FieldName = "DefaultLayout";
            this.colDefaultLayout.Name = "colDefaultLayout";
            // 
            // colTableName
            // 
            this.colTableName.FieldName = "TableName";
            this.colTableName.Name = "colTableName";
            // 
            // colAssemblyName
            // 
            this.colAssemblyName.FieldName = "AssemblyName";
            this.colAssemblyName.Name = "colAssemblyName";
            this.colAssemblyName.Visible = true;
            this.colAssemblyName.VisibleIndex = 7;
            this.colAssemblyName.Width = 124;
            // 
            // colMainAssembly
            // 
            this.colMainAssembly.FieldName = "MainAssembly";
            this.colMainAssembly.Name = "colMainAssembly";
            this.colMainAssembly.Visible = true;
            this.colMainAssembly.VisibleIndex = 8;
            this.colMainAssembly.Width = 123;
            // 
            // colListAssembly
            // 
            this.colListAssembly.FieldName = "ListAssembly";
            this.colListAssembly.Name = "colListAssembly";
            this.colListAssembly.Visible = true;
            this.colListAssembly.VisibleIndex = 9;
            this.colListAssembly.Width = 124;
            // 
            // colMDI
            // 
            this.colMDI.FieldName = "MDI";
            this.colMDI.Name = "colMDI";
            this.colMDI.Visible = true;
            this.colMDI.VisibleIndex = 10;
            // 
            // colTitle
            // 
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 11;
            // 
            // colVisible
            // 
            this.colVisible.FieldName = "Visible";
            this.colVisible.Name = "colVisible";
            this.colVisible.Visible = true;
            this.colVisible.VisibleIndex = 12;
            // 
            // colIconPath
            // 
            this.colIconPath.FieldName = "IconPath";
            this.colIconPath.Name = "colIconPath";
            // 
            // colCheckRight
            // 
            this.colCheckRight.FieldName = "CheckRight";
            this.colCheckRight.Name = "colCheckRight";
            this.colCheckRight.Visible = true;
            this.colCheckRight.VisibleIndex = 13;
            this.colCheckRight.Width = 101;
            // 
            // colVisibleOnDashBoard
            // 
            this.colVisibleOnDashBoard.FieldName = "VisibleOnDashBoard";
            this.colVisibleOnDashBoard.Name = "colVisibleOnDashBoard";
            this.colVisibleOnDashBoard.Visible = true;
            this.colVisibleOnDashBoard.VisibleIndex = 14;
            this.colVisibleOnDashBoard.Width = 158;
            // 
            // colVisibleOnSideBar
            // 
            this.colVisibleOnSideBar.FieldName = "VisibleOnSideBar";
            this.colVisibleOnSideBar.Name = "colVisibleOnSideBar";
            this.colVisibleOnSideBar.Visible = true;
            this.colVisibleOnSideBar.VisibleIndex = 15;
            this.colVisibleOnSideBar.Width = 132;
            // 
            // colIsSeprator
            // 
            this.colIsSeprator.FieldName = "IsSeprator";
            this.colIsSeprator.Name = "colIsSeprator";
            this.colIsSeprator.Visible = true;
            this.colIsSeprator.VisibleIndex = 16;
            this.colIsSeprator.Width = 80;
            // 
            // colOffline
            // 
            this.colOffline.FieldName = "Offline";
            this.colOffline.Name = "colOffline";
            this.colOffline.Visible = true;
            this.colOffline.VisibleIndex = 17;
            // 
            // colExtra1
            // 
            this.colExtra1.FieldName = "Extra1";
            this.colExtra1.Name = "colExtra1";
            // 
            // colExtra2
            // 
            this.colExtra2.FieldName = "Extra2";
            this.colExtra2.Name = "colExtra2";
            // 
            // colImageIndex
            // 
            this.colImageIndex.FieldName = "ImageIndex";
            this.colImageIndex.Name = "colImageIndex";
            this.colImageIndex.Visible = true;
            this.colImageIndex.VisibleIndex = 18;
            this.colImageIndex.Width = 112;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 4;
            this.colId.Width = 60;
            // 
            // colRowId
            // 
            this.colRowId.FieldName = "RowId";
            this.colRowId.Name = "colRowId";
            // 
            // colIsActive
            // 
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 19;
            // 
            // colIsDeleted
            // 
            this.colIsDeleted.FieldName = "IsDeleted";
            this.colIsDeleted.Name = "colIsDeleted";
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            // 
            // colModifyDate
            // 
            this.colModifyDate.FieldName = "ModifyDate";
            this.colModifyDate.Name = "colModifyDate";
            // 
            // colCreateUser
            // 
            this.colCreateUser.FieldName = "CreateUser";
            this.colCreateUser.Name = "colCreateUser";
            // 
            // colModifyUser
            // 
            this.colModifyUser.FieldName = "ModifyUser";
            this.colModifyUser.Name = "colModifyUser";
            // 
            // colIpAddress
            // 
            this.colIpAddress.FieldName = "IpAddress";
            this.colIpAddress.Name = "colIpAddress";
            // 
            // colorRepositoryItemButtonEdit
            // 
            this.colorRepositoryItemButtonEdit.AutoHeight = false;
            this.colorRepositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.colorRepositoryItemButtonEdit.Name = "colorRepositoryItemButtonEdit";
            // 
            // gradeRepositoryItemButtonEdit
            // 
            this.gradeRepositoryItemButtonEdit.AutoHeight = false;
            this.gradeRepositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.gradeRepositoryItemButtonEdit.Name = "gradeRepositoryItemButtonEdit";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cancelSimpleButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.okSimpleButton, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 353);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(767, 37);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(673, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(581, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Admin.ErpModule);
            // 
            // AddMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.OliveDrab;
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(767, 390);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MetroColor = System.Drawing.Color.OliveDrab;
            this.Name = "AddMenuView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Menu";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRepositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradeRepositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colParentId;
        private DevExpress.XtraGrid.Columns.GridColumn colModuleDesc;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colLinkButton;
        private DevExpress.XtraGrid.Columns.GridColumn colShortCutKey;
        private DevExpress.XtraGrid.Columns.GridColumn colPackageId;
        private DevExpress.XtraGrid.Columns.GridColumn colDefaultReport;
        private DevExpress.XtraGrid.Columns.GridColumn colDefaultLayout;
        private DevExpress.XtraGrid.Columns.GridColumn colTableName;
        private DevExpress.XtraGrid.Columns.GridColumn colAssemblyName;
        private DevExpress.XtraGrid.Columns.GridColumn colMainAssembly;
        private DevExpress.XtraGrid.Columns.GridColumn colListAssembly;
        private DevExpress.XtraGrid.Columns.GridColumn colMDI;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colVisible;
        private DevExpress.XtraGrid.Columns.GridColumn colIconPath;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckRight;
        private DevExpress.XtraGrid.Columns.GridColumn colVisibleOnDashBoard;
        private DevExpress.XtraGrid.Columns.GridColumn colVisibleOnSideBar;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSeprator;
        private DevExpress.XtraGrid.Columns.GridColumn colOffline;
        private DevExpress.XtraGrid.Columns.GridColumn colExtra1;
        private DevExpress.XtraGrid.Columns.GridColumn colExtra2;
        private DevExpress.XtraGrid.Columns.GridColumn colImageIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colRowId;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDeleted;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colModifyDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateUser;
        private DevExpress.XtraGrid.Columns.GridColumn colModifyUser;
        private DevExpress.XtraGrid.Columns.GridColumn colIpAddress;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit colorRepositoryItemButtonEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit gradeRepositoryItemButtonEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private System.Windows.Forms.BindingSource bindingSource1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}