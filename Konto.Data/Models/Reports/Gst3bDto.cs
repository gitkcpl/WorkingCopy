using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class Gst3bDto
    {
        public string TransType { get; set; }
        public string SupplyType { get; set; }
        public string StateName { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal UrdTaxable { get; set; }
        public decimal CmpTaxable { get; set; }
        public decimal UrdIgst { get; set; }
        public decimal CmpIgst { get; set; }
    }
}
