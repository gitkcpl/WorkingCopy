using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    [Table("sales_addon")]
    public class SaleAddon
    {
        public long SalesID { get; set; }

        public int? RowID { get; set; }

        public long? AccountID { get; set; }

        public decimal? AmountForCalc { get; set; }

        public decimal? DefaultPer { get; set; }

        public bool? CalcAfterTax { get; set; }

        public decimal Amount { get; set; }

        public bool? NoEffectInBill { get; set; }

        public long? CashLinkID { get; set; }

        public long? BankLinkID { get; set; }

        public long? JvLinkID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Sales Addon ID is required")]
        [Key]
        public long SalesAddonID { get; set; }

        public long? CompanyID { get; set; }

        public bool PostLedger { get; set; }

        public long? CalculateOn { get; set; }

        public long? TaxType { get; set; }

        public bool? RoundOff { get; set; }

        public decimal? PerOfValue { get; set; }

        [MaxLength(1)]
        public string Sign { get; set; }

        [Required(ErrorMessage = "Trans Code is required")]
       
        public Guid TransCode { get; set; }

        public Sale Sale { get; set; }

    }
}
