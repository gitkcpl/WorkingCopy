using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class TakaCuttingListDto
    {
        public string Voucher { get; set; }
        public int? VDate { get; set; }
        public DateTime? VoucherDate
        {
            get
            {
                if (VDate != null)
                {
                    return (DateTime.ParseExact(VDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture));
                }
                else return null;
            }
        }

        public string ProductName{ get; set; }
        public string TakaNo { get; set; }
        public decimal? Qty { get; set; }
        public string PoNo{ get; set; }
        public string ColorName{ get; set; }

        public int? Id { get; set; }

    }
}
