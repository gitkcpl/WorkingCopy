using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Style")]
    public class StyleModel : AuditedEntity
    {
        

        

        [MaxLength(15)]
        [Display(Name = "Style Code")]
        public virtual string StyleCode { get; set; }

        [MaxLength(50)]
        [MinLength(2)]
        [Display(Name = "Style Name")]
        [Required]
        public virtual string StyleName { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }


        public virtual ICollection<ProductModel> Products { get; set; }
    }

}
