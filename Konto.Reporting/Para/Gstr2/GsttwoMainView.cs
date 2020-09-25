using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Reports;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Gstr2
{
    public partial class GsttwoMainView : KontoForm
    {
        public GsttwoMainView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            this.FormClosed += GsttwoMainView_FormClosed;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;
            List<ComboBoxPairs> cbp = new List<ComboBoxPairs>
            {
                new ComboBoxPairs("Purchase Invoice", "PINVOICE"),
                new ComboBoxPairs("Purchase Return", "PRETURN"),
                new ComboBoxPairs("General Expense", "GEXPENSE"),
                new ComboBoxPairs("Debit Note", "DEBIT"),
                new ComboBoxPairs("Credit Note", "CREDIT"),
                new ComboBoxPairs("ALL", "ALL"),

            };
            viewLookUpEdit.Properties.DataSource = cbp;

            fDateEdit.EditValue = KontoGlobals.DFromDate;
            tDateEdit.EditValue = KontoGlobals.DToDate;
            viewLookUpEdit.EditValue = "PINVOICE";
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void GsttwoMainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                var fdate = Convert.ToInt32(fDateEdit.DateTime.ToString("yyyyMMdd"));
                var tdate = Convert.ToInt32(tDateEdit.DateTime.ToString("yyyyMMdd"));
                int typeid = 0;
                int typeId1 = 0;
                var billtype = " ";
                var Billag = "";
                if (viewLookUpEdit.EditValue.ToString() == "PINVOICE")
                {
                    typeid = (int)VoucherTypeEnum.PurchaseInvoice;
                }
                else if (viewLookUpEdit.EditValue.ToString() == "PRETURN")
                {
                    typeid = (int)VoucherTypeEnum.PurchaseReturn;
                }
                else if (viewLookUpEdit.EditValue.ToString() == "GEXPENSE")
                {
                    typeid = (int)VoucherTypeEnum.GenExpense;
                }
                else if (viewLookUpEdit.EditValue.ToString() == "DEBIT")
                {
                    typeid = (int)VoucherTypeEnum.DebitCreditNote;
                    billtype = "DEBIT NOTE";
                    Billag = "PURCHASE";

                }
                else if (viewLookUpEdit.EditValue.ToString() == "CREDIT")
                {
                    typeid = (int)VoucherTypeEnum.DebitCreditNote;
                    billtype = "CREDIT NOTE";
                    Billag = "PURCHASE";
                }
                else
                {
                    typeid = 0;
                    typeId1 = 0;
                }

                using (var _db = new KontoContext())
                {
                    var lst = _db.Database.SqlQuery<GsttwoDto>(
                           "dbo.Gstr2Report @CompanyId={0},@TransTypeId={1},@BillType={2}," +
                           "@FromDate={3},@ToDate={4},@Billag={5},@YearId={6},@TransTypeId1={7}",
                           Convert.ToInt32(KontoGlobals.CompanyId), typeid, billtype, fdate, tdate,
                           Billag, KontoGlobals.YearId, typeId1).ToList();
                    gsttwoDtoBindingSource.DataSource = lst;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }
    }
}
