using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class BankBill
    {
        [Required(ErrorMessage = "Bank ID is required")]
        [Display(Name = "Bank ID")]
        public long BankID { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Sr No")]
        public long? SrNo { get; set; }

        [Display(Name = "Bill Voucher ID")]
        public long? BillVoucherID { get; set; }

        [Required(ErrorMessage = "Bill Close is required")]
        [Display(Name = "Bill Close")]
        public bool BillClose { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Bank Bill ID is required")]
        [Display(Name = "Bank Bill ID")]
        public long BankBillID { get; set; }

        [Required(ErrorMessage = "Bank Trans ID is required")]
        [Display(Name = "Bank Trans ID")]
        public long BankTransID { get; set; }

        [Required(ErrorMessage = "Add Less Amount is required")]
        [Display(Name = "Add Less Amount")]
        public decimal AddLessAmount { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Display(Name = "Pre Paid")]
        public decimal? PrePaid { get; set; }

        [Display(Name = "Jv ID")]
        public long? JvID { get; set; }

        [Display(Name = "Intrst Jv ID")]
        public short? IntrstJvID { get; set; }

        [ForeignKey("BankID")]
        public Bank Bank { get; set; }

        [ForeignKey("BankTransID")]
        public BankTrans BankTrans { get; set; }

    }
}
