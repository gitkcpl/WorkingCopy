using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Weaves.JobCard
{
    public partial class PendingOrderJobCardView : KontoForm
    {
        public VoucherTypeEnum VoucherType { get; set; }
        public int AccId { get; set; }
        private string GridLayoutFileName = "Weaving\\pending_orderJobCard.xml";
        public int[] SelectedRows { get; set; }
        public List<JobCardDto> ItemList { get; set; }

        public PendingOrderJobCardView()
        {
            InitializeComponent();

            this.Load += PendingOrderView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.SelectedRows = gridView1.GetSelectedRows();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1 | Keys.Control))
            {
                KontoUtils.SaveLayoutGrid(this.GridLayoutFileName, this.gridView1);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void PendingOrderView_Load(object sender, EventArgs e)
        {
            try
            {
                this.gridControl1.DataSource = ItemList;
                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, gridView1);

                this.ActiveControl = gridControl1;
            }

            catch (Exception ex)
            {
                Log.Error(ex, "Pending Order JobCard View");
                MessageBox.Show(ex.ToString());
            }
        }
    }
}