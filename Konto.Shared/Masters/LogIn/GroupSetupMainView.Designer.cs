namespace Konto.Shared.Masters.LogIn
{
    partial class GroupSetupMainView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupSetupMainView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.databaseKontoTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.groupNameKontoTextBoxExt = new Konto.Core.Shared.Libs.KontoTextBoxExt();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.databaseKontoTextBoxExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupNameKontoTextBoxExt)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 137);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 37);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseBackColor = true;
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(316, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 31);
            this.cancelSimpleButton.TabIndex = 1;
            this.cancelSimpleButton.Text = "Cancel";
            this.cancelSimpleButton.Click += new System.EventHandler(this.cancelSimpleButton_Click);
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(224, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok [F3]";
            this.okSimpleButton.Click += new System.EventHandler(this.okSimpleButton_Click);
            // 
            // autoLabel6
            // 
            this.autoLabel6.AutoSize = false;
            this.autoLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel6.DX = -147;
            this.autoLabel6.DY = 1;
            this.autoLabel6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel6.LabeledControl = this.databaseKontoTextBoxExt;
            this.autoLabel6.Location = new System.Drawing.Point(13, 82);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(143, 24);
            this.autoLabel6.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel6.TabIndex = 54;
            this.autoLabel6.Text = "*Database Name:";
            this.autoLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel6.ThemeName = "Office2016Colorful";
            // 
            // databaseKontoTextBoxExt
            // 
            this.databaseKontoTextBoxExt.BeforeTouchSize = new System.Drawing.Size(201, 27);
            this.databaseKontoTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.databaseKontoTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.databaseKontoTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.databaseKontoTextBoxExt.EnterMoveNextControl = true;
            this.databaseKontoTextBoxExt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.databaseKontoTextBoxExt.Location = new System.Drawing.Point(160, 81);
            this.databaseKontoTextBoxExt.MaxLength = 200;
            this.databaseKontoTextBoxExt.Name = "databaseKontoTextBoxExt";
            this.databaseKontoTextBoxExt.Size = new System.Drawing.Size(201, 27);
            this.databaseKontoTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.databaseKontoTextBoxExt.TabIndex = 1;
            this.databaseKontoTextBoxExt.Tag = "StateName";
            this.databaseKontoTextBoxExt.ThemeName = "Metro";
            this.databaseKontoTextBoxExt.UseBorderColorOnFocus = true;
            this.databaseKontoTextBoxExt.WordWrap = false;
            // 
            // autoLabel5
            // 
            this.autoLabel5.AutoSize = false;
            this.autoLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel5.DX = -115;
            this.autoLabel5.DY = 1;
            this.autoLabel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel5.LabeledControl = this.groupNameKontoTextBoxExt;
            this.autoLabel5.Location = new System.Drawing.Point(45, 26);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(111, 24);
            this.autoLabel5.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel5.TabIndex = 53;
            this.autoLabel5.Text = "*Group Name";
            this.autoLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel5.ThemeName = "Office2016Colorful";
            // 
            // groupNameKontoTextBoxExt
            // 
            this.groupNameKontoTextBoxExt.BeforeTouchSize = new System.Drawing.Size(201, 27);
            this.groupNameKontoTextBoxExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.groupNameKontoTextBoxExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupNameKontoTextBoxExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.groupNameKontoTextBoxExt.EnterMoveNextControl = true;
            this.groupNameKontoTextBoxExt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupNameKontoTextBoxExt.Location = new System.Drawing.Point(160, 25);
            this.groupNameKontoTextBoxExt.MaxLength = 200;
            this.groupNameKontoTextBoxExt.Name = "groupNameKontoTextBoxExt";
            this.groupNameKontoTextBoxExt.Size = new System.Drawing.Size(201, 27);
            this.groupNameKontoTextBoxExt.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.groupNameKontoTextBoxExt.TabIndex = 0;
            this.groupNameKontoTextBoxExt.Tag = "StateName";
            this.groupNameKontoTextBoxExt.ThemeName = "Metro";
            this.groupNameKontoTextBoxExt.UseBorderColorOnFocus = true;
            this.groupNameKontoTextBoxExt.WordWrap = false;
            // 
            // GroupSetupMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(410, 174);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.databaseKontoTextBoxExt);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.groupNameKontoTextBoxExt);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GroupSetupMainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Group Setup";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.databaseKontoTextBoxExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupNameKontoTextBoxExt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private Core.Shared.Libs.KontoTextBoxExt databaseKontoTextBoxExt;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private Core.Shared.Libs.KontoTextBoxExt groupNameKontoTextBoxExt;
    }
}