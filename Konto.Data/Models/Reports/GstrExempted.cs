using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class GstrExempted
    {
        public string Descriptions { get; set; }
        public decimal NilRated { get; set; }
        public decimal Exempted { get; set; }
        public decimal NonGst { get; set; }
    }
}
