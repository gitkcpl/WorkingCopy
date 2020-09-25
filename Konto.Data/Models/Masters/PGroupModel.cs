using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("PGroup")]
    public class PGroupModel : AuditedEntity
    {
        public PGroupModel()
        {
            IsActive = true;
            
        }

        [MaxLength(15)]
        [Display(Name = "Group Code")]
        public string GroupCode { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Group Name is Required")]
        [MinLength(2)]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        public virtual ICollection<PSubGroupModel> SubGroups { get; set; }

    }
}
