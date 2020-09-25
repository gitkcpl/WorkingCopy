namespace Konto.Shared.Masters.Acc
{
    partial class AccBankView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccBankView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.nameTextBox = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.branchTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.ifscTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.acnoTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nameTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ifscTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.acnoTextBoxExt)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cancelSimpleButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.okSimpleButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 260);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(472, 37);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(378, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 31);
            this.cancelSimpleButton.TabIndex = 6;
            this.cancelSimpleButton.Text = "Cancel";
            this.cancelSimpleButton.Click += new System.EventHandler(this.cancelSimpleButton_Click);
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(286, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // autoLabel1
            // 
            this.autoLabel1.AutoSize = false;
            this.autoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel1.DX = -126;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel1.LabeledControl = this.nameTextBox;
            this.autoLabel1.Location = new System.Drawing.Point(12, 33);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(122, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 18;
            this.autoLabel1.Text = "Bank Name:";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BeforeTouchSize = new System.Drawing.Size(283, 25);
            this.nameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.nameTextBox.EnterMoveNextControl = true;
            this.nameTextBox.Location = new System.Drawing.Point(138, 33);
            this.nameTextBox.MaxLength = 50;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(283, 25);
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
            this.autoLabel2.DX = -126;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel2.LabeledControl = this.branchTextBoxExt;
            this.autoLabel2.Location = new System.Drawing.Point(12, 78);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(122, 24);
            this.autoLabel2.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel2.TabIndex = 20;
            this.autoLabel2.Text = "Branch Name:";
            this.autoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel2.ThemeName = "Office2016Colorful";
            // 
            // branchTextBoxExt
            // 
            this.branchTextBoxExt.BeforeTouchSize = new System.Drawing.Size(283, 25);
            this.branchTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.branchTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.branchTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.branchTextBoxExt.EnterMoveNextControl = true;
            this.branchTextBoxExt.Location = new System.Drawing.Point(138, 78);
            this.branchTextBoxExt.MaxLength = 50;
            this.branchTextBoxExt.Name = "branchTextBoxExt";
            this.branchTextBoxExt.Size = new System.Drawing.Size(283, 25);
            this.branchTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.branchTextBoxExt.TabIndex = 1;
            this.branchTextBoxExt.Tag = "StateName";
            this.branchTextBoxExt.ThemeName = "Metro";
            this.branchTextBoxExt.UseBorderColorOnFocus = true;
            this.branchTextBoxExt.WordWrap = false;
            // 
            // autoLabel3
            // 
            this.autoLabel3.AutoSize = false;
            this.autoLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel3.DX = -126;
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel3.LabeledControl = this.ifscTextBoxExt;
            this.autoLabel3.Location = new System.Drawing.Point(12, 121);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(122, 24);
            this.autoLabel3.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel3.TabIndex = 22;
            this.autoLabel3.Text = "Ifsc Code :";
            this.autoLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel3.ThemeName = "Office2016Colorful";
            // 
            // ifscTextBoxExt
            // 
            this.ifscTextBoxExt.BeforeTouchSize = new System.Drawing.Size(283, 25);
            this.ifscTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.ifscTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ifscTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ifscTextBoxExt.EnterMoveNextControl = true;
            this.ifscTextBoxExt.Location = new System.Drawing.Point(138, 121);
            this.ifscTextBoxExt.MaxLength = 50;
            this.ifscTextBoxExt.Name = "ifscTextBoxExt";
            this.ifscTextBoxExt.Size = new System.Drawing.Size(283, 25);
            this.ifscTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.ifscTextBoxExt.TabIndex = 2;
            this.ifscTextBoxExt.Tag = "StateName";
            this.ifscTextBoxExt.ThemeName = "Metro";
            this.ifscTextBoxExt.UseBorderColorOnFocus = true;
            this.ifscTextBoxExt.WordWrap = false;
            // 
            // autoLabel4
            // 
            this.autoLabel4.AutoSize = false;
            this.autoLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel4.DX = -126;
            this.autoLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel4.LabeledControl = this.acnoTextBoxExt;
            this.autoLabel4.Location = new System.Drawing.Point(12, 162);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(122, 24);
            this.autoLabel4.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel4.TabIndex = 24;
            this.autoLabel4.Text = "Account No.:";
            this.autoLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel4.ThemeName = "Office2016Colorful";
            // 
            // acnoTextBoxExt
            // 
            this.acnoTextBoxExt.BeforeTouchSize = new System.Drawing.Size(283, 25);
            this.acnoTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.acnoTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.acnoTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.acnoTextBoxExt.EnterMoveNextControl = true;
            this.acnoTextBoxExt.Location = new System.Drawing.Point(138, 162);
            this.acnoTextBoxExt.MaxLength = 50;
            this.acnoTextBoxExt.Name = "acnoTextBoxExt";
            this.acnoTextBoxExt.Size = new System.Drawing.Size(283, 25);
            this.acnoTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.acnoTextBoxExt.TabIndex = 3;
            this.acnoTextBoxExt.Tag = "StateName";
            this.acnoTextBoxExt.ThemeName = "Metro";
            this.acnoTextBoxExt.UseBorderColorOnFocus = true;
            this.acnoTextBoxExt.WordWrap = false;
            // 
            // AccBankView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(472, 297);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.acnoTextBoxExt);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.ifscTextBoxExt);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.branchTextBoxExt);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "AccBankView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Details";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nameTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ifscTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.acnoTextBoxExt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Core.Shared.Libs.KontoTextBoxExt nameTextBox;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Core.Shared.Libs.KontoTextBoxExt branchTextBoxExt;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Core.Shared.Libs.KontoTextBoxExt ifscTextBoxExt;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Core.Shared.Libs.KontoTextBoxExt acnoTextBoxExt;
    }
}