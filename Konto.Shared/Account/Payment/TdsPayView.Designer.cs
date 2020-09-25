namespace Konto.Shared.Account.Payment
{
    partial class TdsPayView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TdsPayView));
            this.tdsPerTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.tdsAmtTextEdit = new DevExpress.XtraEditors.SpinEdit();
            this.grossSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.tdsAccLookup = new Konto.Shared.Masters.Acc.AccLookup();
            ((System.ComponentModel.ISupportInitialize)(this.tdsPerTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdsAmtTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grossSpinEdit.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tdsPerTextEdit
            // 
            this.tdsPerTextEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tdsPerTextEdit.EnterMoveNextControl = true;
            this.tdsPerTextEdit.Location = new System.Drawing.Point(110, 56);
            this.tdsPerTextEdit.Name = "tdsPerTextEdit";
            this.tdsPerTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdsPerTextEdit.Properties.Appearance.Options.UseFont = true;
            this.tdsPerTextEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tdsPerTextEdit.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.tdsPerTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tdsPerTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.tdsPerTextEdit.Properties.Mask.EditMask = "N2";
            this.tdsPerTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.tdsPerTextEdit.Properties.MaxLength = 6;
            this.tdsPerTextEdit.Properties.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.tdsPerTextEdit.Size = new System.Drawing.Size(107, 24);
            this.tdsPerTextEdit.TabIndex = 1;
            // 
            // tdsAmtTextEdit
            // 
            this.tdsAmtTextEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tdsAmtTextEdit.EnterMoveNextControl = true;
            this.tdsAmtTextEdit.Location = new System.Drawing.Point(293, 56);
            this.tdsAmtTextEdit.Name = "tdsAmtTextEdit";
            this.tdsAmtTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tdsAmtTextEdit.Properties.Appearance.Options.UseFont = true;
            this.tdsAmtTextEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tdsAmtTextEdit.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.tdsAmtTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tdsAmtTextEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.tdsAmtTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.tdsAmtTextEdit.Properties.MaxLength = 25;
            this.tdsAmtTextEdit.Size = new System.Drawing.Size(102, 24);
            this.tdsAmtTextEdit.TabIndex = 2;
            // 
            // grossSpinEdit
            // 
            this.grossSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.grossSpinEdit.EnterMoveNextControl = true;
            this.grossSpinEdit.Location = new System.Drawing.Point(110, 11);
            this.grossSpinEdit.Name = "grossSpinEdit";
            this.grossSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grossSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.grossSpinEdit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.grossSpinEdit.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.grossSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grossSpinEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.grossSpinEdit.Properties.Mask.EditMask = "N2";
            this.grossSpinEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.grossSpinEdit.Properties.MaxLength = 25;
            this.grossSpinEdit.Size = new System.Drawing.Size(118, 24);
            this.grossSpinEdit.TabIndex = 0;
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 131);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(407, 37);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseBackColor = true;
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(313, 3);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(91, 31);
            this.cancelSimpleButton.TabIndex = 1;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // okSimpleButton
            // 
            this.okSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okSimpleButton.Appearance.Options.UseFont = true;
            this.okSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("okSimpleButton.ImageOptions.SvgImage")));
            this.okSimpleButton.Location = new System.Drawing.Point(221, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok";
            // 
            // autoLabel1
            // 
            this.autoLabel1.DX = -112;
            this.autoLabel1.DY = 3;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.LabeledControl = this.grossSpinEdit;
            this.autoLabel1.Location = new System.Drawing.Point(-2, 14);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(108, 17);
            this.autoLabel1.TabIndex = 23;
            this.autoLabel1.Text = "Accessible Value:";
            // 
            // autoLabel2
            // 
            this.autoLabel2.DX = -50;
            this.autoLabel2.DY = 3;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.LabeledControl = this.tdsPerTextEdit;
            this.autoLabel2.Location = new System.Drawing.Point(60, 59);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(46, 17);
            this.autoLabel2.TabIndex = 24;
            this.autoLabel2.Text = "Tds %:";
            // 
            // autoLabel3
            // 
            this.autoLabel3.DX = -65;
            this.autoLabel3.DY = 3;
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.LabeledControl = this.tdsAmtTextEdit;
            this.autoLabel3.Location = new System.Drawing.Point(228, 59);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(61, 17);
            this.autoLabel3.TabIndex = 25;
            this.autoLabel3.Text = "Amount:";
            // 
            // autoLabel4
            // 
            this.autoLabel4.DX = -59;
            this.autoLabel4.DY = 3;
            this.autoLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.LabeledControl = this.tdsAccLookup;
            this.autoLabel4.Location = new System.Drawing.Point(51, 96);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(55, 17);
            this.autoLabel4.TabIndex = 26;
            this.autoLabel4.Text = "Tds A/c:";
            // 
            // tdsAccLookup
            // 
            this.tdsAccLookup.AgentLookup = null;
            this.tdsAccLookup.FillParty = false;
            this.tdsAccLookup.GroupId = 0;
            this.tdsAccLookup.Location = new System.Drawing.Point(110, 93);
            this.tdsAccLookup.LookupDto = null;
            this.tdsAccLookup.Name = "tdsAccLookup";
            this.tdsAccLookup.Nature = "";
            this.tdsAccLookup.NewGroupId = 0;
            this.tdsAccLookup.PrimaryKey = null;
            this.tdsAccLookup.RequiredField = false;
            this.tdsAccLookup.SelectedText = null;
            this.tdsAccLookup.SelectedValue = null;
            this.tdsAccLookup.Size = new System.Drawing.Size(285, 24);
            this.tdsAccLookup.TabIndex = 3;
            this.tdsAccLookup.TaxType = "TDS";
            this.tdsAccLookup.TransportLookup = null;
            this.tdsAccLookup.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // TdsPayView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.OliveDrab;
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(407, 168);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.grossSpinEdit);
            this.Controls.Add(this.tdsAmtTextEdit);
            this.Controls.Add(this.tdsPerTextEdit);
            this.Controls.Add(this.tdsAccLookup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MetroColor = System.Drawing.Color.OliveDrab;
            this.Name = "TdsPayView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tds Entry";
            ((System.ComponentModel.ISupportInitialize)(this.tdsPerTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdsAmtTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grossSpinEdit.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Masters.Acc.AccLookup tdsAccLookup;
        private DevExpress.XtraEditors.SpinEdit tdsPerTextEdit;
        private DevExpress.XtraEditors.SpinEdit tdsAmtTextEdit;
        private DevExpress.XtraEditors.SpinEdit grossSpinEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
    }
}