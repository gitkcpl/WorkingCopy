namespace Konto.Trading.Cutting
{
    partial class PendingCuttingWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingCuttingWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancelSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.okSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.colLotNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProductId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colProduct = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTransId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colChallanDate = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTakaNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGrayMeter = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colFinMeter = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colTakaId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDesignId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colDesignNo = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colColorName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGradeId = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.colGradeName = new Konto.Core.Shared.Libs.CustomGridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 392);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(885, 37);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // cancelSimpleButton
            // 
            this.cancelSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSimpleButton.Appearance.Options.UseFont = true;
            this.cancelSimpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelSimpleButton.ImageOptions.SvgImage")));
            this.cancelSimpleButton.Location = new System.Drawing.Point(791, 3);
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
            this.okSimpleButton.Location = new System.Drawing.Point(699, 3);
            this.okSimpleButton.Name = "okSimpleButton";
            this.okSimpleButton.Size = new System.Drawing.Size(86, 31);
            this.okSimpleButton.TabIndex = 0;
            this.okSimpleButton.Text = "Ok";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(885, 392);
            this.gridControl1.TabIndex = 12;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Konto.Data.Models.Transaction.TradingDto.PendingForCuttingDto);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(22)))), ((int)(((byte)(91)))));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLotNo,
            this.colChallanNo,
            this.colProductId,
            this.colProduct,
            this.colId,
            this.colTransId,
            this.colChallanDate,
            this.colTakaNo,
            this.colGrayMeter,
            this.colFinMeter,
            this.colTakaId,
            this.colColorId,
            this.colDesignId,
            this.colDesignNo,
            this.colColorName,
            this.colGradeId,
            this.colGradeName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.gridView1.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.gridView1.OptionsLayout.Columns.StoreAllOptions = true;
            this.gridView1.OptionsLayout.Columns.StoreAppearance = true;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAppearance = true;
            this.gridView1.OptionsNavigation.UseTabKey = false;
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colLotNo
            // 
            this.colLotNo.FieldName = "LotNo";
            this.colLotNo.Name = "colLotNo";
            this.colLotNo.Visible = true;
            this.colLotNo.VisibleIndex = 4;
            this.colLotNo.Width = 76;
            // 
            // colChallanNo
            // 
            this.colChallanNo.FieldName = "ChallanNo";
            this.colChallanNo.Name = "colChallanNo";
            this.colChallanNo.Visible = true;
            this.colChallanNo.VisibleIndex = 5;
            this.colChallanNo.Width = 77;
            // 
            // colProductId
            // 
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            // 
            // colProduct
            // 
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.Visible = true;
            this.colProduct.VisibleIndex = 7;
            this.colProduct.Width = 161;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colTransId
            // 
            this.colTransId.FieldName = "TransId";
            this.colTransId.Name = "colTransId";
            // 
            // colChallanDate
            // 
            this.colChallanDate.FieldName = "ChallanDate";
            this.colChallanDate.Name = "colChallanDate";
            this.colChallanDate.Visible = true;
            this.colChallanDate.VisibleIndex = 6;
            this.colChallanDate.Width = 105;
            // 
            // colTakaNo
            // 
            this.colTakaNo.FieldName = "TakaNo";
            this.colTakaNo.Name = "colTakaNo";
            this.colTakaNo.Visible = true;
            this.colTakaNo.VisibleIndex = 3;
            this.colTakaNo.Width = 89;
            // 
            // colGrayMeter
            // 
            this.colGrayMeter.Caption = "Gray Mtrs";
            this.colGrayMeter.FieldName = "GrayMeter";
            this.colGrayMeter.Name = "colGrayMeter";
            this.colGrayMeter.Visible = true;
            this.colGrayMeter.VisibleIndex = 2;
            this.colGrayMeter.Width = 85;
            // 
            // colFinMeter
            // 
            this.colFinMeter.Caption = "Finish Mtrs";
            this.colFinMeter.FieldName = "FinMeter";
            this.colFinMeter.Name = "colFinMeter";
            this.colFinMeter.Visible = true;
            this.colFinMeter.VisibleIndex = 1;
            this.colFinMeter.Width = 85;
            // 
            // colTakaId
            // 
            this.colTakaId.FieldName = "TakaId";
            this.colTakaId.Name = "colTakaId";
            // 
            // colColorId
            // 
            this.colColorId.FieldName = "ColorId";
            this.colColorId.Name = "colColorId";
            // 
            // colDesignId
            // 
            this.colDesignId.FieldName = "DesignId";
            this.colDesignId.Name = "colDesignId";
            // 
            // colDesignNo
            // 
            this.colDesignNo.FieldName = "DesignNo";
            this.colDesignNo.Name = "colDesignNo";
            this.colDesignNo.Visible = true;
            this.colDesignNo.VisibleIndex = 8;
            this.colDesignNo.Width = 96;
            // 
            // colColorName
            // 
            this.colColorName.FieldName = "ColorName";
            this.colColorName.Name = "colColorName";
            this.colColorName.Visible = true;
            this.colColorName.VisibleIndex = 9;
            this.colColorName.Width = 114;
            // 
            // colGradeId
            // 
            this.colGradeId.FieldName = "GradeId";
            this.colGradeId.Name = "colGradeId";
            // 
            // colGradeName
            // 
            this.colGradeName.FieldName = "GradeName";
            this.colGradeName.Name = "colGradeName";
            this.colGradeName.Visible = true;
            this.colGradeName.VisibleIndex = 10;
            this.colGradeName.Width = 108;
            // 
            // PendingCuttingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSimpleButton;
            this.ClientSize = new System.Drawing.Size(885, 429);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PendingCuttingWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending Taka For Cutting";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraEditors.SimpleButton cancelSimpleButton;
        public DevExpress.XtraEditors.SimpleButton okSimpleButton;
        public Core.Shared.Libs.CustomGridControl gridControl1;
        public Core.Shared.Libs.CustomGridView gridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Core.Shared.Libs.CustomGridColumn colLotNo;
        private Core.Shared.Libs.CustomGridColumn colChallanNo;
        private Core.Shared.Libs.CustomGridColumn colProductId;
        private Core.Shared.Libs.CustomGridColumn colProduct;
        private Core.Shared.Libs.CustomGridColumn colId;
        private Core.Shared.Libs.CustomGridColumn colTransId;
        private Core.Shared.Libs.CustomGridColumn colChallanDate;
        private Core.Shared.Libs.CustomGridColumn colTakaNo;
        private Core.Shared.Libs.CustomGridColumn colGrayMeter;
        private Core.Shared.Libs.CustomGridColumn colFinMeter;
        private Core.Shared.Libs.CustomGridColumn colTakaId;
        private Core.Shared.Libs.CustomGridColumn colColorId;
        private Core.Shared.Libs.CustomGridColumn colDesignId;
        private Core.Shared.Libs.CustomGridColumn colDesignNo;
        private Core.Shared.Libs.CustomGridColumn colColorName;
        private Core.Shared.Libs.CustomGridColumn colGradeId;
        private Core.Shared.Libs.CustomGridColumn colGradeName;
    }
}