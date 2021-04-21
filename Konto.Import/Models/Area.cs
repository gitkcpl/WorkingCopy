using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long area_id { get; set; }

        [MaxLength(50)]
        public string area_name { get; set; }

        public long city_id { get; set; }

        [MaxLength(15)]
        public string pin_code { get; set; }

        public int prio { get; set; }

        [ForeignKey("city_id")]
        public City City { get; set; }

    }
}
