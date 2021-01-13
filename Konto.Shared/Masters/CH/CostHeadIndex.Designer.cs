namespace Konto.Shared.Masters.CH
{
    partial class CostHeadIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CostHeadIndex));
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.divNameTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.remarkTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divNameTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.remarkTextBoxExt)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(559, 279);
            this.tabControlAdv1.Size = new System.Drawing.Size(559, 279);
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv4, 0);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv3, 0);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv2, 0);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv1, 0);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Controls.Add(this.autoLabel2);
            this.tabPageAdv1.Controls.Add(this.remarkTextBoxExt);
            this.tabPageAdv1.Controls.Add(this.toggleSwitch1);
            this.tabPageAdv1.Controls.Add(this.autoLabel1);
            this.tabPageAdv1.Controls.Add(this.divNameTextBox);
            this.tabPageAdv1.Size = new System.Drawing.Size(534, 276);
            this.tabPageAdv1.Controls.SetChildIndex(this.panelControl1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.divNameTextBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.toggleSwitch1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.remarkTextBoxExt, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel2, 0);
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
            this.autoLabel1.DX = -174;
            this.autoLabel1.DY = 1;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.Image = ((System.Drawing.Image)(resources.GetObject("autoLabel1.Image")));
            this.autoLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel1.LabeledControl = this.divNameTextBox;
            this.autoLabel1.Location = new System.Drawing.Point(10, 69);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(170, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 18;
            this.autoLabel1.Text = "Cost Center Name: *";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // divNameTextBox
            // 
            this.divNameTextBox.BeforeTouchSize = new System.Drawing.Size(246, 27);
            this.divNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.divNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.divNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.divNameTextBox.EnterMoveNextControl = true;
            this.divNameTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divNameTextBox.Location = new System.Drawing.Point(184, 68);
            this.divNameTextBox.MaxLength = 50;
            this.divNameTextBox.Name = "divNameTextBox";
            this.divNameTextBox.Size = new System.Drawing.Size(246, 27);
            this.divNameTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.divNameTextBox.TabIndex = 0;
            this.divNameTextBox.Tag = "StateName";
            this.divNameTextBox.ThemeName = "Metro";
            this.divNameTextBox.UseBorderColorOnFocus = true;
            this.divNameTextBox.WordWrap = false;
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
            this.autoLabel2.Location = new System.Drawing.Point(109, 112);
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
            this.remarkTextBoxExt.BeforeTouchSize = new System.Drawing.Size(246, 27);
            this.remarkTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.remarkTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.remarkTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.remarkTextBoxExt.EnterMoveNextControl = true;
            this.remarkTextBoxExt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remarkTextBoxExt.Location = new System.Drawing.Point(184, 111);
            this.remarkTextBoxExt.MaxLength = 200;
            this.remarkTextBoxExt.Name = "remarkTextBoxExt";
            this.remarkTextBoxExt.Size = new System.Drawing.Size(246, 27);
            this.remarkTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.remarkTextBoxExt.TabIndex = 1;
            this.remarkTextBoxExt.Tag = "StateName";
            this.remarkTextBoxExt.ThemeName = "Metro";
            this.remarkTextBoxExt.UseBorderColorOnFocus = true;
            this.remarkTextBoxExt.WordWrap = false;
            // 
            // CostHeadIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 305);
            this.Name = "CostHeadIndex";
            this.Text = "Cost Head/Center";
            this.Load += new System.EventHandler(this.DivIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.divNameTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.remarkTextBoxExt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Core.Shared.Libs.KontoTextBoxExt divNameTextBox;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Core.Shared.Libs.KontoTextBoxExt remarkTextBoxExt;
    }
}