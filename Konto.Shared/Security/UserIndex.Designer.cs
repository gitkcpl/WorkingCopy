namespace Konto.Shared.Security
{
    partial class UserIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserIndex));
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.nameTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.passwordTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.confirmTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.roleLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).BeginInit();
            this.tabControlAdv1.SuspendLayout();
            this.tabPageAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.confirmTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roleLookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlAdv1
            // 
            this.tabControlAdv1.BeforeTouchSize = new System.Drawing.Size(510, 287);
            this.tabControlAdv1.Size = new System.Drawing.Size(510, 287);
            this.tabControlAdv1.ThemeStyle.PrimitiveButtonStyle.DisabledNextPageImage = null;
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv2, 0);
            this.tabControlAdv1.Controls.SetChildIndex(this.tabPageAdv1, 0);
            // 
            // tabPageAdv1
            // 
            this.tabPageAdv1.Controls.Add(this.roleLookUpEdit);
            this.tabPageAdv1.Controls.Add(this.autoLabel5);
            this.tabPageAdv1.Controls.Add(this.autoLabel3);
            this.tabPageAdv1.Controls.Add(this.confirmTextBoxExt);
            this.tabPageAdv1.Controls.Add(this.autoLabel2);
            this.tabPageAdv1.Controls.Add(this.passwordTextBoxExt);
            this.tabPageAdv1.Controls.Add(this.toggleSwitch1);
            this.tabPageAdv1.Controls.Add(this.autoLabel1);
            this.tabPageAdv1.Controls.Add(this.nameTextBox);
            this.tabPageAdv1.Size = new System.Drawing.Size(485, 284);
            this.tabPageAdv1.Controls.SetChildIndex(this.panelControl1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.nameTextBox, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.toggleSwitch1, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.passwordTextBoxExt, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel2, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.confirmTextBoxExt, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel3, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.autoLabel5, 0);
            this.tabPageAdv1.Controls.SetChildIndex(this.roleLookUpEdit, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(485, 35);
            // 
            // tabPageAdv2
            // 
            this.tabPageAdv2.Size = new System.Drawing.Size(485, 306);
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.EditValue = true;
            this.toggleSwitch1.EnterMoveNextControl = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(166, 212);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleSwitch1.Properties.Appearance.Options.UseFont = true;
            this.toggleSwitch1.Properties.OffText = "DeActive";
            this.toggleSwitch1.Properties.OnText = "Active";
            this.toggleSwitch1.Size = new System.Drawing.Size(167, 24);
            this.toggleSwitch1.TabIndex = 5;
            // 
            // autoLabel1
            // 
            this.autoLabel1.AutoSize = false;
            this.autoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel1.DX = -127;
            this.autoLabel1.DY = 1;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.Image = ((System.Drawing.Image)(resources.GetObject("autoLabel1.Image")));
            this.autoLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel1.LabeledControl = this.nameTextBox;
            this.autoLabel1.Location = new System.Drawing.Point(40, 52);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(123, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 40;
            this.autoLabel1.Text = " *User Name :";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BeforeTouchSize = new System.Drawing.Size(266, 27);
            this.nameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.nameTextBox.EnterMoveNextControl = true;
            this.nameTextBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.Location = new System.Drawing.Point(167, 51);
            this.nameTextBox.MaxLength = 50;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(266, 27);
            this.nameTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.nameTextBox.TabIndex = 0;
            this.nameTextBox.Tag = "StateName";
            this.nameTextBox.ThemeName = "Metro";
            this.nameTextBox.UseBorderColorOnFocus = true;
            this.nameTextBox.WordWrap = false;
            // 
            // autoLabel2
            // 
            this.autoLabel2.AutoSize = false;
            this.autoLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel2.DX = -113;
            this.autoLabel2.DY = 1;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel2.LabeledControl = this.passwordTextBoxExt;
            this.autoLabel2.Location = new System.Drawing.Point(54, 89);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(109, 24);
            this.autoLabel2.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel2.TabIndex = 44;
            this.autoLabel2.Text = "Password :";
            this.autoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel2.ThemeName = "Office2016Colorful";
            // 
            // passwordTextBoxExt
            // 
            this.passwordTextBoxExt.BeforeTouchSize = new System.Drawing.Size(266, 27);
            this.passwordTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.passwordTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwordTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.passwordTextBoxExt.EnterMoveNextControl = true;
            this.passwordTextBoxExt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTextBoxExt.Location = new System.Drawing.Point(167, 88);
            this.passwordTextBoxExt.MaxLength = 50;
            this.passwordTextBoxExt.Name = "passwordTextBoxExt";
            this.passwordTextBoxExt.PasswordChar = '*';
            this.passwordTextBoxExt.Size = new System.Drawing.Size(266, 27);
            this.passwordTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.passwordTextBoxExt.TabIndex = 1;
            this.passwordTextBoxExt.Tag = "StateName";
            this.passwordTextBoxExt.ThemeName = "Metro";
            this.passwordTextBoxExt.UseBorderColorOnFocus = true;
            this.passwordTextBoxExt.WordWrap = false;
            // 
            // autoLabel3
            // 
            this.autoLabel3.AutoSize = false;
            this.autoLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel3.DX = -144;
            this.autoLabel3.DY = 1;
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel3.LabeledControl = this.confirmTextBoxExt;
            this.autoLabel3.Location = new System.Drawing.Point(22, 124);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(140, 24);
            this.autoLabel3.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel3.TabIndex = 46;
            this.autoLabel3.Text = "Confirm Password :";
            this.autoLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel3.ThemeName = "Office2016Colorful";
            // 
            // confirmTextBoxExt
            // 
            this.confirmTextBoxExt.BeforeTouchSize = new System.Drawing.Size(266, 27);
            this.confirmTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.confirmTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.confirmTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.confirmTextBoxExt.EnterMoveNextControl = true;
            this.confirmTextBoxExt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmTextBoxExt.Location = new System.Drawing.Point(166, 123);
            this.confirmTextBoxExt.MaxLength = 50;
            this.confirmTextBoxExt.Name = "confirmTextBoxExt";
            this.confirmTextBoxExt.PasswordChar = '*';
            this.confirmTextBoxExt.Size = new System.Drawing.Size(266, 27);
            this.confirmTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.confirmTextBoxExt.TabIndex = 2;
            this.confirmTextBoxExt.Tag = "StateName";
            this.confirmTextBoxExt.ThemeName = "Metro";
            this.confirmTextBoxExt.UseBorderColorOnFocus = true;
            this.confirmTextBoxExt.WordWrap = false;
            // 
            // autoLabel5
            // 
            this.autoLabel5.AutoSize = false;
            this.autoLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel5.DX = -86;
            this.autoLabel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel5.LabeledControl = this.roleLookUpEdit;
            this.autoLabel5.Location = new System.Drawing.Point(80, 162);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(82, 24);
            this.autoLabel5.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel5.TabIndex = 48;
            this.autoLabel5.Text = "Role :";
            this.autoLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel5.ThemeName = "Office2016Colorful";
            // 
            // roleLookUpEdit
            // 
            this.roleLookUpEdit.EnterMoveNextControl = true;
            this.roleLookUpEdit.Location = new System.Drawing.Point(166, 162);
            this.roleLookUpEdit.Name = "roleLookUpEdit";
            this.roleLookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.roleLookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roleLookUpEdit.Properties.Appearance.Options.UseFont = true;
            this.roleLookUpEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.roleLookUpEdit.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.roleLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.roleLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RoleName", "")});
            this.roleLookUpEdit.Properties.DisplayMember = "RoleName";
            this.roleLookUpEdit.Properties.ImmediatePopup = true;
            this.roleLookUpEdit.Properties.NullText = "";
            this.roleLookUpEdit.Properties.ShowHeader = false;
            this.roleLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.roleLookUpEdit.Properties.ValueMember = "Id";
            this.roleLookUpEdit.Size = new System.Drawing.Size(267, 24);
            this.roleLookUpEdit.TabIndex = 3;
            // 
            // UserIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 313);
            this.Name = "UserIndex";
            this.Text = "User Master";
            ((System.ComponentModel.ISupportInitialize)(this.tabControlAdv1)).EndInit();
            this.tabControlAdv1.ResumeLayout(false);
            this.tabPageAdv1.ResumeLayout(false);
            this.tabPageAdv1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.confirmTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roleLookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Core.Shared.Libs.KontoTextBoxExt nameTextBox;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Core.Shared.Libs.KontoTextBoxExt passwordTextBoxExt;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Core.Shared.Libs.KontoTextBoxExt confirmTextBoxExt;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private DevExpress.XtraEditors.LookUpEdit roleLookUpEdit;
    }
}