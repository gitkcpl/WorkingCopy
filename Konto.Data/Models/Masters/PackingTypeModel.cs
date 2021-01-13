using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("PackingType")]
    public class PackingTypeModel : AuditedEntity
    {
        public PackingTypeModel()
        {
            IsActive = true;
            IsDeleted = false;
        }

        [MaxLength(50)]
        [MinLength(2)]
        [Required(ErrorMessage = "Packing Type Name is required")]
        [Display(Name = "Packing Type")]
        public string TypeName { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

    }
}
