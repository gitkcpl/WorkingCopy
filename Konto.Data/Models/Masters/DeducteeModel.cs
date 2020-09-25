using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("Deductee")]
    public class DeducteeModel: AuditedEntity
    {
        [MaxLength(100)]
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Descr
        {
            get;set;
        } // varchar(100)

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1
        {
            get; set;
        } // varchar(50)

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2
        {
            get; set;
        } // varcha
    }
}
