using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("WarpItem")]
    public class WarpItemModel : AuditedEntity
    {
        public WarpItemModel()
        {
            IsActive = true;
           
        }
        [MaxLength(50)]
        [Display(Name = "Item Name")]
        [Required]
        public string ItemName { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }
    }
}
