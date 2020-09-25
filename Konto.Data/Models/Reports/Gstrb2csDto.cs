using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class Gstrb2csDto
    {
        public string Type { get; set; }
        public string StateName { get; set; }
        public string GstIn { get; set; }
        public decimal GSTRate { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal Cess { get; set; }
        public int IsRevice { get; set; }

    }
}
