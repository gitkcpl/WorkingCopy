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

namespace Konto.Yarn.JobCard
{
    public partial class PendingOrderView : KontoForm
    {
        public VoucherTypeEnum VoucherType { get; set; }
        private string GridLayoutFileName = "weaves\\Select_PendingOrderInJobCard.xml";
        public PendingOrderDto SelectedRow { get; set; }
        public List<PendingOrderDto> ItemList;
        public PendingOrderView()
        {
            InitializeComponent();
            
            this.Load += TakaViewWindow_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;

            ItemList = new List<PendingOrderDto>();
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
           // this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.SelectedRow = gridView1.GetFocusedRow() as PendingOrderDto;
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
        private void TakaViewWindow_Load(object sender, EventArgs e)
        {
            try
            {
                using(var db = new KontoContext())
                { 
                    if (ItemList.Count == 0)
                    {
                        this.Close();
                      //  this.Dispose();
                        return;
                    }
                    this.gridControl1.DataSource = ItemList;
                    KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, gridView1);
                }
                this.ActiveControl = gridControl1;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Pending Jobcard Order View");
                MessageBox.Show(ex.ToString());
            }
        }
    }
}