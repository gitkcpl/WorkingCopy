using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class SalesRetBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Sales Ret Bill ID is required")]
        [Display(Name = "Sales Ret Bill ID")]
        public long SalesRetBillID { get; set; }

        [Display(Name = "Sales Ret ID")]
        public long? SalesRetID { get; set; }

        [Display(Name = "Sales ID")]
        public long? SalesID { get; set; }

        [Display(Name = "Voucher ID")]
        public long? VoucherID { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [Display(Name = "Bill Close")]
        public bool? BillClose { get; set; }

        [Display(Name = "Company ID")]
        public long? CompanyID { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "B Ill No")]
        public string BIllNo { get; set; }

        [ForeignKey("SalesRetID")]
        public SaleRet SalesRet { get; set; }

        //[ForeignKey("voucher_id")]
        //public voucher Voucher { get; set; }

    }
}
