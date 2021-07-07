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

namespace Konto.Shared.Trans.Common
{
    public partial class PendingOrderView : KontoForm
    {
        public VoucherTypeEnum VoucherType { get; set; }
        public ChallanTypeEnum ChallanType { get; set; }
        public int AccId { get; set; }
        private string GridLayoutFileName = "trans\\pending_order.xml";
        public int[] SelectedRows { get; set; }
        public PendingOrderView()
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
                int compid = 0;
                if (SysParameter.Common_Order)
                    compid = 0;
                else
                    compid = KontoGlobals.CompanyId;

                using (var db = new KontoContext())
                {
                    List<PendingOrderDto> listDtos = new List<PendingOrderDto>();
                    if (ChallanType == ChallanTypeEnum.PURCHASE)
                    {
                        var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                          (int)SpCollectionEnum.PendingOrderonChallan);

                        if (spcol == null)
                        {
                            listDtos = db.Database.SqlQuery<PendingOrderDto>(
                                "dbo.PendingOrderonChallan @CompanyId={0},@AccountId={1},@VoucherTypeID={2}",
                          compid, this.AccId, this.VoucherType).ToList();
                        }
                        else
                        {
                            listDtos = db.Database.SqlQuery<PendingOrderDto>(
                             spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeID={2}",
                             compid, this.AccId, this.VoucherType).ToList();
                        }
                    }
                    else if(ChallanType == ChallanTypeEnum.TRANSFER_IN)
                    {
                        var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                          (int)SpCollectionEnum.PendingTransferOut);

                        if (spcol == null)
                        {
                            listDtos = db.Database.SqlQuery<PendingOrderDto>(
                                "dbo.PendingTransferOut @CompanyId={0},@AccountId={1},@VoucherTypeID={2}," +
                                "@ChallanType={3},@BranchId={4}",
                          compid, this.AccId, (int)VoucherTypeEnum.SalesChallan,(int)ChallanTypeEnum.TRANSFER_OUT,
                          KontoGlobals.BranchId).ToList();
                        }
                        else
                        {
                            listDtos = db.Database.SqlQuery<PendingOrderDto>(
                             spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeID={2},@ChallanType={3},@BranchId={4}",
                             compid, this.AccId, (int)VoucherTypeEnum.SalesChallan, 
                             (int)ChallanTypeEnum.TRANSFER_OUT,KontoGlobals.BranchId).ToList();
                        }
                    }
                    if(listDtos.Count == 0)
                    {
                        this.Close();
                        this.Dispose();
                        return;
                    }
                    this.gridControl1.DataSource = listDtos;
                    KontoUtils.RestoreLayoutGrid(this.GridLayoutFileName, gridView1);
                }
                this.ActiveControl = gridControl1;
            }
            
            catch (Exception ex)
            {
                Log.Error(ex, "Pending Order View");
                MessageBox.Show(ex.ToString());
            }
        }

      

        
    }
}
