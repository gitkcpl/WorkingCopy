using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Transaction
{
    [Table("BillRef")]
    public class BillRefModel : AuditedEntity
    {
        public BillRefModel()
        {
            IsActive = true;
        }

        [Display(Name = "Company Id")]
        public int? CompanyId { get; set; }

        [Display(Name = "Year Id")]
        public int? YearId { get; set; }

        [Display(Name = "Bill Id")]
        [Index]
        public int? BillId { get; set; }

        [Display(Name = "Bill Voucher Id")]
        public int? BillVoucherId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Display(Name = "Voucher Date")]
        public int? VoucherDate { get; set; }

        [Display(Name = "Bill Trans Id")]
        [Index]
        public int? BillTransId { get; set; }

        [Display(Name = "Gross Amt")]
        public decimal GrossAmt { get; set; }

        [Display(Name = "Bill Amt")]
        public decimal BillAmt { get; set; }

        [Display(Name = "Tds Amt")]
        public decimal TdsAmt { get; set; }

        [Display(Name = "Tcs Amt")]
        public decimal TcsAmt { get; set; }

        [Display(Name = "Ret Amt")]
        public decimal RetAmt { get; set; }

        [Display(Name = "Adjust Amt")]
        public decimal AdjustAmt { get; set; }

        [Display(Name = "Account Id")]
        public int? AccountId { get; set; }

        [Display(Name = "Agent Id")]
        public int? AgentId { get; set; }

        [Display(Name = "Item Id")]
        public int? ItemId { get; set; }

        [Display(Name = "Branch Id")]
        public int? BranchId { get; set; }

        [Display(Name = "Total Qty")]
        public decimal TotalQty { get; set; }

        [MaxLength]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Ref Type")]
        public string RefType { get; set; }

    }
}
