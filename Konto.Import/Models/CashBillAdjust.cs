using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class CashBillAdjust
    {
        [Required(ErrorMessage = "Cash ID is required")]
        [Display(Name = "Cash ID")]
        public long CashID { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]
        public long AccountID { get; set; }

        [Required(ErrorMessage = "Per is required")]
        [Display(Name = "Per")]
        public decimal Per { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Adjust In Bill is required")]
        [Display(Name = "Adjust In Bill")]
        public bool AdjustInBill { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Cash Bill Adjust ID is required")]
        [Display(Name = "Cash Bill Adjust ID")]
        public long CashBillAdjustID { get; set; }

        [Required(ErrorMessage = "Cash Bill ID is required")]
        [Display(Name = "Cash Bill ID")]
        public long CashBillID { get; set; }

        [Required(ErrorMessage = "Sr No is required")]
        [Display(Name = "Sr No")]
        public long SrNo { get; set; }

        [Required(ErrorMessage = "Bill Voucher ID is required")]
        [Display(Name = "Bill Voucher ID")]
        public long BillVoucherID { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Display(Name = "Post Date")]
        public int? PostDate { get; set; }

        [Display(Name = "Return Meters")]
        public decimal? ReturnMeters { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [ForeignKey("CashID")]
        public Cash Cash { get; set; }

        //[ForeignKey("account_Id")]
        //public account Account { get; set; }

    }
}
