namespace Konto.Core.Shared.Libs
{
    partial class ListDateRange
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FdateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.TdateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.getSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.FdateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FdateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TdateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TdateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // FdateEdit1
            // 
            this.FdateEdit1.EditValue = new System.DateTime(2020, 3, 14, 17, 37, 52, 761);
            this.FdateEdit1.EnterMoveNextControl = true;
            this.FdateEdit1.Location = new System.Drawing.Point(0, 1);
            this.FdateEdit1.Name = "FdateEdit1";
            this.FdateEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.FdateEdit1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FdateEdit1.Properties.Appearance.Options.UseFont = true;
            this.FdateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.FdateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.FdateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.FdateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.FdateEdit1.Size = new System.Drawing.Size(94, 24);
            this.FdateEdit1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.label1.Location = new System.Drawing.Point(98, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "To";
            // 
            // TdateEdit2
            // 
            this.TdateEdit2.EditValue = new System.DateTime(2020, 3, 14, 17, 37, 52, 761);
            this.TdateEdit2.EnterMoveNextControl = true;
            this.TdateEdit2.Location = new System.Drawing.Point(121, 1);
            this.TdateEdit2.Name = "TdateEdit2";
            this.TdateEdit2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.TdateEdit2.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TdateEdit2.Properties.Appearance.Options.UseFont = true;
            this.TdateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TdateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TdateEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.TdateEdit2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.TdateEdit2.Size = new System.Drawing.Size(94, 24);
            this.TdateEdit2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.label2.Location = new System.Drawing.Point(218, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "View Type:";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.EnterMoveNextControl = true;
            this.lookUpEdit1.Location = new System.Drawing.Point(296, 1);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit1.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEdit1.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descr", "Name1")});
            this.lookUpEdit1.Properties.DisplayMember = "Descr";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ShowHeader = false;
            this.lookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEdit1.Properties.ValueMember = "Id";
            this.lookUpEdit1.Size = new System.Drawing.Size(159, 24);
            this.lookUpEdit1.TabIndex = 2;
            // 
            // getSimpleButton
            // 
            this.getSimpleButton.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.getSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getSimpleButton.Appearance.Options.UseBackColor = true;
            this.getSimpleButton.Appearance.Options.UseFont = true;
            this.getSimpleButton.Location = new System.Drawing.Point(457, 1);
            this.getSimpleButton.Name = "getSimpleButton";
            this.getSimpleButton.Size = new System.Drawing.Size(49, 23);
            this.getSimpleButton.TabIndex = 3;
            this.getSimpleButton.Text = "Get";
            this.getSimpleButton.Click += new System.EventHandler(this.getSimpleButton_Click);
            // 
            // ListDateRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.getSimpleButton);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TdateEdit2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FdateEdit1);
            this.Name = "ListDateRange";
            this.Size = new System.Drawing.Size(514, 25);
            ((System.ComponentModel.ISupportInitialize)(this.FdateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FdateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TdateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TdateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit FdateEdit1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit TdateEdit2;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.SimpleButton getSimpleButton;
    }
}
