using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class MiDto
    {
        public int? Id { get; set; }
        public int? AccID { get; set; }
        public string Party { get; set; }
        public int? VoucherId { get; set; }
        public int? ChallanType { get; set; }
        public int? GodownId { get; set; }
        public int? TypeId { get; set; }
        public string store { get; set; }
        public int? ChlnDate { get; set; }
        public DateTime? ChallanDate
        {
            get
            {
                if (ChlnDate != null)
                { return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
            set { }
        }
        public string VoucherNo { get; set; }
        public decimal? TotalPc { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? Rate { get; set; }
        public string MillLot { get; set; }
        public string Remark { get; set; }
        public string CardNo { get; set; }

        public string QualityWt { get; set; }
        public string MarkaNo { get; set; }
        public string Program { get; set; }
    }
}
