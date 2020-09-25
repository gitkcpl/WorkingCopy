using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class TaxLookupDto : BaseLookupDto
    {
        public string TaxType { get; set; }
        public decimal Sgst { get; set; }
        public decimal Cgst { get; set; }
        public decimal Igst { get; set; }
    }
}
