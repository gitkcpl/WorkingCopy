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
    public partial class PendingSingleOrderView : KontoForm
    {
        public VoucherTypeEnum VoucherType { get; set; } 
        private string GridLayoutFileName = "trans\\pending_Singleorder.xml";
        public PendingOrderDto SelectedRow { get; set; }
        public PendingSingleOrderView()
        {
            InitializeComponent();
            
            this.Load += PendingOrderView_Load;
            this.okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;
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
        private void PendingOrderView_Load(object sender, EventArgs e)
        {
            try
            {
                using(var db = new KontoContext())
                {
                    var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                      (int)SpCollectionEnum.PendingOrderonIssue);
                    List<PendingOrderDto> listDtos = new List<PendingOrderDto>();
                    if (spcol == null)
                    {
                        listDtos = db.Database.SqlQuery<PendingOrderDto>(
                            "dbo.PendingOrderonIssue @CompanyId={0},@AccountId={1},@VoucherTypeID={2},@IssueVoucher={3}",
                      KontoGlobals.CompanyId,0,(int)VoucherTypeEnum.SalesOrder ,(int)this.VoucherType).ToList();
                    }
                    else
                    {
                        listDtos = db.Database.SqlQuery<PendingOrderDto>(
                         spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeID={2},@IssueVoucher={3}",
                      KontoGlobals.CompanyId, 0, (int)VoucherTypeEnum.SalesOrder, (int)this.VoucherType).ToList();
                    }
                    if(listDtos.Count == 0)
                    {
                        this.Close();
                      //  this.Dispose();
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