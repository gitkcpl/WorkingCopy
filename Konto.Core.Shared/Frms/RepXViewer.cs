using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace Konto.Core.Shared.Frms
{
    public partial class RepXViewer : KontoForm
    {
        public XtraReport RepSource { get; set; }
        public RepXViewer()
        {
            InitializeComponent();
            this.Load += RepXViewer_Load;
        }

        private void RepXViewer_Load(object sender, EventArgs e)
        {
            this.documentViewer1.DocumentSource = RepSource;
            this.documentViewer1.InitiateDocumentCreation();
        }
    }
}
