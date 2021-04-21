using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{

    public class BtoB
    {
        public decimal AddAmt { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [Display(Name = "Bill Ref Code")]
        public Guid? BillRefCode { get; set; }

        [Display(Name = "Bill Ref Id")]
        public long? BillRefId { get; set; }

        [Display(Name = "Bill Ref Voucher Id")]
        public long? BillRefVoucherId { get; set; }
        [Key]
        [Required(ErrorMessage = "Bto B Code is required")]
        [Display(Name = "Bto B Code")]
      //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BtoBCode { get; set; }

        public List<BtoBTrans> BtoBTrans { get; set; }

        //[ForeignKey("LedgerID")]
        //public Ledger Ledger { get; set; }

        [Display(Name = "Ledger Id")]
        public long? LedgerId { get; set; }
        public decimal LessAmt { get; set; }
        public bool Pending { get; set; }

        public decimal PrePaid { get; set; }

        [Display(Name = "Ref Code")]
        public Guid? RefCode { get; set; }

        [Display(Name = "Ref Id")]
        public long? RefId { get; set; }

        [Display(Name = "Ref Voucher Id")]
        public long? RefVoucherId { get; set; }

        [MaxLength(30)]
        [StringLength(30)]
        [Display(Name = "Source Type")]
        public string SourceType { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Trans Type")]
        public string TransType { get; set; }

    }


}
