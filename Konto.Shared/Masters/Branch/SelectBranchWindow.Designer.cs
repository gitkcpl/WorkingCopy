namespace Konto.Shared.Masters.Branch
{
    partial class SelectBranchWindow
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
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colBranchCode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBranchName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCompId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAddress1 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAddress2 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCityId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAreaId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPinCode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGstIn = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAadharNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colContactPerson = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colMobileNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRemark = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colExtra1 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colExtra2 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCompany = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDivisions = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRowId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsActive = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsDeleted = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCreateDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colModifyDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCreateUser = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colModifyUser = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIpAddress = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.lkpAction1 = new Konto.Core.Shared.Libs.lkpAction();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // customGridControl1
            // 
            this.customGridControl1.DataSource = this.bindingSource1;
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 29);
            this.customGridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(401, 265);
            this.customGridControl1.TabIndex = 12;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            this.customGridControl1.Click += new System.EventHandler(this.customGridControl1_Click);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Masters.BranchModel);
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.customGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.customGridView1.Appearance.Row.Options.UseFont = true;
            this.customGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBranchCode,
            this.colBranchName,
            this.colCompId,
            this.colAddress1,
            this.colAddress2,
            this.colCityId,
            this.colAreaId,
            this.colPinCode,
            this.colGstIn,
            this.colAadharNo,
            this.colContactPerson,
            this.colMobileNo,
            this.colRemark,
            this.colExtra1,
            this.colExtra2,
            this.colCompany,
            this.colDivisions,
            this.colId,
            this.colRowId,
            this.colIsActive,
            this.colIsDeleted,
            this.colCreateDate,
            this.colModifyDate,
            this.colCreateUser,
            this.colModifyUser,
            this.colIpAddress});
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
            this.customGridView1.OptionsView.ColumnAutoWidth = false;
            this.customGridView1.OptionsView.ShowFooter = true;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            this.customGridView1.RowHeight = 30;
            // 
            // colBranchCode
            // 
            this.colBranchCode.FieldName = "BranchCode";
            this.colBranchCode.Name = "colBranchCode";
            // 
            // colBranchName
            // 
            this.colBranchName.FieldName = "BranchName";
            this.colBranchName.Name = "colBranchName";
            this.colBranchName.Visible = true;
            this.colBranchName.VisibleIndex = 0;
            this.colBranchName.Width = 290;
            // 
            // colCompId
            // 
            this.colCompId.FieldName = "CompId";
            this.colCompId.Name = "colCompId";
            // 
            // colAddress1
            // 
            this.colAddress1.FieldName = "Address1";
            this.colAddress1.Name = "colAddress1";
            // 
            // colAddress2
            // 
            this.colAddress2.FieldName = "Address2";
            this.colAddress2.Name = "colAddress2";
            // 
            // colCityId
            // 
            this.colCityId.FieldName = "CityId";
            this.colCityId.Name = "colCityId";
            // 
            // colAreaId
            // 
            this.colAreaId.FieldName = "AreaId";
            this.colAreaId.Name = "colAreaId";
            // 
            // colPinCode
            // 
            this.colPinCode.FieldName = "PinCode";
            this.colPinCode.Name = "colPinCode";
            // 
            // colGstIn
            // 
            this.colGstIn.FieldName = "GstIn";
            this.colGstIn.Name = "colGstIn";
            // 
            // colAadharNo
            // 
            this.colAadharNo.FieldName = "AadharNo";
            this.colAadharNo.Name = "colAadharNo";
            // 
            // colContactPerson
            // 
            this.colContactPerson.FieldName = "ContactPerson";
            this.colContactPerson.Name = "colContactPerson";
            // 
            // colMobileNo
            // 
            this.colMobileNo.FieldName = "MobileNo";
            this.colMobileNo.Name = "colMobileNo";
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
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
            // colCompany
            // 
            this.colCompany.FieldName = "Company";
            this.colCompany.Name = "colCompany";
            // 
            // colDivisions
            // 
            this.colDivisions.FieldName = "Divisions";
            this.colDivisions.Name = "colDivisions";
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
            // colIsActive
            // 
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
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
            // lkpAction1
            // 
            this.lkpAction1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lkpAction1.Location = new System.Drawing.Point(0, 0);
            this.lkpAction1.Name = "lkpAction1";
            this.lkpAction1.Size = new System.Drawing.Size(401, 29);
            this.lkpAction1.TabIndex = 13;
            // 
            // SelectBranchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(401, 294);
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.lkpAction1);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.Name = "SelectBranchWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Branch/Unit";
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private Core.Shared.Libs.lkpAction lkpAction1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Core.Shared.Libs.CustomGridColumn colBranchCode;
        private Core.Shared.Libs.CustomGridColumn colBranchName;
        private Core.Shared.Libs.CustomGridColumn colCompId;
        private Core.Shared.Libs.CustomGridColumn colAddress1;
        private Core.Shared.Libs.CustomGridColumn colAddress2;
        private Core.Shared.Libs.CustomGridColumn colCityId;
        private Core.Shared.Libs.CustomGridColumn colAreaId;
        private Core.Shared.Libs.CustomGridColumn colPinCode;
        private Core.Shared.Libs.CustomGridColumn colGstIn;
        private Core.Shared.Libs.CustomGridColumn colAadharNo;
        private Core.Shared.Libs.CustomGridColumn colContactPerson;
        private Core.Shared.Libs.CustomGridColumn colMobileNo;
        private Core.Shared.Libs.CustomGridColumn colRemark;
        private Core.Shared.Libs.CustomGridColumn colExtra1;
        private Core.Shared.Libs.CustomGridColumn colExtra2;
        private Core.Shared.Libs.CustomGridColumn colCompany;
        private Core.Shared.Libs.CustomGridColumn colDivisions;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colRowId;
        private Core.Shared.Libs.CustomGridColumn colIsActive;
        private Core.Shared.Libs.CustomGridColumn colIsDeleted;
        private Core.Shared.Libs.CustomGridColumn colCreateDate;
        private Core.Shared.Libs.CustomGridColumn colModifyDate;
        private Core.Shared.Libs.CustomGridColumn colCreateUser;
        private Core.Shared.Libs.CustomGridColumn colModifyUser;
        private Core.Shared.Libs.CustomGridColumn colIpAddress;
    }
}