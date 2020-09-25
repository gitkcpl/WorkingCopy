using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("OrdDelv")]
    public class OrdDelvModel : AuditedEntity
    {
        public OrdDelvModel()
        {
            IsActive = true;
           
        }

        [Display(Name = "Order Id")]
        public int OrdId { get; set; }

        [Display(Name = "Account Id")]
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Party Name is Required")]
        public int AccId { get; set; }

        [Required]
        [Display(Name = "OrdDelv Date")]
        public DateTime OrdDelvDate { get; set; }


        [Required]
        [Display(Name = "Qty")]
        public decimal Qty { get; set; }

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
