using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("PayTerms")]
    public class TermsModel : AuditedEntity
    {
        [MaxLength(100)]
        [Display(Name = "Descr")]
        public string PayDescr { get; set; }

        [Required]
        [Display(Name = "Days")]
        public int Days { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }


    }
}
