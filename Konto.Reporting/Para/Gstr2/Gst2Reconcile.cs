using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Frms;
using Konto.Data;
using Konto.Data.Models.Gstn;
using Konto.Pos.Purchase;
using Konto.Shared.Account.GenExpense;
using Konto.Shared.Trans.PInvoice;
using Serilog;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxProGST.API;
using TaxProGST.JsonModels;
using TaxProGSTApiWinFormsDemo;

namespace Konto.Reporting.Para.Gstr2
{
    public partial class Gst2Reconcile : KontoForm
    {

        GSTSession GstSession = new GSTSession();
        public Gst2Reconcile()
        {
            InitializeComponent();
            this.Load += Gst2Reconcile_Load;
            this.FormClosed += Gst2Reconcile_FormClosed;
            umGridControl.ProcessGridKey += UmGridControl_ProcessGridKey;

            this.FirstActiveControl = textEdit1;
            this.gst2BSimpleButton.Click += Gst2BSimpleButton_ClickAsync;
        }

        private  async  void Gst2BSimpleButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textEdit1.Text))
                {
                    MessageBox.Show("Enter Period In MMYYYY(122020) Format");
                    textEdit1.Focus();
                    return;
                }

                var intprd = textEdit1.Text;

                using (var _db = new KontoContext())
                {
                    var _exist = _db.Gstr2ADumps.Any(x => x.FPrd == intprd && x.TransType == "B2B" && x.Is2B);
                    if (_exist)
                    {
                        if (MessageBox.Show("Data Already Exist for Selected Period. Do you want to download Again..", "Download !", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }

                }


                splashScreenManager1.ShowWaitForm();

                var yr = Convert.ToInt32(textEdit1.Text.Substring(2));
                var mon = Convert.ToInt32(textEdit1.Text.Substring(0, 2));
                var lstday = DateTime.DaysInMonth(yr, mon);

                var fdate = Convert.ToInt32(yr.ToString() + textEdit1.Text.Substring(0, 2) + "01");
                var tdate = Convert.ToInt32(yr.ToString() + textEdit1.Text.Substring(0, 2) + lstday.ToString());


                //txtAPIResponse.Text = "Please wait...";
                await Task.Delay(200);

                if (File.Exists("Response.json"))
                    File.Delete("Response.json");

                TxnRespWithObj<GSTR2BJson> txnResp = new TxnRespWithObj<GSTR2BJson>();
                txnResp = await GSTR2BAPI.GetAllDetails(GstSession, "Get GSTR-2B Data", KontoGlobals.GstIn, textEdit1.Text, "", true, "Response.json");

                if (txnResp.IsSuccess)
                {
                    var gstdumps = new List<Gstr2ADump>();

                    using (var db = new KontoContext())
                    {


                        foreach (var item in txnResp.RespObj.data.docdata.b2b)
                        {

                            foreach (var inv in item.inv)
                            {

                                var bm = new Gstr2ADump();
                                var bts = new List<Gstr2ATransDump>();
                                bm.FileDate = Convert.ToDateTime(item.supfildt);
                                bm.FilePeriod = item.supprd;
                                bm.GstIn = item.ctin;
                                bm.InvoiceDate = inv.dt;
                                bm.InvoiceNo = inv.inum;
                                bm.InvoiceValue = Convert.ToDecimal(inv.val);
                                bm.Pos = inv.pos;
                                bm.TransType = "B2B";
                                bm.FPrd = textEdit1.Text.Trim();
                                bm.CompId = KontoGlobals.CompanyId;
                                bm.YearId = KontoGlobals.YearId;
                                bm.CreateDate = DateTime.Now;
                                bm.Is2B = true;
                                foreach (var det in inv.items)
                                {
                                    var bs = new Gstr2ATransDump();
                                    bs.Cess = Convert.ToDecimal(det.cess);
                                    bs.Cgst = Convert.ToDecimal(det.cgst);
                                    bs.Igst = Convert.ToDecimal(det.igst);
                                    bs.Sgst = Convert.ToDecimal(det.sgst);
                                    bs.Taxable = Convert.ToDecimal(det.txval);
                                    bs.TaxRate = Convert.ToDecimal(det.rt);
                                    bts.Add(bs);
                                }

                                bm.Cess = bts.Sum(x => x.Cess);
                                bm.Cgst = bts.Sum(x => x.Cgst);
                                bm.Igst = bts.Sum(x => x.Igst);
                                bm.Sgst = bts.Sum(x => x.Sgst);
                                bm.Taxable = bts.Sum(x => x.Taxable);
                                bm.TaxRate = bts.FirstOrDefault().TaxRate;
                                bm.Gstr2aTrans = bts;

                                var bill = (from p in db.Bills
                                            join ac in db.Accs on p.AccId equals ac.Id
                                            where p.BillNo == bm.InvoiceNo && ac.GstIn == bm.GstIn
                                            && (p.TotalAmount == bm.InvoiceValue || p.TotalAmount - bm.InvoiceValue <= (decimal)0.5)
                                            && p.VoucherDate >= fdate && p.VoucherDate <= tdate
                                            && p.CompId == KontoGlobals.CompanyId && !p.IsDeleted && p.IsActive
                                            select p).FirstOrDefault();

                                if (bill != null)
                                    bm.Billid = bill.Id;

                                gstdumps.Add(bm);
                            }
                        }

                        //var dmp = gstdumps[0];
                        if (gstdumps.Count > 0)
                        {
                            var prd = gstdumps[0].FilePeriod;
                            var _exist = db.Gstr2ADumps.Where(x => x.FilePeriod == prd && x.Is2B).ToList();

                            if (_exist.Count > 0)
                            {
                                db.Gstr2ADumps.RemoveRange(_exist);
                            }


                            db.Gstr2ADumps.AddRange(gstdumps);
                            db.SaveChanges();

                            gstr2ADumpBindingSource.DataSource = gstdumps;
                            gridControl1.RefreshDataSource();

                            GetMatchData(db, intprd,1);
                        }


                    }



                }
                else
                    MessageBox.Show(txnResp.TxnOutcome);

                splashScreenManager1.CloseWaitForm();

            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.Message);

            }

        }

        private void UmGridControl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            try
            {
                var dr = umGridView.GetRow(umGridView.FocusedRowHandle) as RecoGstr2aUnMatch;
                if (dr == null) return;
                var db = new KontoContext();
                var bll = db.Bills.FirstOrDefault(x => x.Id == dr.Id);
                if (bll == null) return;
                var vw = new KontoMetroForm();
                if (dr.VTypeId == (int)VoucherTypeEnum.PurchaseInvoice)
                {
                    if(KontoGlobals.PackageId == (int)PackageType.POS)
                        vw = new PurchaseIndex();
                    else
                    vw = new PInvoiceIndex();
                }
                else if (dr.VTypeId == (int)VoucherTypeEnum.GenExpense)
                {
                    vw = new GenExpIndex();
                }

                vw.OpenForLookup = true;
                vw.EditKey = bll.Id;
                vw.ShowDialog();

                GetUnmatchData(db);
                umGridControl.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
           

        }

        private void Gst2Reconcile_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabpage = this.Parent as TabPageAdv;
            if (tabpage != null)
            {
                var tabcontrol = tabpage.Parent as TabControlAdv;
                if (tabcontrol != null)
                    tabcontrol.TabPages.Remove(tabpage);
            }
        }

        private void Gst2Reconcile_Load(object sender, EventArgs e)
        {
            try
            {
                GstSession.ApiSetting = TaxProGSTApiWinFormsDemo.Shared.LoadAPISetting();
                GstSession.ApiLoginDetails = TaxProGSTApiWinFormsDemo.Shared.LoadAPILoginDetails();
                gstr2SimpleButton.Click += gstr2SimpleButton_ClickAsync;
                okSimpleButton.Click += OkSimpleButton_Click;
                this.cancelSimpleButton.Click += CancelSimpleButton_Click;

                this.ActiveControl = textEdit1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelSimpleButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(textEdit1.Text))
                //{
                //    MessageBox.Show("Enter Period In MMYYYY(122020) Format");
                //    textEdit1.Focus();
                //    return;
                //}

                //var intprd = textEdit1.Text;

                splashScreenManager1.ShowWaitForm();

                using (var db = new KontoContext())
                {
                    var gstrdumps = db.Gstr2ADumps.Where(x => (x.InvoiceDate >= KontoGlobals.DFromDate
                        && x.InvoiceDate<=KontoGlobals.DToDate) && x.TransType=="B2B" && !x.Is2B && x.CompId== KontoGlobals.CompanyId).ToList();

                    foreach (var item in gstrdumps)
                    {
                        var bill = (from p in db.Bills
                                    join ac in db.Accs on p.AccId equals ac.Id
                                    where p.BillNo == item.InvoiceNo && ac.GstIn == item.GstIn
                                    && (p.TotalAmount == item.InvoiceValue || p.TotalAmount - item.InvoiceValue <= (decimal)0.5)
                                    && p.VoucherDate >= KontoGlobals.FromDate && p.VoucherDate <= KontoGlobals.ToDate
                                    && p.CompId == KontoGlobals.CompanyId && !p.IsDeleted && p.IsActive
                                    select p).FirstOrDefault();

                        item.Billid = 0;
                        if (bill != null)
                            item.Billid = bill.Id;
                    }

                    db.SaveChanges();

                    gstr2ADumpBindingSource.DataSource = gstrdumps;
                    gridControl1.RefreshDataSource();

                    GetMatchData(db, "",0);

                    
                }
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {

                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.ToString());
                Log.Error(ex, "Gstr2a_Reconicle");
            }
        }

        private async void gstr2SimpleButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(textEdit1.Text))
                {
                    MessageBox.Show("Enter Period In MMYYYY(122020) Format");
                    textEdit1.Focus();
                    return;
                }

                var intprd = textEdit1.Text;

                using (var _db = new KontoContext())
                {
                    var _exist = _db.Gstr2ADumps.Any(x => x.FPrd == intprd && x.TransType == "B2B" && !x.Is2B);
                        if (_exist)
                    {
                        if (MessageBox.Show("Data Already Exist for Selected Period. Do you want to download Again..", "Download !", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    
                }


                splashScreenManager1.ShowWaitForm();

                var yr = Convert.ToInt32(textEdit1.Text.Substring(2));
                var mon = Convert.ToInt32(textEdit1.Text.Substring(0, 2));
                var lstday = DateTime.DaysInMonth(yr, mon);

                var fdate = yr.ToString() + textEdit1.Text.Substring(0, 2) + "01";
                var tdate = yr.ToString() + textEdit1.Text.Substring(0, 2) + lstday.ToString();


                //txtAPIResponse.Text = "Please wait...";
                await Task.Delay(200);

                if (File.Exists("Response.json"))
                    File.Delete("Response.json");

               

                TxnRespWithObj<Gstr2Json> txnResp = new TxnRespWithObj<Gstr2Json>();
                txnResp = await GSTR2API.GetGstr2ADataScheduleAsync(GstSession, "B2B", "Get " + "B2B" + " Data", KontoGlobals.GstIn , textEdit1.Text, "", "", "", true, "", "Response.json");

                if (File.Exists("Response.json"))
                {
                    var gstdumps = new List<Gstr2ADump>();

                    using (var db = new KontoContext())
                    {
                        

                        foreach (var item in txnResp.RespObj.b2b)
                        {

                            foreach (var inv in item.inv)
                            {

                                var bm = new Gstr2ADump();
                                var bts = new List<Gstr2ATransDump>();
                                bm.FileDate = Convert.ToDateTime(item.fldtr1);
                                bm.FilePeriod = item.flprdr1;
                                bm.GstIn = item.ctin;
                                bm.InvoiceDate = inv.idt;
                                bm.InvoiceNo = inv.inum;
                                bm.InvoiceValue = Convert.ToDecimal(inv.val);
                                bm.Pos = inv.pos;
                                bm.TransType = "B2B";
                                bm.FPrd = textEdit1.Text.Trim();
                                bm.CompId = KontoGlobals.CompanyId;
                                bm.YearId = KontoGlobals.YearId;
                                bm.CreateDate = DateTime.Now;
                                bm.Is2B = false;
                                foreach (var det in inv.itms)
                                {
                                    var bs = new Gstr2ATransDump();
                                    bs.Cess = Convert.ToDecimal(det.itm_det.csamt);
                                    bs.Cgst = Convert.ToDecimal(det.itm_det.camt);
                                    bs.Igst = Convert.ToDecimal(det.itm_det.iamt);
                                    bs.Sgst = Convert.ToDecimal(det.itm_det.samt);
                                    bs.Taxable = Convert.ToDecimal(det.itm_det.txval);
                                    bs.TaxRate = Convert.ToDecimal(det.itm_det.rt);
                                    bts.Add(bs);
                                }

                                bm.Cess = bts.Sum(x => x.Cess);
                                bm.Cgst = bts.Sum(x => x.Cgst);
                                bm.Igst = bts.Sum(x => x.Igst);
                                bm.Sgst = bts.Sum(x => x.Sgst);
                                bm.Taxable = bts.Sum(x => x.Taxable);
                                bm.TaxRate = bts.FirstOrDefault().TaxRate;
                                bm.Gstr2aTrans = bts;

                                var bill = (from p in db.Bills
                                            join ac in db.Accs on p.AccId equals ac.Id
                                            where p.BillNo == bm.InvoiceNo && ac.GstIn == bm.GstIn
                                            && (p.TotalAmount == bm.InvoiceValue || p.TotalAmount - bm.InvoiceValue <= (decimal)0.5)
                                            && p.VoucherDate>= KontoGlobals.FromDate && p.VoucherDate<=KontoGlobals.ToDate
                                            && p.CompId == KontoGlobals.CompanyId && !p.IsDeleted && p.IsActive
                                            select p).FirstOrDefault();

                                if (bill != null)
                                    bm.Billid = bill.Id;

                                gstdumps.Add(bm);
                            }
                        }

                        //var dmp = gstdumps[0];
                        if (gstdumps.Count > 0)
                        {
                            var prd = gstdumps[0].FilePeriod;
                            var _exist = db.Gstr2ADumps.Where(x => x.FilePeriod == prd && !x.Is2B).ToList();

                            if (_exist.Count > 0)
                            {
                                db.Gstr2ADumps.RemoveRange(_exist);
                            }


                            db.Gstr2ADumps.AddRange(gstdumps);
                            db.SaveChanges();

                            gstr2ADumpBindingSource.DataSource = gstdumps;
                            gridControl1.RefreshDataSource();

                            GetMatchData(db,intprd,0);
                        }

                       
                    }
                   

                    
                }
                else
                    MessageBox.Show( txnResp.TxnOutcome);

                splashScreenManager1.CloseWaitForm();

            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.Message);
                
            }
        }
        private void GetMatchData(KontoContext _db, string prd, int is2B)
        {
            var fdate = KontoGlobals.FromDate.ToString();
            var tdate = KontoGlobals.ToDate.ToString();

            if (!string.IsNullOrEmpty(prd))
            {
                var yr = Convert.ToInt32(textEdit1.Text.Substring(2));
                var mon = Convert.ToInt32(textEdit1.Text.Substring(0, 2));
                var lstday = DateTime.DaysInMonth(yr, mon);

                 fdate = yr.ToString() + textEdit1.Text.Substring(0, 2) + "01";
                 tdate = yr.ToString() + textEdit1.Text.Substring(0, 2) + lstday.ToString();
            }
            else
            {
                prd = "0";
            }
            

            _db.Database.CommandTimeout = 0;

            var lst = _db.Database.SqlQuery<RecoGstr2BtoBDto>(
                   "dbo.gstr2a_reco @fromdate={0},@todate={1},@compid={2}," +
                   "@match={3},@prd={4},@is2b={5}", fdate,tdate,
                   Convert.ToInt32(KontoGlobals.CompanyId), 
                   'Y',prd,is2B).ToList();

            mGridControl.DataSource = lst;


            var lstun = _db.Database.SqlQuery<RecoGstr2aUnMatch>(
                  "dbo.gstr2a_reco @fromdate={0},@todate={1},@compid={2}," +
                  "@match={3}", fdate, tdate,
                  Convert.ToInt32(KontoGlobals.CompanyId),
                  'N').ToList();

            umGridControl.DataSource = lstun;

            var _dupms= gstr2ADumpBindingSource.DataSource as List<Gstr2ADump>;
            var _extra = _dupms.Where(x => x.Billid == 0).ToList();
            eGridControl.DataSource = _extra;

        }

        private void GetUnmatchData(KontoContext _db)
        {
            var yr = Convert.ToInt32(textEdit1.Text.Substring(2));
            var mon = Convert.ToInt32(textEdit1.Text.Substring(0, 2));
            var lstday = DateTime.DaysInMonth(yr, mon);

            var fdate = yr.ToString() + textEdit1.Text.Substring(0, 2) + "01";
            var tdate = yr.ToString() + textEdit1.Text.Substring(0, 2) + lstday.ToString();

            var lstun = _db.Database.SqlQuery<RecoGstr2aUnMatch>(
                "dbo.gstr2a_reco @fromdate={0},@todate={1},@compid={2}," +
                "@match={3}", fdate, tdate,
                Convert.ToInt32(KontoGlobals.CompanyId),
                'N').ToList();

            umGridControl.DataSource = lstun;
        }

        private void authSimpleButton_Click(object sender, EventArgs e)
        {
            var frm = new frmAuthToken();
            frm.ShowDialog();
        }

        private void reco2BSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textEdit1.Text))
                {
                    MessageBox.Show("Enter Period In MMYYYY(122020) Format");
                    textEdit1.Focus();
                    return;
                }
                var intprd = textEdit1.Text;
               
                    var yr = Convert.ToInt32(textEdit1.Text.Substring(2));
                    var mon = Convert.ToInt32(textEdit1.Text.Substring(0, 2));
                    var lstday = DateTime.DaysInMonth(yr, mon);

                    var fdate = Convert.ToInt32(yr.ToString() + textEdit1.Text.Substring(0, 2) + "01");
                    var tdate = Convert.ToInt32(yr.ToString() + textEdit1.Text.Substring(0, 2) + lstday.ToString());
                

               

                splashScreenManager1.ShowWaitForm();

                using (var db = new KontoContext())
                {
                    var gstrdumps = db.Gstr2ADumps.Where(x => x.FPrd==intprd &&  x.TransType == "B2B" && x.Is2B && x.CompId == KontoGlobals.CompanyId).ToList();

                    foreach (var item in gstrdumps)
                    {
                        var bill = (from p in db.Bills
                                    join ac in db.Accs on p.AccId equals ac.Id
                                    where p.BillNo == item.InvoiceNo && ac.GstIn == item.GstIn
                                    && (p.TotalAmount == item.InvoiceValue || p.TotalAmount - item.InvoiceValue <= (decimal)0.5)
                                    && p.VoucherDate >= fdate && p.VoucherDate <= tdate
                                    && p.CompId == KontoGlobals.CompanyId && !p.IsDeleted && p.IsActive
                                    select p).FirstOrDefault();

                        item.Billid = 0;
                        if (bill != null)
                            item.Billid = bill.Id;
                    }

                    db.SaveChanges();

                    gstr2ADumpBindingSource.DataSource = gstrdumps;
                    gridControl1.RefreshDataSource();

                    GetMatchData(db, intprd, 1);


                }
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {

                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.ToString());
                Log.Error(ex, "Gstr2a_Reconicle");
            }
        }
    }
}
