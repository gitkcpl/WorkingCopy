using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class JvTrans
    {
        [Required(ErrorMessage = "Jv ID is required")]
        [Display(Name = "Jv ID")]
        public long JvID { get; set; }

        [Required(ErrorMessage = "Row ID is required")]
        [Display(Name = "Row ID")]
        public int RowID { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]
        public long AccountID { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Method Of Adjust")]
        public int? MethodOfAdjust { get; set; }

        [Display(Name = "Jv Remark")]
        public string JvRemark { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Jv Trans ID is required")]
        [Display(Name = "Jv Trans ID")]
        public long JvTransID { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Display(Name = "Ledger Sel")]
        public bool? LedgerSel { get; set; }

        [Required(ErrorMessage = "Trans Code is required")]
        [Display(Name = "Trans Code")]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransCode { get; set; }

        [ForeignKey("JvID")]
        public Jv Jv { get; set; }

        //[ForeignKey("account_Id")]
        //public account Account { get; set; }

    }
}
