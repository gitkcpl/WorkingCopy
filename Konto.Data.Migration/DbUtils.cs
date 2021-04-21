using Konto.App.Shared;
using Konto.App.Shared.Para;
using Konto.Data.Models.Masters.Dtos;
using Konto.Data.Models.Transaction.Dtos;
using System.Linq;

namespace Konto.Data
{
    
    public class DbUtils
    {
        public static string NextSerialNo(int vid, KontoContext db,int isNew=0)
        {
            SerialDto serialno = null;

            serialno =  db.Database.SqlQuery<SerialDto>(
               "dbo.GenerateSerialNumberSp @voucherid={0},@Companyid={1},@FiscalYearId={2},@BranchId={3},@IsNew={4}",
               vid, KontoGlobals.CompanyId, KontoGlobals.YearId, KontoGlobals.BranchId,isNew).FirstOrDefault();

            return serialno.SerialNumber;
        }

        public static string NextSerialNo(int vid, int isNew = 0)
        {
            SerialDto serialno = null;
            using (var db = new KontoContext())
            {
                serialno = db.Database.SqlQuery<SerialDto>(
                   "dbo.GenerateSerialNumberSp @voucherid={0},@Companyid={1},@FiscalYearId={2},@BranchId={3},@IsNew={4}",
                   vid, KontoGlobals.CompanyId, KontoGlobals.YearId, KontoGlobals.BranchId, isNew).FirstOrDefault();
            }
            return serialno.SerialNumber;
        }

        public static SerialDto NextSerialDto(int vid, KontoContext db)
        {
            SerialDto serialno = null;

            serialno = db.Database.SqlQuery<SerialDto>(
               "dbo.GenerateSerialNumberSp @voucherid={0},@Companyid={1},@FiscalYearId={2},@BranchId={3}",
               vid, KontoGlobals.CompanyId, KontoGlobals.YearId, KontoGlobals.BranchId).FirstOrDefault();

            return serialno;
        }
        public static void UnHoldSerial(int vid,long _SerialValue)
        {
            using(var db = new KontoContext())
            {
                var sr = db.SerialNumbersShelfs.FirstOrDefault(x => x.VoucherId == vid && x.YearId == KontoGlobals.YearId
                    && x.BranchId == KontoGlobals.BranchId && x.Serial_Value == _SerialValue && x.Is_Hold);
                if (sr != null)
                {
                    sr.Is_Hold = false;
                    db.SaveChanges();
                }
            }
        }

        public static void UsedSerial(int vid, long _SerialValue,KontoContext db)
        {
                var sr = db.SerialNumbersShelfs.FirstOrDefault(x => x.VoucherId == vid && x.YearId == KontoGlobals.YearId
                    && x.BranchId == KontoGlobals.BranchId && x.Serial_Value == _SerialValue);
                if (sr != null)
                {
                    db.SerialNumbersShelfs.Remove(sr);
                    //db.SaveChanges();
                }
            
        }
        public static bool CheckExistVoucherNo(int vid,string _vno, KontoContext db, int _key)
        {
            var _exist = db.Bills.Any(x => x.VoucherNo == _vno && x.VoucherId == vid &&
                                      x.VoucherDate >= KontoGlobals.FromDate && x.VoucherDate <= KontoGlobals.ToDate 
                                      && x.CompId == KontoGlobals.CompanyId && x.Id!=_key && !x.IsDeleted && x.IsActive);

            return _exist;
        }
        public static AccLookupDto AccDetails(int id)
        {
            using (KontoContext ctx = new KontoContext())
            {
                var LookupDto = ctx.Database.SqlQuery<AccLookupDto>("EXEC dbo.acclookup @companyid={0},@yearid={1}, @accountid={2}",
                    KontoGlobals.CompanyId, KontoGlobals.YearId, id).FirstOrDefault();
                return LookupDto;
            }
        }

        public static bool CheckExistinBill(int? id, int? vId, KontoContext _context)
        {
            return  _context.BillTrans.Any(x =>
                x.RefId == id && x.RefVoucherId == vId && x.IsDeleted == false && x.IsActive == true);
        }

        public static bool CheckExistinChallan(int? id, int? vId, KontoContext _context)
        {
            return  _context.ChallanTranses.Any(x =>
                x.MiscId == id && x.RefVoucherId == vId && x.IsDeleted == false && x.IsActive == true);
        }

