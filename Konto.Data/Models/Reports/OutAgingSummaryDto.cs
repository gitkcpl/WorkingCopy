using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class OutAgingSummaryDto
    {
        public string Account { get; set; }

        public int AccountId { get; set; }
        public decimal? Pending { get; set; }

        public decimal? Range1Value { get; set; }

        public decimal? Range2Value { get; set; }

        public decimal? Range3Value { get; set; }

        public decimal? Range4Value { get; set; }

        public decimal? AboveRangeValue { get; set; }
        public string PanNo { get; set; }
        public decimal Bal { get; set; }
        public ICollection<OutsAgeingFifoDto> FifoDetails { get; set; }
    }
}
