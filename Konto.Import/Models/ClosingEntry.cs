using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class ClosingEntry
    {
        [Display(Name = "Debit Accont ID")]
        public long? DebitAccontID { get; set; }

        [Display(Name = "Credit Account ID")]
        public long? CreditAccountID { get; set; }

        [Display(Name = "Share Per")]
        public decimal? SharePer { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [Display(Name = "Post Date")]
        public int? PostDate { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Sr No is required")]
        [Display(Name = "Sr No")]
        public int SrNo { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Trans Type")]
        public string TransType { get; set; }

        [Display(Name = "Year ID")]
        public int? YearID { get; set; }

        [Display(Name = "Company ID")]
        public long? CompanyID { get; set; }

        [Display(Name = "Voucher ID")]
        public long? VoucherID { get; set; }

    }
}
