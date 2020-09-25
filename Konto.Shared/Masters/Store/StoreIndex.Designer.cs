namespace Konto.Shared.Masters.Store
{
    partial class StoreIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoreIndex));
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.storeNameTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.storeNameTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(494, 221);
            this.tabControlAdv1.Size = new System.Drawing.Size(494, 221);
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv2, 0);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv1, 0);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Controls.Add(this.toggleSwitch1);
            this.tabPageAdv1.Controls.Add(this.autoLabel1);
            this.tabPageAdv1.Controls.Add(this.storeNameTextBox);
            this.tabPageAdv1.Size = new System.Drawing.Size(469, 218);
            this.tabPageAdv1.Controls.SetChildIndex(this.panelControl1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.storeNameTextBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.toggleSwitch1, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(469, 35);
            // 
            // tabPageAdv2
            // 
            this.tabPageAdv2.Size = new System.Drawing.Size(469, 218);
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.EditValue = true;
            this.toggleSwitch1.EnterMoveNextControl = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(168, 101);
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
            this.autoLabel1.DX = -112;
            this.autoLabel1.DY = 1;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.Image = ((System.Drawing.Image)(resources.GetObject("autoLabel1.Image")));
            this.autoLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel1.LabeledControl = this.storeNameTextBox;
            this.autoLabel1.Location = new System.Drawing.Point(56, 58);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(108, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 21;
            this.autoLabel1.Text = "Store Name:";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // storeNameTextBox
            // 
            this.storeNameTextBox.BeforeTouchSize = new System.Drawing.Size(161, 27);
            this.storeNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.storeNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.storeNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.storeNameTextBox.EnterMoveNextControl = true;
            this.storeNameTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeNameTextBox.Location = new System.Drawing.Point(168, 57);
            this.storeNameTextBox.MaxLength = 50;
            this.storeNameTextBox.Name = "storeNameTextBox";
            this.storeNameTextBox.Size = new System.Drawing.Size(266, 27);
            this.storeNameTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.storeNameTextBox.TabIndex = 0;
            this.storeNameTextBox.Tag = "StateName";
            this.storeNameTextBox.ThemeName = "Metro";
            this.storeNameTextBox.UseBorderColorOnFocus = true;
            this.storeNameTextBox.WordWrap = false;
            // 
            // StoreIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 247);
            this.Name = "StoreIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StoreIndex";
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.storeNameTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Core.Shared.Libs.KontoTextBoxExt storeNameTextBox;
    }
}