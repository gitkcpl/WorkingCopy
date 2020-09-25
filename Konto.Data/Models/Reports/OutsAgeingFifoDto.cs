using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class OutsAgeingFifoDto
    {
        public string Account { get; set; }

        public int AccountId { get; set; }

        public string BillNo { get; set; }

        public string voucher_name { get; set; }

        public DateTime TransDate { get; set; }

        public int? voucher_id { get; set; }

        public string VoucherNo { get; set; }

        public decimal? Pending { get; set; }

        public decimal? Range1Value { get; set; }

        public decimal? Range2Value { get; set; }

        public decimal? Range3Value { get; set; }

        public decimal? Range4Value { get; set; }

        public decimal? AboveRangeValue { get; set; }

        public string PartyEmail { get; set; }

        public string area_name { get; set; }

        public int? area_id { get; set; }

        public string GroupName { get; set; }

        public string PanNo { get; set; }

        public string city_name { get; set; }

        public int? city_id { get; set; }

        public string Address { get; set; }

        public string PartyGroup { get; set; }

        public int? party_id { get; set; }

        public int? CompID { get; set; }

        public string CompName { get; set; }

        public string mobile { get; set; }

        public string Agent { get; set; }
        public decimal Bal { get; set; }

    }
}
