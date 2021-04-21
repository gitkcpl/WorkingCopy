using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long city_id { get; set; }

        [MaxLength(50)]
        public string city_name { get; set; }

        public int state_id { get; set; }

        [MaxLength(15)]
        public string std_code { get; set; }

        public int prio { get; set; }

        [ForeignKey("state_id")]
        public State State { get; set; }

    }
}
