using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Serilog;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Shared.Utils
{
    public partial class UpdateLedgerView : KontoForm
    {
        public UpdateLedgerView()
        {
            InitializeComponent();
            this.Load += UpdateLedgerView_Load;
            this.SaveSimpleButton.Click += SaveSimpleButton_Click;
            this.fromDateEdit.EditValue = DateTime.Now;
            this.toDateEdit.EditValue = DateTime.Now;
        }

        private void SaveSimpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                int fdate = Convert.ToInt32(fromDateEdit.DateTime.ToString("yyyyMMdd"));
                int tdate = Convert.ToInt32(toDateEdit.DateTime.ToString("yyyyMMdd"));
                if (typeLookUpEdit.EditValue.ToString() ==Convert.ToInt32(VoucherTypeEnum.SaleInvoice).ToString())
                {
                    using (var db = new KontoContext())
                    {
                        var intpro = 0;

                        splashScreenManager1.ShowWaitForm();
                        var blls = db.Bills.Where(x => x.TypeId == (int)VoucherTypeEnum.SaleInvoice
                        && x.VoucherDate >= fdate && x.VoucherDate <= tdate && !x.IsDeleted).ToList();
                        using(var _trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var model in blls)
                                {
                                    intpro++;

                                    splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, blls.Count));

                                    var _translist = db.BillTrans.Where(x => x.BillId == model.Id && !x.IsDeleted).ToList();

                                    model.GrossAmount = _translist.Sum(x => x.NetTotal) - _translist.Sum(x => x.Cgst) - _translist.Sum(x => x.Sgst) -
                                           _translist.Sum(x => x.Igst) - _translist.Sum(x => x.Cess);

                                    var ntotal = _translist.Sum(x => x.NetTotal);

                                    var x1 = ntotal - Math.Truncate(ntotal);
                                    bool isEven = false;
                                    if (x1 == (decimal)0.5)
                                    {
                                        ntotal = ntotal + (decimal)0.01;
                                        isEven = true;
                                    }

                                    var round = decimal.Round(ntotal, 0) - ntotal;
                                    if (isEven)
                                    {
                                        round = (decimal)0.5;
                                        ntotal = ntotal + (decimal)0.49;
                                    }
                                    else
                                    {
                                        ntotal = ntotal + round;
                                    }

                                    model.RoundOff = round;
                                    model.TotalAmount = ntotal;

                                    LedgerEff.BillRefEntry("Debit", model, 0, db);       //Insert or update in Billref table

                                    //Insert or update in LedgerTrans table
                                    LedgerEff.LedgerTransEntry("Debit", model, db, _translist);
                                }

                                db.SaveChanges();
                                _trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                _trans.Rollback();
                                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                Log.Error(ex, "Ledger Update");
                                MessageBox.Show(ex.ToString());
                            }
                        }
                     
                    }
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Completed Posting!!!");
                }

                if (typeLookUpEdit.EditValue.ToString() == Convert.ToInt32(VoucherTypeEnum.PurchaseInvoice).ToString())
                {
                    using (var db = new KontoContext())
                    {
                        var intpro = 0;

                        splashScreenManager1.ShowWaitForm();
                        var blls = db.Bills.Where(x => x.TypeId == (int)VoucherTypeEnum.PurchaseInvoice
                        && x.VoucherDate >= fdate && x.VoucherDate <= tdate && !x.IsDeleted).ToList();
                        using (var _trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var model in blls)
                                {
                                    intpro++;

                                    splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, blls.Count));

                                    var _translist = db.BillTrans.Where(x => x.BillId == model.Id && !x.IsDeleted).ToList();

                                    model.GrossAmount = _translist.Sum(x => x.NetTotal) - _translist.Sum(x => x.Cgst) - _translist.Sum(x => x.Sgst) -
                                           _translist.Sum(x => x.Igst) - _translist.Sum(x => x.Cess);

                                    var ntotal = _translist.Sum(x => x.NetTotal);

                                    var x1 = ntotal - Math.Truncate(ntotal);
                                    bool isEven = false;
                                    if (x1 == (decimal)0.5)
                                    {
                                        ntotal = ntotal + (decimal)0.01;
                                        isEven = true;
                                    }

                                    var round = decimal.Round(ntotal, 0) - ntotal;
                                    if (isEven)
                                    {
                                        round = (decimal)0.5;
                                        ntotal = ntotal + (decimal)0.49;
                                    }
                                    else
                                    {
                                        ntotal = ntotal + round;
                                    }

                                    model.RoundOff = round;
                                    model.TotalAmount = ntotal;

                                    LedgerEff.BillRefEntry("Credit", model, 0, db);       //Insert or update in Billref table

                                    //Insert or update in LedgerTrans table
                                    LedgerEff.LedgerTransEntry("Credit", model, db, _translist);
                                }

                                db.SaveChanges();
                                _trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                _trans.Rollback();
                                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                Log.Error(ex, "Ledger Update");
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Completed Posting!!!");
                }

                if (typeLookUpEdit.EditValue.ToString() == Convert.ToInt32(VoucherTypeEnum.GenExpense).ToString())
                {
                    using (var db = new KontoContext())
                    {
                        var intpro = 0;

                        splashScreenManager1.ShowWaitForm();
                        var blls = db.Bills.Where(x => x.TypeId == (int)VoucherTypeEnum.GenExpense
                        && x.VoucherDate >= fdate && x.VoucherDate <= tdate && !x.IsDeleted).ToList();
                        using (var _trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var model in blls)
                                {
                                    intpro++;

                                    splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, blls.Count));

                                    var _translist = db.BillTrans.Where(x => x.BillId == model.Id && !x.IsDeleted).ToList();

                                    model.GrossAmount = _translist.Sum(x => x.NetTotal) - _translist.Sum(x => x.Cgst) - _translist.Sum(x => x.Sgst) -
                                           _translist.Sum(x => x.Igst) - _translist.Sum(x => x.Cess);

                                    var ntotal = _translist.Sum(x => x.NetTotal);

                                    var x1 = ntotal - Math.Truncate(ntotal);
                                    bool isEven = false;
                                    if (x1 == (decimal)0.5)
                                    {
                                        ntotal = ntotal + (decimal)0.01;
                                        isEven = true;
                                    }

                                    var round = decimal.Round(ntotal, 0) - ntotal;
                                    if (isEven)
                                    {
                                        round = (decimal)0.5;
                                        ntotal = ntotal + (decimal)0.49;
                                    }
                                    else
                                    {
                                        ntotal = ntotal + round;
                                    }

                                    model.RoundOff = round;
                                    model.TotalAmount = ntotal;

                                    LedgerEff.BillRefEntry("Credit", model, 0, db);       //Insert or update in Billref table

                                    //Insert or update in LedgerTrans table
                                    LedgerEff.LedgerTransEntry("Credit", model, db, _translist);
                                }

                                db.SaveChanges();
                                _trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                _trans.Rollback();
                                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                Log.Error(ex, "Ledger Update");
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Completed Posting!!!");
                }


                if (typeLookUpEdit.EditValue.ToString() == Convert.ToInt32(VoucherTypeEnum.SaleReturn).ToString())
                {
                    using (var db = new KontoContext())
                    {
                        var intpro = 0;

                        splashScreenManager1.ShowWaitForm();
                        var blls = db.Bills.Where(x => x.TypeId == (int)VoucherTypeEnum.SaleReturn
                        && x.VoucherDate >= fdate && x.VoucherDate <= tdate && !x.IsDeleted).ToList();
                        using (var _trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var model in blls)
                                {
                                    intpro++;

                                    splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, blls.Count));

                                    var _translist = db.BillTrans.Where(x => x.BillId == model.Id && !x.IsDeleted).ToList();

                                    model.GrossAmount = _translist.Sum(x => x.NetTotal) - _translist.Sum(x => x.Cgst) - _translist.Sum(x => x.Sgst) -
                                           _translist.Sum(x => x.Igst) - _translist.Sum(x => x.Cess);

                                    var ntotal = _translist.Sum(x => x.NetTotal);

                                    var x1 = ntotal - Math.Truncate(ntotal);
                                    bool isEven = false;
                                    if (x1 == (decimal)0.5)
                                    {
                                        ntotal = ntotal + (decimal)0.01;
                                        isEven = true;
                                    }

                                    var round = decimal.Round(ntotal, 0) - ntotal;
                                    if (isEven)
                                    {
                                        round = (decimal)0.5;
                                        ntotal = ntotal + (decimal)0.49;
                                    }
                                    else
                                    {
                                        ntotal = ntotal + round;
                                    }

                                    model.RoundOff = round;
                                    model.TotalAmount = ntotal;

                                    LedgerEff.BillRefEntry("Credit", model, 0, db);       //Insert or update in Billref table

                                    //Insert or update in LedgerTrans table
                                    LedgerEff.LedgerTransEntry("Credit", model, db, _translist);
                                }

                                db.SaveChanges();
                                _trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                _trans.Rollback();
                                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                Log.Error(ex, "Ledger Update");
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Completed Posting!!!");
                }


                if (typeLookUpEdit.EditValue.ToString() == Convert.ToInt32(VoucherTypeEnum.PaymentVoucher).ToString())
                {
                    using (var db = new KontoContext())
                    {
                        var intpro = 0;

                        splashScreenManager1.ShowWaitForm();
                        var blls = db.Bills.Where(x => x.TypeId == (int)VoucherTypeEnum.PaymentVoucher
                        && x.VoucherDate >= fdate && x.VoucherDate <= tdate && !x.IsDeleted).ToList();
                        
                        using (var _trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var model in blls)
                                {
                                    intpro++;

                                    splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, blls.Count));

                                    var _translist = db.BillTrans.Where(x => x.BillId == model.Id && !x.IsDeleted).ToList();


                                    LedgerEff.BillRefEntrypayrec("Debit", model, _translist,new List<Data.Models.Transaction.Dtos.BankTransDto>(), db);
                                    //Insert or update in LedgerTrans table
                                    LedgerEff.LedgerTransEntryRecPay("Debit", model, db, _translist, new List<Data.Models.Transaction.Dtos.PendBillListDto>());
                                }

                                db.SaveChanges();
                                _trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                _trans.Rollback();
                                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                Log.Error(ex, "Ledger Update");
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Completed Posting!!!");
                }

                if (typeLookUpEdit.EditValue.ToString() == Convert.ToInt32(VoucherTypeEnum.ReceiptVoucher).ToString())
                {
                    using (var db = new KontoContext())
                    {
                        var intpro = 0;

                        splashScreenManager1.ShowWaitForm();
                        var blls = db.Bills.Where(x => x.TypeId == (int)VoucherTypeEnum.ReceiptVoucher
                        && x.VoucherDate >= fdate && x.VoucherDate <= tdate && !x.IsDeleted).ToList();

                        using (var _trans = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var model in blls)
                                {
                                    intpro++;

                                    splashScreenManager1.SetWaitFormDescription(string.Format("Completed {0} of {1}", intpro, blls.Count));

                                    var _translist = db.BillTrans.Where(x => x.BillId == model.Id && !x.IsDeleted).ToList();


                                    LedgerEff.BillRefEntrypayrec("Credit", model, _translist, new List<Data.Models.Transaction.Dtos.BankTransDto>(), db);
                                    //Insert or update in LedgerTrans table
                                    LedgerEff.LedgerTransEntryRecPay("Credit", model, db, _translist, new List<Data.Models.Transaction.Dtos.PendBillListDto>());
                                }

                                db.SaveChanges();
                                _trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                _trans.Rollback();
                                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                                Log.Error(ex, "Ledger Update");
                                MessageBox.Show(ex.ToString());
                            }
                        }

                    }
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Completed Posting!!!");
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                Log.Error(ex, "Ledger Update");
                MessageBox.Show(ex.ToString());
                
            }
        }

        private void UpdateLedgerView_Load(object sender, EventArgs e)
        {
            using(var db  = new KontoContext())
            {

                var vtype = (from p in db.VoucherTypes
                             where !p.IsDeleted && p.IsActive
                             orderby p.TypeName
                             select new BaseLookupDto()
                             {
                                 DisplayText = p.TypeName,
                                 Id = p.Id
                             }).ToList();

                typeLookUpEdit.Properties.DataSource = vtype;
            }
        }
    }
}
