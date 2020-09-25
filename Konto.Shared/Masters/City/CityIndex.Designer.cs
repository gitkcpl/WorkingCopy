namespace Konto.Shared.Masters.City
{
    partial class CityIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CityIndex));
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.stateIdComboBox = new Konto.Core.Shared.Libs.KontoComboBoxEx();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.cityNameTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.countrySimpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIdComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cityNameTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(622, 266);
            this.tabControlAdv1.Size = new System.Drawing.Size(622, 266);
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            this.tabControlAdv1.SelectedIndexChanged += new System.EventHandler(this.tabControlAdv1_SelectedIndexChanged);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv2, 0);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv1, 0);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Controls.Add(this.toggleSwitch1);
            this.tabPageAdv1.Controls.Add(this.autoLabel2);
            this.tabPageAdv1.Controls.Add(this.stateIdComboBox);
            this.tabPageAdv1.Controls.Add(this.autoLabel1);
            this.tabPageAdv1.Controls.Add(this.cityNameTextBox);
            this.tabPageAdv1.Size = new System.Drawing.Size(597, 263);
            this.tabPageAdv1.Controls.SetChildIndex(this.cityNameTextBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.stateIdComboBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel2, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.panelControl1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.toggleSwitch1, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.countrySimpleButton);
            this.panelControl1.Size = new System.Drawing.Size(597, 35);
            this.panelControl1.Controls.SetChildIndex(this.navAction1, 0);
            this.panelControl1.Controls.SetChildIndex(this.countrySimpleButton, 0);
            // 
            // tabPageAdv2
            // 
            this.tabPageAdv2.Size = new System.Drawing.Size(597, 263);
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.EditValue = true;
            this.toggleSwitch1.EnterMoveNextControl = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(173, 158);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Properties.Appearance.Options.UseFont = true;
            this.toggleSwitch1.Properties.OffText = "DeActive";
            this.toggleSwitch1.Properties.OnText = "Active";
            this.toggleSwitch1.Size = new System.Drawing.Size(167, 24);
            this.toggleSwitch1.TabIndex = 2;
            // 
            // autoLabel2
            // 
            this.autoLabel2.AutoSize = false;
            this.autoLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel2.DX = -126;
            this.autoLabel2.DY = 2;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel2.Image = ((System.Drawing.Image)(resources.GetObject("autoLabel2.Image")));
            this.autoLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel2.LabeledControl = this.stateIdComboBox;
            this.autoLabel2.Location = new System.Drawing.Point(47, 114);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(122, 24);
            this.autoLabel2.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel2.TabIndex = 22;
            this.autoLabel2.Text = "State Name: *";
            this.autoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel2.ThemeName = "Office2016Colorful";
            // 
            // stateIdComboBox
            // 
            this.stateIdComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.stateIdComboBox.BackColor = System.Drawing.Color.White;
            this.stateIdComboBox.EnterMoveNextControl = true;
            this.stateIdComboBox.Location = new System.Drawing.Point(173, 112);
            this.stateIdComboBox.Name = "stateIdComboBox";
            this.stateIdComboBox.Size = new System.Drawing.Size(283, 28);
            this.stateIdComboBox.Style.EditorStyle.BackColor = System.Drawing.Color.White;
            this.stateIdComboBox.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.stateIdComboBox.TabIndex = 1;
            this.stateIdComboBox.Tag = "CountryId";
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
            this.autoLabel1.LabeledControl = this.cityNameTextBox;
            this.autoLabel1.Location = new System.Drawing.Point(88, 69);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(81, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 21;
            this.autoLabel1.Text = "Name: *";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // cityNameTextBox
            // 
            this.cityNameTextBox.BeforeTouchSize = new System.Drawing.Size(283, 27);
            this.cityNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.cityNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cityNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cityNameTextBox.EnterMoveNextControl = true;
            this.cityNameTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cityNameTextBox.Location = new System.Drawing.Point(173, 68);
            this.cityNameTextBox.MaxLength = 50;
            this.cityNameTextBox.Name = "cityNameTextBox";
            this.cityNameTextBox.Size = new System.Drawing.Size(283, 27);
            this.cityNameTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.cityNameTextBox.TabIndex = 0;
            this.cityNameTextBox.Tag = "StateName";
            this.cityNameTextBox.ThemeName = "Metro";
            this.cityNameTextBox.UseBorderColorOnFocus = true;
            this.cityNameTextBox.WordWrap = false;
            // 
            // countrySimpleButton
            // 
            this.countrySimpleButton.AllowFocus = false;
            this.countrySimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countrySimpleButton.Appearance.Options.UseFont = true;
            this.countrySimpleButton.Appearance.Options.UseTextOptions = true;
            this.countrySimpleButton.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.countrySimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("countrySimpleButton.ImageOptions.SvgImage")));
            this.countrySimpleButton.Location = new System.Drawing.Point(506, 2);
            this.countrySimpleButton.Name = "countrySimpleButton";
            this.countrySimpleButton.Size = new System.Drawing.Size(83, 29);
            this.countrySimpleButton.TabIndex = 2;
            this.countrySimpleButton.TabStop = false;
            this.countrySimpleButton.Text = "State";
            this.countrySimpleButton.Click += new System.EventHandler(this.countrySimpleButton_Click);
            // 
            // CityIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 292);
            this.Name = "CityIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "City Master";
            this.Load += new System.EventHandler(this.CityIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateIdComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cityNameTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Core.Shared.Libs.KontoComboBoxEx stateIdComboBox;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Core.Shared.Libs.KontoTextBoxExt cityNameTextBox;
        private DevExpress.XtraEditors.SimpleButton countrySimpleButton;
    }
}