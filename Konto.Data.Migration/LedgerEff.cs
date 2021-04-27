using Konto.App.Shared;
using Konto.Data.Models.Pos;
using Konto.Data.Models.Transaction;
using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data
{
    public class LedgerEff
    {
        //properties

        //Delete Ledger effect and outstanding effect
        public static void DeleteAttachment(int id, int vid, KontoContext db)
        {
            var AttachList = db.Attachments.Where(x => x.RefVoucherId == id && x.VoucherId == vid).ToList();
            foreach (AttachmentModel atc in AttachList)
            {
                atc.IsDeleted = true;
            }
        }
        public static void BtoBEntryUpload(string type, int id, BillModel model, KontoContext db, BillModel OrgBill, Guid BillRefId)
        {
            //Delete from Btob Table
            var billDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.IsActive && k.IsDeleted == false).ToList();
            if (billDel.Count > 0)
            {
                foreach (var bld in billDel)
                {
                    BtoBModel bil = db.BtoBs.FirstOrDefault(k => k.RefId == bld.RefId && k.RefVoucherId == bld.RefVoucherId && k.IsActive && k.IsDeleted == false);
                    if (bil != null)
                    {
                        db.BtoBs.Remove(bil);
                    }
                }
            }
            //Insert in Btob table
            if (model.TotalAmount != 0)
            {
                BtoBModel bill = new BtoBModel
                {
                    BillNo = OrgBill.VoucherNo,
                    Amount = model.TotalAmount,
                    RefCode = BillRefId,
                    BillId = OrgBill.Id,
                    BillVoucherId = OrgBill.VoucherId,
                    BillTransId = OrgBill.Id,
                    TransType = "Return",
                    CompanyId = KontoGlobals.CompanyId,

                    RefId = model.Id,
                    RefTransId = model.Id,
                    RefVoucherId = model.VoucherId,
                    CreateDate = DateTime.Now,
                    CreateUser = KontoGlobals.UserName
                };
                db.BtoBs.Add(bill);
            }
        }

        public static void DeleLedgEffect(BillModel model, KontoContext db)
        {
            //Update Bref Summary of BtoB
            var billDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.IsActive && k.IsDeleted == false).ToList();

            if (billDel.Count > 0)
            {
                db.BtoBs.RemoveRange(billDel);
            }

            //Delete from LedgerTrans
            var dl = db.Ledgers.Where(k => k.RefId == model.RowId && k.IsActive && k.IsDeleted == false).ToList();
            if (dl.Count > 0)
            {
                db.Ledgers.RemoveRange(dl);
                db.SaveChanges();
                foreach (var item in dl)
                {
                    UpdateBalance(Convert.ToInt32(item.AccountId), db);
                }
            }

            //Delete from BillReference table
            var bref = db.BillRefs.Where(k => k.BillId == model.Id && k.BillVoucherId == model.VoucherId && k.IsActive && k.IsDeleted == false).ToList();
            if (bref.Count > 0)
            {
                db.BillRefs.RemoveRange(bref);
            }

        }

        /////// Bill reference entry for Invoice/return/Debit note credit note/GenExpense
        public static void BillRefEntry(string type, BillModel modl, int productId, KontoContext db)
        {

            BillRefModel st = db.BillRefs.FirstOrDefault(k => k.BillId == modl.Id && k.IsActive && k.IsDeleted == false);

            if (st != null) //Update BillRef table
            {

                st.ItemId = productId;


                // st.BillId = modl.Id;
                st.BillTransId = modl.Id;
                st.CompanyId = modl.CompId;
                st.YearId = modl.YearId;
                st.AgentId = modl.AgentId;
                st.AccountId = modl.AccId;
                if (modl.TypeId != (int)VoucherTypeEnum.SalePurchaseOpBill)
                    st.GrossAmt = modl.GrossAmount;
                else
                {
                    st.AdjustAmt = modl.GrossAmount;
                    st.RetAmt = modl.TotalPcs;
                }
                st.BillAmt = modl.TotalAmount;
                st.TdsAmt = modl.TdsAmt;
                st.TcsAmt = modl.TcsAmt;
                st.TotalQty = modl.TotalQty;

                st.BranchId = modl.BranchId;
                st.BillVoucherId = modl.VoucherId;

                if (modl.BillNo == null)
                    st.BillNo = modl.VoucherNo;
                else if (modl.TypeId == (int)VoucherTypeEnum.SaleInvoice)
                    st.BillNo = modl.VoucherNo;
                else
                    st.BillNo = modl.BillNo;

                st.VoucherNo = modl.VoucherNo;
                st.VoucherDate = modl.VoucherDate;

                st.RefType = type;
                st.Remarks = modl.Remarks;

            }
            else // Insert Billref table
            {
                var billr = new BillRefModel();

                billr.ItemId = productId;


                billr.BillId = modl.Id;
                billr.BillTransId = modl.Id;
                billr.CompanyId = modl.CompId;
                billr.YearId = modl.YearId;
                billr.AgentId = modl.AgentId;
                billr.AccountId = modl.AccId;
                billr.GrossAmt = modl.GrossAmount;
                billr.BillAmt = modl.TotalAmount + modl.TcsAmt;
                billr.TdsAmt = modl.TdsAmt;
                billr.TcsAmt = modl.TcsAmt;
                billr.TotalQty = modl.TotalQty;

                billr.BranchId = modl.BranchId;
                billr.BillVoucherId = modl.VoucherId;

                if (modl.BillNo == null)
                    billr.BillNo = modl.VoucherNo;
                else if (modl.TypeId == (int)VoucherTypeEnum.SaleInvoice)
                    billr.BillNo = modl.VoucherNo;
                else
                    billr.BillNo = modl.BillNo;

                billr.VoucherNo = modl.VoucherNo;
                billr.VoucherDate = modl.VoucherDate;

                billr.RefType = type;
                billr.Remarks = modl.Remarks;
                db.BillRefs.Add(billr);
            }

        }

        /////////////////////////////////// Bill reference entry for recpay
        public static void BillRefEntrypayrec(string type, BillModel model, List<BillTransModel> trans, List<BankTransDto> delTrans, KontoContext db)
        {
            foreach (var tr in delTrans)
            {
                var tran = db.BillRefs.Where(k => k.BillId == model.Id && k.BillTransId == tr.Id && k.IsActive && k.IsDeleted == false).ToList();
                if (tran.Count > 0)
                {
                    db.BillRefs.RemoveRange(tran);
                }
                db.SaveChanges();
            }

            foreach (var t in trans)
            {
                BillRefModel st = db.BillRefs.FirstOrDefault(k => k.BillId == model.Id && k.BillTransId == t.Id && k.IsActive && k.IsDeleted == false);

                if (st != null) //Update BillRef table
                {
                    st.BillId = model.Id;
                    st.BillTransId = t.Id;
                    st.CompanyId = model.CompId;
                    st.YearId = model.YearId;

                    st.AccountId = t.ToAccId;
                    st.GrossAmt = t.NetTotal;

                   st.BillAmt = t.NetTotal;
                    st.BillVoucherId = model.VoucherId;
                    st.BillNo = model.VoucherNo;
                    st.VoucherNo = model.VoucherNo;
                    st.VoucherDate = model.VoucherDate;
                    //advance payment tds
                    if (model.TypeId == (int)VoucherTypeEnum.PaymentVoucher && t.TdsAmt > 0 && t.TdsAcId > 0)
                        st.TdsAmt = -1 * t.TdsAmt;

                    st.RefType = type;
                    st.Remarks = model.Remarks;


                   // db.Entry(st).CurrentValues.SetValues(st);
                }
                else // Insert Billref table
                {
                    st = new BillRefModel();
                    st.BillId = model.Id;
                    st.BillTransId = t.Id;
                    st.CompanyId = model.CompId;
                    st.YearId = model.YearId;

                    st.AccountId = t.ToAccId;
                    st.GrossAmt = t.NetTotal;

                   // --if (t.TdsAmt > 0 && t.TdsAcId > 0)
                      // -- st.BillAmt = t.NetTotal + t.TdsAmt;
                   // else
                    st.BillAmt = t.NetTotal;

                    st.BillVoucherId = model.VoucherId;
                    st.BillNo = model.VoucherNo;
                    st.VoucherNo = model.VoucherNo;
                    st.VoucherDate = model.VoucherDate;

                    // advanced payment tds
                    if (model.TypeId == (int)VoucherTypeEnum.PaymentVoucher && t.TdsAmt > 0 && t.TdsAcId > 0)
                        st.TdsAmt = -1 * t.TdsAmt;

                    st.RefType = type;
                    st.Remarks = model.Remarks;
                    db.BillRefs.Add(st);
                }
            }


        }

        /////////////////////////////////// Bill reference entry for Jv
        public static void BillRefEntryJv(BillModel model, List<BillTransModel> trans, List<BankTransDto> delTrans, KontoContext db)
        {
            foreach (var tr in delTrans)
            {
                var tran = db.BillRefs.Where(k => k.BillId == model.Id && k.BillTransId == tr.Id && k.IsActive && k.IsDeleted == false).ToList();
                if (tran.Count > 0)
                {
                    db.BillRefs.RemoveRange(tran);
                }

            }

            foreach (var t in trans)
            {
                BillRefModel st = db.BillRefs.FirstOrDefault(k => k.BillId == model.Id && k.BillTransId == t.Id && k.IsActive && k.IsDeleted == false);

                if (st != null) //Update BillRef table
                {
                    st.BillId = model.Id;
                    st.BillTransId = t.Id;
                    st.CompanyId = model.CompId;
                    st.YearId = model.YearId;
                    st.AccountId = t.ToAccId;

                    st.BillVoucherId = model.VoucherId;
                    st.BillNo = model.VoucherNo;
                    st.VoucherNo = model.VoucherNo;
                    st.VoucherDate = model.VoucherDate;

                    if (t.Total > 0)
                    {
                        st.GrossAmt = t.Total;
                        st.BillAmt = t.Total;
                        st.RefType = "CREDIT";
                    }
                    else if (t.NetTotal > 0)
                    {
                        st.GrossAmt = t.NetTotal;
                        st.BillAmt = t.NetTotal;
                        st.RefType = "DEBIT";
                    }

                    st.Remarks = model.Remarks;


                    db.Entry(st).CurrentValues.SetValues(st);
                }
                else // Insert Billref table
                {
                    if (t.Total > 0)
                    {
                        BillRefModel br = new BillRefModel
                        {

                            BillId = model.Id,
                            BillTransId = t.Id,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            AccountId = t.ToAccId,

                            GrossAmt = t.Total,
                            BillAmt = t.Total,
                            RefType = "CREDIT",

                            BillVoucherId = model.VoucherId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,


                            Remarks = model.Remarks,



                        };
                        db.BillRefs.Add(br);
                    }
                    else if (t.NetTotal > 0)
                    {
                        BillRefModel br = new BillRefModel
                        {

                            BillId = model.Id,
                            BillTransId = t.Id,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            AccountId = t.ToAccId,

                            GrossAmt = t.NetTotal,
                            BillAmt = t.NetTotal,
                            RefType = "DEBIT",

                            BillVoucherId = model.VoucherId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,


                            Remarks = model.Remarks,



                        };
                        db.BillRefs.Add(br);
                    }
                }
            }


        }

        //////////////////////////////////////Bill Adjustment in Return/invoice and debit note credit note
        public static void BtoBEntry(string type, int id, BillModel model, KontoContext db, List<PendBillListDto> billList)
        {
            //Delete from Btob Table
            var billDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.IsActive && k.IsDeleted == false).ToList();
            if (billDel.Count > 0)
            {
                db.BtoBs.RemoveRange(billDel);
                //foreach (var bld in billDel)
                //{
                //    BtoBModel bil = db.BtoBs.FirstOrDefault(k => k.RefId == bld.RefId && k.RefVoucherId == bld.RefVoucherId && k.IsActive && k.IsDeleted == false);
                //    if (bil != null)
                //    {
                //        db.BtoBs.Remove(bil);
                //    }

                //}

            }
            //Insert in Btob table
            if (billList != null && billList.Count > 0)
            {
                foreach (var bl in billList)
                {
                    if (bl.Amount != 0)
                    {
                        //  BtoBModel bil = db.BtoBs.FirstOrDefault(k => k.BillId == bill.BillId && k.BillVoucherId == bill.BillVoucherId && k.IsActive == true && k.IsDeleted == false);
                        BtoBModel bill = new BtoBModel
                        {
                            BillNo = bl.BillNo,
                            Amount = bl.Amount,
                            RefCode = bl.RefCode,
                            BillId = bl.BillId,
                            BillVoucherId = bl.BillVoucherId,
                            BillTransId = bl.BillTransId,
                            TransType = type,
                            CompanyId = KontoGlobals.CompanyId,

                            RefId = model.Id,
                            RefTransId = model.Id,
                            RefVoucherId = model.VoucherId,

                        };

                        db.BtoBs.Add(bill);

                    }

                    //BillRefModel bref = db.BillRefs.FirstOrDefault(k => k.BillId == bl.BillId && k.BillVoucherId == bl.BillVoucherId && k.IsActive && k.IsDeleted == false);
                    //if (bref != null)
                    //{
                    //    var sumAmount = db.BtoBs.Where(p =>
                    //        p.BillId == bl.BillId && p.BillVoucherId == bl.BillVoucherId &&
                    //        p.IsActive && p.IsDeleted == false && p.TransType == type).Sum(p => p.Amount);

                    //    if (type == "Payment")
                    //    {
                    //        bref.AdjustAmt = Convert.ToDecimal(sumAmount);
                    //    }
                    //    else
                    //    {
                    //        bref.RetAmt = Convert.ToDecimal(sumAmount);
                    //    }
                    //    bref.ModifyDate = DateTime.Now;
                    //    bref.ModifyUser = user;
                    //    //   db.Entry(bref).CurrentValues.SetValues(bref);

                    //}
                }

            }
        }

        /////////////////////////////////////// Bill Adjustment for receipt/payment entry
        public static async Task BtoBEntrypayrec(string type, string user, int id, BillModel model, KontoContext db, List<BillTransModel> trans, List<BillTransModel> delTrans, List<PendBillListDto> billList, int compid, int fromdate, int todate)
        {
            //Delete if grid record deleted
            foreach (var tr in delTrans)
            {
                var bilDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.IsActive && k.IsDeleted == false).ToList();
                db.BtoBs.RemoveRange(bilDel);
            }
            await db.SaveChangesAsync();

            //Delete from Btob Table

            var billDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.IsActive == true && k.IsDeleted == false).ToList();
            if (billDel.Count > 0 && billList.Count > 0)
            {

                db.BtoBs.RemoveRange(billDel);


            }
            await db.SaveChangesAsync();
            //Insert in Btob table

            if (billList.Count > 0)
            {

                foreach (var bl in billList)
                {
                    if (bl.Amount != null && bl.Amount != 0)
                    {
                        //  BtoBModel bil = db.BtoBs.FirstOrDefault(k => k.BillId == bill.BillId && k.BillVoucherId == bill.BillVoucherId && k.IsActive == true && k.IsDeleted == false);
                        BtoBModel bill = new BtoBModel
                        {
                            BillNo = bl.BillNo,
                            Amount = bl.Amount,
                            RefCode = bl.RefCode,
                            BillId = bl.BillId,
                            CompanyId = compid,
                            BillVoucherId = bl.BillVoucherId,
                            BillTransId = bl.BillTransId,
                            TransType = type,

                            RefId = model.Id,
                            RefTransId = bl.RefTransId,
                            RefVoucherId = model.VoucherId,
                            CreateDate = DateTime.Now,
                            CreateUser = user
                        };

                        db.BtoBs.Add(bill);
                        await db.SaveChangesAsync();
                    }

                    //BillRefModel bref = db.BillRefs.FirstOrDefault(k => k.BillId == bl.BillId && k.BillVoucherId == bl.BillVoucherId && k.IsActive && k.IsDeleted == false);
                    //if (bref != null)
                    //{
                    //    var sumAmount = db.BtoBs.Where(p =>
                    //        p.BillId == bl.BillId && p.BillVoucherId == bl.BillVoucherId &&
                    //        p.IsActive && p.IsDeleted == false && p.TransType == type).Sum(p => p.Amount);
                    //    if (sumAmount != 0)
                    //    {
                    //        if (type == "Payment")
                    //        {
                    //            bref.AdjustAmt = Convert.ToDecimal(sumAmount);
                    //        }
                    //        else
                    //        {
                    //            bref.RetAmt = Convert.ToDecimal(sumAmount);
                    //        }
                    //        bref.ModifyDate = DateTime.Now;
                    //        bref.ModifyUser = user;
                    //    }
                    //    db.Entry(bref).CurrentValues.SetValues(bref);

                    //}
                }
                await db.SaveChangesAsync();
            }
        }

        /////////////////////////////////////// Bill Adjustment for receipt/payment entry
        public static async Task BtoBEntryJv(string type, string user, int id, BillModel model, KontoContext db, List<BillTransModel> trans, List<BillTransModel> delTrans, List<PendBillListDto> billList, int compid, int fromdate, int todate)
        {
            //Delete if grid record delete
            foreach (var tr in delTrans)
            {
                var bilDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.RefTransId == tr.Id && k.IsActive && k.IsDeleted == false).ToList();
                db.BtoBs.RemoveRange(bilDel);
            }

            // Delete from Btob Table           
            var billDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.IsActive == true && k.IsDeleted == false).ToList();
            if (billDel.Count > 0)
            {
                foreach (var bld in billDel)
                {
                    BtoBModel bil = db.BtoBs.FirstOrDefault(k => k.RefId == bld.RefId && k.RefVoucherId == bld.RefVoucherId && k.IsActive == true && k.IsDeleted == false);
                    if (bil != null)
                    {
                        db.BtoBs.Remove(bil);
                    }
                }
                await db.SaveChangesAsync();
            }

            //Insert in Btob table

            if (billList.Count > 0)
            {
                foreach (var bl in billList)
                {
                    if (bl.Amount != null)
                    {
                        //  BtoBModel bil = db.BtoBs.FirstOrDefault(k => k.BillId == bill.BillId && k.BillVoucherId == bill.BillVoucherId && k.IsActive == true && k.IsDeleted == false);
                        BtoBModel bill = new BtoBModel
                        {
                            BillNo = bl.BillNo,
                            Amount = bl.Amount,
                            RefCode = bl.RefCode,
                            BillId = bl.BillId,
                            CompanyId = compid,
                            BillVoucherId = bl.BillVoucherId,
                            BillTransId = bl.RefTransId,
                            TransType = type,

                            RefId = model.Id,
                            RefTransId = bl.RefTransId,
                            RefVoucherId = model.VoucherId,
                            CreateDate = DateTime.Now,
                            CreateUser = user
                        };

                        db.BtoBs.Add(bill);
                        await db.SaveChangesAsync();
                    }
                }
                await db.SaveChangesAsync();
            }
        }

        // Ledger effect for Invoice and Return Module
        public static void LedgerTransEntry(string type, BillModel model, KontoContext db, List<BillTransModel> trans, BillPay _bp = null)
        {
            var st = db.Ledgers.Where(k => k.RefId == model.RowId && k.IsActive && k.IsDeleted == false).ToList();
            if (st.Count > 0) //Delete from LedgerTrans if exist
            {
               
                db.Ledgers.RemoveRange(st);

                 db.SaveChanges();
                foreach (var item in st)
                {
                    UpdateBalance(Convert.ToInt32(item.AccountId), db);
                }
            }
            // Insert in LedgerTrans Table

            var list = new List<LedgerTransModel>();

            LedgerTransModel ledger;

            decimal net = Math.Abs(trans.Sum(k => k.NetTotal));
            decimal cgst = Math.Abs(trans.Sum(k => k.Cgst));
            decimal sgst =Math.Abs(trans.Sum(k => k.Sgst));
            decimal igst = Math.Abs(trans.Sum(k => k.Igst));
            decimal custom = Math.Abs(trans.Sum(k => k.CustomD));
            decimal tds = model.TdsAmt;
            decimal tcs = trans.Sum(k => k.TcsAmt);


            decimal freight = Math.Abs( trans.Sum(k => k.Freight));
            decimal cess = Math.Abs(trans.Sum(k => k.Cess));

            decimal taxable =  net - cgst - sgst - igst - cess;

            //taxable = taxable - tcs;

            // if item level tcs amount not in avalble

            if (tcs == 0)
                tcs = model.TcsAmt;
            decimal total = net + tcs;

            //decimal roundoff = model.TotalAmount - total;
            decimal roundoff = model.RoundOff != null ? (decimal)model.RoundOff : 0;

            if(model.TotalAmount < 0)
            {
                if (roundoff < 0)
                    roundoff = Math.Abs(roundoff);
                else
                    roundoff = -1* roundoff;
            }


            decimal totalAmt = Math.Abs(model.TotalAmount);


            bool sez = model.BillType == "SEZ Supplies with Payment" || model.BillType == "SEZ Supplies without Payment" ||
                model.BillType == "Received from SEZ";

            bool gst = model.BillType == "Regular";

            bool import = model.BillType == "Import";
            bool export = model.BillType == "Deemed Exp";
            bool rcmy = model.Rcm == "YES";

            bool rcmn = model.Rcm == "NO";

            if (rcmy || import || sez || export)
            {
                taxable = net;
            }
            //   if (model.HasteId == null)
            //       model.HasteId = 28;

            //for party
            ledger = new LedgerTransModel
            {
                RefId = model.RowId,
                VoucherId = model.VoucherId,
                CompanyId = model.CompId,
                YearId = model.YearId,
                BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                VoucherNo = model.VoucherNo,
                VoucherDate = model.VoucherDate,
                TransDate = model.VDate,
                Remark = model.Remarks,
                Narration = model.Remarks,
                LrNo = model.DocNo,
                LrDate = model.DocDate,

                AccountId = model.AccId,
                RefAccountId = model.BookAcId == null || model.BookAcId == 0 ? trans[0].ToAccId : model.BookAcId,
                Debit = type == "Debit" ? Convert.ToDecimal(totalAmt) : 0,
                Credit = type == "Debit" ? 0 : Convert.ToDecimal(totalAmt),
                BilllAmount = Convert.ToDecimal(totalAmt),
                Amount = type == "Debit" ? Convert.ToDecimal(totalAmt) : -1 * Convert.ToDecimal(totalAmt),
                TransCode = trans.FirstOrDefault().RowId
            };
            list.Add(ledger);
           
            // for books 
            if (model.BookAcId != null && model.BookAcId > 0)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,


                    AccountId = model.BookAcId,
                    RefAccountId = model.AccId,
                    Debit = type == "Debit" ? 0 : taxable,
                    Credit = type == "Debit" ? taxable : 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = type == "Debit" ? -1 * Convert.ToDecimal(taxable) : Convert.ToDecimal(taxable)
                };
                list.Add(ledger);
                
            }

            //for multiple books as for gstexpenses
            if (trans.Any(x => x.ToAccId > 0))
            {
                foreach (var item in trans)
                {
                    if (item.ToAccId == null || item.ToAccId == 0) continue;
                    var itemtaxable = item.NetTotal - item.Cess - item.Cgst - item.Sgst - item.Igst;
                    if (rcmy) itemtaxable = item.NetTotal;

                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,

                        TransCode = item.RowId,
                        AccountId = item.ToAccId,
                        RefAccountId = model.AccId,
                        Debit = type == "Debit" ? 0 : itemtaxable,
                        Credit = type == "Debit" ? itemtaxable : 0,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = type == "Debit" ? -1 * Convert.ToDecimal(itemtaxable) : Convert.ToDecimal(itemtaxable)
                    };
                    list.Add(ledger);
                   
                }
            }


            //check for line level tds posting
            if(trans.Any(x=>x.TdsAcId >0 && x.TdsAmt >0) )
            {
                foreach (var item in trans)
                {
                    if (item.TdsAcId <= 0 || item.TdsAmt <= 0) continue;
                    ledger = new LedgerTransModel // tds account posting
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,

                        AccountId = item.TdsAcId,
                        RefAccountId = model.AccId,
                        Debit = type == "Credit" ? 0 : Convert.ToDecimal(item.TdsAmt),
                        Credit = type == "Credit" ? Convert.ToDecimal(item.TdsAmt) : 0,
                        BilllAmount = Convert.ToDecimal(item.NetTotal),
                        Amount = type == "Credit" ? -1 * Convert.ToDecimal(item.TdsAmt) : Convert.ToDecimal(item.TdsAmt)
                    };
                    list.Add(ledger);


                    ledger = new LedgerTransModel // effect in party account
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,


                        //  AccountId = type == "Credit" ? model.AccId : model.HasteId,
                        //  RefAccountId = type == "Credit" ? model.HasteId : model.AccId,
                        AccountId = model.AccId,
                        RefAccountId = item.TdsAcId,
                        Debit = type == "Credit" ? Convert.ToDecimal(item.TdsAmt) : 0,
                        Credit = type == "Credit" ? 0 : Convert.ToDecimal(item.TdsAmt),
                        BilllAmount = Convert.ToDecimal(item.NetTotal),
                        Amount = type == "Credit" ? -1 * Convert.ToDecimal(item.TdsAmt) : Convert.ToDecimal(item.TdsAmt)
                    };
                    list.Add(ledger);
                }
            }

            //sgst
            if (sgst != 0)
            {
                ledger = new LedgerTransModel();


                ledger.RefId = model.RowId;
                ledger.VoucherId = model.VoucherId;
                ledger.CompanyId = model.CompId;
                ledger.YearId = model.YearId;
                ledger.BillNo = type == "Debit" ? model.VoucherNo : model.BillNo;
                ledger.VoucherNo = model.VoucherNo;
                ledger.VoucherDate = model.VoucherDate;
                ledger.TransDate = model.VDate;
                ledger.Remark = model.Remarks;
                ledger.Narration = model.Remarks;
                ledger.LrNo = model.DocNo;
                ledger.LrDate = model.DocDate;

                if (model.TypeId == 12 || model.TypeId == 19 || (model.TypeId == 24 && model.Extra1 == "SALE"))
                    ledger.AccountId = 16; // output sgst
                else
                    ledger.AccountId = 12; // input sgst

                //ledger.AccountId = type == "Debit" ? 16 : 12;
                ledger.RefAccountId = rcmy ? 24 : model.AccId;
                ledger.Debit = type == "Debit" ? 0 : Convert.ToDecimal(sgst);
                ledger.Credit = type == "Debit" ? Convert.ToDecimal(sgst) : 0;
                ledger.BilllAmount = Convert.ToDecimal(totalAmt);
                ledger.Amount = type == "Debit" ? -1 * Convert.ToDecimal(sgst) : Convert.ToDecimal(sgst);
               

                list.Add(ledger);
               

                if (rcmy)
                {
                    ledger = new LedgerTransModel();
                    //{
                    ledger.RefId = model.RowId;
                    ledger.VoucherId = model.VoucherId;
                    ledger.CompanyId = model.CompId;
                    ledger.YearId = model.YearId;
                    ledger.BillNo = type == "Debit" ? model.VoucherNo : model.BillNo;
                    ledger.VoucherNo = model.VoucherNo;
                    ledger.VoucherDate = model.VoucherDate;
                    ledger.TransDate = model.VDate;
                    ledger.Remark = model.Remarks;
                    ledger.Narration = model.Remarks;
                    ledger.LrNo = model.DocNo;
                    ledger.LrDate = model.DocDate;


                    ledger.AccountId = 24;
                    ledger.RefAccountId = 12;

                    ledger.Debit = type == "Credit" ? 0 : Convert.ToDecimal(sgst);
                    ledger.Credit = type == "Credit" ? Convert.ToDecimal(sgst) : 0;
                    ledger.BilllAmount = Convert.ToDecimal(totalAmt);
                    ledger.Amount = -1 * Convert.ToDecimal(sgst);
                   // };
                    list.Add(ledger);
                   
                }
            }
            //cgst
            if (cgst != 0)
            {
                ledger = new LedgerTransModel();
                //{
                ledger.RefId = model.RowId;
                ledger.VoucherId = model.VoucherId;
                ledger.CompanyId = model.CompId;
                ledger.YearId = model.YearId;
                ledger.BillNo = type == "Debit" ? model.VoucherNo : model.BillNo;
                ledger.VoucherNo = model.VoucherNo;
                ledger.VoucherDate = model.VoucherDate;
                ledger.TransDate = model.VDate;
                ledger.Remark = model.Remarks;
                ledger.Narration = model.Remarks;
                ledger.LrNo = model.DocNo;
                ledger.LrDate = model.DocDate;

                if (model.TypeId == 12 || model.TypeId == 19 || (model.TypeId == 24 && model.Extra1 == "SALE"))
                    ledger.AccountId = 17; // output cgst
                else
                    ledger.AccountId = 13; //input cgst

                ledger.RefAccountId = rcmy ? 25 : model.AccId;
                ledger.Debit = type == "Debit" ? 0 : Convert.ToDecimal(cgst);
                ledger.Credit = type == "Debit" ? Convert.ToDecimal(cgst) : 0;
                ledger.BilllAmount = Convert.ToDecimal(totalAmt);
                ledger.Amount = type == "Debit" ? -1 * Convert.ToDecimal(cgst) : Convert.ToDecimal(cgst);
               // };
                list.Add(ledger);
              
                if (rcmy)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,


                        AccountId = 25,
                        RefAccountId = 13,
                        Debit = type == "Credit" ? 0 : Convert.ToDecimal(cgst),
                        Credit = type == "Credit" ? Convert.ToDecimal(cgst) : 0,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = -1 * Convert.ToDecimal(cgst)
                    };
                    list.Add(ledger);
                   
                }

            }
            //igst ledger posting
            if (igst != 0)
            {
                ledger = new LedgerTransModel();
                //{
                ledger.RefId = model.RowId;
                ledger.VoucherId = model.VoucherId;
                ledger.CompanyId = model.CompId;
                ledger.YearId = model.YearId;
                ledger.BillNo = type == "Debit" ? model.VoucherNo : model.BillNo;
                ledger.VoucherNo = model.VoucherNo;
                ledger.VoucherDate = model.VoucherDate;
                ledger.TransDate = model.VDate;
                ledger.Remark = model.Remarks;
                ledger.Narration = model.Remarks;
                ledger.LrNo = model.DocNo;
                ledger.LrDate = model.DocDate;

                if (model.TypeId == 12 || model.TypeId == 19 || (model.TypeId == 24 && model.Extra1 == "SALE"))
                    ledger.AccountId = 18;
                else
                    ledger.AccountId = 14;

                // 26 igst rcm payble, 23 custom dutty payble,22- igst receibavle,33 igst payble
                ledger.RefAccountId = rcmy ? 26 : import ? 23 : (sez || export) && type == "Debit" ? 22 : sez && type == "Credit" ? 33 : model.AccId;
                ledger.Debit = type == "Debit" ? 0 : Convert.ToDecimal(igst);
                ledger.Credit = type == "Debit" ? Convert.ToDecimal(igst) : 0;
                ledger.BilllAmount = Convert.ToDecimal(totalAmt);
                ledger.Amount = type == "Debit" ? -1 * Convert.ToDecimal(igst) : Convert.ToDecimal(igst);
                //};

                list.Add(ledger);
                

                if (rcmy || import || sez || export)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,

                        AccountId = rcmy ? 26 : import ? 23 : (sez || export) && type == "Debit" ? 22 : sez && type == "Credit" ? 33 : model.AccId,
                        RefAccountId = type == "Debit" ? 18 : 14,
                        Credit = type == "Debit" ? 0 : Convert.ToDecimal(igst + cess),
                        Debit = type == "Debit" ? Convert.ToDecimal(igst + cess) : 0,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = type == "Debit" ? -1 * Convert.ToDecimal(igst + cess) : Convert.ToDecimal(igst + cess)
                    };
                    list.Add(ledger);
                   
                }
            }
            // cess entry
            if (cess != 0)
            {
                ledger = new LedgerTransModel();
                //{
                ledger.RefId = model.RowId;
                ledger.VoucherId = model.VoucherId;
                ledger.CompanyId = model.CompId;
                ledger.YearId = model.YearId;
                ledger.BillNo = type == "Debit" ? model.VoucherNo : model.BillNo;
                ledger.VoucherNo = model.VoucherNo;
                ledger.VoucherDate = model.VoucherDate;
                ledger.TransDate = model.VDate;
                ledger.Remark = model.Remarks;
                ledger.Narration = model.Remarks;
                ledger.LrNo = model.DocNo;
                ledger.LrDate = model.DocDate;


                // ledger.AccountId = type == "Debit" ? 19 : 15;
                if (model.TypeId == 12 || model.TypeId == 19 || (model.TypeId == 24 && model.Extra1 == "SALE"))
                    ledger.AccountId = 19; // output cess
                else
                    ledger.AccountId = 15; // input cess

                
                ledger.RefAccountId =  model.AccId;
                ledger.Debit = type == "Debit" ? 0 : Convert.ToDecimal(cess);
                ledger.Credit = type == "Debit" ? Convert.ToDecimal(cess) : 0;
                ledger.BilllAmount = Convert.ToDecimal(totalAmt);
                ledger.Amount = type == "Debit" ? -1 * Convert.ToDecimal(cess) : Convert.ToDecimal(cess);
                //};

                list.Add(ledger);
               
            }

            //tds entry
            if (tds != 0)
            {
                ledger = new LedgerTransModel // tds account posting
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,



                    //   AccountId = type == "Credit" ? model.HasteId : model.AccId,
                    // RefAccountId = type == "Credit"? model.AccId : model.HasteId,

                    AccountId = model.HasteId,
                    RefAccountId = model.AccId,
                    Debit = type == "Credit" ? 0 : Convert.ToDecimal(tds),
                    Credit = type == "Credit" ? Convert.ToDecimal(tds) : 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = type == "Credit" ? -1 * Convert.ToDecimal(tds) : Convert.ToDecimal(tds)
                };
                list.Add(ledger);
                

                ledger = new LedgerTransModel // effect in party account
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,


                    //  AccountId = type == "Credit" ? model.AccId : model.HasteId,
                    //  RefAccountId = type == "Credit" ? model.HasteId : model.AccId,
                    AccountId = model.AccId,
                    RefAccountId = model.HasteId,
                    Debit = type == "Credit" ? Convert.ToDecimal(tds) : 0,
                    Credit = type == "Credit" ? 0 : Convert.ToDecimal(tds),
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = type == "Credit" ? -1 * Convert.ToDecimal(tds) : Convert.ToDecimal(tds)
                };
                list.Add(ledger);
                

            }
            // Tcs effect entry for purchase bill

            if (tcs != 0)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,


                    AccountId = type == "Credit" ? 32 : 31,
                    RefAccountId = model.AccId,
                    Debit = type == "Credit" ? Convert.ToDecimal(tcs) : 0,
                    Credit = type == "Credit" ? 0 : Convert.ToDecimal(tcs),
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(tcs)
                };
                list.Add(ledger);
              

            }

            //round of
            if (roundoff != 0)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = type == "Debit" ? model.VoucherNo : model.BillNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,


                    AccountId = 29,
                    RefAccountId = model.AccId
                };
                if (roundoff < 0 && type == "Credit")
                {
                    ledger.Credit = -1 * roundoff;
                    ledger.Debit = 0;
                    ledger.Amount = Convert.ToDecimal(roundoff);
                }
                else if (roundoff > 0 && type == "Credit")
                {
                    ledger.Debit = roundoff;
                    ledger.Credit = 0;
                    ledger.Amount = Convert.ToDecimal(roundoff);
                }
                else if (roundoff < 0 && type == "Debit")
                {
                    ledger.Debit = -1 * roundoff;
                    ledger.Credit = 0;
                    ledger.Amount = -1 * Convert.ToDecimal(roundoff);
                }
                else if (type == "Debit")
                {
                    ledger.Credit = roundoff;
                    ledger.Debit = 0;
                    ledger.Amount = -1 * Convert.ToDecimal(roundoff);
                }

                ledger.BilllAmount = Convert.ToDecimal(totalAmt);

                list.Add(ledger);
               

            }

            //custom duty in case of purchase
            //if (custom != 0 && type =="Credit")
            //{
            //    ledger = new LedgerTransModel
            //    {
            //        RefId = model.RowId,
            //        VoucherId = model.VoucherId,
            //        CompanyId = model.CompId,
            //        YearId = model.YearId,
            //        BillNo = model.VoucherNo,
            //        VoucherNo = model.VoucherNo,
            //        VoucherDate = model.VoucherDate,
            //        TransDate = model.VDate,
            //        Remark = model.Remarks,
            //        Narration = model.Remarks,
            //        LrNo = model.DocNo,
            //        LrDate = model.DocDate,
            //        CreateDate = DateTime.Now,
            //        CreateUser = user,

            //        AccountId = 14,
            //        RefAccountId = 23,
            //        Debit = Convert.ToDecimal(custom),
            //        Credit = 0,
            //        BilllAmount = Convert.ToDecimal(totalAmt),
            //        Amount = Convert.ToDecimal(custom)
            //    };
            //    list.Add(ledger);

            //    ledger = new LedgerTransModel
            //    {
            //        RefId = model.RowId,
            //        VoucherId = model.VoucherId,
            //        CompanyId = model.CompId,
            //        YearId = model.YearId,
            //        BillNo = model.VoucherNo,
            //        VoucherNo = model.VoucherNo,
            //        VoucherDate = model.VoucherDate,
            //        TransDate = model.VDate,
            //        Remark = model.Remarks,
            //        Narration = model.Remarks,
            //        LrNo = model.DocNo,
            //        LrDate = model.DocDate,
            //        CreateDate = DateTime.Now,
            //        CreateUser = user,

            //        AccountId = 23,
            //        RefAccountId = 14,
            //        Debit = 0,
            //        Credit = Convert.ToDecimal(custom),
            //        BilllAmount = Convert.ToDecimal(totalAmt),
            //        Amount = -1 * Convert.ToDecimal(custom)
            //    };
            //    list.Add(ledger);

            //}



            if (model.TypeId == (int)VoucherTypeEnum.SaleInvoice && KontoGlobals.PackageId == (int)PackageType.POS)
            {



                if (_bp == null)
                {
                    var bps = db.BillPays.Where(x => x.BillId == model.Id).ToList();
                    foreach (var item in bps)
                    {
                        list.AddRange(Pos_Receipt(db, item, model));
                    }
                }
                else
                {
                    list.AddRange(Pos_Receipt(db, _bp, model));
                }
            }


            db.Ledgers.AddRange(list);
            db.SaveChanges();
            foreach (var item in list)
            {
                UpdateBalance(Convert.ToInt32(item.AccountId), db);
            }

        }

        ///////////////////////////Ledger effect of Receipt/Payment entry
        public static void LedgerTransEntryRecPay(string type, BillModel model, KontoContext db,
            List<BillTransModel> trans, List<PendBillListDto> billList)
        {
            var st = db.Ledgers.Where(k => k.RefId == model.RowId && k.IsActive && k.IsDeleted == false).ToList();
            if (st.Count > 0) //Delete from LedgerTrans if exist
            {
                db.Ledgers.RemoveRange(st);
                db.SaveChanges();
                foreach (var item in st)
                {
                    UpdateBalance(Convert.ToInt32(item.AccountId), db);
                }

            }

            List<LedgerTransModel> list = new List<LedgerTransModel>();

            LedgerTransModel ledger;
            foreach (var tr in trans)
            {
                if (type == "Debit")
                {
                    if (tr.ToAccId != null)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,

                            AccountId = tr.ToAccId,
                            RefAccountId = model.AccId,
                            Debit = Convert.ToDecimal(tr.NetTotal),
                            Credit = 0,
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = Convert.ToDecimal(tr.NetTotal)
                        };

                        list.Add(ledger);
                    }

                    if (model.AccId != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,

                            AccountId = model.AccId,
                            RefAccountId = tr.ToAccId,
                            Debit = 0,
                            Credit = Convert.ToDecimal(tr.NetTotal),
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = -1 * Convert.ToDecimal(tr.NetTotal)
                        };

                        list.Add(ledger);
                    }

                    if (tr.Sgst != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,

                            AccountId = 16,
                            RefAccountId = tr.ToAccId,
                            Debit = 0,
                            Credit = Convert.ToDecimal(tr.Sgst),
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = -1 * Convert.ToDecimal(tr.Sgst)
                        };

                        list.Add(ledger);
                    }

                    if (tr.Cgst != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,

                            AccountId = 17,
                            RefAccountId = tr.ToAccId,
                            Debit = 0,
                            Credit = Convert.ToDecimal(tr.Cgst),
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = -1 * Convert.ToDecimal(tr.Cgst)
                        };

                        list.Add(ledger);

                    }

                    if (tr.Igst != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,

                            AccountId = 18,
                            RefAccountId = tr.ToAccId,
                            Debit = 0,
                            Credit = Convert.ToDecimal(tr.Igst),
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = -1 * Convert.ToDecimal(tr.Igst)
                        };

                        list.Add(ledger);

                    }


                    // tds on advanced.....................
                    if (model.TypeId == (int)VoucherTypeEnum.PaymentVoucher && tr.TdsAmt > 0 && tr.TdsAcId > 0)
                    {
                        ledger = new LedgerTransModel // tds account posting
                        {
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = tr.Remark,
                            Narration = "tds @" + tr.TdsPer.ToString("F") + " %",
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,
                            AccountId = tr.TdsAcId,
                            RefAccountId = tr.ToAccId,
                            Debit = 0,
                            Credit = tr.TdsAmt,
                            BilllAmount = tr.NetTotal,
                            Amount = -1 * tr.TdsAmt
                        };
                        list.Add(ledger);


                        ledger = new LedgerTransModel // effect in party account
                        {
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = tr.Remark,
                            Narration = "tds @" + tr.TdsPer.ToString("F") + " %",
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            AccountId = tr.ToAccId,
                            RefAccountId = tr.TdsAcId,
                            Debit = tr.TdsAmt,
                            Credit = 0,
                            BilllAmount = tr.NetTotal,
                            Amount = tr.TdsAmt
                        };
                        list.Add(ledger);
                    }

                    // If Any Charges in Bill Adjustment
                    if (billList != null)
                    {
                        var bls = billList.Where(x => x.RefTransId == tr.Id).ToList();
                        foreach (var p in bls)
                        {
                            if (p != null)
                            {
                                var adl1 = db.RPSets.FirstOrDefault(k => k.Field == "Adl1" && k.RecPay == "P" 
                                                                    && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                                                    && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);
                                if (p.Adla1 != 0 && adl1 != null)
                                {
                                    LedgerTransModel l = new LedgerTransModel();

                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    //    var adl1 = db.RPSets.FirstOrDefault(k => k.Field == "Adl1" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl1 != null)
                                    //{
                                    //    l.RefAccountId = adl1.AccountId;
                                    //}
                                    l.RefAccountId = adl1.AccountId;

                                    if (p.Adla1 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla1);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla1);
                                        l.Amount = Convert.ToDecimal(p.Adla1);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla1);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla1);
                                        l.Amount = Convert.ToDecimal(p.Adla1);
                                    }

                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;
                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl1" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.AccountId = adl1.AccountId;

                                    if (p.Adla1 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla1);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla1);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla1);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla1);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla1);
                                        m.Amount = Convert.ToDecimal(p.Adla1);
                                    }

                                    list.Add(m);

                                }
                                var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl2" && k.RecPay == "P" &&
                                k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla2 != 0 && adl2 != null)
                                {
                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    // var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl2" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    l.RefAccountId = adl2.AccountId;
                                    //}
                                    l.RefAccountId = adl2.AccountId;
                                    if (p.Adla2 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla2);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla2);
                                        l.Amount = Convert.ToDecimal(p.Adla2);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla2);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla2);
                                        l.Amount = Convert.ToDecimal(p.Adla2);
                                    }

                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;
                                    m.RefAccountId = tr.ToAccId;
                                    //var adl3 = db.RPSets.FirstOrDefault(k => k.Field == "Adl2" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl3 != null)
                                    //{
                                    //    m.AccountId = adl3.AccountId;
                                    //}
                                    m.AccountId = adl2.AccountId;
                                    if (p.Adla2 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla2);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla2);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla2);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla2);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla2);
                                        m.Amount = Convert.ToDecimal(p.Adla2);
                                    }

                                    list.Add(m);
                                }

                                var adl3 = db.RPSets.FirstOrDefault(k => k.Field == "Adl3" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla3 != 0 && adl3 != null)
                                {

                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    //var adl3 = db.RPSets.FirstOrDefault(k => k.Field == "Adl3" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl3 != null)
                                    //{
                                    //    l.RefAccountId = adl3.AccountId;
                                    //}
                                    l.RefAccountId = adl3.AccountId;
                                    if (p.Adla3 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla3);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla3);
                                        l.Amount = Convert.ToDecimal(p.Adla3);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla3);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla3);
                                        l.Amount = Convert.ToDecimal(p.Adla3);
                                    }

                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;
                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl3" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.AccountId = adl3.AccountId;
                                    if (p.Adla3 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla3);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla3);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla3);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla3);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla3);
                                        m.Amount = Convert.ToDecimal(p.Adla3);
                                    }

                                    list.Add(m);
                                }
                                var adl4 = db.RPSets.FirstOrDefault(k => k.Field == "Adl4" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla4 != 0 && adl4 != null )
                                {

                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    //var adl4 = db.RPSets.FirstOrDefault(k => k.Field == "Adl4" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl4 != null)
                                    //{
                                    //    l.RefAccountId = adl4.AccountId;
                                    //}
                                    l.RefAccountId = adl4.AccountId;
                                    if (p.Adla4 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla4);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla4);
                                        l.Amount = Convert.ToDecimal(p.Adla4);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla4);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla4);
                                        l.Amount = Convert.ToDecimal(p.Adla4);
                                    }
                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;
                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl4" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.RefAccountId = adl4.AccountId;
                                    if (p.Adla4 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla4);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla4);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla4);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla4);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla4);
                                        m.Amount = Convert.ToDecimal(p.Adla4);
                                    }

                                    list.Add(m);
                                }

                                var adl5 = db.RPSets.FirstOrDefault(k => k.Field == "Adl5" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla5 != 0 && adl5 != null)
                                {

                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;
                                    l.CreateDate = DateTime.Now;

                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    //var adl5 = db.RPSets.FirstOrDefault(k => k.Field == "Adl5" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl5 != null)
                                    //{
                                    //    l.RefAccountId = adl5.AccountId;
                                    //}
                                    l.RefAccountId = adl5.AccountId;
                                    if (p.Adla5 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla5);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla5);
                                        l.Amount = Convert.ToDecimal(p.Adla5);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla5);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla5);
                                        l.Amount = Convert.ToDecimal(p.Adla5);
                                    }
                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;
                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl5" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.AccountId = adl5.AccountId;
                                    if (p.Adla5 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla5);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla5);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla5);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla5);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla5);
                                        m.Amount = Convert.ToDecimal(p.Adla5);
                                    }

                                    list.Add(m);
                                }
                                var adl6 = db.RPSets.FirstOrDefault(k => k.Field == "Adl6" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla6 != 0 && adl6 != null)
                                {
                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    //var adl6 = db.RPSets.FirstOrDefault(k => k.Field == "Adl6" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl6 != null)
                                    //{
                                    //    l.RefAccountId = adl6.AccountId;
                                    //}
                                    l.RefAccountId = adl6.AccountId;
                                    if (p.Adla6 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla6);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla6);
                                        l.Amount = Convert.ToDecimal(p.Adla6);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla6);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla6);
                                        l.Amount = Convert.ToDecimal(p.Adla6);
                                    }
                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;
                                    m.CreateDate = DateTime.Now;

                                    m.ChqDate = tr.ChequeDate;
                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl6" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.AccountId = adl6.AccountId;
                                    if (p.Adla6 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla6);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla6);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla6);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla6);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla6);
                                        m.Amount = Convert.ToDecimal(p.Adla6);
                                    }

                                    list.Add(m);
                                }
                                var adl7 = db.RPSets.FirstOrDefault(k => k.Field == "Adl7" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla7 != 0 && adl7 != null)
                                {
                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    //var adl7 = db.RPSets.FirstOrDefault(k => k.Field == "Adl7" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl7 != null)
                                    //{
                                    //    l.RefAccountId = adl7.AccountId;
                                    //}
                                    l.RefAccountId = adl7.AccountId;
                                    if (p.Adla7 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla7);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla7);
                                        l.Amount = Convert.ToDecimal(p.Adla7);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla7);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla7);
                                        l.Amount = Convert.ToDecimal(p.Adla7);
                                    }

                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;
                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl7" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.AccountId = adl7.AccountId;

                                    if (p.Adla7 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla7);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla7);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla7);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla7);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla7);
                                        m.Amount = Convert.ToDecimal(p.Adla7);
                                    }

                                    list.Add(m);
                                }
                                var adl8 = db.RPSets.FirstOrDefault(k => k.Field == "Adl8" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla8 != 0 && adl8 != null)
                                {
                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    //var adl8 = db.RPSets.FirstOrDefault(k => k.Field == "Adl8" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl8 != null)
                                    //{
                                    //    l.RefAccountId = adl8.AccountId;
                                    //}
                                    l.RefAccountId = adl8.AccountId;
                                    if (p.Adla8 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla8);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla8);
                                        l.Amount = Convert.ToDecimal(p.Adla8);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla8);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla8);
                                        l.Amount = Convert.ToDecimal(p.Adla8);
                                    }
                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;
                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl8" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.AccountId = adl8.AccountId;

                                    if (p.Adla8 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla8);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla8);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla8);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla8);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla8);
                                        m.Amount = Convert.ToDecimal(p.Adla8);
                                    }

                                    list.Add(m);
                                }
                                var adl9 = db.RPSets.FirstOrDefault(k => k.Field == "Adl9" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla9 != 0 && adl9 != null)
                                {
                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;
                                    l.AccountId = tr.ToAccId;
                                    //var adl9 = db.RPSets.FirstOrDefault(k => k.Field == "Adl9" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl9 != null)
                                    //{
                                    //    l.RefAccountId = adl9.AccountId;
                                    //}
                                    l.RefAccountId = adl9.AccountId;
                                    if (p.Adla9 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla9);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla9);
                                        l.Amount = Convert.ToDecimal(p.Adla9);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla9);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla9);
                                        l.Amount = Convert.ToDecimal(p.Adla9);
                                    }
                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;

                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl9" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.AccountId = adl9.AccountId;
                                    if (p.Adla9 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla9);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla9);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla9);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla9);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla9);
                                        m.Amount = Convert.ToDecimal(p.Adla9);
                                    }

                                    list.Add(m);
                                }
                                var adl10 = db.RPSets.FirstOrDefault(k => k.Field == "Adl10" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                                && k.CompId == KontoGlobals.CompanyId && !k.IsDeleted);

                                if (p.Adla10 != 0 && adl10 != null)
                                {
                                    LedgerTransModel l = new LedgerTransModel();
                                    l.TransCode = p.RefCode;
                                    l.RefId = model.RowId;
                                    l.VoucherId = model.VoucherId;
                                    l.CompanyId = model.CompId;
                                    l.YearId = model.YearId;
                                    l.BillNo = model.VoucherNo;
                                    l.VoucherNo = model.VoucherNo;
                                    l.VoucherDate = model.VoucherDate;
                                    l.TransDate = model.VDate;
                                    l.Remark = model.Remarks;
                                    l.Narration = tr.Remark;

                                    l.ChqNo = tr.ChequeNo;
                                    l.ChqDate = tr.ChequeDate;

                                    l.AccountId = tr.ToAccId;
                                    //var adl10 = db.RPSets.FirstOrDefault(k => k.Field == "Adl10" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl10 != null)
                                    //{
                                    //    l.RefAccountId = adl10.AccountId;
                                    //}
                                    l.RefAccountId = adl10.AccountId;
                                    if (p.Adla10 > 0)
                                    {
                                        l.Debit = Convert.ToDecimal(p.Adla10);
                                        l.Credit = 0;
                                        l.BilllAmount = Convert.ToDecimal(p.Adla10);
                                        l.Amount = Convert.ToDecimal(p.Adla10);
                                    }
                                    else
                                    {
                                        l.Debit = 0;
                                        l.Credit = -1 * Convert.ToDecimal(p.Adla10);
                                        l.BilllAmount = -1 * Convert.ToDecimal(p.Adla10);
                                        l.Amount = Convert.ToDecimal(p.Adla10);
                                    }

                                    list.Add(l);

                                    LedgerTransModel m = new LedgerTransModel();

                                    m.TransCode = p.RefCode;
                                    m.RefId = model.RowId;
                                    m.VoucherId = model.VoucherId;
                                    m.CompanyId = model.CompId;
                                    m.YearId = model.YearId;
                                    m.BillNo = model.VoucherNo;
                                    m.VoucherNo = model.VoucherNo;
                                    m.VoucherDate = model.VoucherDate;
                                    m.TransDate = model.VDate;
                                    m.Remark = model.Remarks;
                                    m.Narration = tr.Remark;

                                    m.ChqNo = tr.ChequeNo;
                                    m.ChqDate = tr.ChequeDate;

                                    m.RefAccountId = tr.ToAccId;
                                    //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl10" && k.RecPay == "P" && k.YearId == yearid);
                                    //if (adl2 != null)
                                    //{
                                    //    m.AccountId = adl2.AccountId;
                                    //}
                                    m.AccountId = adl10.AccountId;
                                    if (p.Adla10 > 0)
                                    {
                                        m.Debit = 0;
                                        m.Credit = Convert.ToDecimal(p.Adla10);
                                        m.BilllAmount = Convert.ToDecimal(p.Adla10);
                                        m.Amount = -1 * Convert.ToDecimal(p.Adla10);
                                    }
                                    else
                                    {
                                        m.Debit = -1 * Convert.ToDecimal(p.Adla10);
                                        m.Credit = 0;
                                        m.BilllAmount = -1 * Convert.ToDecimal(p.Adla10);
                                        m.Amount = Convert.ToDecimal(p.Adla10);
                                    }
                                    list.Add(m);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (tr.ToAccId != null)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,
                            AccountId = tr.ToAccId,
                            RefAccountId = model.AccId,
                            Debit = 0,
                            Credit = Convert.ToDecimal(tr.NetTotal),
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = -1 * Convert.ToDecimal(tr.NetTotal)
                        };

                        list.Add(ledger);

                    }

                    if (model.AccId != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,
                            AccountId = model.AccId,
                            RefAccountId = tr.ToAccId,
                            Debit = Convert.ToDecimal(tr.NetTotal),
                            Credit = 0,
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = Convert.ToDecimal(tr.NetTotal)
                        };

                        list.Add(ledger);
                    }

                    if (tr.Sgst != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,
                            AccountId = 12,
                            RefAccountId = model.AccId,
                            Debit = Convert.ToDecimal(tr.Sgst),
                            Credit = 0,
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = Convert.ToDecimal(tr.Sgst)
                        };
                        list.Add(ledger);

                    }

                    if (tr.Cgst != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                            ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,
                            AccountId = 13,
                            RefAccountId = model.AccId,
                            Debit = Convert.ToDecimal(tr.Cgst),
                            Credit = 0,
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = Convert.ToDecimal(tr.Cgst)
                        };
                        list.Add(ledger);
                    }

                    if (tr.Igst != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            TransCode = tr.RowId,
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = model.Remarks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,

                           ChqNo = tr.ChequeNo,
                            ChqDate = tr.ChequeDate,

                            AccountId = 14,
                            RefAccountId = model.AccId,
                            Debit = Convert.ToDecimal(tr.Igst),
                            Credit = 0,
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = Convert.ToDecimal(tr.Igst)
                        };
                        list.Add(ledger);
                    }

                   
                    // If Any Charges in Bill Adjustment
                    if (billList != null)
                    {
                        var bls = billList.Where(x => x.RefTransId == tr.Id).ToList();
                        foreach (var p in bls)
                        {
                            var adl1 = db.RPSets.FirstOrDefault(k => k.Field == "Adl1" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla1 != 0 && adl1 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();

                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;
                                l.AccountId = tr.ToAccId;
                                //var adl1 = db.RPSets.FirstOrDefault(k => k.Field == "Adl1" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl1 != null)
                                //{
                                //    l.RefAccountId = adl1.AccountId;
                                //}
                                l.RefAccountId = adl1.AccountId;
                                if (p.Adla1 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla1);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla1);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla1);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla1);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla1);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla1);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();

                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;

                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl1" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl1.AccountId;
                                if (p.Adla1 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla1);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla1);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla1);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla1);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla1);
                                    m.Amount = Convert.ToDecimal(p.Adla1);
                                }

                                list.Add(m);
                            }
                            var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl2" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla2 != 0 && adl2 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;
                                l.AccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl2" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    l.RefAccountId = adl2.AccountId;
                                //}
                                l.RefAccountId = adl2.AccountId;
                                if (p.Adla2 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla2);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla2);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla2);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla2);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla2);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla2);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();

                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl3 = db.RPSets.FirstOrDefault(k => k.Field == "Adl2" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl3 != null)
                                //{
                                //    m.AccountId = adl3.AccountId;
                                //}
                                m.AccountId = adl2.AccountId;
                                if (p.Adla2 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla2);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla2);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla2);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla2);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla2);
                                    m.Amount = Convert.ToDecimal(p.Adla2);
                                }

                                list.Add(m);
                            }

                            var adl3 = db.RPSets.FirstOrDefault(k => k.Field == "Adl3" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla3 != 0 && adl3 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;
                                l.AccountId = tr.ToAccId;
                                //var adl3 = db.RPSets.FirstOrDefault(k => k.Field == "Adl3" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl3 != null)
                                //{
                                //    l.RefAccountId = adl3.AccountId;
                                //}
                                l.RefAccountId = adl3.AccountId;
                                if (p.Adla3 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla3);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla3);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla3);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla3);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla3);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla3);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();

                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl3" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl3.AccountId;
                                if (p.Adla3 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla3);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla3);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla3);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla3);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla3);
                                    m.Amount = Convert.ToDecimal(p.Adla3);
                                }

                                list.Add(m);
                            }
                            var adl4 = db.RPSets.FirstOrDefault(k => k.Field == "Adl4" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla4 != 0 && adl4 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;


                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;

                                l.AccountId = tr.ToAccId;
                                //var adl4 = db.RPSets.FirstOrDefault(k => k.Field == "Adl4" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl4 != null)
                                //{
                                //    l.RefAccountId = adl4.AccountId;
                                //}
                                l.RefAccountId = adl4.AccountId;
                                if (p.Adla4 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla4);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla4);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla4);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla4);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla4);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla4);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();
                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl4" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl4.AccountId;
                                if (p.Adla4 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla4);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla4);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla4);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla4);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla4);
                                    m.Amount = Convert.ToDecimal(p.Adla4);
                                }

                                list.Add(m);
                            }
                            var adl5 = db.RPSets.FirstOrDefault(k => k.Field == "Adl5" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla5 != 0 && adl5 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;
                                l.AccountId = tr.ToAccId;
                                //var adl5 = db.RPSets.FirstOrDefault(k => k.Field == "Adl5" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl5 != null)
                                //{
                                //    l.RefAccountId = adl5.AccountId;
                                //}
                                l.RefAccountId = adl5.AccountId;
                                if (p.Adla5 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla5);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla5);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla5);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla5);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla5);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla5);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();
                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl5" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl5.AccountId;
                                if (p.Adla5 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla5);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla5);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla5);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla5);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla5);
                                    m.Amount = Convert.ToDecimal(p.Adla5);
                                }

                                list.Add(m);
                            }
                            var adl6 = db.RPSets.FirstOrDefault(k => k.Field == "Adl6" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla6 != 0 && adl6 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;
                                l.AccountId = tr.ToAccId;
                                //var adl6 = db.RPSets.FirstOrDefault(k => k.Field == "Adl6" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl6 != null)
                                //{
                                //    l.RefAccountId = adl6.AccountId;
                                //}
                                l.RefAccountId = adl6.AccountId;
                                if (p.Adla6 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla6);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla6);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla6);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla6);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla6);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla6);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();

                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl6" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl6.AccountId;
                                if (p.Adla6 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla6);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla6);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla6);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla6);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla6);
                                    m.Amount = Convert.ToDecimal(p.Adla6);
                                }

                                list.Add(m);
                            }
                            var adl7 = db.RPSets.FirstOrDefault(k => k.Field == "Adl7" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla7 != 0 && adl7 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;
                                l.AccountId = tr.ToAccId;
                                //var adl7 = db.RPSets.FirstOrDefault(k => k.Field == "Adl7" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl7 != null)
                                //{
                                //    l.RefAccountId = adl7.AccountId;
                                //}
                                l.RefAccountId = adl7.AccountId;
                                if (p.Adla7 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla7);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla7);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla7);

                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla7);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla7);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla7);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();

                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl7" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl7.AccountId;
                                if (p.Adla7 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla7);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla7);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla7);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla7);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla7);
                                    m.Amount = Convert.ToDecimal(p.Adla7);
                                }

                                list.Add(m);
                            }
                            var adl8 = db.RPSets.FirstOrDefault(k => k.Field == "Adl8" && k.RecPay == "P" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla8 != 0 && adl8 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;

                                l.AccountId = tr.ToAccId;
                                //var adl8 = db.RPSets.FirstOrDefault(k => k.Field == "Adl8" && k.RecPay == "P" && k.YearId == yearid);
                                //if (adl8 != null)
                                //{
                                //    l.RefAccountId = adl8.AccountId;
                                //}
                                l.RefAccountId = adl8.AccountId;
                                if (p.Adla8 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla8);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla8);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla8);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla8);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla8);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla8);
                                }
                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();
                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl8" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl8.AccountId;
                                if (p.Adla8 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla8);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla8);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla8);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla8);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla8);
                                    m.Amount = Convert.ToDecimal(p.Adla8);
                                }

                                list.Add(m);
                            }
                            var adl9 = db.RPSets.FirstOrDefault(k => k.Field == "Adl9" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla9 != 0 && adl9 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;

                                l.AccountId = tr.ToAccId;
                                //var adl9 = db.RPSets.FirstOrDefault(k => k.Field == "Adl9" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl9 != null)
                                //{
                                //    l.RefAccountId = adl9.AccountId;
                                //}
                                l.RefAccountId = adl9.AccountId;
                                if (p.Adla9 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla9);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla9);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla9);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla9);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla9);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla9);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();

                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl9" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl9.AccountId;
                                if (p.Adla9 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla9);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla9);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla9);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla9);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla9);
                                    m.Amount = Convert.ToDecimal(p.Adla9);
                                }

                                list.Add(m);
                            }
                            var adl10 = db.RPSets.FirstOrDefault(k => k.Field == "Adl10" && k.RecPay == "R" && k.YearId == KontoGlobals.YearId && k.Drcr != "Y"
                            && k.CompId == KontoGlobals.CompanyId);

                            if (p.Adla10 != 0 && adl10 != null)
                            {
                                LedgerTransModel l = new LedgerTransModel();
                                l.TransCode = p.RefCode;
                                l.RefId = model.RowId;
                                l.VoucherId = model.VoucherId;
                                l.CompanyId = model.CompId;
                                l.YearId = model.YearId;
                                l.BillNo = model.VoucherNo;
                                l.VoucherNo = model.VoucherNo;
                                l.VoucherDate = model.VoucherDate;
                                l.TransDate = model.VDate;
                                l.Remark = model.Remarks;
                                l.Narration = tr.Remark;

                                l.ChqNo = tr.ChequeNo;
                                l.ChqDate = tr.ChequeDate;
                                l.AccountId = tr.ToAccId;
                                //var adl10 = db.RPSets.FirstOrDefault(k => k.Field == "Adl10" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl10 != null)
                                //{
                                //    l.RefAccountId = adl10.AccountId;
                                //}
                                l.RefAccountId = adl10.AccountId;
                                if (p.Adla10 > 0)
                                {
                                    l.Debit = 0;
                                    l.Credit = Convert.ToDecimal(p.Adla10);
                                    l.BilllAmount = Convert.ToDecimal(p.Adla10);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla10);
                                }
                                else
                                {
                                    l.Debit = -1 * Convert.ToDecimal(p.Adla10);
                                    l.Credit = 0;
                                    l.BilllAmount = -1 * Convert.ToDecimal(p.Adla10);
                                    l.Amount = -1 * Convert.ToDecimal(p.Adla10);
                                }

                                list.Add(l);

                                LedgerTransModel m = new LedgerTransModel();

                                m.TransCode = p.RefCode;
                                m.RefId = model.RowId;
                                m.VoucherId = model.VoucherId;
                                m.CompanyId = model.CompId;
                                m.YearId = model.YearId;
                                m.BillNo = model.VoucherNo;
                                m.VoucherNo = model.VoucherNo;
                                m.VoucherDate = model.VoucherDate;
                                m.TransDate = model.VDate;
                                m.Remark = model.Remarks;
                                m.Narration = tr.Remark;

                                m.ChqNo = tr.ChequeNo;
                                m.ChqDate = tr.ChequeDate;
                                m.RefAccountId = tr.ToAccId;
                                //var adl2 = db.RPSets.FirstOrDefault(k => k.Field == "Adl10" && k.RecPay == "R" && k.YearId == yearid);
                                //if (adl2 != null)
                                //{
                                //    m.AccountId = adl2.AccountId;
                                //}
                                m.AccountId = adl10.AccountId;
                                if (p.Adla10 > 0)
                                {
                                    m.Debit = Convert.ToDecimal(p.Adla10);
                                    m.Credit = 0;
                                    m.BilllAmount = Convert.ToDecimal(p.Adla10);
                                    m.Amount = -1 * Convert.ToDecimal(p.Adla10);
                                }
                                else
                                {
                                    m.Debit = 0;
                                    m.Credit = -1 * Convert.ToDecimal(p.Adla10);
                                    m.BilllAmount = -1 * Convert.ToDecimal(p.Adla10);
                                    m.Amount = Convert.ToDecimal(p.Adla10);
                                }

                                list.Add(m);
                            }
                        }
                    }
                }
            }
            db.Ledgers.AddRange(list);
            db.SaveChanges();
            foreach (var item in list)
            {
                UpdateBalance(Convert.ToInt32(item.AccountId), db);
            }
        }

        ///////////////////////////Ledger effect of Jv entry
        public static void LedgerTransEntryJv(string user, BillModel model, KontoContext db,
            List<BillTransModel> trans, List<BankTransDto> jvs =null )
        {
            var st = db.Ledgers.Where(k => k.RefId == model.RowId && k.IsActive && k.IsDeleted == false).ToList();
            if (st.Count > 0) //Delete from LedgerTrans if exist
            {
                db.Ledgers.RemoveRange(st);
                db.SaveChanges();
                foreach (var item in st)
                {
                    UpdateBalance(Convert.ToInt32(item.AccountId), db);
                }
                // db.SaveChanges();
            }

            List<LedgerTransModel> list = new List<LedgerTransModel>();

           


            LedgerTransModel ledger;
            if (trans.Count > 2)
            {


                foreach (var tr in trans)
                {
                    string remks = string.Empty;
                    var jvss = new List<BankTransDto>();

                    if (tr.Total > 0)
                    {
                        jvss = jvs.Where(x => x.ToAccId != tr.ToAccId && x.NetTotal > 0).ToList();

                        foreach (var jv in jvss)
                        {
                            remks = remks + jv.Particular + " " + jv.NetTotal.ToString("F") + " Dr" + ", ";
                        }

                        jvss = jvs.Where(x => x.ToAccId != tr.ToAccId && x.Total > 0).ToList();

                        foreach (var jv in jvss)
                        {
                            remks = remks + jv.Particular + " " + jv.Total.ToString("F") + " Cr" + ", ";
                        }

                    }

                    if(tr.NetTotal >0 )
                    {
                        

                        jvss = jvs.Where(x => x.ToAccId != tr.ToAccId && x.Total > 0).ToList();

                        foreach (var jv in jvss)
                        {
                            remks = remks + jv.Particular + " " + jv.Total.ToString("F") + " Cr" + ", ";
                        }

                        jvss = jvs.Where(x => x.ToAccId != tr.ToAccId && x.NetTotal > 0).ToList();

                        foreach (var jv in jvss)
                        {
                            remks = remks + jv.Particular + " " + jv.Total.ToString("F") + " Dr" + ", ";
                        }
                    }


                    if (tr.Total != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                            Remark = remks,
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,
                            CreateDate = DateTime.Now,
                            CreateUser = user,
                            
                            AccountId = tr.ToAccId,
                            RefAccountId = 30,
                            Debit = 0,
                            Credit = Convert.ToDecimal(tr.Total),
                            BilllAmount = Convert.ToDecimal(tr.Total),
                            Amount = -1 * Convert.ToDecimal(tr.Total)
                        };

                        list.Add(ledger);
                    }

                    if (tr.NetTotal != 0)
                    {
                        ledger = new LedgerTransModel
                        {
                            RefId = model.RowId,
                            VoucherId = model.VoucherId,
                            CompanyId = model.CompId,
                            YearId = model.YearId,
                            BillNo = model.VoucherNo,
                            VoucherNo = model.VoucherNo,
                            VoucherDate = model.VoucherDate,
                            TransDate = model.VDate,
                           
                            Narration = tr.Remark,
                            LrNo = model.DocNo,
                            LrDate = model.DocDate,
                            CreateDate = DateTime.Now,
                            CreateUser = user,
                            Remark = remks,
                            AccountId = tr.ToAccId,
                            RefAccountId = 30,
                            Debit = Convert.ToDecimal(tr.NetTotal),
                            Credit = 0,
                            BilllAmount = Convert.ToDecimal(tr.NetTotal),
                            Amount = Convert.ToDecimal(tr.NetTotal)
                        };

                        list.Add(ledger);
                    }
                }
            }
            else
            {

                if (trans[0].Total != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = trans[0].Remark,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = trans[0].ToAccId,
                        RefAccountId = trans[1].ToAccId,
                        Debit = 0,
                        Credit = Convert.ToDecimal(trans[0].Total),
                        BilllAmount = Convert.ToDecimal(trans[0].Total),
                        Amount = -1 * Convert.ToDecimal(trans[0].Total)
                    };

                    list.Add(ledger);
                }

                if (trans[0].NetTotal != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = trans[0].Remark,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = trans[0].ToAccId,
                        RefAccountId = trans[1].ToAccId,
                        Debit = Convert.ToDecimal(trans[0].NetTotal),
                        Credit = 0,
                        BilllAmount = Convert.ToDecimal(trans[0].NetTotal),
                        Amount = Convert.ToDecimal(trans[0].NetTotal)
                    };

                    list.Add(ledger);
                }

                if (trans[1].Total != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = trans[1].Remark,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = trans[1].ToAccId,
                        RefAccountId = trans[0].ToAccId,
                        Debit = 0,
                        Credit = Convert.ToDecimal(trans[1].Total),
                        BilllAmount = Convert.ToDecimal(trans[1].Total),
                        Amount = -1 * Convert.ToDecimal(trans[1].Total)
                    };

                    list.Add(ledger);
                }

                if (trans[1].NetTotal != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = trans[1].Remark,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = trans[1].ToAccId,
                        RefAccountId = trans[0].ToAccId,
                        Debit = Convert.ToDecimal(trans[1].NetTotal),
                        Credit = 0,
                        BilllAmount = Convert.ToDecimal(trans[1].NetTotal),
                        Amount = Convert.ToDecimal(trans[1].NetTotal)
                    };

                    list.Add(ledger);
                }

            }

            db.Ledgers.AddRange(list);
            db.SaveChanges();
            foreach (var item in list)
            {
                UpdateBalance(Convert.ToInt32(item.AccountId), db);
            }
        }

        ///////////////////////////Ledger effect of GenExpense entry
        public static void  LedgerTransEntryGenExp(string user, BillModel model, KontoContext db,
            List<BillTransModel> trans, string transtype, int compid, int fromdate, int todate)
        {
            var st = db.Ledgers.Where(k => k.RefId == model.RowId && k.IsActive && k.IsDeleted == false).ToList();
            if (st.Count > 0) //Delete from LedgerTrans if exist
            {
                db.Ledgers.RemoveRange(st);
                db.SaveChanges();
                foreach (var item in st)
                {
                    UpdateBalance(Convert.ToInt32(item.AccountId), db);
                }
                //..db.SaveChanges();
            }

            List<LedgerTransModel> list = new List<LedgerTransModel>();

            decimal net = trans.Sum(k => k.NetTotal);
            decimal cgst = trans.Sum(k => k.Cgst);
            decimal sgst = trans.Sum(k => k.Sgst);
            decimal igst = trans.Sum(k => k.Igst);
            decimal custom = trans.Sum(k => k.CustomD);
            decimal tds = model.TdsAmt;
            decimal tcs = model.TcsAmt;
            decimal freight = trans.Sum(k => k.Freight);
            decimal cess = trans.Sum(k => k.Cess);
            decimal taxable = net - cgst - sgst - igst;
            decimal roundoff = net + tcs - model.TotalAmount;
            decimal totalAmt = decimal.Round(model.TotalAmount + tcs, 0);

            bool rcmy = model.Rcm == "YES";

            bool rcmn = model.Rcm == "NO";

            LedgerTransModel ledger;
            foreach (var tr in trans)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = model.AccId,
                    RefAccountId = tr.ToAccId,
                    Debit = 0,
                    Credit = Convert.ToDecimal(tr.NetTotal),
                    BilllAmount = Convert.ToDecimal(tr.NetTotal),
                    Amount = -1 * Convert.ToDecimal(tr.NetTotal)
                };

                list.Add(ledger);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = model.AccId,
                    RefAccountId = tr.ToAccId,
                    Debit = Convert.ToDecimal(tr.NetTotal),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(tr.NetTotal),
                    Amount = Convert.ToDecimal(tr.NetTotal)
                };

                list.Add(ledger);
            }

            if (sgst != 0 && rcmn)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 12,
                    RefAccountId = model.AccId,
                    Debit = Convert.ToDecimal(sgst),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(sgst)
                };
                list.Add(ledger);

            }

            if (sgst != 0 && rcmy)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 12,
                    RefAccountId = 24,
                    Debit = Convert.ToDecimal(sgst),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(sgst)
                };
                list.Add(ledger);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 24,
                    RefAccountId = 12,
                    Debit = 0,
                    Credit = Convert.ToDecimal(sgst),
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = -1 * Convert.ToDecimal(sgst)
                };
                list.Add(ledger);

            }

            if (cgst != 0 && rcmn)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 13,
                    RefAccountId = model.AccId,
                    Debit = Convert.ToDecimal(cgst),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(cgst)
                };
                list.Add(ledger);
            }

            if (cgst > 0 && rcmy)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 13,
                    RefAccountId = 25,
                    Debit = Convert.ToDecimal(cgst),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(cgst)
                };
                list.Add(ledger);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 25,
                    RefAccountId = 13,
                    Debit = 0,
                    Credit = Convert.ToDecimal(cgst),
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = -1 * Convert.ToDecimal(cgst)
                };
                list.Add(ledger);
            }

            if (igst != 0 && rcmn)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 14,
                    RefAccountId = model.AccId,
                    Debit = Convert.ToDecimal(igst),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(igst)
                };
                list.Add(ledger);
            }

            if (igst > 0 && rcmy)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 14,
                    RefAccountId = 26,
                    Debit = Convert.ToDecimal(igst),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(igst)
                };
                list.Add(ledger);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 26,
                    RefAccountId = 14,
                    Debit = 0,
                    Credit = Convert.ToDecimal(igst),
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = -1 * Convert.ToDecimal(igst)
                };
                list.Add(ledger);

            }


            if (tcs != 0)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 31,
                    RefAccountId = model.AccId,
                    Debit = Convert.ToDecimal(tcs),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(tcs)
                };
                list.Add(ledger);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = model.AccId,
                    RefAccountId = 31,
                    Debit = 0,
                    Credit = Convert.ToDecimal(tcs),
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = -1 * Convert.ToDecimal(tcs)
                };
                list.Add(ledger);

            }

            if (tds != 0)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 27,
                    RefAccountId = model.AccId,
                    Debit = 0,
                    Credit = Convert.ToDecimal(tds),
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = -1 * Convert.ToDecimal(tds)
                };
                list.Add(ledger);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = model.AccId,
                    RefAccountId = 27,
                    Debit = Convert.ToDecimal(tds),
                    Credit = 0,
                    BilllAmount = Convert.ToDecimal(totalAmt),
                    Amount = Convert.ToDecimal(tds)
                };
                list.Add(ledger);
            }

            if (roundoff != 0)
            {
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = model.VoucherDate,
                    TransDate = model.VDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,
                    CreateDate = DateTime.Now,
                    CreateUser = user,

                    AccountId = 29,
                    RefAccountId = model.AccId
                };
                if (roundoff > 0)
                {
                    ledger.Debit = Convert.ToDecimal(roundoff);
                    ledger.Credit = 0;
                }
                else
                {
                    ledger.Debit = 0;
                    ledger.Credit = Convert.ToDecimal(roundoff);
                }

                ledger.BilllAmount = Convert.ToDecimal(totalAmt);
                ledger.Amount = Convert.ToDecimal(roundoff);

                list.Add(ledger);

            }

            db.Ledgers.AddRange(list);
            foreach (var item in list)
            {
                UpdateBalance(Convert.ToInt32(item.AccountId), db);
            }
            //await db.SaveChangesAsync();
        }

        /////////////////////////// Ledger effect for DRCR Note
        public static async Task LedgerTransEntryDrcr(string type, string user, BillModel model, KontoContext db, List<BillTransModel> trans, string transtype, int compid, int fromdate, int todate)
        {
            var st = db.Ledgers.Where(k => k.RefId == model.RowId && k.IsActive && k.IsDeleted == false).ToList();
            if (st.Count > 0) //Delete from LedgerTrans if exist
            {
                db.Ledgers.RemoveRange(st);
                foreach (var item in st)
                {
                    UpdateBalance(Convert.ToInt32(item.AccountId), db);
                }
                db.SaveChanges();
            }
            // Insert in LedgerTrans Table

            var list = new List<LedgerTransModel>();

            LedgerTransModel ledger;

            decimal net = trans.Sum(k => k.NetTotal);
            decimal cgst = trans.Sum(k => k.Cgst);
            decimal sgst = trans.Sum(k => k.Sgst);
            decimal igst = trans.Sum(k => k.Igst);
            decimal taxable = net - cgst - sgst - igst;
            decimal roundoff = net - model.TotalAmount;
            decimal totalAmt = model.TotalAmount;

            if (type == "Debit")
            {
                if (model.AccId != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = model.AccId,
                        RefAccountId = model.BookAcId,
                        Debit = Convert.ToDecimal(totalAmt),
                        Credit = 0,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = Convert.ToDecimal(totalAmt)
                    };

                    list.Add(ledger);
                }

                if (model.BookAcId != null)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = model.BookAcId,
                        RefAccountId = model.AccId,
                        Debit = 0,
                        Credit = taxable,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = -1 * Convert.ToDecimal(taxable)
                    };

                    list.Add(ledger);
                }

                if (sgst != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = 16,
                        RefAccountId = model.AccId,
                        Debit = 0,
                        Credit = Convert.ToDecimal(sgst),
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = -1 * Convert.ToDecimal(sgst)
                    };

                    list.Add(ledger);
                }

                if (cgst != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = 17,
                        RefAccountId = model.AccId,
                        Debit = 0,
                        Credit = Convert.ToDecimal(cgst),
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = -1 * Convert.ToDecimal(cgst)
                    };

                    list.Add(ledger);

                }

                if (igst != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = 18,
                        RefAccountId = model.AccId,
                        Debit = 0,
                        Credit = Convert.ToDecimal(igst),
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = -1 * Convert.ToDecimal(igst)
                    };

                    list.Add(ledger);

                }

                if (roundoff != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = 29,
                        RefAccountId = model.AccId
                    };
                    if (roundoff > 0)
                    {
                        ledger.Debit = Convert.ToDecimal(roundoff);
                        ledger.Credit = 0;
                    }
                    else
                    {
                        ledger.Debit = 0;
                        ledger.Credit = Convert.ToDecimal(roundoff);
                    }

                    ledger.BilllAmount = Convert.ToDecimal(totalAmt);
                    ledger.Amount = Convert.ToDecimal(roundoff);

                    list.Add(ledger);

                }
            }
            else if (type == "Credit")
            {
                if (model.AccId > 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = model.AccId,
                        RefAccountId = model.BookAcId,
                        Debit = 0,
                        Credit = Convert.ToDecimal(totalAmt),
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = -1 * Convert.ToDecimal(totalAmt)
                    };
                    list.Add(ledger);
                }

                if (model.BookAcId > 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = model.BookAcId,
                        RefAccountId = model.AccId,
                        Debit = Convert.ToDecimal(taxable),
                        Credit = 0,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = Convert.ToDecimal(taxable)
                    };
                    list.Add(ledger);
                }


                if (sgst != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = 12,
                        RefAccountId = model.AccId,
                        Debit = Convert.ToDecimal(sgst),
                        Credit = 0,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = Convert.ToDecimal(sgst)
                    };
                    list.Add(ledger);

                }

                if (cgst != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = 13,
                        RefAccountId = model.AccId,
                        Debit = Convert.ToDecimal(cgst),
                        Credit = 0,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = Convert.ToDecimal(cgst)
                    };
                    list.Add(ledger);
                }

                if (igst != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = 14,
                        RefAccountId = model.AccId,
                        Debit = Convert.ToDecimal(igst),
                        Credit = 0,
                        BilllAmount = Convert.ToDecimal(totalAmt),
                        Amount = Convert.ToDecimal(igst)
                    };
                    list.Add(ledger);
                }

                if (roundoff != 0)
                {
                    ledger = new LedgerTransModel
                    {
                        RefId = model.RowId,
                        VoucherId = model.VoucherId,
                        CompanyId = model.CompId,
                        YearId = model.YearId,
                        BillNo = model.VoucherNo,
                        VoucherNo = model.VoucherNo,
                        VoucherDate = model.VoucherDate,
                        TransDate = model.VDate,
                        Remark = model.Remarks,
                        Narration = model.Remarks,
                        LrNo = model.DocNo,
                        LrDate = model.DocDate,
                        CreateDate = DateTime.Now,
                        CreateUser = user,

                        AccountId = 29,
                        RefAccountId = model.AccId
                    };
                    if (roundoff > 0)
                    {
                        ledger.Debit = 0;
                        ledger.Credit = Convert.ToDecimal(roundoff);
                    }
                    else
                    {
                        ledger.Debit = Convert.ToDecimal(roundoff);
                        ledger.Credit = 0;
                    }

                    ledger.BilllAmount = Convert.ToDecimal(totalAmt);
                    ledger.Amount = Convert.ToDecimal(roundoff);
                    list.Add(ledger);

                }
            }

            db.Ledgers.AddRange(list);
            foreach (var item in list)
            {
                UpdateBalance(Convert.ToInt32(item.AccountId), db);
            }
            await db.SaveChangesAsync();
        }

        public static bool DataFreezeStatus(int MDate, int VTypeId, KontoContext _db)
        {
            DataFreezeModel cds = _db.DFreeze.Where(p => p.VoucherTypeID == VTypeId && p.FromDate <= MDate && p.ToDate >= MDate && p.Freeze == true
             && p.CompanyID == KontoGlobals.CompanyId).FirstOrDefault();

            if (cds != null)
            {
                return false;
            }
            return true;
        }

        //////////////////////////////////////Bill Adjustment in Return/invoice and debit note credit note
        public static async Task BtoBEntryUploadReturn(string type, string user, BillModel model, KontoContext db, BillModel OrgBill, BillRefModel billR, int compid)
        {
            //Delete from Btob Table
            var billDel = db.BtoBs.Where(k => k.RefId == model.Id && k.RefVoucherId == model.VoucherId && k.IsActive && k.IsDeleted == false).ToList();
            if (billDel.Count > 0)
            {
                foreach (var bld in billDel)
                {
                    BtoBModel bil = db.BtoBs.FirstOrDefault(k => k.RefId == bld.RefId && k.RefVoucherId == bld.RefVoucherId && k.IsActive && k.IsDeleted == false);
                    if (bil != null)
                    {
                        db.BtoBs.Remove(bil);
                    }
                }
                await db.SaveChangesAsync();
            }
            //Insert in Btob table
            if (OrgBill.TotalAmount != 0)
            {
                BtoBModel bill = new BtoBModel
                {
                    BillNo = OrgBill.VoucherNo,
                    Amount = model.TotalAmount,
                    RefCode = billR.RowId,
                    //RefCode = OrgBill.RowId,
                    BillId = OrgBill.Id,
                    BillVoucherId = OrgBill.VoucherId,
                    BillTransId = OrgBill.Id,
                    TransType = type,
                    CompanyId = compid,

                    RefId = model.Id,
                    RefTransId = model.Id,
                    RefVoucherId = model.VoucherId,
                    CreateDate = DateTime.Now,
                    CreateUser = user
                };

                db.BtoBs.Add(bill);
                await db.SaveChangesAsync();
            }
        }

        public static void UpdateBalance(int _accid, KontoContext db)
        {
            var _sum = (from b in db.Ledgers
                        where b.AccountId == _accid
                         && b.VoucherDate >= KontoGlobals.FromDate &&
                         b.VoucherDate <= KontoGlobals.ToDate &&
                         b.CompanyId == KontoGlobals.CompanyId
                         group b by 0 into g
                        select new
                        {
                            Debit = g.Sum(x => x.Debit),
                            Credit = g.Sum(x => x.Credit)
                        }).FirstOrDefault();
            var _bal = db.AccBals.FirstOrDefault(x => x.CompId == KontoGlobals.CompanyId &&
                        x.YearId == KontoGlobals.YearId && x.AccId == _accid);
            if (_sum != null)
                _bal.Bal = _bal.OpBal + _sum.Debit -_sum.Credit;
            else
                _bal.Bal = _bal.OpBal;



        }

        public static List<LedgerTransModel> Pos_Receipt(KontoContext db, BillPay bp, BillModel model)
        {


            var list = new List<LedgerTransModel>();

            LedgerTransModel ledger;

            if (bp.DiscAmt > 0) // for Post Sale Discount 
            {
                var para = db.CompParas.FirstOrDefault(x => x.ParaId == 267 && x.CompId == KontoGlobals.CompanyId);


                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = bp.PayDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,

                    AccountId = model.AccId,
                    RefAccountId = Convert.ToInt32(para.ParaValue),
                    Debit = 0,
                    Credit = bp.DiscAmt,
                    BilllAmount = model.TotalAmount,
                    Amount = -1*bp.DiscAmt,
                    TransCode = model.RowId
                };
                list.Add(ledger);
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = bp.PayDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,

                    RefAccountId = model.AccId,
                    AccountId = Convert.ToInt32(para.ParaValue),
                    Credit = 0,
                    Debit = bp.DiscAmt,
                    BilllAmount = model.TotalAmount,
                    Amount = bp.DiscAmt,
                    TransCode = model.RowId
                };
                list.Add(ledger);
            }

            if (Convert.ToInt32(bp.Pay1Id) > 0 && bp.Pay1Amt != 0) // for cash
            {
                var pay1 = db.Hastes.Find(bp.Pay1Id);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = bp.PayDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,

                    AccountId = model.AccId,
                    RefAccountId = pay1.AccId,
                    Debit = bp.Pay1Amt < 0 ? Math.Abs(bp.Pay1Amt) :  0,
                    Credit = bp.Pay1Amt < 0 ? 0 : bp.Pay1Amt - bp.ChangeAmt,
                    BilllAmount = model.TotalAmount,
                    Amount = -1 * (bp.Pay1Amt - bp.ChangeAmt),
                    TransCode = model.RowId
                };
                list.Add(ledger);
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = bp.PayDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,

                    RefAccountId = model.AccId,
                    AccountId = pay1.AccId,
                    Credit = bp.Pay1Amt < 0 ? Math.Abs(bp.Pay1Amt) : 0,
                    Debit = bp.Pay1Amt < 0 ? 0 : bp.Pay1Amt - bp.ChangeAmt,
                    BilllAmount = model.TotalAmount,
                    Amount = (bp.Pay1Amt - bp.ChangeAmt),
                    TransCode = model.RowId
                };
                list.Add(ledger);
            }

            if (Convert.ToInt32(bp.Pay2Id) > 0 && bp.Pay2Amt > 0) // for 2nd payment
            {
                var pay2 = db.Hastes.Find(bp.Pay2Id);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = bp.PayDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,

                    AccountId = model.AccId,
                    RefAccountId = pay2.AccId,
                    Debit = bp.Pay2Amt < 0 ? Math.Abs(bp.Pay2Amt) : 0,
                    Credit = bp.Pay2Amt < 0 ? 0 : bp.Pay2Amt,
                    BilllAmount = model.TotalAmount,
                    Amount = -1 * bp.Pay2Amt,
                    TransCode = model.RowId
                };
                list.Add(ledger);
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = bp.PayDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,

                    RefAccountId = model.AccId,
                    AccountId = pay2.AccId,
                    Credit = bp.Pay2Amt < 0 ? Math.Abs(bp.Pay2Amt) :0,
                    Debit = bp.Pay2Amt <0 ? 0 : bp.Pay2Amt,
                    BilllAmount = model.TotalAmount,
                    Amount = bp.Pay2Amt,
                    TransCode = model.RowId
                };
                list.Add(ledger);
            }

            if (Convert.ToInt32(bp.Pay3Id) > 0 && bp.Pay3Amt > 0) // for 2nd payment
            {
                var pay3 = db.Hastes.Find(bp.Pay3Id);

                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = bp.PayDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,

                    AccountId = model.AccId,
                    RefAccountId = pay3.AccId,
                    Debit = bp.Pay3Amt < 0 ? Math.Abs(bp.Pay3Amt) : 0,
                    Credit = bp.Pay3Amt < 0 ? 0 : bp.Pay3Amt,
                    BilllAmount = model.TotalAmount,
                    Amount = -1 * bp.Pay3Amt,
                    TransCode = model.RowId
                };
                list.Add(ledger);
                ledger = new LedgerTransModel
                {
                    RefId = model.RowId,
                    VoucherId = model.VoucherId,
                    CompanyId = model.CompId,
                    YearId = model.YearId,
                    BillNo = model.VoucherNo,
                    VoucherNo = model.VoucherNo,
                    VoucherDate = bp.PayDate,
                    Remark = model.Remarks,
                    Narration = model.Remarks,
                    LrNo = model.DocNo,
                    LrDate = model.DocDate,

                    RefAccountId = model.AccId,
                    AccountId = pay3.AccId,
                    Credit = bp.Pay3Amt < 0 ? Math.Abs(bp.Pay3Amt) : 0,
                    Debit = bp.Pay3Amt < 0 ? 0 : bp.Pay3Amt,
                    BilllAmount = model.TotalAmount,
                    Amount = bp.Pay3Amt,
                    TransCode = model.RowId
                };
                list.Add(ledger);
            }

            return list;

        }

    }
}
