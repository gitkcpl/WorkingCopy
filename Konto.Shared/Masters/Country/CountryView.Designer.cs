namespace Konto.Shared.Masters.Country
{
    partial class CountryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountryView));
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.countryNameTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
        
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryNameTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(501, 271);
            this.tabControlAdv1.Size = new System.Drawing.Size(501, 271);
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            this.tabControlAdv1.SelectedIndexChanged += new System.EventHandler(this.tabControlAdv1_SelectedIndexChanged);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv2, 0);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv1, 0);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Controls.Add(this.toggleSwitch1);
            this.tabPageAdv1.Controls.Add(this.autoLabel1);
            this.tabPageAdv1.Controls.Add(this.countryNameTextBox);
            this.tabPageAdv1.Size = new System.Drawing.Size(498, 247);
          
            this.tabPageAdv1.Controls.SetChildIndex(this.panelControl1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.countryNameTextBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.toggleSwitch1, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(498, 35);
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(397, 2);
            this.cancelSimpleButton.TabIndex = 3;
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(305, 2);
            this.okSimpleButton.TabIndex = 2;
            this.okSimpleButton.Click += new System.EventHandler(this.okSimpleButton_Click);
            // 
            // tabPageAdv2
            // 
            this.tabPageAdv2.Size = new System.Drawing.Size(498, 247);
            // 
            // panelControl2
            // 
       
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.EnterMoveNextControl = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(110, 147);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Properties.Appearance.Options.UseFont = true;
            this.toggleSwitch1.Properties.OffText = "DeActive";
            this.toggleSwitch1.Properties.OnText = "Active";
            this.toggleSwitch1.Size = new System.Drawing.Size(167, 24);
            this.toggleSwitch1.TabIndex = 1;
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
            this.autoLabel1.LabeledControl = this.countryNameTextBox;
            this.autoLabel1.Location = new System.Drawing.Point(25, 101);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(81, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 9;
            this.autoLabel1.Text = "Name: *";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // countryNameTextBox
            // 
            this.countryNameTextBox.BeforeTouchSize = new System.Drawing.Size(337, 27);
            this.countryNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.countryNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.countryNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.countryNameTextBox.EnterMoveNextControl = true;
            this.countryNameTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countryNameTextBox.Location = new System.Drawing.Point(110, 100);
            this.countryNameTextBox.Name = "countryNameTextBox";
            this.countryNameTextBox.Size = new System.Drawing.Size(337, 27);
            this.countryNameTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.countryNameTextBox.TabIndex = 0;
            this.countryNameTextBox.ThemeName = "Metro";
            this.countryNameTextBox.UseBorderColorOnFocus = true;
            this.countryNameTextBox.WordWrap = false;
            // 
            // CountryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 297);
            this.Name = "CountryView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Country Master";
            this.Load += new System.EventHandler(this.CountryView_Load);
            this.Shown += new System.EventHandler(this.CountryView_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
         
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryNameTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Core.Shared.Libs.KontoTextBoxExt countryNameTextBox;
    }
}