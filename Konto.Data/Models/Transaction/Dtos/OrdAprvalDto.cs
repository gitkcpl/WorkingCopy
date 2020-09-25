using DevExpress.Data.Details;
using System;
using System.Globalization;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class OrdAprvalDto
    {
        public int? OrdDate { get; set; }
        public DateTime? OrderDate
        {
            get
            {
                if (OrdDate != null)
                { return (DateTime.ParseExact(OrdDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }
        public string OrderNo { get; set; }
        public string Supplier { get; set; }
        public string Product { get; set; }
        public string Color { get; set; }
        public decimal? Qty { get; set; }
        public decimal? PendQty { get; set; }
        public decimal? RcptQty { get; set; }

        public decimal? Rate { get; set; }
        public decimal? Total { get; set; }
        public decimal? NetTotal { get; set; }
        public int TransId { get; set; }
    }
}
