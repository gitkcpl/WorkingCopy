using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class HistoryDto
    {
        public string VoucherNo { get; set; }
        public int VoucherDate { get; set; }
        public DateTime? Date {
            get
            {
                if (VoucherDate != 0)
                {
                    return (DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture));
                }
                else return null;
            }
        }
        public string Party { get; set; }
        public  decimal Qty { get; set; }
        public decimal Rate { get; set; }
        
    }
}
