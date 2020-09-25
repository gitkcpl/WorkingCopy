using System;

namespace Konto.Data.Models.Reports
{
    public class LedgertransDto
    {

        public DateTime TransDate { get; set; }

        public string VoucherNo { get; set; }

        public string BillNo { get; set; }

        public string Voucher { get; set; }

        public string Particular { get; set; }

        public decimal? DebitAmt { get; set; }

        public decimal? CreditAmt { get; set; }

        public string BalanceAmt { get; set; }

        public decimal? DebitInt { get; set; }

        public decimal? CreditInt { get; set; }

        public string IntAmt { get; set; }
        public string TotalBal { get; set; }
        public int? Days { get; set; }

        public string Bal { get; set; }

        public decimal? Amount { get; set; }

        public string Narration { get; set; }

        public string Chequeno { get; set; }

        public string AccountName { get; set; }

        public int flag { get; set; }

        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int? ReferenceAccountId { get; set; }

        public int? VTypeId { get; set; }

        public int? AccountId { get; set; }

        public string Remarks { get; set; }

        public int VoucherID { get; set; }

        public string AcGroup { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string GstIn { get; set; }

        public string PanNo { get; set; }

        public string Email { get; set; }

        public string MobileNo { get; set; }

        public string PinCode { get; set; }

        public string AreaName { get; set; }

        public string CityName { get; set; }

        public string StateName { get; set; }
        public bool? Audit { get; set; }
        public bool? IsSelected { get; set; }
        public Guid RefRowID { get; set; }

        public string Agent { get; set; }
        public int VoucherDate { get; set; }
       
       
    }
}
