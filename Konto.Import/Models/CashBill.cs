using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class CashBill
    {
        [Required(ErrorMessage = "Cash ID is required")]
        [Display(Name = "Cash ID")]
        public long CashID { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Sr No is required")]
        [Display(Name = "Sr No")]
        public long SrNo { get; set; }

        [Required(ErrorMessage = "Bill Voucher ID is required")]
        [Display(Name = "Bill Voucher ID")]
        public long BillVoucherID { get; set; }

        [Required(ErrorMessage = "Bill Close is required")]
        [Display(Name = "Bill Close")]
        public bool BillClose { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Cash Bill ID is required")]
        [Display(Name = "Cash Bill ID")]
        public long CashBillID { get; set; }

        [Required(ErrorMessage = "Cash Trans ID is required")]
        [Display(Name = "Cash Trans ID")]
        public long CashTransID { get; set; }

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
        public long? IntrstJvID { get; set; }

        [ForeignKey("CashID")]
        public Cash Cash { get; set; }

        [ForeignKey("CashTransID")]
        public CashTrans CashTrans { get; set; }

    }
}
