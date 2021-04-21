using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Keysoft.Erp.Data.Models
{
    [Table("purchase_addon")]
    public class PurchaseAddOn
    {
        [Required(ErrorMessage = "Purchase ID is required")]
        public long PurchaseID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Account ID is required")]
        public long AccountID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Amount For Calc is required")]
        public decimal AmountForCalc { get; set; } // numeric(26,2), not null
        [Required(ErrorMessage = "Default Per is required")]
        public decimal DefaultPer { get; set; } // numeric(6,2), not null
        [Required(ErrorMessage = "Calc After Tax is required")]
        public bool CalcAfterTax { get; set; } // bit, not null
        public decimal? Amount { get; set; } // money, null
        [Required(ErrorMessage = "No Effect In Bill is required")]
        public bool NoEffectInBill { get; set; } // bit, not null
        public long? CashLinkID { get; set; } // bigint, null
        public long? BankLinkID { get; set; } // bigint, null
        public long? JvLinkID { get; set; } // bigint, null
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Purchase Addon ID is required")]
        public long PurchaseAddonID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Company ID is required")]
        public long CompanyID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Post Ledger is required")]
        public bool PostLedger { get; set; } // bit, not null
        public long? CalculateOn { get; set; } // bigint, null
        public long? TaxType { get; set; } // bigint, null
        public bool? IsRound { get; set; } // bit, null
        public decimal? PerOfValue { get; set; } // decimal(18,2), null
        [MaxLength(1)]
        public string Sign { get; set; } // varchar(1), null
      
        public Guid TransCode { get; set; }
        public Purchase Purchase { get; set; }
    }

}
