namespace Konto.Core.Shared.Frms
{
    partial class KontoArDesignerView
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
            this.endUserDesigner1 = new GrapeCity.ActiveReports.Samples.EndUserDesigner.EndUserDesigner();
            this.SuspendLayout();
            // 
            // endUserDesigner1
            // 
            this.endUserDesigner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endUserDesigner1.Location = new System.Drawing.Point(0, 0);
            this.endUserDesigner1.Name = "endUserDesigner1";
            this.endUserDesigner1.Size = new System.Drawing.Size(800, 450);
            this.endUserDesigner1.TabIndex = 0;
            // 
            // KontoArDesignerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.endUserDesigner1);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.MinimizeBox = true;
            this.Name = "KontoArDesignerView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Konto Report Designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public GrapeCity.ActiveReports.Samples.EndUserDesigner.EndUserDesigner endUserDesigner1;
    }
}