using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("cost_heads")]
    public class CostHeadModel : AuditedEntity
    {
        [MaxLength(50)]
        [Display(Name = "Division Name")]
        public virtual string HeadName { get; set; }

        [Display(Name = "Branch Id")]
        public virtual int? BranchId { get; set; }

        [MaxLength(200)]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [ForeignKey("BranchId")]
        public virtual BranchModel Branch { get; set; }
    }
}
