using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class LotAssignDto
    {
        public int BalId { get; set; }
        public string IssueNo { get; set; }

        public int? ChlnDate { get; set; }

        public DateTime? ChallanDate
        {
            get
            {
                if (ChlnDate != null)
                { return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }

        public string MillName { get; set; }

        public string LotNo { get; set; }

        public string Quality { get; set; }

        public int? Pcs { get; set; }

        public decimal Meter { get; set; }
    }
}
