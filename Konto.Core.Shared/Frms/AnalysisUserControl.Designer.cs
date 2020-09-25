namespace Konto.Core.Shared.Frms
{
    partial class AnalysisUserControl
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
            this.c1FlexPivotPage1 = new C1.Win.FlexPivot.C1FlexPivotPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.listDateRange1 = new Konto.Core.Shared.Libs.ListDateRange();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexPivotPage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1FlexPivotPage1
            // 
            this.c1FlexPivotPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexPivotPage1.Location = new System.Drawing.Point(0, 37);
            this.c1FlexPivotPage1.Margin = new System.Windows.Forms.Padding(2);
            this.c1FlexPivotPage1.Name = "c1FlexPivotPage1";
            this.c1FlexPivotPage1.Size = new System.Drawing.Size(704, 423);
            this.c1FlexPivotPage1.TabIndex = 4;
            this.c1FlexPivotPage1.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.c1FlexPivotPage1.Load += new System.EventHandler(this.c1FlexPivotPage1_Load);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.listDateRange1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(704, 37);
            this.panelControl1.TabIndex = 3;
            // 
            // listDateRange1
            // 
            this.listDateRange1.FromDate = 0;
            this.listDateRange1.IsAnalysis = false;
            this.listDateRange1.KontoGrid = null;
            this.listDateRange1.Location = new System.Drawing.Point(5, 5);
            this.listDateRange1.Name = "listDateRange1";
            this.listDateRange1.SelectedItem = null;
            this.listDateRange1.Size = new System.Drawing.Size(513, 25);
            this.listDateRange1.TabIndex = 0;
            this.listDateRange1.ToDate = 0;
            this.listDateRange1.VoucherType = Konto.App.Shared.VoucherTypeEnum.None;
            // 
            // AnalysisUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.c1FlexPivotPage1);
            this.Controls.Add(this.panelControl1);
            this.Name = "AnalysisUserControl";
            this.Size = new System.Drawing.Size(704, 460);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexPivotPage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.FlexPivot.C1FlexPivotPage c1FlexPivotPage1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Libs.ListDateRange listDateRange1;
    }
}
