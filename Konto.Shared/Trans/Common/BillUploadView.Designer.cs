namespace Konto.Shared.Trans.Common
{
    partial class BillUploadView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillUploadView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.voucherLookup11 = new Konto.Shared.Masters.Voucher.VoucherLookup();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.TemplateAsLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.accLookup = new Konto.Shared.Masters.Acc.AccLookup();
            this.bookLookup = new Konto.Shared.Masters.Acc.AccLookup();
            this.agentLookup = new Konto.Shared.Masters.Acc.AccLookup();
            this.SaveSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.jobBookLayoutControlItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Konto.Shared.Masters.Acc.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TemplateAsLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobBookLayoutControlItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.voucherLookup11);
            this.layoutControl1.Controls.Add(this.customGridControl1);
            this.layoutControl1.Controls.Add(this.cancelSimpleButton);
            this.layoutControl1.Controls.Add(this.okSimpleButton);
            this.layoutControl1.Controls.Add(this.TemplateAsLookUpEdit);
            this.layoutControl1.Controls.Add(this.accLookup);
            this.layoutControl1.Controls.Add(this.bookLookup);
            this.layoutControl1.Controls.Add(this.SaveSimpleButton);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(427, 352, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(800, 450);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // voucherLookup11
            // 
            this.voucherLookup11.GroupDto = null;
            this.voucherLookup11.Location = new System.Drawing.Point(449, 12);
            this.voucherLookup11.Name = "voucherLookup11";
            this.voucherLookup11.PrimaryKey = null;
            this.voucherLookup11.RequiredField = true;
            this.voucherLookup11.SelectedText = null;
            this.voucherLookup11.SelectedValue = null;
            this.voucherLookup11.Size = new System.Drawing.Size(339, 22);
            this.voucherLookup11.TabIndex = 14;
            this.voucherLookup11.VTypeId = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // customGridControl1
            // 
            this.customGridControl1.Location = new System.Drawing.Point(12, 104);
            this.customGridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(776, 334);
            this.customGridControl1.TabIndex = 13;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.Row.Options.UseFont = true;
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
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(665, 64);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(123, 36);
            this.cancelSimpleButton.StyleController = this.layoutControl1;
            this.cancelSimpleButton.TabIndex = 10;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(12, 64);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(164, 36);
            this.okSimpleButton.StyleController = this.layoutControl1;
            this.okSimpleButton.TabIndex = 6;
            this.okSimpleButton.Text = "Select File";
            this.okSimpleButton.Click += new System.EventHandler(this.okSimpleButton_Click);
            // 
            // TemplateAsLookUpEdit
            // 
            this.TemplateAsLookUpEdit.EnterMoveNextControl = true;
            this.TemplateAsLookUpEdit.Location = new System.Drawing.Point(84, 12);
            this.TemplateAsLookUpEdit.Name = "TemplateAsLookUpEdit";
            this.TemplateAsLookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.TemplateAsLookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemplateAsLookUpEdit.Properties.Appearance.Options.UseFont = true;
            this.TemplateAsLookUpEdit.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemplateAsLookUpEdit.Properties.AppearanceDropDown.Options.UseFont = true;
            this.TemplateAsLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TemplateAsLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Name3")});
            this.TemplateAsLookUpEdit.Properties.DisplayMember = "DisplayText";
            this.TemplateAsLookUpEdit.Properties.ImmediatePopup = true;
            this.TemplateAsLookUpEdit.Properties.NullText = "";
            this.TemplateAsLookUpEdit.Properties.ShowHeader = false;
            this.TemplateAsLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.TemplateAsLookUpEdit.Properties.ValueMember = "Id";
            this.TemplateAsLookUpEdit.Size = new System.Drawing.Size(289, 24);
            this.TemplateAsLookUpEdit.StyleController = this.layoutControl1;
            this.TemplateAsLookUpEdit.TabIndex = 2;
            this.TemplateAsLookUpEdit.EditValueChanged += new System.EventHandler(this.TemplateAsLookUpEdit_EditValueChanged);
            // 
            // accLookup
            // 
            this.accLookup.AgentLookup = null;
            this.accLookup.FillParty = true;
            this.accLookup.GroupId = 0;
            this.accLookup.Location = new System.Drawing.Point(84, 40);
            this.accLookup.LookupDto = null;
            this.accLookup.Name = "accLookup";
            this.accLookup.Nature = null;
            this.accLookup.NewGroupId = App.Shared.LedgerGroupEnum.SUNDRY_DEBTORS;
            this.accLookup.PrimaryKey = null;
            this.accLookup.RequiredField = true;
            this.accLookup.SelectedText = null;
            this.accLookup.SelectedValue = null;
            this.accLookup.Size = new System.Drawing.Size(289, 20);
            this.accLookup.TabIndex = 5;
            this.accLookup.TaxType = null;
            this.accLookup.TransportLookup = null;
            this.accLookup.VoucherType = Konto.App.Shared.VoucherTypeEnum.SaleInvoice;
            // 
            // bookLookup
            // 
            this.bookLookup.AgentLookup = this.agentLookup;
            this.bookLookup.FillParty = false;
            this.bookLookup.GroupId = 0;
            this.bookLookup.Location = new System.Drawing.Point(449, 38);
            this.bookLookup.LookupDto = null;
            this.bookLookup.Name = "bookLookup";
            this.bookLookup.Nature = "";
            this.bookLookup.NewGroupId = 0;
            this.bookLookup.PrimaryKey = null;
            this.bookLookup.RequiredField = true;
            this.bookLookup.SelectedText = null;
            this.bookLookup.SelectedValue = null;
            this.bookLookup.Size = new System.Drawing.Size(339, 22);
            this.bookLookup.TabIndex = 9;
            this.bookLookup.TaxType = null;
            this.bookLookup.TransportLookup = null;
            this.bookLookup.VoucherType = Konto.App.Shared.VoucherTypeEnum.SaleInvoice;
            // 
            // agentLookup
            // 
            this.agentLookup.AgentLookup = null;
            this.agentLookup.FillParty = false;
            this.agentLookup.GroupId = 31;
            this.agentLookup.Location = new System.Drawing.Point(770, 33);
            this.agentLookup.LookupDto = null;
            this.agentLookup.Name = "agentLookup";
            this.agentLookup.Nature = null;
            this.agentLookup.NewGroupId = App.Shared.LedgerGroupEnum.CREDITORS_FOR_BROKERAGE;
            this.agentLookup.PrimaryKey = null;
            this.agentLookup.RequiredField = false;
            this.agentLookup.SelectedText = null;
            this.agentLookup.SelectedValue = null;
            this.agentLookup.Size = new System.Drawing.Size(169, 23);
            this.agentLookup.TabIndex = 6;
            this.agentLookup.TaxType = null;
            this.agentLookup.TransportLookup = null;
            this.agentLookup.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // SaveSimpleButton
            // 
            this.SaveSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveSimpleButton.Appearance.Options.UseFont = true;
            this.SaveSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("SaveSimpleButton.ImageOptions.SvgImage")));
            this.SaveSimpleButton.Location = new System.Drawing.Point(495, 64);
            this.SaveSimpleButton.Name = "SaveSimpleButton";
            this.SaveSimpleButton.Size = new System.Drawing.Size(166, 36);
            this.SaveSimpleButton.StyleController = this.layoutControl1;
            this.SaveSimpleButton.TabIndex = 6;
            this.SaveSimpleButton.Text = "Save";
            this.SaveSimpleButton.Click += new System.EventHandler(this.SaveSimpleButton_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem18,
            this.layoutControlItem4,
            this.jobBookLayoutControlItem,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem3,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(800, 450);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.AllowHtmlStringInCaption = true;
            this.layoutControlItem18.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem18.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem18.Control = this.TemplateAsLookUpEdit;
            this.layoutControlItem18.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem18.CustomizationFormText = "<color=255, 0, 0>*</color> Template:";
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(365, 28);
            this.layoutControlItem18.Text = "<color=255, 0, 0>*</color> Template:";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(69, 17);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AllowHtmlStringInCaption = true;
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.accLookup;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "<color=255, 0, 0>*</color>Party:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(365, 24);
            this.layoutControlItem4.Text = "Party:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(69, 17);
            // 
            // jobBookLayoutControlItem
            // 
            this.jobBookLayoutControlItem.AllowHtmlStringInCaption = true;
            this.jobBookLayoutControlItem.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobBookLayoutControlItem.AppearanceItemCaption.Options.UseFont = true;
            this.jobBookLayoutControlItem.Control = this.bookLookup;
            this.jobBookLayoutControlItem.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.jobBookLayoutControlItem.CustomizationFormText = "<color=255, 0, 0>*</color>Book:";
            this.jobBookLayoutControlItem.Location = new System.Drawing.Point(365, 26);
            this.jobBookLayoutControlItem.Name = "jobBookLayoutControlItem";
            this.jobBookLayoutControlItem.Size = new System.Drawing.Size(415, 26);
            this.jobBookLayoutControlItem.Text = "<color=255, 0, 0>*</color>Book :";
            this.jobBookLayoutControlItem.TextSize = new System.Drawing.Size(69, 17);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.okSimpleButton;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(168, 40);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cancelSimpleButton;
            this.layoutControlItem2.Location = new System.Drawing.Point(653, 52);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(127, 40);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.SaveSimpleButton;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem5.Location = new System.Drawing.Point(483, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(170, 40);
            this.layoutControlItem5.Text = "layoutControlItem1";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.customGridControl1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 92);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(780, 338);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(168, 52);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(315, 40);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AllowHtmlStringInCaption = true;
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.voucherLookup11;
            this.layoutControlItem3.Location = new System.Drawing.Point(365, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(415, 26);
            this.layoutControlItem3.Text = "<color=255, 0, 0>*</color>Voucher:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(69, 17);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // BillUploadView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.layoutControl1);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "BillUploadView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload For Templates";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TemplateAsLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobBookLayoutControlItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LookUpEdit TemplateAsLookUpEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private Masters.Acc.AccLookup accLookup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Masters.Acc.AccLookup bookLookup;
        private Masters.Acc.AccLookup agentLookup;
        private DevExpress.XtraLayout.LayoutControlItem jobBookLayoutControlItem;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton SaveSimpleButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private Masters.Voucher.VoucherLookup voucherLookup11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}