using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class PurchaseRetBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Pur Ret Bill ID is required")]
        [Display(Name = "Pur Ret Bill ID")]
        public long PurRetBillID { get; set; }

        [Required(ErrorMessage = "Purchase Ret ID is required")]
        [Display(Name = "Purchase Ret ID")]
        public long PurchaseRetID { get; set; }

        [Required(ErrorMessage = "Sr No is required")]
        [Display(Name = "Sr No")]
        public long SrNo { get; set; }

        [Required(ErrorMessage = "Bill Voucher ID is required")]
        [Display(Name = "Bill Voucher ID")]
        public long BillVoucherID { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [Display(Name = "Bill Close")]
        public bool? BillClose { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [ForeignKey("PurchaseRetID")]
        public PurchaseRet PurchaseRet { get; set; }

        //[ForeignKey("PurchaseID")]
        //public purchase Purchase { get; set; }

        //[ForeignKey("voucher_id")]
        //public voucher Voucher { get; set; }
    }
}

