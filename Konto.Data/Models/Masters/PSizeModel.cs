using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("PSize")]
    public class PSizeModel : AuditedEntity
    {
        public PSizeModel()
        {
            IsActive = true;
          
        }
        [MaxLength(15)]
        [Display(Name = "Size Code")]
        public string SizeCode { get; set; }

        [MaxLength(50)]
        [Required]
        [MinLength(1)]
        [Display(Name = "Size Name")]
        public string SizeName { get; set; }

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
