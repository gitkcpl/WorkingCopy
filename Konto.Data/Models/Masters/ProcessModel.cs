using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("Process")]
    public class ProcessModel : AuditedEntity
    {
        [MaxLength(50)]
        [MinLength(2)]
        [Required(ErrorMessage = "Process Name is required")]
        [Display(Name = "Process Name")]
        public string ProcessName { get; set; }

        [MaxLength(15)]
        [Display(Name = "HsnCode")]
        public string HsnCode { get; set; }

        [Required(ErrorMessage = "Priority Required")]
        [Display(Name = "Priority")]
        public int? Priority { get; set; }

       
        public int? TaxId { get; set; }
        
        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [ForeignKey("TaxId")]
        public TaxModel TaxMaster { get; set; }
    }
}
