using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class OutSummaryDTO
    {

        public string Account { get; set; }


        public int? AccountId { get; set; }


        public string Agent { get; set; }

        public string MobileNo { get; set; }


        public decimal? GrossAmt { get; set; }

        public decimal? BillAmt { get; set; }

        public decimal? AdjustAmt { get; set; }

        public decimal? ReturnAmt { get; set; }

        public decimal? TdsAmt { get; set; }

        public decimal? TcsAmt { get; set; }

        public decimal? PendingAmt { get; set; }

        public string Pending
        {
            get
            {
                if (this.PendingAmt > 0)
                    return Convert.ToDecimal(this.PendingAmt).ToString("F") + " Dr";
                else
                    return Convert.ToDecimal(this.PendingAmt).ToString("F") + " Cr";
            }
        }


        public int? Days { get; set; }


        public string Bal { get; set; }

        public ICollection<OutsDto> OutsDetails { get; set; }
    }
}
