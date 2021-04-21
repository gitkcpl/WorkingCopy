using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Transport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long trans_id { get; set; }

        [MaxLength(100)]
        public string trans_name { get; set; }

        [MaxLength(100)]
        public string print_name { get; set; }

        [MaxLength(200)]
        public string address { get; set; }

        public long? area_id { get; set; }

        public long? city_id { get; set; }

        [MaxLength(15)]
        public string pin_code { get; set; }

        [MaxLength(50)]
        public string phone { get; set; }

        [MaxLength(50)]
        public string mobile { get; set; }

        [MaxLength(50)]
        public string fax { get; set; }

        [MaxLength(50)]
        public string email { get; set; }

        public int? prio { get; set; }

        [MaxLength(200)]
        public string remark { get; set; }

        [ForeignKey("area_id")]
        public Area Area { get; set; }

        [ForeignKey("city_id")]
        public City City { get; set; }

    }
}
