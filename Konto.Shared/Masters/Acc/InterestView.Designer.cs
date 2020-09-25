namespace Konto.Shared.Masters.Acc
{
    partial class InterestView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterestView));
            this.tdsAccLookup = new Konto.Shared.Masters.Acc.AccLookup();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.interestAccLookup = new Konto.Shared.Masters.Acc.AccLookup();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.drCrComboBoxEx = new Konto.Core.Shared.Libs.KontoComboBoxEx();
            this.intPerSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.tdsPerspinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drCrComboBoxEx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intPerSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdsPerspinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tdsAccLookup
            // 
            this.tdsAccLookup.FillParty = true;
            this.tdsAccLookup.GroupId = 0;
            this.tdsAccLookup.Location = new System.Drawing.Point(95, 66);
            this.tdsAccLookup.LookupDto = null;
            this.tdsAccLookup.Name = "tdsAccLookup";
            this.tdsAccLookup.Nature = null;
            this.tdsAccLookup.NewGroupId = 0;
            this.tdsAccLookup.PrimaryKey = null;
            this.tdsAccLookup.RequiredField = false;
            this.tdsAccLookup.SelectedText = null;
            this.tdsAccLookup.SelectedValue = null;
            this.tdsAccLookup.Size = new System.Drawing.Size(264, 24);
            this.tdsAccLookup.TabIndex = 2;
            this.tdsAccLookup.TaxType = "TDS";
            this.tdsAccLookup.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // autoLabel1
            // 
            this.autoLabel1.AutoSize = false;
            this.autoLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel1.DX = -86;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel1.LabeledControl = this.tdsAccLookup;
            this.autoLabel1.Location = new System.Drawing.Point(9, 66);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(82, 24);
            this.autoLabel1.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel1.TabIndex = 19;
            this.autoLabel1.Text = "Tds A/c :";
            this.autoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel1.ThemeName = "Office2016Colorful";
            // 
            // autoLabel2
            // 
            this.autoLabel2.AutoSize = false;
            this.autoLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel2.DX = -86;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel2.LabeledControl = this.tdsPerspinEdit;
            this.autoLabel2.Location = new System.Drawing.Point(9, 98);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(82, 24);
            this.autoLabel2.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel2.TabIndex = 21;
            this.autoLabel2.Text = "Tds % :";
            this.autoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel2.ThemeName = "Office2016Colorful";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 165);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(392, 37);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(298, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 31);
            this.cancelSimpleButton.TabIndex = 6;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(206, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // autoLabel3
            // 
            this.autoLabel3.AutoSize = false;
            this.autoLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel3.DX = -86;
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel3.LabeledControl = this.intPerSpinEdit;
            this.autoLabel3.Location = new System.Drawing.Point(9, 6);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(82, 24);
            this.autoLabel3.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel3.TabIndex = 24;
            this.autoLabel3.Text = "Interest % :";
            this.autoLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel3.ThemeName = "Office2016Colorful";
            // 
            // autoLabel4
            // 
            this.autoLabel4.AutoSize = false;
            this.autoLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel4.DX = -86;
            this.autoLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel4.LabeledControl = this.interestAccLookup;
            this.autoLabel4.Location = new System.Drawing.Point(9, 36);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(82, 24);
            this.autoLabel4.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel4.TabIndex = 26;
            this.autoLabel4.Text = "Interest A/c :";
            this.autoLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel4.ThemeName = "Office2016Colorful";
            // 
            // interestAccLookup
            // 
            this.interestAccLookup.FillParty = true;
            this.interestAccLookup.GroupId = 0;
            this.interestAccLookup.Location = new System.Drawing.Point(95, 36);
            this.interestAccLookup.LookupDto = null;
            this.interestAccLookup.Name = "interestAccLookup";
            this.interestAccLookup.Nature = "EXPENSE";
            this.interestAccLookup.NewGroupId = 0;
            this.interestAccLookup.PrimaryKey = null;
            this.interestAccLookup.RequiredField = false;
            this.interestAccLookup.SelectedText = null;
            this.interestAccLookup.SelectedValue = null;
            this.interestAccLookup.Size = new System.Drawing.Size(264, 24);
            this.interestAccLookup.TabIndex = 1;
            this.interestAccLookup.TaxType = "";
            this.interestAccLookup.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // autoLabel5
            // 
            this.autoLabel5.AutoSize = false;
            this.autoLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.autoLabel5.DX = -86;
            this.autoLabel5.DY = 2;
            this.autoLabel5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.autoLabel5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoLabel5.LabeledControl = this.drCrComboBoxEx;
            this.autoLabel5.Location = new System.Drawing.Point(9, 130);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(82, 24);
            this.autoLabel5.Style = Syncfusion.Windows.Forms.Tools.AutoLabelStyle.Office2016Colorful;
            this.autoLabel5.TabIndex = 28;
            this.autoLabel5.Text = "Tds Dr/Cr :";
            this.autoLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoLabel5.ThemeName = "Office2016Colorful";
            // 
            // drCrComboBoxEx
            // 
            this.drCrComboBoxEx.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.drCrComboBoxEx.EnterMoveNextControl = true;
            this.drCrComboBoxEx.Location = new System.Drawing.Point(95, 128);
            this.drCrComboBoxEx.Name = "drCrComboBoxEx";
            this.drCrComboBoxEx.Size = new System.Drawing.Size(120, 28);
            this.drCrComboBoxEx.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.drCrComboBoxEx.TabIndex = 4;
            // 
            // intPerSpinEdit
            // 
            this.intPerSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.intPerSpinEdit.EnterMoveNextControl = true;
            this.intPerSpinEdit.Location = new System.Drawing.Point(95, 6);
            this.intPerSpinEdit.Name = "intPerSpinEdit";
            this.intPerSpinEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.intPerSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intPerSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.intPerSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.intPerSpinEdit.Size = new System.Drawing.Size(120, 24);
            this.intPerSpinEdit.TabIndex = 0;
            // 
            // tdsPerspinEdit
            // 
            this.tdsPerspinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tdsPerspinEdit.EnterMoveNextControl = true;
            this.tdsPerspinEdit.Location = new System.Drawing.Point(95, 98);
            this.tdsPerspinEdit.Name = "tdsPerspinEdit";
            this.tdsPerspinEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.tdsPerspinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdsPerspinEdit.Properties.Appearance.Options.UseFont = true;
            this.tdsPerspinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tdsPerspinEdit.Size = new System.Drawing.Size(120, 24);
            this.tdsPerspinEdit.TabIndex = 3;
            // 
            // InterestView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionFont = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(392, 202);
            this.Controls.Add(this.tdsPerspinEdit);
            this.Controls.Add(this.intPerSpinEdit);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.drCrComboBoxEx);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.interestAccLookup);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.tdsAccLookup);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "InterestView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tds Settings";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drCrComboBoxEx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intPerSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdsPerspinEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AccLookup tdsAccLookup;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private AccLookup interestAccLookup;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private Core.Shared.Libs.KontoComboBoxEx drCrComboBoxEx;
        private DevExpress.XtraEditors.SpinEdit intPerSpinEdit;
        private DevExpress.XtraEditors.SpinEdit tdsPerspinEdit;
    }
}