        public static ProductDetailsDto GetProductDetails(int id)
        {

            using (var _context = new KontoContext())
            {
                _context.Database.CommandTimeout = 0;
                var _model = (from pd in _context.Products
                              join bal in _context.StockBals on pd.Id equals bal.ProductId
                              join pr in _context.Prices on pd.Id equals pr.ProductId

                              join cat in _context.CategroyModels on pd.CategoryId equals cat.Id into cat_join
                              from cat in cat_join.DefaultIfEmpty()

                              join grp in _context.PGroups on pd.GroupId equals grp.Id into grp_join
                              from grp in grp_join.DefaultIfEmpty()

                              join sub in _context.PSubGroups on pd.SubGroupId equals sub.Id into sub_join
                              from sub in sub_join.DefaultIfEmpty()

                              join pt in _context.ProductTypes on pd.PTypeId equals pt.Id
                              join tx in _context.TaxMasters on pd.TaxId equals tx.Id
                              join um in _context.Uoms on pd.UomId equals um.Id

                              join sz in _context.SizeModels on pd.SizeId equals sz.Id into sz_join
                              from sz1 in sz_join.DefaultIfEmpty()

                              join ac in _context.Accs on pd.VendorId equals ac.Id into ac_join
                              from ac in ac_join.DefaultIfEmpty()

                              join br in _context.Brands on pd.BrandId equals br.Id into br_join
                              from br in br_join.DefaultIfEmpty()

                              join cl in _context.ColorModels on pd.ColorId equals cl.Id into cl_join
                              from cl in cl_join.DefaultIfEmpty()

                              where bal.CompanyId == KontoGlobals.CompanyId && bal.BranchId == KontoGlobals.BranchId && bal.YearId == KontoGlobals.YearId &&
                              !pd.IsDeleted && pd.Id == id
                              select new ProductDetailsDto()
                              {
                                  CheckNegative = pd.CheckNegative,
                                  BarCode = pd.BarCode,
                                  CatName = cat.CatName,
                                  DealerPrice = pr.DealerPrice,
                                  GroupName = grp.GroupName,
                                  ProductName = pd.ProductName,
                                  HsnCode = pd.HsnCode,
                                  Id = pd.Id,
                                  OpPcs = bal.OpNos,
                                  OpQty = bal.OpQty,
                                  ProductCode = pd.ProductCode,
                                  ProductType = pt.TypeName,
                                  SaleRate = pr.SaleRate,
                                  StockPcs = bal.BalNos + bal.OpNos,
                                  StockQty = bal.BalQty + bal.OpQty,
                                  SubName = sub.SubName,
                                  TaxName = tx.TaxName,
                                  UnitName = um.UnitName,
                                  UomId = pd.UomId,
                                  PurUomId = pd.PurUomId,
                                  PTypeId = pd.PTypeId,
                                  Vendor = ac.AccName,
                                  Sgst = tx.Sgst,
                                  Cgst = tx.Cgst,
                                  Igst = tx.Igst,
                                  Cess = tx.CessRate,
                                  SerialReq = pd.SerialReq,
                                  Cut = pd.Cut,
                                  TaxId = pd.TaxId,
                                  SaleRateTaxInc = pd.SaleRateTaxInc,
                                  SizeName = sz1.SizeName,
                                  BrandId = pd.BrandId,
                                  BrandName = br.BrandName,
                                  GroupId = pd.GroupId,
                                  CategroyId = pd.CategoryId,
                                  ColorId = pd.ColorId,
                                  ColorName = cl.ColorName,
                                  Mrp = pr.Mrp,
                                  Qty = pr.Qty,
                                  Rate1 = pr.Rate1,
                                  Rate2 = pr.Rate2,
                                  SizeId = pd.SizeId,
                                  SubGrupId = pd.SubGroupId,
                                  StyleNo = pr.BatchNo
                              }).FirstOrDefault();

                return _model;
            }
        }



