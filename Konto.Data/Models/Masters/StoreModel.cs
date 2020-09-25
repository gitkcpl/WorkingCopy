using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("Store")]
    public class StoreModel : AuditedEntity
    {
        public StoreModel()
        {
            IsActive = true;
           
        }

        [MaxLength(50)]
        [MinLength(2)]
        [Required(ErrorMessage = "Store Name is required")]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [Display(Name = "Branch Id")]
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Branch Name is Required")]
        public int BranchId { get; set; }

        [MaxLength]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }


        [ForeignKey("BranchId")]
        public virtual BranchModel Branch { get; set; }
    }
}
