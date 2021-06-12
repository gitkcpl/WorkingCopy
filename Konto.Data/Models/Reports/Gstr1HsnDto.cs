using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class Gstr1HsnDto
    {
        public string HsnCode { get; set; }
        public string Description { get; set; }
        public string Uqc { get; set; }
        public int? UomId { get; set; }
        public decimal TotalQty { get; set; }
        public decimal BillAmount { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal TaxPer { get; set; }
        public decimal IgstAmt { get; set; }
        public decimal CgstAmt { get; set; }
        public decimal SgstAmt { get; set; }
        public decimal Cess { get; set; }

        public int GroupId { get; set; }
    }
}
