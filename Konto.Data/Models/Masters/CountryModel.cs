using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Country")]
    public class CountryModel : AuditedEntity
    {
        public CountryModel()
        {
            IsActive = true;
            IsDeleted = false;
        }

        [MaxLength(15)]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        public virtual ICollection<StateModel> States { get; set; }
    }
}