        // get data by barcode
        public static ProductDetailsDto GetProductDetails(string barcode)
        {

            using (var _context = new KontoContext())
            {
                _context.Database.CommandTimeout = 0;
                var _model = (from pd in _context.Products
                              join bal in _context.StockBals on pd.Id equals bal.ProductId
                              join pr in _context.Prices on pd.Id equals pr.ProductId

                              join cat in _context.CategroyModels on pd.CategoryId equals cat.Id into cat_join
                              from cat in cat_join.DefaultIfEmpty()

                              join grp in _context.PGroups on pd.GroupId equals grp.Id into grp_join
                              from grp in grp_join.DefaultIfEmpty()

                              join sub in _context.PSubGroups on pd.SubGroupId equals sub.Id into sub_join
                              from sub in sub_join.DefaultIfEmpty()

                              join pt in _context.ProductTypes on pd.PTypeId equals pt.Id
                              join tx in _context.TaxMasters on pd.TaxId equals tx.Id
                              join um in _context.Uoms on pd.UomId equals um.Id

                              join sz in _context.SizeModels on pd.SizeId equals sz.Id into sz_join
                              from sz1 in sz_join.DefaultIfEmpty()

                              join ac in _context.Accs on pd.VendorId equals ac.Id into ac_join
                              from ac in ac_join.DefaultIfEmpty()

                              join br in _context.Brands on pd.BrandId equals br.Id into br_join
                              from br in br_join.DefaultIfEmpty()

                              join cl in _context.ColorModels on pd.ColorId equals cl.Id into cl_join
                              from cl in cl_join.DefaultIfEmpty()

                              join bc in _context.ItemBatches on pd.Id equals bc.ProductId into bc_join
                              from bc in bc_join.DefaultIfEmpty()

                              where bal.CompanyId == KontoGlobals.CompanyId && bal.BranchId == KontoGlobals.BranchId && bal.YearId == KontoGlobals.YearId &&
                              !pd.IsDeleted && pd.BarCode == barcode
                              select new ProductDetailsDto()
                              {
                                  CheckNegative = pd.CheckNegative,
                                  BarCode = pd.BarCode,
                                  CatName = cat.CatName,
                                  DealerPrice = pr.DealerPrice,
                                  GroupName = grp.GroupName,
                                  ProductName = pd.ProductName,
                                  HsnCode = pd.HsnCode,
                                  Id = pd.Id,
                                  OpPcs = bal.OpNos,
                                  OpQty = bal.OpQty,
                                  ProductCode = pd.ProductCode,
                                  ProductType = pt.TypeName,
                                  SaleRate = bc!=null ? bc.SaleRate : pr.SaleRate,
                                  StockPcs = bal.BalNos + bal.OpNos,
                                  StockQty = bal.BalQty + bal.OpQty,
                                  SubName = sub.SubName,
                                  TaxName = tx.TaxName,
                                  UnitName = um.UnitName,
                                  UomId = pd.UomId,
                                  PurUomId = pd.PurUomId,
                                  PTypeId = pd.PTypeId,
                                  Vendor = ac.AccName,
                                  Sgst = tx.Sgst,
                                  Cgst = tx.Cgst,
                                  Igst = tx.Igst,
                                  Cess = tx.CessRate,
                                  SerialReq = pd.SerialReq,
                                  Cut = pd.Cut,
                                  TaxId = pd.TaxId,
                                  SaleRateTaxInc = pd.SaleRateTaxInc,
                                  SizeName = sz1.SizeName,
                                  BrandId = pd.BrandId,
                                  BrandName = br.BrandName,
                                  GroupId = pd.GroupId,
                                  CategroyId = pd.CategoryId,
                                  ColorId = pd.ColorId,
                                  ColorName = cl.ColorName,
                                  Mrp = bc==null ? pr.Mrp : bc.Mrp,
                                  Qty = pr.Qty,
                                  Rate1 = bc != null ? bc.BulkRate : pr.Rate1 ,
                                  Rate2 = bc!=null ? bc.SemiBulkRate : pr.Rate2,
                                  SizeId = pd.SizeId,
                                  SubGrupId = pd.SubGroupId,
                                  StyleNo = pr.BatchNo,
                                  Description = pd.ProductDesc,
                                  ProfitPer = pd.Price1
                              }).FirstOrDefault();

                return _model;
            }
        }

        public static decimal GetCurrentStock(int productid, int branchid)
        {
            using (var db = new KontoContext())
            {
                var st = db.StockBals.FirstOrDefault(x => x.CompanyId == KontoGlobals.CompanyId &&
                                    x.YearId == KontoGlobals.YearId && x.BranchId == branchid && x.ProductId == productid);

                var stb = (from p in db.StockTranses
                           where p.ItemId == productid && p.BranchId == branchid
                           group p by 1 into g
                           select new
                           {
                               Stock = g.Sum(x => x.RcptQty) - g.Sum(x => x.IssueQty)
                           }).FirstOrDefault();
                if (stb != null)
                {
                    var stock = st.OpQty + stb.Stock;
                    return stock;
                }
                else
                    if (st != null)
                    return st.OpQty;
                else
                    return 0;

            }
        }

        public static void SetSysParameter()
        {
            // global system level parameter reading
            var db = new KontoContext();
            var sysparas = db.SysParas.Where(x => x.Category == "sys");

            foreach (var item in sysparas)
            {
                if (item.Id == 500)
                    SysParameter.AspUserId = item.DefaultValue;
                else if (item.Id == 501)
                    SysParameter.AspUserPass = item.DefaultValue;
                else if (item.Id == 503)
                    SysParameter.AspGspName = item.DefaultValue;
                else if (item.Id == 504)
                    SysParameter.AspApiBaseUrl = item.DefaultValue;
                else if (item.Id == 505)
                    SysParameter.Common_Order = item.DefaultValue == "Y" ? true : false;
                else if (item.Id == 506)
                    SysParameter.Common_Stock = item.DefaultValue == "Y" ? true : false;
            }
        }

        public static void Update_Account_Balance(KontoContext db = null)
        {
            if (db == null)
                db = new KontoContext();


            db.Database.ExecuteSqlCommand("dbo.update_account_balance @fromdate={0},@todate={1}," +
                "@compid={2}, @yearid={3}", KontoGlobals.FromDate,
                KontoGlobals.ToDate, KontoGlobals.CompanyId, KontoGlobals.YearId);
        }
    }
}
