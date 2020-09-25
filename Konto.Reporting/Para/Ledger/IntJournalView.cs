using DevExpress.XtraGrid.Views.Grid;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Reports;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Reporting.Para.Ledger
{
    public partial class IntJournalView : KontoForm
    {
        public int [] Rows { get; set; }
        public GridView IntGridView { get; set; }
        public List<LedgertransDto> Trans { get; set; }
        public decimal InterestPer { get; set; }
        public IntJournalView()
        {
            InitializeComponent();
            this.okSimpleButton.Click += OkSimpleButton_Click;
            voucherDateEdit.EditValue = KontoGlobals.DToDate;
            voucherLookup1.SetDefault();
            
        }


        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            if(interestLookup.SelectedValue == null || Convert.ToInt32(interestLookup.SelectedValue) == 0)
            {
                MessageBox.Show("Please Select Interest Account..");
                interestLookup.Focus();
                return;
            }
            if(tdsPerSpinEdit.Value>0 && Convert.ToInt32(tdsLookup.SelectedValue) == 0)
            {
                MessageBox.Show("Please Select Tds Account..");
                tdsLookup.Focus();
                return;
            }
            List<LedgertransDto> _temp = new List<LedgertransDto>();
            foreach (var item in this.Rows)
            {
                if (this.IntGridView.IsGroupRow(item)) continue;
                var rw = this.IntGridView.GetRow(item) as LedgertransDto;
                if (rw != null)
                    _temp.Add(rw);
            }

            var grps = (from p in _temp
                       group p by p.AccountId into g
                       select new { AccId = g.Key }).ToList();

            using(var db = new KontoContext())
            {
                using (var _tr = db.Database.BeginTransaction())
                {
                    try
                    {
                        //check for previus interest posting entry

                        var _exist = db.Bills.Where(x => x.OceanFrtP == 999).ToList();
                        foreach (var item in _exist)
                        {
                            var bts = db.BillTrans.Where(x => x.BillId == item.Id).ToList();
                            foreach (var item1 in bts)
                            {
                                item1.IsDeleted = true;
                            }
                            item.IsDeleted = true;
                            LedgerEff.DeleLedgEffect(item, db);
                        }
                      

                        foreach (var grp in grps)
                        {
                            var JVModel = new BillModel();

                            JVModel.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

                            decimal totlDebit = Trans.Where(k => k.AccountId == grp.AccId).Sum(k => k.DebitInt)
                                           != null ? (decimal)Trans.Where(k => k.AccountId == grp.AccId).Sum(k => k.DebitInt) : 0;
                            decimal totlCredit = Trans.Where(k => k.AccountId == grp.AccId).Sum(k => k.CreditInt)
                                != null ? (decimal)Trans.Where(k => k.AccountId == grp.AccId).Sum(k => k.CreditInt) : 0;

                            decimal totlAmt = totlDebit - totlCredit;

                            if (totlAmt < 0)
                                totlAmt = totlAmt * -1;

                            if (intRoundcheckEdit.CheckState == CheckState.Checked)
                            {
                                JVModel.GrossAmount = Decimal.Round(Convert.ToDecimal(totlAmt), 0);
                                JVModel.TotalAmount = Decimal.Round(Convert.ToDecimal(totlAmt), 0);
                                totlAmt = decimal.Round(Convert.ToDecimal(totlAmt), 0);
                            }
                            else
                            {
                                JVModel.GrossAmount = decimal.Round(totlAmt, 2, MidpointRounding.AwayFromZero);
                                JVModel.TotalAmount = decimal.Round(totlAmt, 2, MidpointRounding.AwayFromZero);
                                totlAmt = decimal.Round(Convert.ToDecimal(totlAmt), 2,MidpointRounding.AwayFromZero);
                            }

                            JVModel.CompId = KontoGlobals.CompanyId;
                            JVModel.YearId = KontoGlobals.YearId;
                            JVModel.BranchId = KontoGlobals.BranchId;
                            JVModel.TypeId = (int)VoucherTypeEnum.JournalVoucher;
                            JVModel.TotalQty = 0;
                            JVModel.OceanFrtP = 999;
                            if (JVModel.Id == 0)
                            {
                                JVModel.AccId = 1;
                                JVModel.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);

                                JVModel.VoucherNo = DbUtils.NextSerialNo(JVModel.VoucherId, db);
                                JVModel.CreateUser = KontoGlobals.UserName;
                                db.Bills.Add(JVModel);
                                db.SaveChanges();
                            }
                            
                            var BTrans = new List<BillTransModel>();

                            for (int i = 0; i < 2; i++)
                            {
                                var btModel = new BillTransModel();

                                btModel.BillId = JVModel.Id;
                                btModel.Rate = 0;
                                btModel.Remark = "Interest @" + InterestPer.ToString("F") + "%";
                                if (i == 1)
                                {
                                    btModel.ToAccId = grp.AccId;
                                    if (crDrRadioGroup.SelectedIndex ==0)
                                    {
                                        btModel.Total = totlAmt;//credit
                                    }
                                    else
                                    {
                                        btModel.NetTotal = totlAmt;//debit
                                    }
                                }
                                else
                                {
                                    btModel.ToAccId = Convert.ToInt32(interestLookup.SelectedValue);
                                    if (crDrRadioGroup.SelectedIndex == 1)
                                    {
                                        btModel.Total = totlAmt;//credit
                                    }
                                    else
                                    {
                                        btModel.NetTotal = totlAmt;//debit
                                    }
                                }

                                btModel.RpType = "On Account";
                                btModel.CreateUser = KontoGlobals.UserName;

                                BTrans.Add(btModel);
                            }
                            db.BillTrans.AddRange(BTrans);
                            db.SaveChanges();
                            var DelTrans = new List<BankTransDto>();

                            LedgerEff.BillRefEntryJv(JVModel, BTrans, DelTrans, db);

                            //Insert or update in LedgerTrans table
                            LedgerEff.LedgerTransEntryJv(KontoGlobals.UserName, JVModel, db,BTrans);

                        }

                        if(tdsPerSpinEdit.Value > 0)
                        {
                            foreach (var grp in grps)
                            {
                                var JVModel = new BillModel();

                                JVModel.VoucherDate = Convert.ToInt32(voucherDateEdit.DateTime.ToString("yyyyMMdd"));

                                decimal totlDebit = Trans.Where(k => k.AccountId == grp.AccId).Sum(k => k.DebitInt)
                                               != null ? (decimal)Trans.Where(k => k.AccountId == grp.AccId).Sum(k => k.DebitInt) : 0;
                                decimal totlCredit = Trans.Where(k => k.AccountId == grp.AccId).Sum(k => k.CreditInt)
                                    != null ? (decimal)Trans.Where(k => k.AccountId == grp.AccId).Sum(k => k.CreditInt) : 0;

                                 decimal totlAmt = ((totlDebit - totlCredit) * tdsPerSpinEdit.Value) / 100;

                                if (totlAmt < 0)
                                    totlAmt = totlAmt * -1;

                                if (tdsRoundCheckEdit.CheckState == CheckState.Checked)
                                {
                                    JVModel.GrossAmount = decimal.Round(Convert.ToDecimal(totlAmt), 0);
                                    JVModel.TotalAmount = decimal.Round(Convert.ToDecimal(totlAmt), 0);
                                    totlAmt = decimal.Round(Convert.ToDecimal(totlAmt), 0);
                                }
                                else
                                {
                                    JVModel.GrossAmount = decimal.Round(totlAmt, 2, MidpointRounding.AwayFromZero);
                                    JVModel.TotalAmount = decimal.Round(totlAmt, 2, MidpointRounding.AwayFromZero);
                                    totlAmt = decimal.Round(Convert.ToDecimal(totlAmt), 2, MidpointRounding.AwayFromZero);
                                }

                                JVModel.CompId = KontoGlobals.CompanyId;
                                JVModel.YearId = KontoGlobals.YearId;
                                JVModel.BranchId = KontoGlobals.BranchId;
                                JVModel.TypeId = (int)VoucherTypeEnum.JournalVoucher;
                                JVModel.TotalQty = 0;
                                JVModel.OceanFrtP = 999;
                                if (JVModel.Id == 0)
                                {
                                    JVModel.AccId = 1;
                                    JVModel.VoucherId = Convert.ToInt32(voucherLookup1.SelectedValue);

                                    JVModel.VoucherNo = DbUtils.NextSerialNo(JVModel.VoucherId, db);
                                    JVModel.CreateUser = KontoGlobals.UserName;
                                    db.Bills.Add(JVModel);
                                    db.SaveChanges();
                                }

                                var BTrans = new List<BillTransModel>();

                                for (int i = 0; i < 2; i++)
                                {
                                    var btModel = new BillTransModel();

                                    btModel.BillId = JVModel.Id;
                                    btModel.Rate = 0;
                                    btModel.Remark = "Tds @" + tdsPerSpinEdit.Value.ToString("F") + "%";
                                    if (i == 1)
                                    {
                                        btModel.ToAccId = grp.AccId;
                                        if (crDrRadioGroup.SelectedIndex == 1)
                                        {
                                            btModel.Total = totlAmt;//credit
                                        }
                                        else
                                        {
                                            btModel.NetTotal = totlAmt;//debit
                                        }
                                    }
                                    else
                                    {
                                        btModel.ToAccId = Convert.ToInt32(tdsLookup.SelectedValue);
                                        if (crDrRadioGroup.SelectedIndex == 0)
                                        {
                                            btModel.Total = totlAmt;//credit
                                        }
                                        else
                                        {
                                            btModel.NetTotal = totlAmt;//debit
                                        }
                                    }

                                    btModel.RpType = "On Account";
                                    btModel.CreateUser = KontoGlobals.UserName;

                                    BTrans.Add(btModel);
                                }
                                db.BillTrans.AddRange(BTrans);
                                db.SaveChanges();
                                var DelTrans = new List<BankTransDto>();

                                LedgerEff.BillRefEntryJv(JVModel, BTrans, DelTrans, db);

                                //Insert or update in LedgerTrans table
                                LedgerEff.LedgerTransEntryJv(KontoGlobals.UserName, JVModel, db, BTrans);

                            }
                        }

                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _tr.Rollback();
                        MessageBox.Show(ex.ToString());
                        
                    }
                    _tr.Commit();
                    MessageBox.Show("Interest/Tds Journal Posted Successfully");
                }
            }
        }
    }
}
