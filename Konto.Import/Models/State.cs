using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
   public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int state_id { get; set; }

        [MaxLength(50)]
        public string state_name { get; set; }

        public int country_id { get; set; }

        public int prio { get; set; }

        //public country Country { get; set; }

        public List<City> Cities { get; set; }

    }
}
