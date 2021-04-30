using Keysoft.Erp.Data;
using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Core.Shared.Libs;
using Konto.Data;
using Konto.Data.Models.Masters;
using Konto.Data.Models.Pos;
using Konto.Data.Models.Transaction;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Konto.Import
{
    public partial class ImportView : KontoForm
    {
        public ImportView()
        {
            InitializeComponent();
            okSimpleButton.Click += OkSimpleButton_Click;
            this.Load += ImportView_Load;
        }

        private void ImportView_Load(object sender, EventArgs e)
        {
            fDateEdit1.DateTime = DateTime.Now;
            tDateEdit2.DateTime = DateTime.Now;
        }

        private void OkSimpleButton_Click(object sender, EventArgs e)
        {
            var sdb = new ImpContext();

            using(var db = new KontoContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Database.CommandTimeout = 0;
                using (var _tran = db.Database.BeginTransaction())
                {
                    try
                    {

                      //  splashScreenManager1.ShowWaitForm();
                      //  splashScreenManager1.SetWaitFormCaption("Importing Sales..");
                        Purchase(db, sdb);
                        PurchaseReturn(db, sdb);
                        Sales(db, sdb);
                        
                        DbUtils.Update_Account_Balance(db);

                        _tran.Commit();
                       // splashScreenManager1.CloseWaitForm();
                        MessageBox.Show("Purchase/sales Imported Successfully");
                    }
                    catch (Exception ex)
                    {
                        _tran.Rollback();
                       // if (splashScreenManager1.IsSplashFormVisible) splashScreenManager1.CloseWaitForm();
                        Log.Error(ex, "Import Error");
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void Sales(KontoContext db, ImpContext sdb)
        {
            var fdt = Convert.ToInt32(fDateEdit1.DateTime.ToString("yyyyMMdd"));
            var tdt = Convert.ToInt32(tDateEdit2.DateTime.ToString("yyyyMMdd"));
            
            var spurs = sdb.Sales.Include("SaleTrans").Include("SalesPay")
                            .Where(x => x.VoucherDate >= fdt && x.VoucherDate <= tdt).ToList();

            
            progressBarControl1.Properties.Maximum = spurs.Count;
            progressBarControl1.Properties.Minimum = 0;
            progressBarControl1.Properties.Step = 1;
            progressBarControl1.Properties.PercentView = true;
            int i = 1;
            foreach (var spur in spurs)
            {

               // splashScreenManager1.SetWaitFormDescription("Importing " + i.ToString() + " of " + spurs.Count());
                var model = db.Bills.SingleOrDefault(x => x.RefId == spur.SalesID && x.RefVoucherId == spur.SalesID && x.IsActive && !x.IsDeleted);

                if (model != null) continue;

                //check for exist account
                var checkAcc = db.Accs.SingleOrDefault(x => x.CollById == spur.AccountID);
                if (checkAcc == null)
                    checkAcc = CreateAccount(db, (int)spur.AccountID, sdb); //create account if not exists

                //check bok
                var chkbk = db.Accs.SingleOrDefault(x => x.CollById == spur.SalesAcID);
                if (chkbk == null)
                    chkbk = CreateAccount(db, (int)spur.SalesAcID, sdb); //create account if not exists

                if (model == null)
                {
                    model = new BillModel
                    {
                        AccId = checkAcc.Id,
                        BillType = "Regular",
                        VoucherId = db.Vouchers.FirstOrDefault(x => x.VTypeId == (int)VoucherTypeEnum.SaleInvoice).Id,
                        VoucherDate = (int) spur.VoucherDate,
                        Rcm = "NO",
                        Itc = "Inputs",
                        BookAcId = chkbk.Id,
                        RcdDate = KontoUtils.IToD((int) spur.VoucherDate),
                        VoucherNo = spur.BillNo,
                        RefNo = spur.ChallanNo,
                        BillNo = spur.BillNo,
                        EmpId = 1,
                        StoreId = 1,
                        Remarks = spur.SalesRemark,
                        DocNo = spur.LrNo,
                        DocDate = KontoUtils.IToD((int) spur.LrDate),
                        TypeId = (int)VoucherTypeEnum.SaleInvoice,
                        CompId = KontoGlobals.CompanyId,
                        YearId = KontoGlobals.YearId,
                        BranchId = KontoGlobals.BranchId,
                        RoundOff = spur.RoundOff,
                        Duedays = spur.Due_Days,
                        TdsAmt = spur.TdsAmount == null ? 0 : (int)spur.TdsAmount,
                        TdsPer = spur.TdsPer == null ? 0 : (int)spur.TdsPer,
                        GrossAmount = spur.TotalGross ?? 0,
                        TotalAmount = spur.BillAmount ?? 0,
                        TotalQty = spur.TotalQty ?? 0,
                        TotalPcs = spur.TotalPcs ?? 0,
                        IsActive = true,
                        RefId = (int)spur.SalesID,
                        RefVoucherId = (int)spur.VoucherID,
                        StateId =24

                    };

                    db.Bills.Add(model);
                    db.SaveChanges();

                    //var strs = sdb.PurchaseTranses.Where(x => x.PurchaseID == spur.PurchaseID).ToList();
                    List<BillTransModel> bts = new List<BillTransModel>();
                    bool flg = false;
                    foreach (var str in spur.SaleTrans)
                    {

                        var spd = sdb.Items.Find(str.ItemID);
                        
                        //check for item exist
                        var chkitem = db.Products.SingleOrDefault(x => x.ParentItemId == str.ItemID);
                        if (chkitem == null)
                            chkitem = CreateProduct(db, (int)str.ItemID, sdb);

                        var bt = new BillTransModel();


                        bt.Cgst = str.VatAmount ?? 0;
                        bt.CgstPer = str.VatPer ?? 0;
                        bt.Cut = str.Cut ?? 0;
                        bt.ProductId = chkitem.Id;
                        bt.BillId = model.Id;
                        bt.Qty = str.Qty ?? 0;
                        bt.Pcs = (int)str.Pcs;
                        bt.SgstPer = str.AdVatPer ?? 0;
                        bt.IgstPer = str.CSTPer ?? 0;
                        bt.CgstPer = str.VatPer ?? 0;

                        var rt = Convert.ToDecimal(spd.StandardWeight ?? 0);
                        
                        if(rt > 0)
                            rt = decimal.Round((rt * 100) / (100 + (bt.SgstPer + bt.CgstPer + bt.IgstPer)), 2, MidpointRounding.AwayFromZero);

                        bt.Rate = rt > 0 ? rt : str.Rate ?? 0;

                        var bankacid = spur.SalesPay.FirstOrDefault().BankAcID;

                        //bt.SaleRate = str.RetailRate ?? 0;
                        if (rt > 0 && (bankacid ?? 0)  == 0 )
                        {
                            flg = true;
                            bt.Total = decimal.Round( bt.Qty * bt.Rate,2,MidpointRounding.AwayFromZero);
                            bt.Disc = str.DiscPer ?? 0;
                            if(bt.Disc > 0)
                            {
                                bt.DiscAmt = decimal.Round(bt.Total * bt.Disc / 100, 2, MidpointRounding.AwayFromZero);
                            }
                            else 
                                bt.DiscAmt = str.DiscAmount ?? 0;

                            bt.FreightRate = str.ServiceTaxPer ?? 0;
                            bt.Freight = str.ServiceTaxAmount ?? 0;

                            
                            bt.Sgst = decimal.Round((bt.Total - bt.Disc + bt.Freight) * bt.SgstPer / 100, 2, MidpointRounding.AwayFromZero);

                            
                            bt.Cgst = decimal.Round((bt.Total - bt.Disc + bt.Freight) * bt.CgstPer / 100, 2, MidpointRounding.AwayFromZero);

                            bt.Igst = decimal.Round((bt.Total - bt.Disc + bt.Freight) * bt.IgstPer / 100, 2, MidpointRounding.AwayFromZero);


                            bt.NetTotal = bt.Total - bt.DiscAmt + bt.Freight + bt.Sgst + bt.Cgst + bt.Igst;
                        }
                        else
                        {
                            bt.Total = str.Total ?? 0;
                            bt.Disc = str.DiscPer ?? 0;
                            bt.DiscAmt = str.DiscAmount ?? 0;
                            bt.FreightRate = str.ServiceTaxPer ?? 0;
                            bt.Freight = str.ServiceTaxAmount ?? 0;
                            bt.Sgst = str.AdVatAmount ?? 0;
                            bt.Cgst = str.VatAmount ?? 0;
                            bt.Igst = str.CSTAmount ?? 0;
                           
                            bt.NetTotal = str.NetTotal ?? 0;
                        }
                        bt.Remark = str.ItemRemark;
                        bt.UomId = GetUnitId(str.UnitID ?? 0);
                        
                        bts.Add(bt);

                    }

                    db.BillTrans.AddRange(bts);
                   

                    if (flg)
                    {
                        if ((spur.RoundOff ?? 0) != 0){
                            var total = bts.Sum(x => x.NetTotal);
                            var round = decimal.Round(total, 0) - total;
                            
                            model.TotalAmount = total + round; 
                            model.RoundOff = model.TotalAmount - total;
                        }
                        else
                            model.TotalAmount= bts.Sum(x => x.NetTotal);
                    }
                    else
                    {
                        model.TotalAmount = spur.BillAmount ?? 0;
                        model.RoundOff = spur.RoundOff ?? 0;
                    }
                        
                    

                    var bp = new BillPay();

                    foreach (var py in spur.SalesPay)
                    {
                        bp = new BillPay();

                        bp.IsActive = true;
                        bp.BillId = model.Id;
                        
                        if (py.CashAcID != null)
                            bp.Pay1Id = 1;
                        
                        if (py.BankAcID != null)
                            bp.Pay2Id = 2;

                        if (flg)
                            bp.Pay1Amt = model.TotalAmount;
                        else
                            bp.Pay1Amt = py.CashAmt ?? 0;

                        bp.Pay2Amt = py.CardAmt ?? 0;
                        bp.PayDate = py.PaymentDate ?? 0;
                        bp.DiscAmt = py.DiscAmt ?? 0;

                        bp.BillId = model.Id;
                        db.BillPays.Add(bp);
                    }

                   // db.SaveChanges();

                   // LedgerEff.BillRefEntry("Debit", model, 0, db);       //Insert or update in Billref table

                    //Insert or update in LedgerTrans table

                    if(spur.BillAmount > 0)
                        LedgerEff.LedgerTransEntry("Debit", model, db, bts,bp);
                    else
                        LedgerEff.LedgerTransEntry("Credit", model, db, bts, bp);

                    // Insert in BtoB for BillAdjustment
                    //    LedgerEff.BtoBEntry("PInvoice", model.Id, model, db, null);

                    //foreach (var item in bts)
                    //{ 
                    //    bool IsIssue = true;
                    //    string TableName = "SalesInvoice";

                    //    var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                    //    if (stockReq == "No")
                    //    {
                    //        continue;
                    //    }
                    //    StockEffect.StockTransBillEntry(model, item, IsIssue, TableName, db);
                    //}
                }

                progressBarControl1.PerformStep();
                progressBarControl1.Update();
                i++;
            }

            db.SaveChanges();

        }
        private void Purchase(KontoContext db,ImpContext sdb)
        {
            var fdt = Convert.ToInt32( fDateEdit1.DateTime.ToString("yyyyMMdd"));
            var tdt = Convert.ToInt32( tDateEdit2.DateTime.ToString("yyyyMMdd"));

            var spurs = sdb.Purchases.Include("PurchaseTrans")
                            .Where(x => x.VoucherDate >= fdt && x.VoucherDate <= tdt).ToList();

            foreach (var spur in spurs)
            {
                var model = db.Bills.SingleOrDefault(x =>x.RefId == spur.PurchaseID && x.RefVoucherId == spur.VoucherID && x.IsActive && !x.IsDeleted);
                
                if (model != null) continue;

                //check for exist account
                var checkAcc = db.Accs.SingleOrDefault(x => x.CollById == spur.AccountID);
                if (checkAcc == null)
                    checkAcc = CreateAccount(db, (int) spur.AccountID,sdb); //create account if not exists

                //check bok
                var chkbk = db.Accs.SingleOrDefault(x => x.CollById == spur.PurchaseAcID);
                if(chkbk==null)
                    chkbk = CreateAccount(db, (int)spur.PurchaseAcID, sdb); //create account if not exists

                if (model == null)
                {
                    model = new BillModel
                    {
                        AccId = checkAcc.Id,
                        BillType = "Regular",
                        VoucherId = db.Vouchers.FirstOrDefault(x => x.VTypeId == (int)VoucherTypeEnum.PurchaseInvoice).Id,
                        VoucherDate = spur.VoucherDate,Rcm="NO",Itc= "Inputs",
                        BookAcId = chkbk.Id,RcdDate = KontoUtils.IToD(spur.VoucherDate),
                        VoucherNo = spur.VoucherNo,RefNo = spur.ChallanNo,BillNo = spur.BillNo,
                        EmpId=1,StoreId=1,Remarks = spur.PurchaseRemark,DocNo = spur.LrNo,DocDate= KontoUtils.IToD(spur.LrDate),
                        TypeId = (int)VoucherTypeEnum.PurchaseInvoice,CompId = KontoGlobals.CompanyId,
                        YearId = KontoGlobals.YearId,BranchId = KontoGlobals.BranchId,RoundOff = spur.RoundOff,
                        Duedays = spur.DueDays,TdsAmt =spur.TdsAmount ==null ? 0:  (int) spur.TdsAmount,
                        TdsPer = spur.TDSPer==null ? 0 :  (int) spur.TDSPer,GrossAmount = spur.TotalGross,
                        TotalAmount = spur.BillAmount,TotalQty = spur.TotalQty,TotalPcs = spur.TotalPcs,
                        IsActive = true,RefId = (int) spur.PurchaseID,RefVoucherId = (int) spur.VoucherID

                    };

                    db.Bills.Add(model);
                    db.SaveChanges();

                    //var strs = sdb.PurchaseTranses.Where(x => x.PurchaseID == spur.PurchaseID).ToList();
                    List<BillTransModel> bts = new List<BillTransModel>();
                    foreach (var str in spur.PurchaseTrans)
                    {

                        //check for item exist
                        var chkitem = db.Products.SingleOrDefault(x => x.ParentItemId == str.ItemID);
                        if (chkitem == null)
                            chkitem = CreateProduct(db, (int)str.ItemID, sdb);

                        var bt = new BillTransModel
                        {
                            Cgst = str.VatAmount,
                            CgstPer = str.VatPer,
                            Cut = str.Cut,
                            ProductId = chkitem.Id,
                            BillId = model.Id,
                            Qty = str.Qty,
                            Pcs = (int)str.Pcs,
                            Rate = str.Rate,
                            Total = str.Total,
                            Disc = str.DiscPer,
                            DiscAmt = str.DiscAmount,
                            FreightRate = str.ExcisePer ?? 0,
                            Freight = str.ExciseAmount ?? 0,
                            Sgst = str.AdVatAmount,
                            SgstPer = str.AdVatPer,
                            Igst = str.CSTAmount,
                            IgstPer = str.CSTPer,
                            NetTotal = str.NetTotal,
                            Remark = str.ItemRemark,UomId = GetUnitId(str.UnitID ?? 0)
                        };
                        bts.Add(bt);
                                
                    }
                    db.BillTrans.AddRange(bts);

                    db.SaveChanges();

                    LedgerEff.BillRefEntry("Credit", model, 0, db);       //Insert or update in Billref table

                    //Insert or update in LedgerTrans table
                    LedgerEff.LedgerTransEntry("Credit", model, db, bts);

                    // Insert in BtoB for BillAdjustment
                   // LedgerEff.BtoBEntry("PInvoice", model.Id, model, db, null);

                    //foreach (var item in bts)
                    //{
                    //    bool IsIssue = false;
                    //    string TableName = "PurchaseInvoice";

                    //    var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                    //    if (stockReq == "No")
                    //    {
                    //        continue;
                    //    }
                    //    StockEffect.StockTransBillEntry(model, item, IsIssue, TableName, db);
                    //}
                }

            }

            db.SaveChanges();

           

        }

        private void PurchaseReturn(KontoContext db, ImpContext sdb)
        {
            var fdt = Convert.ToInt32(fDateEdit1.DateTime.ToString("yyyyMMdd"));
            var tdt = Convert.ToInt32(tDateEdit2.DateTime.ToString("yyyyMMdd"));

            var spurs = sdb.PurchaseRets.Include("PurchaseRetTrans")
                            .Where(x => x.VoucherDate >= fdt && x.VoucherDate <= tdt).ToList();

            foreach (var spur in spurs)
            {
                var bb = sdb.B2Bs.FirstOrDefault(x => x.RefCode == spur.RetCode && x.TransType =="rg");

                var model = db.Bills.SingleOrDefault(x => x.RefId == spur.PurchaseRetID && x.RefVoucherId == spur.VoucherID && x.IsActive && !x.IsDeleted);

                if (model != null) continue;

                //check for exist account
                var checkAcc = db.Accs.SingleOrDefault(x => x.CollById == spur.AccountID);
                if (checkAcc == null)
                    checkAcc = CreateAccount(db, (int)spur.AccountID, sdb); //create account if not exists

                //check bok
                var chkbk = db.Accs.SingleOrDefault(x => x.CollById == spur.PurchaseRetAcID);
                if (chkbk == null)
                    chkbk = CreateAccount(db, (int)spur.PurchaseRetAcID, sdb); //create account if not exists

                if (model == null)
                {
                    model = new BillModel
                    {
                        AccId = checkAcc.Id,
                        BillType = "Regular",
                        VoucherId = db.Vouchers.FirstOrDefault(x => x.VTypeId == (int) VoucherTypeEnum.PurchaseReturn)
                            .Id,
                        VoucherDate = spur.VoucherDate,
                        Rcm = "NO",
                        Itc = "Inputs",
                        BookAcId = chkbk.Id,
                        RcdDate =  KontoUtils.IToD( Convert.ToInt32(spur.OrderDate)),
                        VoucherNo = spur.VoucherNo,
                        RefNo = spur.ChallanNo,
                        BillNo = spur.BillNo,
                        EmpId = 1,
                        StoreId = 1,
                        Remarks = spur.PurchaseRemark,
                        DocNo = spur.LrNo,
                        //DocDate = KontoUtils.IToD(spur.LrDate),
                        TypeId = (int) VoucherTypeEnum.PurchaseReturn,
                        CompId = KontoGlobals.CompanyId,
                        YearId = KontoGlobals.YearId,
                        BranchId = KontoGlobals.BranchId,
                        RoundOff = spur.RoundOff,
                        //  Duedays = spur.DueDays,
                        // TdsAmt = spur.TdsAmount == null ? 0 : (int)spur.TdsAmount,
                        // TdsPer = spur.TDSPer == null ? 0 : (int)spur.TDSPer,
                        GrossAmount = spur.TotalGross,
                        TotalAmount = spur.BillAmount,
                        TotalQty = spur.TotalQty,
                        TotalPcs = spur.TotalPcs,
                        IsActive = true,
                        RefId = (int) spur.PurchaseRetID,
                        RefVoucherId = (int) spur.VoucherID,


                    };

                    db.Bills.Add(model);
                    db.SaveChanges();

                    //var strs = sdb.PurchaseTranses.Where(x => x.PurchaseID == spur.PurchaseID).ToList();
                    List<BillTransModel> bts = new List<BillTransModel>();
                    foreach (var str in spur.PurchaseRetTrans)
                    {

                        //check for item exist
                        var chkitem = db.Products.SingleOrDefault(x => x.ParentItemId == str.ItemID);
                        if (chkitem == null)
                            chkitem = CreateProduct(db, (int) str.ItemID, sdb);

                        var bt = new BillTransModel
                        {
                            Cgst = str.VatAmount,
                            CgstPer = str.VatPer,
                            Cut = str.Cut,
                            ProductId = chkitem.Id,
                            BillId = model.Id,
                            Qty = str.Qty,
                            Pcs = (int) str.Pcs,
                            Rate = str.Rate,
                            Total = str.Total,
                            Disc = str.DiscPer,
                            DiscAmt = str.DiscAmount,
                            FreightRate = str.ExcisePer,
                            Freight = str.ExciseAmount,
                            Sgst = str.AdVatAmount,
                            SgstPer = str.AdVatPer,
                            Igst = str.CSTAmount,
                            IgstPer = str.CSTPer,
                            NetTotal = str.NetTotal,
                            Remark = str.ItemRemark,
                            UomId = GetUnitId(str.UnitID ?? 0)
                        };
                        bts.Add(bt);

                    }

                    db.BillTrans.AddRange(bts);

                    if (bb != null)
                    {

                        var dpb = db.Bills.FirstOrDefault(x => x.RefId == bb.BillRefId
                                                               && x.RefVoucherId == bb.BillRefVoucherId);

                        if (dpb != null)
                        {
                            var br = db.BillRefs.FirstOrDefault(x => x.BillId == dpb.Id);
                            if (br != null)
                            {
                                var btob = new BtoBModel();


                                btob.Amount = bb.Amount;
                                btob.BillId = dpb.Id;
                                btob.RefId = model.Id;
                                btob.RefVoucherId = model.VoucherId;
                                btob.RefTransId = model.Id;
                                btob.BillNo = dpb.BillNo;
                                btob.BillTransId = dpb.Id;
                                btob.BillVoucherId = dpb.VoucherId;
                                btob.CompanyId = KontoGlobals.CompanyId;
                                btob.TransType = "Return";
                               
                                btob.RefCode = br.RowId;
                                db.BtoBs.Add(btob);
                            }
                        }
                    }
                





                    db.SaveChanges();

                    LedgerEff.BillRefEntry("Debit", model, 0, db);       //Insert or update in Billref table

                    //Insert or update in LedgerTrans table
                    LedgerEff.LedgerTransEntry("Debit", model, db, bts);

                    // Insert in BtoB for BillAdjustment
                    // LedgerEff.BtoBEntry("PInvoice", model.Id, model, db, null);

                    //foreach (var item in bts)
                    //{
                    //    bool IsIssue = false;
                    //    string TableName = "PurchaseInvoice";

                    //    var stockReq = db.Products.FirstOrDefault(k => k.Id == item.ProductId).StockReq;
                    //    if (stockReq == "No")
                    //    {
                    //        continue;
                    //    }
                    //    StockEffect.StockTransBillEntry(model, item, IsIssue, TableName, db);
                    //}
                }

            }

            db.SaveChanges();



        }

        private ProductModel CreateProduct(KontoContext db, long itemid, ImpContext sdb)
        {

            var sitem = sdb.Items.Find(itemid);
            string hsncode = "NA";
            if (!string.IsNullOrEmpty(sitem.HsnCode))
                hsncode = sitem.HsnCode;

            var pd = new ProductModel
            {
                ProductName= sitem.item_name,
                ProductDesc = sitem.print_name,ProductCode = sitem.item_code,
                BarCode = sitem.remark ?? sitem.item_code,Cut = sitem.cut ?? 0 ,
                BatchReq="No",CheckNegative= false,HsnCode= hsncode,
                PTypeId=(int)ProductTypeEnum.FINISH,PurUomId = GetUnitId( sitem.unit_id ?? 0) ,
                UomId = GetUnitId(sitem.unit_id ?? 0),IsActive= true,SerialReq="No",
                StockReq="Yes",ItemType="I",PurDisc=  sitem.dp_disc_per ?? 0,SaleDisc = sitem.Sale_Disc_Per ?? 0,
                SaleRateTaxInc = sitem.sale_vat_inc == null ? false: Convert.ToBoolean(sitem.sale_vat_inc),
                StyleId=1,GroupId=1,SubGroupId=1,CategoryId=1,SizeId=1,ColorId=1,TaxId = GetTaxId(sitem.VatClassID ?? 0),
                ParentItemId = (int) itemid

            };
            db.Products.Add(pd);
            db.SaveChanges();

            var pp = new PriceModel
            {
                ProductId = pd.Id,
                BatchNo = sitem.StyleNo,
                Mrp = sitem.mrp ?? 0,
                SaleRate = sitem.sale_rate ?? 0,
                DealerPrice = sitem.dealer_price ?? 0,
                BranchId = KontoGlobals.BranchId,
                Rate1 = sitem.WholeSaleRate ?? 0,
                Rate2 = sitem.SemiWholeSaleRate ?? 0
            };
            db.Prices.Add(pp);

            var complist = db.Companies.Where(p => p.IsActive && !p.IsDeleted).ToList();
            var yearlist = db.FinYears.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
            // var storelist = db.Stores.Where(x => x.IsActive && !x.IsDeleted).ToList();
            var branchlist = db.Branches.Where(x => x.IsActive && !x.IsDeleted).ToList();
            foreach (var comp in complist)
            {
                foreach (var yr in yearlist)
                {
                    foreach (var branch in branchlist)
                    {
                        // foreach (var store in storelist)
                        // {
                        StockBalModel _model = new StockBalModel();
                        
                           _model = new StockBalModel();

                            _model.ProductId = pd.Id;
                            _model.ItemCode = pd.RowId;

                            _model.CompanyId = comp.Id;
                            _model.YearId = yr.Id;
                            _model.BranchId = branch.Id;
                            _model.GodownId = KontoGlobals.GodownId;


                            _model.BalQty = 0;
                            _model.RowId = Guid.NewGuid();
                            _model.CreateUser = KontoGlobals.UserName;
                            _model.CreateDate = DateTime.Now;
                            _model.OpNos = 0;
                            _model.OpQty = 0;

                            db.StockBals.Add(_model);
                        

                        //}
                    }
                }
            }
            db.SaveChanges();


            return pd;
        }
        private int GetUnitId(long _unitname)
        {
            switch (_unitname)
            {
                case 1:
                    return 1;
                case 2:
                    return 6;
                case 3:
                    return 26;
                case 4:
                    return 14;
                case 5:
                    return 9;
                case 6:
                    return 20;
                case 7:
                    return 28;
                case 8:
                    return 28;
                case 9:
                    return 28;
                case 10:
                    return 28;
                case 11:
                    return 28;
                case 12:
                    return 9;
                case 13:
                    return 26;
                default:
                    return 28;
            }
        }

        private int GetTaxId(int _taxid)
        {
            switch (_taxid)
            {
                case 5:
                    return 3;
                case 6:
                    return 2;
                case 7:
                    return 1;
                case 8:
                    return 4;
                case 11:
                    return 7;
                default:
                    return 7;
            }
                
        }
        private AccModel CreateAccount(KontoContext db, int accid,ImpContext sdb)
        {
            var sac = sdb.Accounts.Find(accid);

            var ac = new AccModel()
            {
                AccName = sac.account_name,
                PrintName = sac.print_name,
                GstIn = sac.RegNo,
                CollById = (int)sac.account_Id,
                GroupId = sac.group_id== 22 ? 21 : 25,
                BToB="Yes",AgentId=1,CrDays= sac.cr_days,
                CrLimit= sac.cr_limit,IoTax="NA",PanNo= sac.pan_no,
                PGroupId=1,TcsReq="NO",TdsReq="NO",VatTds="REG",
                DeducteeId=1,NopId=1,TransportId=1,EmpId=1,Grade="A",
                CollDay="Monday",IsActive=true,
            };

            db.Accs.Add(ac);
            db.SaveChanges();

            //check for city && area exist in destination db
            int cityid = 1;
            int areaid = 1;
            var sct = sdb.Cities.Include("State").FirstOrDefault(x=>x.city_id== (int)sac.account_city);
            
            if(sct!=null)
            {
                var ct = db.Cities.FirstOrDefault(x => x.CityName == sct.city_name);
                if(ct==null)
                {
                    var stt = db.States.FirstOrDefault(x => x.StateName.ToUpper() == sct.State.state_name.ToUpper());
                    if(stt==null)
                    {
                        stt = new StateModel
                        {
                            CountryId = 1,
                            StateName = sct.State.state_name.ToUpper(),
                            GstCode = sct.State.prio.ToString(),IsActive=true
                        };
                        db.States.Add(stt);
                        db.SaveChanges();

                    }
                    ct = new CityModel
                    {
                        CityName = sct.city_name,
                        StateId = stt.Id,IsActive= true
                    };
                    db.Cities.Add(ct);
                    db.SaveChanges();
                }
                
                
                
                cityid = ct.Id;
                if (sac.account_area != null)
                {
                    var sar = sdb.Areas.FirstOrDefault(x => x.area_id == (int)sac.account_area);
                    if (sar != null)
                    {
                        var ar = db.Areas.FirstOrDefault(x => x.AreaName.ToUpper() == sar.area_name.ToUpper());
                        if (ar == null)
                        {
                            ar = new AreaModel { AreaName = sar.area_name.ToUpper(), CityId = cityid, IsActive = true };
                            db.Areas.Add(ar);
                            db.SaveChanges();
                            areaid = ar.Id;
                        }
                    }
                }
            }

            string address1 = KontoUtils.TruncateLongString(sac.address, 100);
            string address2 = string.Empty;

            if (sac.address.Length > 100)
                address2 = sac.address.Substring(100, Math.Min(sac.address.Length - 100, 100));

            var _addr = new AccAddressModel
            {
                AccId = ac.Id,
                CityId = cityid,
                AreaId = areaid,
                ContactPerson = sac.contact_person,
                AddressType = "Mailing Address",
                Address1 = address1,
                Address2 = address2,
                Email = sac.email,
                MobileNo = sac.mobile != null ? sac.mobile.Substring(0, Math.Min(sac.mobile.Length, 10)) : string.Empty,
                Phone = sac.telo,
                PinCode = sac.pin_code,
                Website = sac.website
            };

            db.AccAddresses.Add(_addr);
            db.SaveChanges();

            var yearlist = db.FinYears.ToList();
            var complist = db.Companies.ToList();
            foreach (var yr in yearlist)
            {
                foreach (var comp in complist)
                {
                    var _model = new AccBalModel
                    {
                        AccId = ac.Id,
                        AccRowId = ac.RowId,
                        AddressId = _addr.Id,
                        Bal = 0,
                        GroupId = (int) ac.GroupId,
                        CompId = comp.Id,
                        YearId = yr.Id,
                        OpBal = 0,
                        Address1 = _addr.Address1,
                        Address2 = _addr.Address2,
                        AreaId = _addr.AreaId,
                        CityId = _addr.CityId,
                        Email = _addr.Email,
                        MobileNo = _addr.MobileNo,
                        PinCode = _addr.PinCode,
                    };
                    db.AccBals.Add(_model);
                }
            }

            db.SaveChanges();

            return ac;
        }
    }
}
