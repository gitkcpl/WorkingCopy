using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class gstr1_docDto
    {
        public string DocType { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Total { get; set; }
        public string StratBill { get; set; }
        public string EndBill { get; set; }
        public int TotalCancel { get; set; }
        public int VoucherID { get; set; }
    }
}
