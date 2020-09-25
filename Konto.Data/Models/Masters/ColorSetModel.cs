using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("ColorSet")]
    public class ColorSetModel : AuditedEntity
    {
       
        [Required]
        [Display(Name = "Item Id")]
        public virtual int ItemId { get; set; }

        [Display(Name = "Color Id")]
        public virtual int? ColorId { get; set; }

        [Required]
        [Display(Name = "Rate")]
        public virtual decimal Rate { get; set; }

        [Required]
        [Display(Name = "Min Qty")]
        public virtual decimal MinQty { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }

       
    }

}
