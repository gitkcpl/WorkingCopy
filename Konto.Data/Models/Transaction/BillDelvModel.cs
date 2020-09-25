using Konto.Data.Models.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Transaction
{
    [Table("BillDelv")]
    public class BillDelvModel : BaseEntity
    {
        public BillDelvModel()
        {
            IsActive = true;
            Id = 0;
        }

        [Display(Name = "Bill Id")]
        public int BillId { get; set; }

        [Display(Name = "Account Id")]
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Party Name is Required")]
        public int AccId { get; set; }

        [Required]
        [Display(Name = "BillDel Date")]
        public DateTime BillDelDate { get; set; }


        [Required]
        [Display(Name = "Qty")]
        public decimal Qty { get; set; }


        [Required]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }


        [MaxLength]
        [MinLength(2)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [NotMapped]
        public string PartyName { get; set; }


        [NotMapped]
        public string Address { get; set; }

        [ForeignKey("AccId")]
        public virtual AccModel Acc { get; set; }

    }
}
