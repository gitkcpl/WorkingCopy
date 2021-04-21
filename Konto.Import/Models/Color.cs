using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "itemcol id")]
        public long itemcol_id { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "col name")]
        public string col_name { get; set; }

        [Required]
        [Display(Name = "prio")]
        public int prio { get; set; }

        [Display(Name = "Col Grp ID")]
        public short? ColGrpID { get; set; }

    }
}
