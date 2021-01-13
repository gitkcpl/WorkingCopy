namespace Konto.Shared.Masters.Position
{
    partial class PositionIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PositionIndex));
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.remarkTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.posNameTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.remarkTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posNameTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Controls.Add(this.autoLabel2);
            this.tabPageAdv1.Controls.Add(this.remarkTextBoxExt);
            this.tabPageAdv1.Controls.Add(this.toggleSwitch1);
            this.tabPageAdv1.Controls.Add(this.autoLabel1);
            this.tabPageAdv1.Controls.Add(this.posNameTextBox);
            this.tabPageAdv1.Size = new System.Drawing.Size(534, 276);
            this.tabPageAdv1.Controls.SetChildIndex(this.posNameTextBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.toggleSwitch1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.remarkTextBoxExt, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel2, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.panelControl1, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(534, 35);
            // 
            // tabPageAdv2
            // 
            this.tabPageAdv2.Size = new System.Drawing.Size(534, 276);
            // 
            // tabPageAdv3
            // 
            this.tabPageAdv3.Size = new System.Drawing.Size(534, 276);
            // 
            // tabPageAdv4
            // 
            this.tabPageAdv4.Size = new System.Drawing.Size(534, 276);
            // 
            // autoLabel1
            // 
            this.autoLabel1.AutoSize = false;
            this.autoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel1.DX = -143;
            this.autoLabel1.DY = 1;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.Image = ((System.Drawing.Image)(resources.GetObject("autoLabel1.Image")));
            this.autoLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel1.LabeledControl = this.posNameTextBox;
            this.autoLabel1.Location = new System.Drawing.Point(21, 69);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(139, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 18;
            this.autoLabel1.Text = "Machine Position Name: *";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.EditValue = true;
            this.toggleSwitch1.EnterMoveNextControl = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(147, 162);
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
            this.autoLabel2.DX = -75;
            this.autoLabel2.DY = 1;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel2.LabeledControl = this.remarkTextBoxExt;
            this.autoLabel2.Location = new System.Drawing.Point(89, 112);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(71, 24);
            this.autoLabel2.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel2.TabIndex = 21;
            this.autoLabel2.Text = "Remark:";
            this.autoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel2.ThemeName = "Office2016Colorful";
            // 
            // remarkTextBoxExt
            // 
            this.remarkTextBoxExt.BeforeTouchSize = new System.Drawing.Size(266, 27);
            this.remarkTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.remarkTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.remarkTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.remarkTextBoxExt.EnterMoveNextControl = true;
            this.remarkTextBoxExt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remarkTextBoxExt.Location = new System.Drawing.Point(164, 111);
            this.remarkTextBoxExt.MaxLength = 200;
            this.remarkTextBoxExt.Name = "remarkTextBoxExt";
            this.remarkTextBoxExt.Size = new System.Drawing.Size(266, 27);
            this.remarkTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.remarkTextBoxExt.TabIndex = 1;
            this.remarkTextBoxExt.Tag = "StateName";
            this.remarkTextBoxExt.ThemeName = "Metro";
            this.remarkTextBoxExt.UseBorderColorOnFocus = true;
            this.remarkTextBoxExt.WordWrap = false;
            // 
            // posNameTextBox
            // 
            this.posNameTextBox.BeforeTouchSize = new System.Drawing.Size(266, 27);
            this.posNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.posNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.posNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.posNameTextBox.EnterMoveNextControl = true;
            this.posNameTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.posNameTextBox.Location = new System.Drawing.Point(164, 68);
            this.posNameTextBox.MaxLength = 50;
            this.posNameTextBox.Name = "posNameTextBox";
            this.posNameTextBox.Size = new System.Drawing.Size(266, 27);
            this.posNameTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.posNameTextBox.TabIndex = 0;
            this.posNameTextBox.Tag = "StateName";
            this.posNameTextBox.ThemeName = "Metro";
            this.posNameTextBox.UseBorderColorOnFocus = true;
            this.posNameTextBox.WordWrap = false;
            // 
            // PositionIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 305);
            this.Name = "PositionIndex";
            this.Text = "DivIndex";
            this.Load += new System.EventHandler(this.DivIndex_Load);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.remarkTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posNameTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Core.Shared.Libs.KontoTextBoxExt remarkTextBoxExt;
        private Core.Shared.Libs.KontoTextBoxExt posNameTextBox;
    }
}