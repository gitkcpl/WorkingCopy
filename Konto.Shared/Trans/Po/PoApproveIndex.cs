using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Transaction.Dtos;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Konto.Shared.Trans.Po
{
    public partial class PoApproveIndex : KontoForm
    {
        int _vtypeid = 0;
        public PoApproveIndex()
        {
            InitializeComponent();
            this.Load += PoApproveIndex_Load;
            this.FormClosed += PoApproveIndex_FormClosed;
            fDateEdit.DateTime = KontoGlobals.DFromDate;
            tDateEdit.DateTime = KontoGlobals.DToDate;

            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("PENDING","PENDING"),
                new ComboBoxPairs("REJECTED","REJECTED"),
                new ComboBoxPairs("APPROVED", "APPROVED"),
                new ComboBoxPairs("CANCELED","CANCELED"),
                new ComboBoxPairs("CLOSED","CLOSED")
            };
            typeLookUpEdit.Properties.DataSource = cbp;
            statusLookUpEdit.Properties.DataSource = cbp;
            getSimpleButton.Click += GetSimpleButton_Click;
            okSimpleButton.Click += OkSimpleButton_Click;
            cancelSimpleButton.Click += CancelSimpleButton_Click;


            this.FirstActiveControl = fDateEdit;

        }

        private void PoApproveIndex_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(statusLookUpEdit.Text))
            {
                MessageBox.Show("Please Select a status to change");
                statusLookUpEdit.Focus();
                return;
            }

            using (var db = new KontoContext())
            {
                foreach (var item in gridView1.GetSelectedRows())
                {
                    var ord = gridView1.GetRow(item) as OrdAprvalDto;
                    var order = db.OrdTranses.Find(ord.TransId);
                    if (order != null)
                    {
                        order.OrdStatus = statusLookUpEdit.Text;
                    }
                }
                db.SaveChanges();
            }

            MessageBox.Show("Order Status updated successfully");
        }

        private void GetSimpleButton_Click(object sender, EventArgs e)
        {
            var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
            var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
            
            using (var db = new KontoContext())
            {
                db.Database.CommandTimeout = 0;
                var spcol = db.SpCollections.FirstOrDefault(k => k.Id ==
                                           (int)SpCollectionEnum.GetOrderApproveList);

                var list = new List<OrdAprvalDto>();
                if (spcol == null)
                {
                   list = 
                      db.Database.SqlQuery<OrdAprvalDto>(
                       "dbo.GetOrderApproveList @VouchreID={0},@CompanyId={1},@YearId={2},@FromDate={3},@ToDate={4},@OStatus={5}",
                       _vtypeid, KontoGlobals.CompanyId, KontoGlobals.YearId, fdate, tdate, typeLookUpEdit.Text).ToList();
                }
                else
                {
                   list = db.Database.SqlQuery<OrdAprvalDto>(
                        spcol.Name + " @VouchreID={0},@CompanyId={1},@YearId={2},@FromDate={3},@ToDate={4},@OStatus={5}",
                        _vtypeid, KontoGlobals.CompanyId, KontoGlobals.YearId, fdate, tdate,typeLookUpEdit.Text).ToList();
                }
                bindingSource1.DataSource = list;
            }
        }

        private void PoApproveIndex_Load(object sender, EventArgs e)
        {
            typeLookUpEdit.EditValue = "PENDING";
            if ((int)this.Tag == MenuId.Purchase_Order)
                _vtypeid = (int)VoucherTypeEnum.PurchaseOrder;
            else
            {
                this.Text = "Sales Order Aproval";
                _vtypeid = (int)VoucherTypeEnum.SalesOrder;
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
