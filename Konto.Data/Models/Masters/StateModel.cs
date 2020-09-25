using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("State")]
    public class StateModel : AuditedEntity
    {
        public StateModel()
        {
            IsActive = true;
            IsDeleted = false;
        }

        [MaxLength(50)]
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [Display(Name = "Country Id")]
        public int CountryId { get; set; }

        [MaxLength(5)]
        [Display(Name = "Gst Code")]
        public string GstCode { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        // dbo.State.CountryId -> dbo.Country.Id (FK_State_Country)
        [ForeignKey("CountryId")]
        public virtual CountryModel Country { get; set; }

        public virtual ICollection<CityModel> Cities { get; set; }
    }
}
