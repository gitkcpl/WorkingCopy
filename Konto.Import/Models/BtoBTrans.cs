using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Keysoft.Erp.Data.Models
{
    public class BtoBTrans
    {
        [Key]
        [Required(ErrorMessage = "Trans Code is required")]
        [Display(Name = "Trans Code")]
     //   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransCode { get; set; }

        [Display(Name = "Bto B Code")]
        public Guid? BtoBCode { get; set; }

        [Display(Name = "Account ID")]
        public long? AccountID { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Post Date")]
        public int? PostDate { get; set; }

        [Required(ErrorMessage = "Per is required")]
        [Display(Name = "Per")]
        public decimal Per { get; set; }

        public string BillNo { get; set; }

        [ForeignKey("BtoBCode")]
        public BtoB BtoB { get; set; }

    }
}
