using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class BankBillAdjust
    {
        [Required(ErrorMessage = "Bank ID is required")]
        [Display(Name = "Bank ID")]
        public long BankID { get; set; }

        [Required(ErrorMessage = "Row ID is required")]
        [Display(Name = "Row ID")]
        public int RowID { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]
        public long AccountID { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Required(ErrorMessage = "Sign Type is required")]
        [Display(Name = "Sign Type")]
        public string SignType { get; set; }

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
        [Required(ErrorMessage = "Bank Bill Adjust ID is required")]
        [Display(Name = "Bank Bill Adjust ID")]
        public long BankBillAdjustID { get; set; }

        [Required(ErrorMessage = "Bank Bill ID is required")]
        [Display(Name = "Bank Bill ID")]
        public long BankBillID { get; set; }

        [Display(Name = "Sr No")]
        public long? SrNo { get; set; }

        [Display(Name = "Bill Voucher ID")]
        public long? BillVoucherID { get; set; }

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

        [ForeignKey("BankID")]
        public Bank Bank { get; set; }

        

    }
}
