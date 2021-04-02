
namespace Konto.Shared.Trans.Common
{
    partial class EinvView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EinvView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.transCatLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.eInvBillkontoTextEdit = new DevExpress.XtraEditors.MemoEdit();
            this.errorMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transCatLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eInvBillkontoTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorMemoEdit.Properties)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 240);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(397, 37);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(303, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(211, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(181, 2);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Size = new System.Drawing.Size(213, 232);
            this.pictureEdit1.TabIndex = 10;
            // 
            // autoLabel2
            // 
            this.autoLabel2.DY = -21;
            this.autoLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.LabeledControl = this.transCatLookUpEdit;
            this.autoLabel2.Location = new System.Drawing.Point(12, 4);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Position = Syncfusion.Windows.Forms.Tools.AutoLabelPosition.Top;
            this.autoLabel2.Size = new System.Drawing.Size(143, 17);
            this.autoLabel2.TabIndex = 14;
            this.autoLabel2.Text = "*Transaction Category";
            // 
            // transCatLookUpEdit
            // 
            this.transCatLookUpEdit.EnterMoveNextControl = true;
            this.transCatLookUpEdit.Location = new System.Drawing.Point(12, 25);
            this.transCatLookUpEdit.Name = "transCatLookUpEdit";
            this.transCatLookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transCatLookUpEdit.Properties.Appearance.Options.UseFont = true;
            this.transCatLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.transCatLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("_Key", "Name1")});
            this.transCatLookUpEdit.Properties.DisplayMember = "_Key";
            this.transCatLookUpEdit.Properties.ImmediatePopup = true;
            this.transCatLookUpEdit.Properties.NullText = "";
            this.transCatLookUpEdit.Properties.ShowFooter = false;
            this.transCatLookUpEdit.Properties.ShowHeader = false;
            this.transCatLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.transCatLookUpEdit.Properties.ValueMember = "_Value";
            this.transCatLookUpEdit.Size = new System.Drawing.Size(163, 24);
            this.transCatLookUpEdit.TabIndex = 13;
            // 
            // eInvBillkontoTextEdit
            // 
            this.eInvBillkontoTextEdit.EditValue = "";
            this.eInvBillkontoTextEdit.Location = new System.Drawing.Point(12, 55);
            this.eInvBillkontoTextEdit.Name = "eInvBillkontoTextEdit";
            this.eInvBillkontoTextEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eInvBillkontoTextEdit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.eInvBillkontoTextEdit.Properties.Appearance.Options.UseFont = true;
            this.eInvBillkontoTextEdit.Properties.Appearance.Options.UseForeColor = true;
            this.eInvBillkontoTextEdit.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eInvBillkontoTextEdit.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.eInvBillkontoTextEdit.Properties.ReadOnly = true;
            this.eInvBillkontoTextEdit.Size = new System.Drawing.Size(163, 88);
            this.eInvBillkontoTextEdit.TabIndex = 15;
            // 
            // errorMemoEdit
            // 
            this.errorMemoEdit.EditValue = "";
            this.errorMemoEdit.Location = new System.Drawing.Point(12, 149);
            this.errorMemoEdit.Name = "errorMemoEdit";
            this.errorMemoEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorMemoEdit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.errorMemoEdit.Properties.Appearance.Options.UseFont = true;
            this.errorMemoEdit.Properties.Appearance.Options.UseForeColor = true;
            this.errorMemoEdit.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorMemoEdit.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.errorMemoEdit.Properties.ReadOnly = true;
            this.errorMemoEdit.Size = new System.Drawing.Size(163, 85);
            this.errorMemoEdit.TabIndex = 16;
            // 
            // EinvView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 277);
            this.Controls.Add(this.errorMemoEdit);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.transCatLookUpEdit);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.eInvBillkontoTextEdit);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "EinvView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "E-Invoice";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transCatLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eInvBillkontoTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorMemoEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private DevExpress.XtraEditors.LookUpEdit transCatLookUpEdit;
        private DevExpress.XtraEditors.MemoEdit eInvBillkontoTextEdit;
        private DevExpress.XtraEditors.MemoEdit errorMemoEdit;
    }
}