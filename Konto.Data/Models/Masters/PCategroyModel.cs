using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("PCategory")]
    public class PCategroyModel : AuditedEntity
    {
        public PCategroyModel()
        {
            IsActive = true;
        }

        [MaxLength(15)]
        [Display(Name = "Cat Code")]
        public string CatCode { get; set; }

        [MaxLength(50)]
        [Required]
        [MinLength(2)]
        [Display(Name = "Cat Name")]
        public string CatName { get; set; }

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
