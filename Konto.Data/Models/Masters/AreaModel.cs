using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("Area")]
    public class AreaModel: AuditedEntity
    {
        public AreaModel()
        {
            IsActive = true;
            IsDeleted = false;
        }

        [MaxLength(50)]
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }

        [Display(Name = "City Id")]
        public int CityId { get; set; }

        [MaxLength(10)]
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [ForeignKey("CityId")]
        public virtual CityModel City { get; set; }



    }
}
