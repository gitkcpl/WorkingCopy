
namespace Konto.Reporting.Wvs
{
    partial class UpdateJobRateView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateJobRateView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.fromDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.toDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.rateSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.empLookup1 = new Konto.Shared.Masters.Emp.EmpLookup();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.MachineNolookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.productLookup1 = new Konto.Shared.Masters.Item.ProductLookup();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MachineNolookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cancelSimpleButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.okSimpleButton, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 203);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(404, 37);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(310, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(218, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok [F3]";
            this.okSimpleButton.Click += new System.EventHandler(this.okSimpleButton_Click);
            // 
            // fromDateEdit
            // 
            this.fromDateEdit.EditValue = null;
            this.fromDateEdit.EnterMoveNextControl = true;
            this.fromDateEdit.Location = new System.Drawing.Point(94, 12);
            this.fromDateEdit.Name = "fromDateEdit";
            this.fromDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDateEdit.Properties.Appearance.Options.UseFont = true;
            this.fromDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fromDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fromDateEdit.Properties.DisplayFormat.FormatString = "";
            this.fromDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fromDateEdit.Properties.EditFormat.FormatString = "";
            this.fromDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fromDateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.fromDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.fromDateEdit.Size = new System.Drawing.Size(107, 24);
            this.fromDateEdit.TabIndex = 0;
            // 
            // autoLabel1
            // 
            this.autoLabel1.DX = -90;
            this.autoLabel1.DY = 3;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.LabeledControl = this.fromDateEdit;
            this.autoLabel1.Location = new System.Drawing.Point(4, 15);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(86, 17);
            this.autoLabel1.TabIndex = 12;
            this.autoLabel1.Text = "From Period:";
            // 
            // toDateEdit
            // 
            this.toDateEdit.EditValue = null;
            this.toDateEdit.EnterMoveNextControl = true;
            this.toDateEdit.Location = new System.Drawing.Point(249, 12);
            this.toDateEdit.Name = "toDateEdit";
            this.toDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateEdit.Properties.Appearance.Options.UseFont = true;
            this.toDateEdit.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateEdit.Properties.AppearanceDropDown.Options.UseFont = true;
            this.toDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.toDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.toDateEdit.Properties.DisplayFormat.FormatString = "";
            this.toDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.toDateEdit.Properties.EditFormat.FormatString = "";
            this.toDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.toDateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.toDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.toDateEdit.Size = new System.Drawing.Size(112, 24);
            this.toDateEdit.TabIndex = 1;
            // 
            // autoLabel2
            // 
            this.autoLabel2.DX = -29;
            this.autoLabel2.DY = 3;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.LabeledControl = this.toDateEdit;
            this.autoLabel2.Location = new System.Drawing.Point(220, 15);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(25, 17);
            this.autoLabel2.TabIndex = 14;
            this.autoLabel2.Text = "To:";
            // 
            // rateSpinEdit
            // 
            this.rateSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.rateSpinEdit.Location = new System.Drawing.Point(94, 53);
            this.rateSpinEdit.Name = "rateSpinEdit";
            this.rateSpinEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rateSpinEdit.Properties.Appearance.Options.UseFont = true;
            this.rateSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rateSpinEdit.Size = new System.Drawing.Size(100, 24);
            this.rateSpinEdit.TabIndex = 2;
            // 
            // autoLabel3
            // 
            this.autoLabel3.DX = -67;
            this.autoLabel3.DY = 3;
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.LabeledControl = this.rateSpinEdit;
            this.autoLabel3.Location = new System.Drawing.Point(27, 56);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(63, 17);
            this.autoLabel3.TabIndex = 16;
            this.autoLabel3.Text = "Job Rate:";
            // 
            // empLookup1
            // 
            this.empLookup1.Location = new System.Drawing.Point(94, 95);
            this.empLookup1.LookupTitle = null;
            this.empLookup1.Name = "empLookup1";
            this.empLookup1.PrimaryKey = null;
            this.empLookup1.RequiredField = false;
            this.empLookup1.SelectedText = null;
            this.empLookup1.SelectedValue = null;
            this.empLookup1.Size = new System.Drawing.Size(276, 24);
            this.empLookup1.TabIndex = 3;
            // 
            // autoLabel4
            // 
            this.autoLabel4.DX = -74;
            this.autoLabel4.DY = 3;
            this.autoLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel4.LabeledControl = this.empLookup1;
            this.autoLabel4.Location = new System.Drawing.Point(20, 98);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(70, 17);
            this.autoLabel4.TabIndex = 18;
            this.autoLabel4.Text = "Employee:";
            // 
            // autoLabel5
            // 
            this.autoLabel5.DX = -58;
            this.autoLabel5.DY = 3;
            this.autoLabel5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel5.LabeledControl = this.productLookup1;
            this.autoLabel5.Location = new System.Drawing.Point(39, 135);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(54, 17);
            this.autoLabel5.TabIndex = 20;
            this.autoLabel5.Text = "Quality:";
            // 
            // MachineNolookUpEdit
            // 
            this.MachineNolookUpEdit.EnterMoveNextControl = true;
            this.MachineNolookUpEdit.Location = new System.Drawing.Point(94, 165);
            this.MachineNolookUpEdit.Name = "MachineNolookUpEdit";
            this.MachineNolookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.MachineNolookUpEdit.Properties.Appearance.Options.UseFont = true;
            this.MachineNolookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MachineNolookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Machine")});
            this.MachineNolookUpEdit.Properties.DisplayMember = "DisplayText";
            this.MachineNolookUpEdit.Properties.NullText = "";
            this.MachineNolookUpEdit.Properties.ValueMember = "Id";
            this.MachineNolookUpEdit.Size = new System.Drawing.Size(276, 24);
            this.MachineNolookUpEdit.TabIndex = 5;
            // 
            // autoLabel6
            // 
            this.autoLabel6.DX = -88;
            this.autoLabel6.DY = 3;
            this.autoLabel6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel6.LabeledControl = this.MachineNolookUpEdit;
            this.autoLabel6.Location = new System.Drawing.Point(6, 168);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(84, 17);
            this.autoLabel6.TabIndex = 22;
            this.autoLabel6.Text = "Machine No:";
            // 
            // productLookup1
            // 
            this.productLookup1.GroupDto = null;
            this.productLookup1.Location = new System.Drawing.Point(97, 132);
            this.productLookup1.LookupTitle = null;
            this.productLookup1.Name = "productLookup1";
            this.productLookup1.PrimaryKey = null;
            this.productLookup1.PTypeId = Konto.App.Shared.ProductTypeEnum.NA;
            this.productLookup1.RequiredField = false;
            this.productLookup1.SelectedText = null;
            this.productLookup1.SelectedValue = null;
            this.productLookup1.Size = new System.Drawing.Size(273, 24);
            this.productLookup1.TabIndex = 23;
            this.productLookup1.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // UpdateJobRateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.CadetBlue;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(404, 240);
            this.Controls.Add(this.productLookup1);
            this.Controls.Add(this.autoLabel6);
            this.Controls.Add(this.MachineNolookUpEdit);
            this.Controls.Add(this.autoLabel5);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.empLookup1);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.rateSpinEdit);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.toDateEdit);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.fromDateEdit);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MetroColor = System.Drawing.Color.CadetBlue;
            this.Name = "UpdateJobRateView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Job Rate";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MachineNolookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private DevExpress.XtraEditors.DateEdit fromDateEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private DevExpress.XtraEditors.DateEdit toDateEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private DevExpress.XtraEditors.SpinEdit rateSpinEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Shared.Masters.Emp.EmpLookup empLookup1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private DevExpress.XtraEditors.LookUpEdit MachineNolookUpEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private Shared.Masters.Item.ProductLookup productLookup1;
    }
}