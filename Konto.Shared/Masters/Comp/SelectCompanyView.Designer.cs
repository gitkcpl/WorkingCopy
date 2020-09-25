namespace Konto.Shared.Masters.Comp
{
    partial class SelectCompanyView
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.lkpAction1 = new Konto.Core.Shared.Libs.lkpAction();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colCompName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPrintName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSortName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAddress1 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAddress2 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCityId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPincode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colStateId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFAddress1 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFAddress2 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFCityId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFPincode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFStateId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colMobile = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPhone = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colEmail = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colWebsite = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPara = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGstIn = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAadharNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTdsAcNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRemark = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colAcNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBankName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colHolyWorld = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIfsCode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colInsurance = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSendFrom = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colSendPass = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colLogoPath = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colExtra1 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colExtra2 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colNobId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colEmailPass = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colPromotionalAPI = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransactionalAPI = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colBranches = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRowId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsActive = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsDeleted = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCreateDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colModifyDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCreateUser = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colModifyUser = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIpAddress = new Konto.Core.Shared.Libs.CustomGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Masters.CompModel);
            // 
            // lkpAction1
            // 
            this.lkpAction1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lkpAction1.Location = new System.Drawing.Point(0, 0);
            this.lkpAction1.Name = "lkpAction1";
            this.lkpAction1.Size = new System.Drawing.Size(582, 29);
            this.lkpAction1.TabIndex = 11;
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
            this.customGridControl1.Size = new System.Drawing.Size(582, 283);
            this.customGridControl1.TabIndex = 0;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.customGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.customGridView1.Appearance.Row.Options.UseFont = true;
            this.customGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCompName,
            this.colPrintName,
            this.colSortName,
            this.colAddress1,
            this.colAddress2,
            this.colCityId,
            this.colPincode,
            this.colStateId,
            this.colFAddress1,
            this.colFAddress2,
            this.colFCityId,
            this.colFPincode,
            this.colFStateId,
            this.colMobile,
            this.colPhone,
            this.colEmail,
            this.colWebsite,
            this.colPara,
            this.colGstIn,
            this.colPanNo,
            this.colAadharNo,
            this.colTdsAcNo,
            this.colRemark,
            this.colAcNo,
            this.colBankName,
            this.colHolyWorld,
            this.colIfsCode,
            this.colInsurance,
            this.colSendFrom,
            this.colSendPass,
            this.colLogoPath,
            this.colExtra1,
            this.colExtra2,
            this.colNobId,
            this.colEmailPass,
            this.colPromotionalAPI,
            this.colTransactionalAPI,
            this.colBranches,
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
            // colCompName
            // 
            this.colCompName.FieldName = "CompName";
            this.colCompName.Name = "colCompName";
            this.colCompName.Visible = true;
            this.colCompName.VisibleIndex = 0;
            this.colCompName.Width = 384;
            // 
            // colPrintName
            // 
            this.colPrintName.FieldName = "PrintName";
            this.colPrintName.Name = "colPrintName";
            // 
            // colSortName
            // 
            this.colSortName.FieldName = "SortName";
            this.colSortName.Name = "colSortName";
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
            // colPincode
            // 
            this.colPincode.FieldName = "Pincode";
            this.colPincode.Name = "colPincode";
            // 
            // colStateId
            // 
            this.colStateId.FieldName = "StateId";
            this.colStateId.Name = "colStateId";
            // 
            // colFAddress1
            // 
            this.colFAddress1.FieldName = "FAddress1";
            this.colFAddress1.Name = "colFAddress1";
            // 
            // colFAddress2
            // 
            this.colFAddress2.FieldName = "FAddress2";
            this.colFAddress2.Name = "colFAddress2";
            // 
            // colFCityId
            // 
            this.colFCityId.FieldName = "FCityId";
            this.colFCityId.Name = "colFCityId";
            // 
            // colFPincode
            // 
            this.colFPincode.FieldName = "FPincode";
            this.colFPincode.Name = "colFPincode";
            // 
            // colFStateId
            // 
            this.colFStateId.FieldName = "FStateId";
            this.colFStateId.Name = "colFStateId";
            // 
            // colMobile
            // 
            this.colMobile.FieldName = "Mobile";
            this.colMobile.Name = "colMobile";
            // 
            // colPhone
            // 
            this.colPhone.FieldName = "Phone";
            this.colPhone.Name = "colPhone";
            // 
            // colEmail
            // 
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            // 
            // colWebsite
            // 
            this.colWebsite.FieldName = "Website";
            this.colWebsite.Name = "colWebsite";
            // 
            // colPara
            // 
            this.colPara.FieldName = "Para";
            this.colPara.Name = "colPara";
            // 
            // colGstIn
            // 
            this.colGstIn.FieldName = "GstIn";
            this.colGstIn.Name = "colGstIn";
            this.colGstIn.Visible = true;
            this.colGstIn.VisibleIndex = 1;
            this.colGstIn.Width = 144;
            // 
            // colPanNo
            // 
            this.colPanNo.FieldName = "PanNo";
            this.colPanNo.Name = "colPanNo";
            // 
            // colAadharNo
            // 
            this.colAadharNo.FieldName = "AadharNo";
            this.colAadharNo.Name = "colAadharNo";
            // 
            // colTdsAcNo
            // 
            this.colTdsAcNo.FieldName = "TdsAcNo";
            this.colTdsAcNo.Name = "colTdsAcNo";
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            // 
            // colAcNo
            // 
            this.colAcNo.FieldName = "AcNo";
            this.colAcNo.Name = "colAcNo";
            // 
            // colBankName
            // 
            this.colBankName.FieldName = "BankName";
            this.colBankName.Name = "colBankName";
            // 
            // colHolyWorld
            // 
            this.colHolyWorld.FieldName = "HolyWorld";
            this.colHolyWorld.Name = "colHolyWorld";
            // 
            // colIfsCode
            // 
            this.colIfsCode.FieldName = "IfsCode";
            this.colIfsCode.Name = "colIfsCode";
            // 
            // colInsurance
            // 
            this.colInsurance.FieldName = "Insurance";
            this.colInsurance.Name = "colInsurance";
            // 
            // colSendFrom
            // 
            this.colSendFrom.FieldName = "SendFrom";
            this.colSendFrom.Name = "colSendFrom";
            // 
            // colSendPass
            // 
            this.colSendPass.FieldName = "SendPass";
            this.colSendPass.Name = "colSendPass";
            // 
            // colLogoPath
            // 
            this.colLogoPath.FieldName = "LogoPath";
            this.colLogoPath.Name = "colLogoPath";
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
            // colNobId
            // 
            this.colNobId.FieldName = "NobId";
            this.colNobId.Name = "colNobId";
            // 
            // colEmailPass
            // 
            this.colEmailPass.FieldName = "EmailPass";
            this.colEmailPass.Name = "colEmailPass";
            // 
            // colPromotionalAPI
            // 
            this.colPromotionalAPI.FieldName = "PromotionalAPI";
            this.colPromotionalAPI.Name = "colPromotionalAPI";
            // 
            // colTransactionalAPI
            // 
            this.colTransactionalAPI.FieldName = "TransactionalAPI";
            this.colTransactionalAPI.Name = "colTransactionalAPI";
            // 
            // colBranches
            // 
            this.colBranches.FieldName = "Branches";
            this.colBranches.Name = "colBranches";
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
            // SelectCompanyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(582, 312);
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.lkpAction1);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.Name = "SelectCompanyView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select A Company";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private Core.Shared.Libs.lkpAction lkpAction1;
        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private Core.Shared.Libs.CustomGridColumn colCompName;
        private Core.Shared.Libs.CustomGridColumn colPrintName;
        private Core.Shared.Libs.CustomGridColumn colSortName;
        private Core.Shared.Libs.CustomGridColumn colAddress1;
        private Core.Shared.Libs.CustomGridColumn colAddress2;
        private Core.Shared.Libs.CustomGridColumn colCityId;
        private Core.Shared.Libs.CustomGridColumn colPincode;
        private Core.Shared.Libs.CustomGridColumn colStateId;
        private Core.Shared.Libs.CustomGridColumn colFAddress1;
        private Core.Shared.Libs.CustomGridColumn colFAddress2;
        private Core.Shared.Libs.CustomGridColumn colFCityId;
        private Core.Shared.Libs.CustomGridColumn colFPincode;
        private Core.Shared.Libs.CustomGridColumn colFStateId;
        private Core.Shared.Libs.CustomGridColumn colMobile;
        private Core.Shared.Libs.CustomGridColumn colPhone;
        private Core.Shared.Libs.CustomGridColumn colEmail;
        private Core.Shared.Libs.CustomGridColumn colWebsite;
        private Core.Shared.Libs.CustomGridColumn colPara;
        private Core.Shared.Libs.CustomGridColumn colGstIn;
        private Core.Shared.Libs.CustomGridColumn colPanNo;
        private Core.Shared.Libs.CustomGridColumn colAadharNo;
        private Core.Shared.Libs.CustomGridColumn colTdsAcNo;
        private Core.Shared.Libs.CustomGridColumn colRemark;
        private Core.Shared.Libs.CustomGridColumn colAcNo;
        private Core.Shared.Libs.CustomGridColumn colBankName;
        private Core.Shared.Libs.CustomGridColumn colHolyWorld;
        private Core.Shared.Libs.CustomGridColumn colIfsCode;
        private Core.Shared.Libs.CustomGridColumn colInsurance;
        private Core.Shared.Libs.CustomGridColumn colSendFrom;
        private Core.Shared.Libs.CustomGridColumn colSendPass;
        private Core.Shared.Libs.CustomGridColumn colLogoPath;
        private Core.Shared.Libs.CustomGridColumn colExtra1;
        private Core.Shared.Libs.CustomGridColumn colExtra2;
        private Core.Shared.Libs.CustomGridColumn colNobId;
        private Core.Shared.Libs.CustomGridColumn colEmailPass;
        private Core.Shared.Libs.CustomGridColumn colPromotionalAPI;
        private Core.Shared.Libs.CustomGridColumn colTransactionalAPI;
        private Core.Shared.Libs.CustomGridColumn colBranches;
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