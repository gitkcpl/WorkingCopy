using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
   public  class Uom
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        [Display(Name = "unit id")]
        public long unit_id { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "unit name")]
        public string unit_name { get; set; }

        [Display(Name = "prio")]
        public int? prio { get; set; }

        [Required]
        [Display(Name = "No Of Decimal")]
        public byte NoOfDecimal { get; set; }

        [Required]
        [Display(Name = "Applicable On")]
        public bool ApplicableOn { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Gst Unit")]
        public string GstUnit { get; set; }



    }
}
