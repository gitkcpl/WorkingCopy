using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class JvBill
    {
        [Required(ErrorMessage = "Jv ID is required")]
        [Display(Name = "Jv ID")]
        public long JvID { get; set; }

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
        [Required(ErrorMessage = "Jv Bill ID is required")]
        [Display(Name = "Jv Bill ID")]
        public long JvBillID { get; set; }

        [Required(ErrorMessage = "Jv Trans ID is required")]
        [Display(Name = "Jv Trans ID")]
        public long JvTransID { get; set; }

        [Required(ErrorMessage = "Add Less Amount is required")]
        [Display(Name = "Add Less Amount")]
        public decimal AddLessAmount { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Display(Name = "Pre Paid")]
        public decimal? PrePaid { get; set; }

        [Display(Name = "Intrst Jv ID")]
        public short? IntrstJvID { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Required(ErrorMessage = "Bill Type is required")]
        [Display(Name = "Bill Type")]
        public string BillType { get; set; }

        [ForeignKey("JvID")]
        public Jv Jv { get; set; }

        [ForeignKey("JvTransID")]
        public JvTrans JvTrans { get; set; }

    }
}
