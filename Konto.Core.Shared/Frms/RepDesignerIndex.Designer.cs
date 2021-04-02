
namespace Konto.Core.Shared.Frms
{
    partial class RepDesignerIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepDesignerIndex));
            this.activeSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.stimulsoftSimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // activeSimpleButton
            // 
            this.activeSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activeSimpleButton.Appearance.Options.UseFont = true;
            this.activeSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("activeSimpleButton.ImageOptions.SvgImage")));
            this.activeSimpleButton.Location = new System.Drawing.Point(30, 49);
            this.activeSimpleButton.Name = "activeSimpleButton";
            this.activeSimpleButton.Size = new System.Drawing.Size(130, 46);
            this.activeSimpleButton.TabIndex = 0;
            this.activeSimpleButton.Text = "Active Report";
            // 
            // stimulsoftSimpleButton
            // 
            this.stimulsoftSimpleButton.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stimulsoftSimpleButton.Appearance.Options.UseFont = true;
            this.stimulsoftSimpleButton.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("stimulsoftSimpleButton.ImageOptions.SvgImage")));
            this.stimulsoftSimpleButton.Location = new System.Drawing.Point(191, 49);
            this.stimulsoftSimpleButton.Name = "stimulsoftSimpleButton";
            this.stimulsoftSimpleButton.Size = new System.Drawing.Size(134, 46);
            this.stimulsoftSimpleButton.TabIndex = 1;
            this.stimulsoftSimpleButton.Text = "Stimulsoft";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(285, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // RepDesignerIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 137);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.stimulsoftSimpleButton);
            this.Controls.Add(this.activeSimpleButton);
            this.Name = "RepDesignerIndex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose Designer";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton activeSimpleButton;
        private DevExpress.XtraEditors.SimpleButton stimulsoftSimpleButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}