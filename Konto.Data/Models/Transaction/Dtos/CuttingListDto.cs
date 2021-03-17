using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class CuttingListDto
    {
        public string LotNo { get; set; }
        public string ChallanNo { get; set; }
        public int? ChlnDate { get; set; }
        public DateTime? ChallanDate
        {
            get
            {
                if (ChlnDate != null)
                {
                    return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture));
                }
                else return null;
            }
        }

        public string TakaNo { get; set; }
        public int Pcs { get; set; }
        public string Product { get; set; }
        public int? ProductId { get; set; }

        public string DesignName { get; set; }
        public string DesignNo { get; set; }
        public int? DesignId { get; set; }

        public string ColorName { get; set; }
        public int? ColorId { get; set; }

        public int? LotPcs { get; set; }

        public int? Id { get; set; }
        public int? TransId { get; set; }
        public int? TakaId { get; set; }
        public bool IsChecked { get; set; }
        public string TakaVNo { get; set; }
        public string RefNo { get; set; }
        public decimal? TakaMtr { get; set; }
        public decimal? FinMeter { get; set; }
        public int? ProdOutId { get; set; }
        public int? RefVoucherId { get; set; }

    }
}
