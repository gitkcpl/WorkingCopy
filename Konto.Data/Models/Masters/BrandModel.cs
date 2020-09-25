using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Brand")]
    public class BrandModel : AuditedEntity
    {
        public BrandModel()
        {
            
            IsActive = true;
            
        }

        [MaxLength(15)]
        [Display(Name = "Brand Code")]
        public string BrandCode { get; set; }

        [MaxLength(50)]
        [MinLength(2)]
        [Required(ErrorMessage = "Brand Name is required")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

    }
}
