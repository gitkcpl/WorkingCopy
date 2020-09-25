namespace Konto.Shared.Masters.FinYear
{
    partial class SelectYearView
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
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.lkpAction1 = new Konto.Core.Shared.Libs.lkpAction();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.colYearCode = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFromDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colToDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colExtra1 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colExtra2 = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRowId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsActive = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIsDeleted = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCreateDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colModifyDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCreateUser = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colModifyUser = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colIpAddress = new Konto.Core.Shared.Libs.CustomGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
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
            this.customGridControl1.Size = new System.Drawing.Size(404, 315);
            this.customGridControl1.TabIndex = 14;
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
            this.colYearCode,
            this.colFromDate,
            this.colToDate,
            this.colFDate,
            this.colTDate,
            this.colExtra1,
            this.colExtra2,
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
            // lkpAction1
            // 
            this.lkpAction1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lkpAction1.Location = new System.Drawing.Point(0, 0);
            this.lkpAction1.Name = "lkpAction1";
            this.lkpAction1.Size = new System.Drawing.Size(404, 29);
            this.lkpAction1.TabIndex = 15;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Masters.FinYearModel);
            // 
            // colYearCode
            // 
            this.colYearCode.FieldName = "YearCode";
            this.colYearCode.Name = "colYearCode";
            this.colYearCode.Visible = true;
            this.colYearCode.VisibleIndex = 0;
            this.colYearCode.Width = 185;
            // 
            // colFromDate
            // 
            this.colFromDate.FieldName = "FromDate";
            this.colFromDate.Name = "colFromDate";
            // 
            // colToDate
            // 
            this.colToDate.FieldName = "ToDate";
            this.colToDate.Name = "colToDate";
            // 
            // colFDate
            // 
            this.colFDate.FieldName = "FDate";
            this.colFDate.Name = "colFDate";
            this.colFDate.Visible = true;
            this.colFDate.VisibleIndex = 1;
            this.colFDate.Width = 113;
            // 
            // colTDate
            // 
            this.colTDate.FieldName = "TDate";
            this.colTDate.Name = "colTDate";
            this.colTDate.Visible = true;
            this.colTDate.VisibleIndex = 2;
            this.colTDate.Width = 112;
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
            // SelectYearView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(404, 344);
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.lkpAction1);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.Name = "SelectYearView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select A Year";
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private Core.Shared.Libs.lkpAction lkpAction1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Core.Shared.Libs.CustomGridColumn colYearCode;
        private Core.Shared.Libs.CustomGridColumn colFromDate;
        private Core.Shared.Libs.CustomGridColumn colToDate;
        private Core.Shared.Libs.CustomGridColumn colFDate;
        private Core.Shared.Libs.CustomGridColumn colTDate;
        private Core.Shared.Libs.CustomGridColumn colExtra1;
        private Core.Shared.Libs.CustomGridColumn colExtra2;
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