using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PaymentHistoryDto
    {
        public string VoucherNo { get; set; }
        public int ChlnDate { get; set; }
        public DateTime VoucherDate
        {
            get
            {

                {
                    return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture));
                }

            }
        }
        public decimal Amount { get; set; }
        public string ChqNo { get; set; }
        public string Remark { get; set; }
        public string Type { get; set; }
    }
}
