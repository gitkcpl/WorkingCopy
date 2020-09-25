using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Division")]
    public class DivisionModel : AuditedEntity
    {
        [MaxLength(50)]
        [Display(Name = "Division Name")]
        public virtual string DivisionName { get; set; }

        [Display(Name = "Branch Id")]
        public virtual int? BranchId { get; set; }

        [MaxLength(200)]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [ForeignKey("BranchId")]
        public virtual BranchModel Branch { get; set; }
    }
}
