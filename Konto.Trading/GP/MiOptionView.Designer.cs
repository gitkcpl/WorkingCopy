namespace Konto.Trading.GP
{
    partial class MiOptionView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiOptionView));
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.accLookup1 = new Konto.Shared.Masters.Acc.AccLookup();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.challanCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.challanCheckEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "A";
            this.radioGroup1.Location = new System.Drawing.Point(12, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("A", "Ascending"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("D", "Desending"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("N", "No Order")});
            this.radioGroup1.Size = new System.Drawing.Size(378, 50);
            this.radioGroup1.TabIndex = 0;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.Location = new System.Drawing.Point(24, 83);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(62, 17);
            this.autoLabel1.TabIndex = 2;
            this.autoLabel1.Text = "Taka/Lot:";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(218, 81);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.checkEdit1.Properties.Appearance.Options.UseFont = true;
            this.checkEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.checkEdit1.Properties.Caption = "Divide In Equal Meters";
            this.checkEdit1.Size = new System.Drawing.Size(170, 21);
            this.checkEdit1.TabIndex = 2;
            // 
            // accLookup1
            // 
            this.accLookup1.AgentLookup = null;
            this.accLookup1.FillParty = false;
            this.accLookup1.GroupId = 0;
            this.accLookup1.Location = new System.Drawing.Point(90, 122);
            this.accLookup1.LookupDto = null;
            this.accLookup1.Name = "accLookup1";
            this.accLookup1.Nature = null;
            this.accLookup1.NewGroupId = 0;
            this.accLookup1.PrimaryKey = null;
            this.accLookup1.RequiredField = true;
            this.accLookup1.SelectedText = null;
            this.accLookup1.SelectedValue = null;
            this.accLookup1.Size = new System.Drawing.Size(298, 24);
            this.accLookup1.TabIndex = 3;
            this.accLookup1.TaxType = null;
            this.accLookup1.TransportLookup = null;
            this.accLookup1.VoucherType = Konto.App.Shared.VoucherTypeEnum.MillIssue;
            // 
            // autoLabel2
            // 
            this.autoLabel2.DX = -73;
            this.autoLabel2.DY = 3;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.LabeledControl = this.accLookup1;
            this.autoLabel2.Location = new System.Drawing.Point(17, 125);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(69, 17);
            this.autoLabel2.TabIndex = 5;
            this.autoLabel2.Text = "Mill/Party:";
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 184);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 37);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(306, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(214, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok [F3]";
            this.okSimpleButton.Click += new System.EventHandler(this.okSimpleButton_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.textEdit1.Location = new System.Drawing.Point(90, 80);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textEdit1.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.textEdit1.Properties.Mask.EditMask = "N";
            this.textEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEdit1.Size = new System.Drawing.Size(123, 24);
            this.textEdit1.TabIndex = 1;
            // 
            // challanCheckEdit
            // 
            this.challanCheckEdit.Location = new System.Drawing.Point(90, 157);
            this.challanCheckEdit.Name = "challanCheckEdit";
            this.challanCheckEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.challanCheckEdit.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.challanCheckEdit.Properties.Appearance.Options.UseFont = true;
            this.challanCheckEdit.Properties.Appearance.Options.UseForeColor = true;
            this.challanCheckEdit.Properties.Caption = "Seperate Challan For Each Lot";
            this.challanCheckEdit.Size = new System.Drawing.Size(243, 21);
            this.challanCheckEdit.TabIndex = 8;
            // 
            // MiOptionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.MediumVioletRed;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 221);
            this.Controls.Add(this.challanCheckEdit);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.accLookup1);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.textEdit1);
            this.MetroColor = System.Drawing.Color.MediumVioletRed;
            this.Name = "MiOptionView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mill Issue Option";
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.challanCheckEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private Shared.Masters.Acc.AccLookup accLookup1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private DevExpress.XtraEditors.SpinEdit textEdit1;
        private DevExpress.XtraEditors.CheckEdit challanCheckEdit;
    }
}