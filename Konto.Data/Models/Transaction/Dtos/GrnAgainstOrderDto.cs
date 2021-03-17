using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class GrnAgainstOrderDto
    {
        public int Id { get; set; }
        public int VTypeId { get; set; }
        public string   ChallanNo { get; set; }
        public string VoucherNo { get; set; }
        public string BillNo { get; set; }
        public int VoucherDate { get; set; }
        public string ProductName { get; set; }
        public int Pcs { get; set; }
        public Decimal Qty { get; set; }
        public decimal Rate { get; set; }

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
    }
}
