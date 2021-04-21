using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
   public class SalesPurchaseFooter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Sr No is required")]
        public int SrNo { get; set; } // int, not null

        public long? AccountID { get; set; } // bigint, null

        public long? ShowIn { get; set; } // bigint, null

        public decimal? DefaultPer { get; set; } // numeric(6,2), null

        public bool? PostLedger { get; set; } // bit, null

        public int? OrderIndex { get; set; } // int, null

        public long? CalculateOn { get; set; } // bigint, null

        [Required(ErrorMessage = "Round Off is required")]
        public bool RoundOff { get; set; } // bit, not null

        public decimal? PerOfValue { get; set; } // decimal(18,2), null

        [MaxLength(1)]
        public string Sign { get; set; } // varchar(1), null

        public bool? NoEffectInBill { get; set; } // bit, null

        public long? revAccountID { get; set; } // bigint, null

        public long? RebateAccountID { get; set; }

        public long? ImpPaybleAccountID { get; set; }

        [ForeignKey ("AccountID")]
        public virtual account account { get; set; }
    }
}

