using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("PSubGroup")]
    public class PSubGroupModel : AuditedEntity
    {
        public PSubGroupModel()
        {
            IsActive = true;
        }

        [MaxLength(15)]
        [Display(Name = "Sub Code")]
        public string SubCode { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Sub Group Name")]
        public string SubName { get; set; }

        [Required]
        [Range(1, 99999)]
        [Display(Name = "Group Id")]
        public int PGroupId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [ForeignKey("PGroupId")]
        public virtual PGroupModel PGroup { get; set; }

    }
}
