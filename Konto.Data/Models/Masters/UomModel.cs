using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Uom")]
    public class UomModel : AuditedEntity
    {
        public UomModel()
        {
           
            IsActive = true;

        }

        [MinLength(2)]
        [MaxLength(3)]
        [Required(ErrorMessage = "Unit Code is required")]
        [Display(Name = "Unit Code")]
        public string UnitCode { get; set; }

        [MaxLength(50)]
        [MinLength(2)]
        [Required(ErrorMessage = "Unit Name is required")]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        public int Nod { get; set; }

        [MaxLength(1)]
        [Required]
        public string RateOn { get; set; }

        [MaxLength(50)]
        [Display(Name = "Gst Unit")]
        [Required]
        public string GSTUnit { get; set; }

        [MaxLength(100)]
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
