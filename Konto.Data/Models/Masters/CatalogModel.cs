using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Catalog")]
    public class CatalogModel : AuditedEntity
    {
        public CatalogModel()
        {
            IsActive = true;
        }

        [MaxLength(50)]
        [Display(Name = "CatalogNo")]
        public string CatalogNo { get; set; }

        [MaxLength(500)]
        [Required]
        [MinLength(2)]
        [Display(Name = "Catalog Name")]
        public string CatalogName { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

    }
}
