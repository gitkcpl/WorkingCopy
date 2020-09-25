using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class LedgerMonthlyDto
    {
        public int AccId { get; set; }
        public int CompanyId { get; set; }
        public string Particular { get; set; }
        public decimal DebitAmt { get; set; }
        public decimal CreditAmt { get; set; }
        public decimal Amount { get; set; }
        public string Diff { get; set; }
        public int FinYear { get; set; }
        public int FinMonth { get; set; }
        public string BalanceAmt { get; set; }
        public string AccName { get; set; }



    }
}
