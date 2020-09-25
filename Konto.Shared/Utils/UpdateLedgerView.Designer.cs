namespace Konto.Shared.Utils
{
    partial class UpdateLedgerView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateLedgerView));
            this.fromDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.toDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.typeLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.SaveSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Konto.Shared.Masters.Acc.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeLookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // fromDateEdit
            // 
            this.fromDateEdit.EditValue = null;
            this.fromDateEdit.EnterMoveNextControl = true;
            this.fromDateEdit.Location = new System.Drawing.Point(97, 12);
            this.fromDateEdit.Name = "fromDateEdit";
            this.fromDateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.fromDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDateEdit.Properties.Appearance.Options.UseFont = true;
            this.fromDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fromDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fromDateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.fromDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.fromDateEdit.Size = new System.Drawing.Size(124, 24);
            this.fromDateEdit.TabIndex = 0;
            // 
            // autoLabel1
            // 
            this.autoLabel1.DX = -87;
            this.autoLabel1.DY = 3;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.LabeledControl = this.fromDateEdit;
            this.autoLabel1.Location = new System.Drawing.Point(10, 15);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(83, 17);
            this.autoLabel1.TabIndex = 6;
            this.autoLabel1.Text = "From Period";
            // 
            // autoLabel2
            // 
            this.autoLabel2.DX = -26;
            this.autoLabel2.DY = 3;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.LabeledControl = this.toDateEdit;
            this.autoLabel2.Location = new System.Drawing.Point(291, 15);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(22, 17);
            this.autoLabel2.TabIndex = 8;
            this.autoLabel2.Text = "To";
            // 
            // toDateEdit
            // 
            this.toDateEdit.EditValue = null;
            this.toDateEdit.EnterMoveNextControl = true;
            this.toDateEdit.Location = new System.Drawing.Point(317, 12);
            this.toDateEdit.Name = "toDateEdit";
            this.toDateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.toDateEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateEdit.Properties.Appearance.Options.UseFont = true;
            this.toDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.toDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.toDateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.toDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.toDateEdit.Size = new System.Drawing.Size(124, 24);
            this.toDateEdit.TabIndex = 1;
            // 
            // typeLookUpEdit
            // 
            this.typeLookUpEdit.EnterMoveNextControl = true;
            this.typeLookUpEdit.Location = new System.Drawing.Point(97, 58);
            this.typeLookUpEdit.Name = "typeLookUpEdit";
            this.typeLookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLookUpEdit.Properties.Appearance.Options.UseFont = true;
            this.typeLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.typeLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayText", "Name1")});
            this.typeLookUpEdit.Properties.DisplayMember = "DisplayText";
            this.typeLookUpEdit.Properties.ImmediatePopup = true;
            this.typeLookUpEdit.Properties.NullText = "";
            this.typeLookUpEdit.Properties.ShowHeader = false;
            this.typeLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.typeLookUpEdit.Properties.ValueMember = "Id";
            this.typeLookUpEdit.Size = new System.Drawing.Size(344, 24);
            this.typeLookUpEdit.TabIndex = 2;
            // 
            // autoLabel3
            // 
            this.autoLabel3.DX = -93;
            this.autoLabel3.DY = 3;
            this.autoLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.LabeledControl = this.typeLookUpEdit;
            this.autoLabel3.Location = new System.Drawing.Point(4, 61);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(89, 17);
            this.autoLabel3.TabIndex = 10;
            this.autoLabel3.Text = "Voucher Type";
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseBackColor = true;
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(349, 104);
            this.cancelSimpleButton.Name = "cancelSimpleButton";
            this.cancelSimpleButton.Size = new System.Drawing.Size(92, 36);
            this.cancelSimpleButton.TabIndex = 4;
            this.cancelSimpleButton.Text = "Cancel";
            // 
            // SaveSimpleButton
            // 
            this.SaveSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveSimpleButton.Appearance.Options.UseFont = true;
            this.SaveSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("SaveSimpleButton.ImageOptions.SvgImage")));
            this.SaveSimpleButton.Location = new System.Drawing.Point(261, 104);
            this.SaveSimpleButton.Name = "SaveSimpleButton";
            this.SaveSimpleButton.Size = new System.Drawing.Size(82, 36);
            this.SaveSimpleButton.TabIndex = 3;
            this.SaveSimpleButton.Text = "Save";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // UpdateLedgerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(453, 141);
            this.Controls.Add(this.cancelSimpleButton);
            this.Controls.Add(this.SaveSimpleButton);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.typeLookUpEdit);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.toDateEdit);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.fromDateEdit);
            this.Name = "UpdateLedgerView";
            this.Text = "UpdateLedgerView";
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeLookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit fromDateEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private DevExpress.XtraEditors.DateEdit toDateEdit;
        private DevExpress.XtraEditors.LookUpEdit typeLookUpEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        private DevExpress.XtraEditors.SimpleButton SaveSimpleButton;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}