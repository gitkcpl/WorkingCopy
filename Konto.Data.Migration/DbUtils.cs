using Konto.App.Shared;
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
                                      && x.CompId == KontoGlobals.CompanyId && x.Id!=_key && !x.IsDeleted);

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
    }
}
