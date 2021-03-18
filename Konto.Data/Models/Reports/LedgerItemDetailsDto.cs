using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class LedgerItemDetailsDto 
    {
        public string Description { get; set; }
        public int Pcs { get; set; }
        public decimal Qty { get; set; }

        public string Unit { get; set; }
        public decimal Rate { get; set; }
        public decimal Taxable { get; set; }
        public decimal GstRate { get; set; }
        public decimal GstAmt { get; set; }
        public decimal NetTotal { get; set; }
        public decimal DiscAmt { get; set; }
        public string Remark { get; set; }
    }
}
