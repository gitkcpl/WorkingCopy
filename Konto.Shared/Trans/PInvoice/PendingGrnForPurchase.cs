using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Trans.PInvoice
{
    public partial class PendingGrnForPurchase : KontoForm
    {
        private string GridLayoutFileName = "trans\\pending_pur_challan.xml";
        public int AccId { get; set; }
        public VoucherTypeEnum VoucherType { get; set; }
        public ChallanTypeEnum ChallanType { get; set; }
        public string ChallanTypeId { get; set; }
        public int[] SelectedRows { get; set; }
        public PendingGrnForPurchase()
        {
            InitializeComponent();
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            this.Load += PendingGrnForPurchase_Load;
        }

        private void PendingGrnForPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                using(var db = new KontoContext())
                {
                    db.Database.CommandTimeout = 0;
                    var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                                      (int)SpCollectionEnum.PendingChallanOnInvoice);
                    var listDtos = new List<PendingChallanOnInvoiceDto>();
                    if (spcol == null)
                    {
                        listDtos = db.Database.SqlQuery<PendingChallanOnInvoiceDto>(
                       "dbo.PendingChallanOnInvoice @CompanyId={0},@AccountId={1},@VoucherTypeID={2},@ChallanTypeID={3}",
                       KontoGlobals.CompanyId, this.AccId, (int)VoucherType, ChallanTypeId).ToList();
                    }
                    else
                    {
                        listDtos = db.Database.SqlQuery<PendingChallanOnInvoiceDto>(
                         spcol.Name + "  @CompanyId={0},@AccountId={1},@VoucherTypeID={2},@ChallanTypeID={3}",
                          KontoGlobals.CompanyId, this.AccId, (int)VoucherType, ChallanTypeId).ToList();
                    }
                    if (listDtos.Count == 0)
                    {
                        this.Close();
                        this.Dispose();
                        return;
                    }
                    this.gridControl1.DataSource = listDtos;
                   
                }
                KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, gridView1);
                this.ActiveControl = gridControl1;
            }
            catch (Exception ex)
            {

                Log.Error(ex, "Pending Purchase Challan");
                MessageBox.Show(ex.ToString());
            }
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
        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            this.SelectedRows = gridView1.GetSelectedRows();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }
    }
}
