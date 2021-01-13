using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("Position")]
    public class PositionModel : AuditedEntity
    {
        public PositionModel()
        {
            IsActive = true;
            IsDeleted = false;
        }

        [MaxLength(50)]
        [Display(Name = "Position Name")]
        public string PositionName { get; set; }
         [MaxLength(500)]
        [Display(Name = "Remarks")]
        public string Remark { get; set; }

        //[Display(Name = "Machine Id")]
        //public int MacId { get; set; }
         
        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }
    }
}