using Konto.Core.Shared;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Outs
{
    public partial class RangeParaView : KontoForm
    {
        public int Range1 { get; set; }
        public int Range2 { get; set; }
        public int Range3 { get; set; }
        public int Range4 { get; set; }
        public int Range5 { get; set; }
        public RangeParaView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            //range1TextEdit.Text = ConfigurationManager.AppSettings["Range1"];
            //range2TextEdit2.Text = ConfigurationManager.AppSettings["Range2"];
            //range3TextEdit3.Text = ConfigurationManager.AppSettings["Range3"]; 
            //range4TextEdit4.Text = ConfigurationManager.AppSettings["Range4"];
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(range1TextEdit.Text))
            {
                MessageBox.Show("Invalid Range1");
                range1TextEdit.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(range2TextEdit2.Text))
            {
                MessageBox.Show("Invalid Range2");
                range2TextEdit2.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(range3TextEdit3.Text))
            {
                MessageBox.Show("Invalid Range2");
                range3TextEdit3.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(range4TextEdit4.Text))
            {
                MessageBox.Show("Invalid Range4");
                range4TextEdit4.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(range5TextEdit5.Text))
            {
                MessageBox.Show("Invalid Range5");
                range5TextEdit5.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            Range1 = Convert.ToInt32(range1TextEdit.Text);
            Range2 = Convert.ToInt32(range2TextEdit2.Text);
            Range3 = Convert.ToInt32(range3TextEdit3.Text);
            Range4 = Convert.ToInt32(range4TextEdit4.Text);
            Range5 = Convert.ToInt32(range5TextEdit5.Text);
            this.Close();
        }
    }
}
