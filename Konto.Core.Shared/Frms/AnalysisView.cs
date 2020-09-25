using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Core.Shared.Frms
{
    public partial class AnalysisView : KontoForm
    {
        public DataTable DataTable { get; set; }
        public AnalysisView()
        {
            InitializeComponent();
            this.Load += AnalysisView_Load;
        }

        private void AnalysisView_Load(object sender, EventArgs e)
        {
            this.c1FlexPivotPage1.DataSource = this.DataTable;
            
        }
    }
}
