using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("LedgerTrans")]
    public class LedgerTransModel : AuditedEntity
    {
        public LedgerTransModel()
        {
            this.IsActive = true;
        }

        [Required(ErrorMessage = "Ref Id is required")]
        [Display(Name = "Ref Id")]
        [Index]
        public Guid RefId { get; set; }

        [Display(Name = "Trans Type")]
        public int? TransType { get; set; }

        [Required(ErrorMessage = "Company Id is required")]
        [Display(Name = "Company Id")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Year Id is required")]
        [Display(Name = "Year Id")]
        public int YearId { get; set; }

        [Required(ErrorMessage = "Branch Id is required")]
        [Display(Name = "Branch Id")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Voucher Id is required")]
        [Display(Name = "Voucher Id")]
        public int VoucherId { get; set; }

        [Required(ErrorMessage = "Voucher Date is required")]
        [Display(Name = "Voucher Date")]
        public int? VoucherDate { get; set; }

        [Required(ErrorMessage = "Trans Date is required")]
        [Display(Name = "Trans Date")]
        public DateTime TransDate { get; set; }

        [MaxLength(30)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [MaxLength(30)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Required(ErrorMessage = "Account Id is required")]
        [Display(Name = "Account Id")]
        public int? AccountId { get; set; }

        [Display(Name = "Ref Account Id")]
        public int? RefAccountId { get; set; }

        [Required(ErrorMessage = "Debit is required")]
        [Display(Name = "Debit")]
        public decimal Debit { get; set; }

        [Required(ErrorMessage = "Credit is required")]
        [Display(Name = "Credit")]
        public decimal Credit { get; set; }

        [Required(ErrorMessage = "Billl Amount is required")]
        [Display(Name = "Billl Amount")]
        public decimal BilllAmount { get; set; }

        [Display(Name = "Chq Date")]
        public DateTime? ChqDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Chq No")]
        public string ChqNo { get; set; }

        [Display(Name = "Agent Id")]
        public int? AgentId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Table Name")]
        public string TableName { get; set; }

        [Display(Name = "Key Fld Value")]
        public int? KeyFldValue { get; set; }

        [Display(Name = "Narration")]
        public string Narration { get; set; }

        [MaxLength(2000)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Lr Date")]
        public DateTime? LrDate { get; set; }

        [Display(Name = "Lr No")]
        public string LrNo { get; set; }

        [Display(Name = "Recon Date")]
        public int? ReconDate { get; set; }

        [Display(Name = "Audit")]
        public bool? Audit { get; set; }

        [Display(Name = "Adjust Amount")]
        public decimal? AdjustAmount { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Trans Code")]
        public Guid? TransCode { get; set; }

        [Display(Name = "Ret Amount")]
        public decimal? RetAmount { get; set; }

        [Display(Name = "Tds Amount")]
        public decimal? TdsAmount { get; set; }

        [MaxLength(1)]
        [Display(Name = "Op Bill")]
        public string OpBill { get; set; }

        [NotMapped]
        [Display(Name = "Account")]
        public string Account { get; set; }

        [NotMapped]
        [Display(Name = "RefAccount")]
        public string RefAccount { get; set; }

    }
}
