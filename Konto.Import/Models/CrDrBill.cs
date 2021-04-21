using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class CrDrBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Bill Id is required")]
        [Display(Name = "Bill Id")]
        public int BillId { get; set; }

        [Display(Name = "Id")]
        public long? Id { get; set; }

        [Display(Name = "Bill Voucher ID")]
        public long? BillVoucherID { get; set; }

        [Display(Name = "Sr No")]
        public long? SrNo { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Bill Close is required")]
        [Display(Name = "Bill Close")]
        public bool BillClose { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [ForeignKey("Id")]
        public CrDrNote CrDrNote { get; set; }
    }
}

