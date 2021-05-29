using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Konto.Core.Shared;

namespace Konto.Pos.Sales
{
    public partial class PosRateType : KontoForm
    {
        public string SelectedRate { get; set; }
        public PosRateType()
        {
            InitializeComponent();
            this.Load += PosRateType_Load;
        }

        private void PosRateType_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.listBoxControl1;
            this.listBoxControl1.SelectedIndex = 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.SelectedRate = listBoxControl1.SelectedValue.ToString();
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
