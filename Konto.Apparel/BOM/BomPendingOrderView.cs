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

namespace Konto.Apparel.BOM
{
    public partial class BomPendingOrderView : KontoForm
    {
        public VoucherTypeEnum VoucherType { get; set; }
        public int AccId { get; set; }
        private string GridLayoutFileName = "trans\\bom_pending_order.xml";
        public int[] SelectedRows { get; set; }
        public BomPendingOrderView()
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
                    //var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                    //  (int)SpCollectionEnum.PendingOrderonChallan);
                    List<PendingOrderDto> listDtos = new List<PendingOrderDto>();
                    //if (spcol == null)
                    //{
                        listDtos = db.Database.SqlQuery<PendingOrderDto>(
                            "dbo.PendingOrderonBom @CompanyId={0},@AccountId={1},@VoucherTypeID={2}",
                      KontoGlobals.CompanyId, this.AccId,VoucherTypeEnum.SalesOrder).ToList();
                    //}
                    //else
                    //{
                    //    listDtos = db.Database.SqlQuery<PendingOrderDto>(
                    //     spcol.Name + " @CompanyId={0},@AccountId={1},@VoucherTypeID={2}",
                    //     KontoGlobals.CompanyId, this.AccId, this.VoucherType).ToList();
                    //}
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
                Log.Error(ex, "Bom Pending Order View");
                MessageBox.Show(ex.ToString());
            }
        }

      

        
    }
}
