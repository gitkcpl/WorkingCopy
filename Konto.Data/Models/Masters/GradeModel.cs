using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Grade")]
    [Serializable]
    public class GradeModel : AuditedEntity
    {
        public GradeModel()
        {
            this.IsActive = true;
        }

        [MaxLength(50)]
        [Display(Name = "Grade Name")]
        [Required]
        public string GradeName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [Display(Name = "RefGradeId")]
        public int? RefGradeId { get; set; }

        [Display(Name = "Start Weight")]
        public decimal StartWt { get; set; }

        [Display(Name = "End Weight")]
        public decimal EndWt { get; set; }

        [Display(Name = "Rate Difference")]
        public decimal RateDiff { get; set; }
    }
}
