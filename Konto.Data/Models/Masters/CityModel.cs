using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("City")]
    public class CityModel : AuditedEntity
    {
        public CityModel()
        {
            IsActive = true;
            IsDeleted = false;
        }

        [MaxLength(50)]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Display(Name = "State Id")]
        public int StateId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Std Code")]
        public string StdCode { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [ForeignKey("StateId")]
        public virtual StateModel State { get; set; }

        public virtual ICollection<AreaModel> Areas
        {
            get;
            set;
        }

    }
}
