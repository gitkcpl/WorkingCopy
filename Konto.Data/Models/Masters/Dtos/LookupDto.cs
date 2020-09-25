using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class CityLookupDto
    {
        [Key]
        public int Id { get; set; }
        public string DisplayText { get; set; }

        public string State { get; set; }
    }
}
