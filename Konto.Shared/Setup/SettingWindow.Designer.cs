namespace Konto.Shared.Setup
{
    partial class SettingWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.paraDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colParaId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCompId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDescr = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDefaultValue = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colValueDescr = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colCategory = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colParaValue = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colRemark = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paraDtoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 339);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(656, 37);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(562, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(470, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 5;
            this.okSimpleButton.Text = "Ok [F3]";
            // 
            // customGridControl1
            // 
            this.customGridControl1.DataSource = this.paraDtoBindingSource;
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 0);
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(656, 339);
            this.customGridControl1.TabIndex = 5;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // paraDtoBindingSource
            // 
            this.paraDtoBindingSource.DataSource = typeof(Konto.Data.Models.Admin.Dtos.ParaDto);
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.Row.Options.UseFont = true;
            this.customGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colParaId,
            this.colId,
            this.colCompId,
            this.colDescr,
            this.colDefaultValue,
            this.colValueDescr,
            this.colCategory,
            this.colParaValue,
            this.colRemark});
            this.customGridView1.GridControl = this.customGridControl1;
            this.customGridView1.Name = "customGridView1";
            this.customGridView1.OptionsCustomization.AllowColumnMoving = false;
            this.customGridView1.OptionsCustomization.AllowGroup = false;
            this.customGridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.customGridView1.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.customGridView1.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.customGridView1.OptionsView.ColumnAutoWidth = false;
            this.customGridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // colParaId
            // 
            this.colParaId.FieldName = "ParaId";
            this.colParaId.Name = "colParaId";
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colCompId
            // 
            this.colCompId.FieldName = "CompId";
            this.colCompId.Name = "colCompId";
            // 
            // colDescr
            // 
            this.colDescr.FieldName = "Descr";
            this.colDescr.Name = "colDescr";
            this.colDescr.OptionsColumn.AllowEdit = false;
            this.colDescr.Visible = true;
            this.colDescr.VisibleIndex = 0;
            this.colDescr.Width = 197;
            // 
            // colDefaultValue
            // 
            this.colDefaultValue.FieldName = "DefaultValue";
            this.colDefaultValue.Name = "colDefaultValue";
            // 
            // colValueDescr
            // 
            this.colValueDescr.FieldName = "ValueDescr";
            this.colValueDescr.Name = "colValueDescr";
            this.colValueDescr.OptionsColumn.AllowEdit = false;
            this.colValueDescr.Visible = true;
            this.colValueDescr.VisibleIndex = 2;
            this.colValueDescr.Width = 318;
            // 
            // colCategory
            // 
            this.colCategory.FieldName = "Category";
            this.colCategory.Name = "colCategory";
            // 
            // colParaValue
            // 
            this.colParaValue.FieldName = "ParaValue";
            this.colParaValue.Name = "colParaValue";
            this.colParaValue.Visible = true;
            this.colParaValue.VisibleIndex = 1;
            this.colParaValue.Width = 138;
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            // 
            // SettingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionFont = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(656, 376);
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "SettingWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paraDtoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private System.Windows.Forms.BindingSource paraDtoBindingSource;
        private Core.Shared.Libs.CustomGridColumn colParaId;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colCompId;
        private Core.Shared.Libs.CustomGridColumn colDescr;
        private Core.Shared.Libs.CustomGridColumn colDefaultValue;
        private Core.Shared.Libs.CustomGridColumn colValueDescr;
        private Core.Shared.Libs.CustomGridColumn colCategory;
        private Core.Shared.Libs.CustomGridColumn colParaValue;
        private Core.Shared.Libs.CustomGridColumn colRemark;
    }
}