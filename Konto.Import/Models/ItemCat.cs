using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class ItemCat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "itemcat id is required")]
        public long itemcat_id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "cat name is required")]
        public string cat_name { get; set; }

        [Required(ErrorMessage = "prio is required")]
        public int prio { get; set; }

    }
}
