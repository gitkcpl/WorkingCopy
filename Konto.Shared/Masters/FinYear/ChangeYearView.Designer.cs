
namespace Konto.Shared.Masters.FinYear
{
    partial class ChangeYearView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeYearView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.fromDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.toDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 145);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(331, 37);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(237, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(145, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok";
            // 
            // fromDateEdit
            // 
            this.fromDateEdit.EditValue = null;
            this.fromDateEdit.EnterMoveNextControl = true;
            this.fromDateEdit.Location = new System.Drawing.Point(119, 25);
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
            this.fromDateEdit.Properties.ReadOnly = true;
            this.fromDateEdit.Size = new System.Drawing.Size(134, 24);
            this.fromDateEdit.TabIndex = 9;
            // 
            // autoLabel1
            // 
            this.autoLabel1.DX = -79;
            this.autoLabel1.DY = 3;
            this.autoLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.LabeledControl = this.fromDateEdit;
            this.autoLabel1.Location = new System.Drawing.Point(40, 28);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(75, 17);
            this.autoLabel1.TabIndex = 10;
            this.autoLabel1.Text = "From Date:";
            // 
            // autoLabel2
            // 
            this.autoLabel2.DX = -61;
            this.autoLabel2.DY = 3;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.LabeledControl = this.toDateEdit;
            this.autoLabel2.Location = new System.Drawing.Point(58, 74);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(57, 17);
            this.autoLabel2.TabIndex = 12;
            this.autoLabel2.Text = "To Date:";
            // 
            // toDateEdit
            // 
            this.toDateEdit.EditValue = null;
            this.toDateEdit.EnterMoveNextControl = true;
            this.toDateEdit.Location = new System.Drawing.Point(119, 71);
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
            this.toDateEdit.Properties.ReadOnly = true;
            this.toDateEdit.Size = new System.Drawing.Size(134, 24);
            this.toDateEdit.TabIndex = 11;
            // 
            // ChangeYearView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(331, 182);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.toDateEdit);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.fromDateEdit);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChangeYearView";
            this.Text = "New Financial Year";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private DevExpress.XtraEditors.DateEdit fromDateEdit;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private DevExpress.XtraEditors.DateEdit toDateEdit;
    }
}