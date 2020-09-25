using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class TrialDto 
    {
        public TrialDto()
        {

        }

        public int AcId { get; set; }

        public string GroupName { get; set; }
        public string Party { get; set; }

        public decimal OpDebit { get; set; }
        public decimal OpCredit { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal ClDebit { get; set; }
        public decimal ClCredit { get; set; }
        public bool Audit { get; set; }
    }
}
