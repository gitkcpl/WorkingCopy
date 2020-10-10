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

namespace Konto.Shared.Trans.GRN
{
    public partial class PendingReturnableForGrnView : KontoForm
    {
        public VoucherTypeEnum VoucherType { get; set; }
        public ChallanTypeEnum ChallanType { get; set; }
        public int AccId { get; set; }
        private string GridLayoutFileName = "trans\\pending_returnable_for_grn.xml";
        public int[] SelectedRows { get; set; }
        public PendingReturnableForGrnView()
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
                using(var db = new KontoContext())
                {
                    var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                      (int)SpCollectionEnum.PendingReturnableForGrn);
                    List<PendingReturnableDto> listDtos = new List<PendingReturnableDto>();
                    if (spcol == null)
                    {
                        listDtos = db.Database.SqlQuery<PendingReturnableDto>(
                            "dbo.PendingReturnable @CompanyId={0},@AccountId={1},@VoucherTypeID={2}," +
                            "@ChallanType={3}",
                      KontoGlobals.CompanyId, this.AccId,this.VoucherType ,this.ChallanType).ToList();
                    }
                    else
                    {
                        listDtos = db.Database.SqlQuery<PendingReturnableDto>(
                         spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeID={2},@ChallanType={3}",
                         KontoGlobals.CompanyId, this.AccId, this.VoucherType,this.ChallanType).ToList();
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
                Log.Error(ex, "Pending Returnable View");
                MessageBox.Show(ex.ToString());
            }
        }

      

        
    }
}
