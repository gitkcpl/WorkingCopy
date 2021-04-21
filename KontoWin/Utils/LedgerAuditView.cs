using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Import;
using Konto.Shared.Account.DRCRNote;
using Konto.Shared.Account.GenExpense;
using Konto.Shared.Account.Jv;
using Konto.Shared.Account.Payment;
using Konto.Shared.Account.Receipt;
using Konto.Shared.Trans.PInvoice;
using Konto.Shared.Trans.PReturn;
using Konto.Shared.Trans.SInvoice;
using Konto.Shared.Trans.SReturn;
using Konto.Trading.GP;
using Konto.Trading.JobReceipt;
using Konto.Trading.MillReceipt;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KontoWin.Utils
{
    public partial class LedgerAuditView : KontoForm
    {
        public LedgerAuditView()
        {
            InitializeComponent();
            this.Load += LedgerAuditView_Load;
            this.customGridControl1.ProcessGridKey += CustomGridControl1_ProcessGridKey;
            this.FormClosed += LedgerAuditView_FormClosed;
            this.cancelSimpleButton.Click += CancelSimpleButton_Click;

        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void LedgerAuditView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void CustomGridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (this.customGridView1.FocusedRowHandle < 0) return;

            var row = customGridView1.GetDataRow(customGridView1.FocusedRowHandle);
            if (row == null) return;
            ShowZoom(row);
        }
        private void ShowZoom(DataRow err)
        {
            var db = new KontoContext();
            Guid _id = (Guid) err["RefId"];
            var bll = db.Bills.FirstOrDefault(x => x.RowId == _id);
            if (bll == null) return;
            var vw = new KontoMetroForm();
            if ((int) err["VTypeId"] == (int)VoucherTypeEnum.SaleInvoice)
            {
                vw = new SInvoiceIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.PurchaseInvoice)
            {
                vw = new PInvoiceIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.SaleReturn)
            {
                vw = new SReturnIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.PurchaseReturn)
            {
                vw = new PReturnIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.GrayPurchaseInvoice)
            {
                vw = new GPIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.ReceiptVoucher)
            {
                vw = new ReceiptIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.PaymentVoucher)
            {
                vw = new PaymentIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.JournalVoucher)
            {
                vw = new JvIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.DebitCreditNote)
            {
                vw = new DRCRNoteIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.GenExpense)
            {
                vw = new GenExpIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.MillReceiptVoucher)
            {
                vw = new MrvIndex();
            }
            else if ((int)err["VTypeId"] == (int)VoucherTypeEnum.JobReceiptVoucher)
            {
                vw = new JrIndex();
            }

            vw.OpenForLookup = true;
            vw.EditKey = bll.Id;
            vw.ShowDialog();
        }
        private void LedgerAuditView_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void GetData()
        {
            using (var con = new SqlConnection(KontoGlobals.sqlConnectionString.ConnectionString))
            {

                using (var cmd = new SqlCommand("dbo.ledger_audit", con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@fromDate", SqlDbType.Int).Value = KontoGlobals.FromDate;
                    cmd.Parameters.Add("@todate", SqlDbType.Int).Value = KontoGlobals.ToDate;
                    cmd.Parameters.Add("@compid", SqlDbType.Int).Value = KontoGlobals.CompanyId;

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    var DtCriterias = new DataTable();
                    DtCriterias.Load(cmd.ExecuteReader());
                    con.Close();
                    customGridControl1.DataSource = DtCriterias;
                }
            }
        }
        private void okSimpleButton_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var frm = new ImportView();
            frm.ShowDialog();
            //var frm = new Konto.Reporting.GE.RepGateEntryView();
            //var _tab = this.Parent.Parent as TabControlAdv;
            //if (_tab == null) return;
            //var pg1 = new TabPageAdv();
            //pg1.Text = "GateEntry Report";
            //_tab.TabPages.Add(pg1);
            //_tab.SelectedTab = pg1;
            //frm.WindowState = FormWindowState.Maximized;
            //frm.TopLevel = false;
            //frm.Parent = pg1;
            //frm.Location = new Point(pg1.Location.X + pg1.Width / 2 - frm.Width / 2, pg1.Location.Y + pg1.Height / 2 - frm.Height / 2);
            //frm.Show();// = true;
        }
    }
}
