using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class SaleRetAddon
    {
        [Required(ErrorMessage = "Sales Ret ID is required")]
        [Display(Name = "Sales Ret ID")]
        public long SalesRetID { get; set; }

        [Required(ErrorMessage = "Row ID is required")]
        [Display(Name = "Row ID")]
        public int RowID { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]
        public long AccountID { get; set; }

        [Required(ErrorMessage = "Amount For Calc is required")]
        [Display(Name = "Amount For Calc")]
        public decimal AmountForCalc { get; set; }

        [Required(ErrorMessage = "Default Per is required")]
        [Display(Name = "Default Per")]
        public decimal DefaultPer { get; set; }

        [Required(ErrorMessage = "Calc After Tax is required")]
        [Display(Name = "Calc After Tax")]
        public bool CalcAfterTax { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "No Effect In Bill is required")]
        [Display(Name = "No Effect In Bill")]
        public bool NoEffectInBill { get; set; }

        [Display(Name = "Cash Link ID")]
        public long? CashLinkID { get; set; }

        [Display(Name = "Bank Link ID")]
        public long? BankLinkID { get; set; }

        [Display(Name = "Jv Link ID")]
        public long? JvLinkID { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Sales Ret Addon ID is required")]
        [Display(Name = "Sales Ret Addon ID")]
        public long SalesRetAddonID { get; set; }

        [Required(ErrorMessage = "Post Ledger is required")]
        [Display(Name = "Post Ledger")]
        public bool PostLedger { get; set; }

        [Display(Name = "Calculate On")]
        public long? CalculateOn { get; set; }

        [Display(Name = "Tax Type")]
        public long? TaxType { get; set; }

        [Display(Name = "Round Off")]
        public bool? RoundOff { get; set; }

        [Display(Name = "Per Of Value")]
        public decimal? PerOfValue { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Sign")]
        public string Sign { get; set; }

        [Required(ErrorMessage = "Trans Code is required")]
        [Display(Name = "Trans Code")]
     //   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransCode { get; set; }

        [ForeignKey("SalesRetID")]
        public SaleRet SalesRet { get; set; }

      //  [ForeignKey("account_Id")]
      //  public account Account { get; set; }

    }
}
