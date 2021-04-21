using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class CashTrans
    {
        [Required(ErrorMessage = "Cash ID is required")]
        [Display(Name = "Cash ID")]
        public long CashID { get; set; }

        [Required(ErrorMessage = "Row ID is required")]
        [Display(Name = "Row ID")]
        public int RowID { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]
        public long AccountID { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Method Of Adjust is required")]
        [Display(Name = "Method Of Adjust")]
        public int MethodOfAdjust { get; set; }

        [Display(Name = "Cash Remark")]
        public string CashRemark { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Cash Trans ID is required")]
        [Display(Name = "Cash Trans ID")]
        public long CashTransID { get; set; }

        [Display(Name = "Recon Date")]
        public int? ReconDate { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Display(Name = "Bill Party Account ID")]
        public long? BillPartyAccountID { get; set; }

        [Display(Name = "Transfer Data")]
        public bool? TransferData { get; set; }

        [Required(ErrorMessage = "Trans Code is required")]
        [Display(Name = "Trans Code")]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransCode { get; set; }

        [ForeignKey("CashID")]
        public Cash Cash { get; set; }

        //[ForeignKey("AccountID")]
        //public account Account { get; set; }

        //[ForeignKey("BillPartyAccountID")]
        //public account Account1 { get; set; }

    }
}
