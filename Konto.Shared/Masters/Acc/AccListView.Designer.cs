namespace Konto.Shared.Masters.Acc
{
    partial class AccListView
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
            Syncfusion.Windows.Forms.Tools.MetroSplitButtonRenderer metroSplitButtonRenderer1 = new Syncfusion.Windows.Forms.Tools.MetroSplitButtonRenderer();
            this.splitButton1 = new Syncfusion.Windows.Forms.Tools.SplitButton();
            this.deprToolstripitem = new Syncfusion.Windows.Forms.Tools.toolstripitem();
            this.interestToolstripitem = new Syncfusion.Windows.Forms.Tools.toolstripitem();
            this.opStockToolstripitem = new Syncfusion.Windows.Forms.Tools.toolstripitem();
            this.tcsToolstripitem = new Syncfusion.Windows.Forms.Tools.toolstripitem();
            this.tdsToolstripitem = new Syncfusion.Windows.Forms.Tools.toolstripitem();
            this.customGridControl1 = new Konto.Core.Shared.Libs.CustomGridControl();
            this.customGridView1 = new Konto.Core.Shared.Libs.CustomGridView();
            this.opBalsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.bankSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.addressSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.importToolstripitem = new Syncfusion.Windows.Forms.Tools.toolstripitem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.addressSimpleButton);
            this.panelControl2.Controls.Add(this.bankSimpleButton);
            this.panelControl2.Controls.Add(this.opBalsimpleButton);
            this.panelControl2.Controls.Add(this.splitButton1);
            this.panelControl2.Size = new System.Drawing.Size(849, 35);
            this.panelControl2.Controls.SetChildIndex(this.listAction1, 0);
            this.panelControl2.Controls.SetChildIndex(this.splitButton1, 0);
            this.panelControl2.Controls.SetChildIndex(this.opBalsimpleButton, 0);
            this.panelControl2.Controls.SetChildIndex(this.bankSimpleButton, 0);
            this.panelControl2.Controls.SetChildIndex(this.addressSimpleButton, 0);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(800, 35);
            // 
            // splitButton1
            // 
            this.splitButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.splitButton1.BeforeTouchSize = new System.Drawing.Size(94, 24);
            this.splitButton1.DropDownIconColor = System.Drawing.Color.White;
            this.splitButton1.DropDownItems.Add(this.deprToolstripitem);
            this.splitButton1.DropDownItems.Add(this.interestToolstripitem);
            this.splitButton1.DropDownItems.Add(this.opStockToolstripitem);
            this.splitButton1.DropDownItems.Add(this.tcsToolstripitem);
            this.splitButton1.DropDownItems.Add(this.tdsToolstripitem);
            this.splitButton1.DropDownItems.Add(this.importToolstripitem);
            this.splitButton1.DropDownPosition = Syncfusion.Windows.Forms.Tools.Position.Bottom;
            this.splitButton1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitButton1.ForeColor = System.Drawing.Color.White;
            this.splitButton1.Location = new System.Drawing.Point(677, 5);
            this.splitButton1.MinimumSize = new System.Drawing.Size(75, 23);
            this.splitButton1.Name = "splitButton1";
            metroSplitButtonRenderer1.SplitButton = this.splitButton1;
            this.splitButton1.Renderer = metroSplitButtonRenderer1;
            this.splitButton1.ShowDropDownOnButtonClick = false;
            this.splitButton1.Size = new System.Drawing.Size(94, 24);
            this.splitButton1.Style = Syncfusion.Windows.Forms.Tools.SplitButtonVisualStyle.Metro;
            this.splitButton1.TabIndex = 4;
            this.splitButton1.Text = "Others";
            this.splitButton1.ThemeName = "Metro";
            // 
            // deprToolstripitem
            // 
            this.deprToolstripitem.Name = "deprToolstripitem";
            this.deprToolstripitem.Size = new System.Drawing.Size(23, 23);
            this.deprToolstripitem.Text = "Depreciation";
            // 
            // interestToolstripitem
            // 
            this.interestToolstripitem.Name = "interestToolstripitem";
            this.interestToolstripitem.Size = new System.Drawing.Size(23, 23);
            this.interestToolstripitem.Text = "Interest";
            // 
            // opStockToolstripitem
            // 
            this.opStockToolstripitem.Name = "opStockToolstripitem";
            this.opStockToolstripitem.Size = new System.Drawing.Size(23, 23);
            this.opStockToolstripitem.Text = "OpStock";
            // 
            // tcsToolstripitem
            // 
            this.tcsToolstripitem.Name = "tcsToolstripitem";
            this.tcsToolstripitem.Size = new System.Drawing.Size(23, 23);
            this.tcsToolstripitem.Text = "TCS";
            // 
            // tdsToolstripitem
            // 
            this.tdsToolstripitem.Name = "tdsToolstripitem";
            this.tdsToolstripitem.Size = new System.Drawing.Size(23, 23);
            this.tdsToolstripitem.Text = "TDS";
            // 
            // customGridControl1
            // 
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridControl1.Location = new System.Drawing.Point(0, 35);
            this.customGridControl1.MainView = this.customGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(800, 276);
            this.customGridControl1.TabIndex = 11;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customGridView1});
            // 
            // customGridView1
            // 
            this.customGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customGridView1.Appearance.Row.Options.UseFont = true;
            this.customGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridView1.GridControl = this.customGridControl1;
            this.customGridView1.Name = "customGridView1";
            this.customGridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.customGridView1.OptionsBehavior.Editable = false;
            this.customGridView1.OptionsCustomization.AllowRowSizing = true;
            this.customGridView1.OptionsCustomization.QuickCustomizationIcons.Image = null;
            this.customGridView1.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.Empty;
            this.customGridView1.OptionsLayout.Columns.StoreAllOptions = true;
            this.customGridView1.OptionsLayout.Columns.StoreAppearance = true;
            this.customGridView1.OptionsLayout.StoreAllOptions = true;
            this.customGridView1.OptionsLayout.StoreAppearance = true;
            this.customGridView1.OptionsView.ColumnAutoWidth = false;
            this.customGridView1.OptionsView.ShowFooter = true;
            this.customGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // opBalsimpleButton
            // 
            this.opBalsimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opBalsimpleButton.Appearance.Options.UseFont = true;
            this.opBalsimpleButton.Location = new System.Drawing.Point(474, 2);
            this.opBalsimpleButton.Name = "opBalsimpleButton";
            this.opBalsimpleButton.Size = new System.Drawing.Size(49, 29);
            this.opBalsimpleButton.TabIndex = 5;
            this.opBalsimpleButton.Text = "Op Bal.";
            // 
            // bankSimpleButton
            // 
            this.bankSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bankSimpleButton.Appearance.Options.UseFont = true;
            this.bankSimpleButton.Location = new System.Drawing.Point(526, 2);
            this.bankSimpleButton.Name = "bankSimpleButton";
            this.bankSimpleButton.Size = new System.Drawing.Size(77, 29);
            this.bankSimpleButton.TabIndex = 6;
            this.bankSimpleButton.Text = "Bank Details";
            // 
            // addressSimpleButton
            // 
            this.addressSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressSimpleButton.Appearance.Options.UseFont = true;
            this.addressSimpleButton.Location = new System.Drawing.Point(606, 2);
            this.addressSimpleButton.Name = "addressSimpleButton";
            this.addressSimpleButton.Size = new System.Drawing.Size(66, 29);
            this.addressSimpleButton.TabIndex = 7;
            this.addressSimpleButton.Text = "Addresses";
            // 
            // importToolstripitem
            // 
            this.importToolstripitem.Name = "importToolstripitem";
            this.importToolstripitem.Size = new System.Drawing.Size(23, 23);
            this.importToolstripitem.Text = "Excel Import";
            // 
            // AccListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customGridControl1);
            this.KontoGrid = this.customGridControl1;
            this.KontoView = this.customGridView1;
            this.Name = "AccListView";
            this.Size = new System.Drawing.Size(849, 311);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panelControl3, 0);
            this.Controls.SetChildIndex(this.customGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Core.Shared.Libs.CustomGridControl customGridControl1;
        private Core.Shared.Libs.CustomGridView customGridView1;
        private Syncfusion.Windows.Forms.Tools.SplitButton splitButton1;
        private Syncfusion.Windows.Forms.Tools.toolstripitem deprToolstripitem;
        private Syncfusion.Windows.Forms.Tools.toolstripitem interestToolstripitem;
        private Syncfusion.Windows.Forms.Tools.toolstripitem opStockToolstripitem;
        private Syncfusion.Windows.Forms.Tools.toolstripitem tcsToolstripitem;
        private Syncfusion.Windows.Forms.Tools.toolstripitem tdsToolstripitem;
        private DevExpress.XtraEditors.SimpleButton opBalsimpleButton;
        private DevExpress.XtraEditors.SimpleButton addressSimpleButton;
        private DevExpress.XtraEditors.SimpleButton bankSimpleButton;
        private Syncfusion.Windows.Forms.Tools.toolstripitem importToolstripitem;
    }
}
