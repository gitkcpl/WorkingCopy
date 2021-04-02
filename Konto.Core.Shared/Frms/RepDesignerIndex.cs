using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Core.Shared.Frms
{
    public partial class RepDesignerIndex : KontoForm
    {
        public RepDesignerIndex()
        {
            InitializeComponent();
            this.activeSimpleButton.Click += ActiveSimpleButton_Click;
            this.stimulsoftSimpleButton.Click += StimulsoftSimpleButton_Click;
        }

        private void StimulsoftSimpleButton_Click(object sender, EventArgs e)
        {
            
            var rep = new StiReport();
            rep.Design();
            this.Close();
        }

        private void ActiveSimpleButton_Click(object sender, EventArgs e)
        {
           
            var frm = new KontoArDesignerView();
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
            this.Close();
            frm.Activate();
            frm.Focus();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            String phoneNo = "919712221160";
            String message = "Hello. Sending message via whatsapp";
            //String message = "Hello. Sending message via &help=0 whatsapp";
            message = Uri.EscapeDataString(message);
            String uriText = "whatsapp://send?phone=" + phoneNo + "&text=" + message;

            Process.Start(new ProcessStartInfo(uriText) { UseShellExecute = true });
        }
    }
}
