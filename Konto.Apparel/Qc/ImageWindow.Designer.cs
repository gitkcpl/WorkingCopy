namespace Konto.Apparel.Qc
{
    partial class ImageWindowFrm
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
            this.pImageModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Konto.Shared.Masters.Acc.WaitForm1), true, true);
            this.btnno = new System.Windows.Forms.Button();
            this.imageListview = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.Yes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pImageModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pImageModelBindingSource
            // 
            this.pImageModelBindingSource.DataSource = typeof(Konto.Data.Models.Masters.PImageModel);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // btnno
            // 
            this.btnno.Location = new System.Drawing.Point(761, 414);
            this.btnno.Name = "btnno";
            this.btnno.Size = new System.Drawing.Size(75, 23);
            this.btnno.TabIndex = 0;
            this.btnno.Text = "No";
            this.btnno.UseVisualStyleBackColor = true;
            this.btnno.Click += new System.EventHandler(this.btnno_Click);
            // 
            // imageListview
            // 
            this.imageListview.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListview.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListview.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(-2, 5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(853, 403);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(680, 414);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(75, 23);
            this.Yes.TabIndex = 0;
            this.Yes.Text = "Yes";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // ImageWindowFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.CaptionButtonColor = System.Drawing.Color.White;
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 449);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Yes);
            this.Controls.Add(this.btnno);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(150)))), ((int)(((byte)(205)))));
            this.Name = "ImageWindowFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Images";
            ((System.ComponentModel.ISupportInitialize)(this.pImageModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private System.Windows.Forms.BindingSource pImageModelBindingSource;
        private System.Windows.Forms.Button btnno;
        private System.Windows.Forms.ImageList imageListview;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button Yes;
    }
}