using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("Color")]
    public class ColorModel : AuditedEntity
    {
        public ColorModel()
        {
            IsActive = true;
        }

        [MaxLength(15)]
        [Display(Name = "Color Code")]
        public string ColorCode { get; set; }

        [MaxLength(50)]
        [Required]
        [MinLength(2)]
        [Display(Name = "Color Name")]
        public string ColorName { get; set; }

        [MaxLength(50)]
        [Display(Name = "RGB")]
        public string RGB { get; set; }

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
