using Konto.Core.Shared;
using Konto.Data.Models.Masters;
using Serilog;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Konto.Apparel.Qc
{
    public partial class ImageWindowFrm : KontoForm
    {
        DataTable _dataTable;
        public bool _responce = true;

        public List<PImageModel> pimagelist = new List<PImageModel>();
        public string remark = "";
        public ImageWindowFrm()
        {
            InitializeComponent();
            this.Load += ImageWindowFrm_Load;
        }
      
        private void ImageWindowFrm_Load(object sender, EventArgs e)
        {
            try
            {
                int cnt = 0;

                this.listView1.View = View.LargeIcon;
                this.imageListview.ImageSize = new Size(256, 256);
                this.listView1.LargeImageList = imageListview;
                foreach (var item in pimagelist)
                {
                    this.imageListview.Images.Add(Image.FromFile(item.ImagePath));
                     
                    ListViewItem ListItem = new ListViewItem();
                    ListItem.ImageIndex = cnt;
                    cnt = cnt + 1;
                    ListItem.Tag = item.ImagePath;
                    ListItem.Text = "test" + cnt;
                    this.listView1.Items.Add(ListItem);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Apparel Image Window Control Error");
                MessageBoxAdv.Show(this, "Error While Generating Apparel Layer Quality Control !!", "Exception ", ex.ToString());
            }    
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width
            int sourceWidth = imgToResize.Width;
            //Get the image current heightc
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        private void dyesSimpleButton_Click(object sender, EventArgs e)
        {
            _responce = true;
            this.Close();
            this.Dispose();
        }

        private void noSimpleButton_Click(object sender, EventArgs e)
        {
            _responce = false;
            this.Close();
            this.Dispose();
        }

        private void Yes_Click(object sender, EventArgs e)
        {
          ////  var frm = new RemarkWindowFrm();
            //frm.ShowDialog();
          //  remark = frm.remark;
           // this.Close();
           // this.Dispose();
        }

        private void btnno_Click(object sender, EventArgs e)
        {
           // var frm = new RemarkWindowFrm();
           // frm.ShowDialog();
          //  remark = frm.remark;
           // this.Close();
           // this.Dispose();
        }
    }
}
