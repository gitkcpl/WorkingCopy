using Konto.Core.Shared;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Trans.Common
{
    public partial class HistoryView : KontoForm
    {
        public List<HistoryDto> historyDtos = new List<HistoryDto>();
        public HistoryView()
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.Load += HistoryView_Load;
        }

        private void HistoryView_Load(object sender, EventArgs e)
        {
            historyDtoBindingSource.DataSource = historyDtos;
            gridControl1.RefreshDataSource();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        
    }
}
