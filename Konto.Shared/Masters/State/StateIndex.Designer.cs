namespace Konto.Shared.Masters.State
{
    partial class StateIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateIndex));
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.stateNameTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.countryIdComboBox = new Konto.Core.Shared.Libs.KontoComboBoxEx();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.gstCodeTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.countryBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.countrySimpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stateNameTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryIdComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gstCodeTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(634, 314);
            this.tabControlAdv1.Size = new System.Drawing.Size(634, 314);
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            this.tabControlAdv1.SelectedIndexChanged += new System.EventHandler(this.tabControlAdv1_SelectedIndexChanged);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv2, 0);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv1, 0);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Controls.Add(this.autoLabel3);
            this.tabPageAdv1.Controls.Add(this.gstCodeTextBox);
            this.tabPageAdv1.Controls.Add(this.toggleSwitch1);
            this.tabPageAdv1.Controls.Add(this.autoLabel2);
            this.tabPageAdv1.Controls.Add(this.countryIdComboBox);
            this.tabPageAdv1.Controls.Add(this.autoLabel1);
            this.tabPageAdv1.Controls.Add(this.stateNameTextBox);
            this.tabPageAdv1.Size = new System.Drawing.Size(609, 311);
            this.tabPageAdv1.Controls.SetChildIndex(this.panelControl1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.stateNameTextBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.countryIdComboBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel2, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.toggleSwitch1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.gstCodeTextBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel3, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.countrySimpleButton);
            this.panelControl1.Size = new System.Drawing.Size(609, 35);
            this.panelControl1.Controls.SetChildIndex(this.navAction1, 0);
            this.panelControl1.Controls.SetChildIndex(this.countrySimpleButton, 0);
            // 
            // tabPageAdv2
            // 
            this.tabPageAdv2.Size = new System.Drawing.Size(609, 311);
            // 
            // autoLabel1
            // 
            this.autoLabel1.AutoSize = false;
            this.autoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel1.DX = -85;
            this.autoLabel1.DY = 1;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.Image = ((System.Drawing.Image)(resources.GetObject("autoLabel1.Image")));
            this.autoLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel1.LabeledControl = this.stateNameTextBox;
            this.autoLabel1.Location = new System.Drawing.Point(102, 87);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(81, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 11;
            this.autoLabel1.Text = "Name: *";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // stateNameTextBox
            // 
            this.stateNameTextBox.BeforeTouchSize = new System.Drawing.Size(102, 27);
            this.stateNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.stateNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stateNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.stateNameTextBox.EnterMoveNextControl = true;
            this.stateNameTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stateNameTextBox.Location = new System.Drawing.Point(187, 86);
            this.stateNameTextBox.MaxLength = 50;
            this.stateNameTextBox.Name = "stateNameTextBox";
            this.stateNameTextBox.Size = new System.Drawing.Size(283, 27);
            this.stateNameTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.stateNameTextBox.TabIndex = 0;
            this.stateNameTextBox.Tag = "StateName";
            this.stateNameTextBox.ThemeName = "Metro";
            this.stateNameTextBox.UseBorderColorOnFocus = true;
            this.stateNameTextBox.WordWrap = false;
            // 
            // countryIdComboBox
            // 
            this.countryIdComboBox.BackColor = System.Drawing.Color.White;
            this.countryIdComboBox.EnterMoveNextControl = true;
            this.countryIdComboBox.Location = new System.Drawing.Point(187, 130);
            this.countryIdComboBox.Name = "countryIdComboBox";
            this.countryIdComboBox.Size = new System.Drawing.Size(283, 28);
            this.countryIdComboBox.Style.EditorStyle.BackColor = System.Drawing.Color.White;
            this.countryIdComboBox.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.countryIdComboBox.TabIndex = 1;
            this.countryIdComboBox.Tag = "CountryId";
            this.countryIdComboBox.ThemeName = "Metro";
            // 
            // autoLabel2
            // 
            this.autoLabel2.AutoSize = false;
            this.autoLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel2.DX = -149;
            this.autoLabel2.DY = 2;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel2.Image = ((System.Drawing.Image)(resources.GetObject("autoLabel2.Image")));
            this.autoLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel2.LabeledControl = this.countryIdComboBox;
            this.autoLabel2.Location = new System.Drawing.Point(38, 132);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(145, 24);
            this.autoLabel2.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel2.TabIndex = 12;
            this.autoLabel2.Text = "Country Name: *";
            this.autoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel2.ThemeName = "Office2016Colorful";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.EditValue = true;
            this.toggleSwitch1.EnterMoveNextControl = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(303, 176);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Properties.Appearance.Options.UseFont = true;
            this.toggleSwitch1.Properties.OffText = "DeActive";
            this.toggleSwitch1.Properties.OnText = "Active";
            this.toggleSwitch1.Size = new System.Drawing.Size(167, 24);
            this.toggleSwitch1.TabIndex = 3;
            // 
            // autoLabel3
            // 
            this.autoLabel3.AutoSize = false;
            this.autoLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel3.DX = -110;
            this.autoLabel3.DY = 1;
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel3.Image = ((System.Drawing.Image)(resources.GetObject("autoLabel3.Image")));
            this.autoLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel3.LabeledControl = this.gstCodeTextBox;
            this.autoLabel3.Location = new System.Drawing.Point(77, 179);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(106, 24);
            this.autoLabel3.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel3.TabIndex = 15;
            this.autoLabel3.Text = "GST Code: *";
            this.autoLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel3.ThemeName = "Office2016Colorful";
            // 
            // gstCodeTextBox
            // 
            this.gstCodeTextBox.BeforeTouchSize = new System.Drawing.Size(102, 27);
            this.gstCodeTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.gstCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gstCodeTextBox.EnterMoveNextControl = true;
            this.gstCodeTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gstCodeTextBox.Location = new System.Drawing.Point(187, 178);
            this.gstCodeTextBox.MaxLength = 5;
            this.gstCodeTextBox.Name = "gstCodeTextBox";
            this.gstCodeTextBox.Size = new System.Drawing.Size(102, 27);
            this.gstCodeTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.gstCodeTextBox.TabIndex = 2;
            this.gstCodeTextBox.Tag = "GstCode";
            this.gstCodeTextBox.ThemeName = "Metro";
            this.gstCodeTextBox.UseBorderColorOnFocus = true;
            this.gstCodeTextBox.WordWrap = false;
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.countryBarButtonItem);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // countryBarButtonItem
            // 
            this.countryBarButtonItem.Caption = "Country";
            this.countryBarButtonItem.Id = 14;
            this.countryBarButtonItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("countryBarButtonItem.ImageOptions.SvgImage")));
            this.countryBarButtonItem.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countryBarButtonItem.ItemAppearance.Normal.Options.UseFont = true;
            this.countryBarButtonItem.Name = "countryBarButtonItem";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Country";
            this.barButtonItem1.Id = 15;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // countrySimpleButton
            // 
            this.countrySimpleButton.AllowFocus = false;
            this.countrySimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countrySimpleButton.Appearance.Options.UseFont = true;
            this.countrySimpleButton.Appearance.Options.UseTextOptions = true;
            this.countrySimpleButton.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.countrySimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("countrySimpleButton.ImageOptions.SvgImage")));
            this.countrySimpleButton.Location = new System.Drawing.Point(505, 2);
            this.countrySimpleButton.Name = "countrySimpleButton";
            this.countrySimpleButton.Size = new System.Drawing.Size(90, 29);
            this.countrySimpleButton.TabIndex = 1;
            this.countrySimpleButton.Text = "Country";
            this.countrySimpleButton.Click += new System.EventHandler(this.countrySimpleButton_Click);
            // 
            // StateIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 340);
            this.Name = "StateIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "State Master";
            this.Load += new System.EventHandler(this.StateIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stateNameTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryIdComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gstCodeTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Core.Shared.Libs.KontoTextBoxExt stateNameTextBox;
        private Core.Shared.Libs.KontoComboBoxEx countryIdComboBox;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Core.Shared.Libs.KontoTextBoxExt gstCodeTextBox;
        private DevExpress.XtraBars.BarButtonItem countryBarButtonItem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.SimpleButton countrySimpleButton;
    }
}