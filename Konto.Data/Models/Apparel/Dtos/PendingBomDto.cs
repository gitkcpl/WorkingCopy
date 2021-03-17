using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Apparel.Dtos
{
    public class PendingBomDto
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public int VoucherNo { get; set; }

        public decimal TargetQty { get; set; }
        public string ProductName { get; set; }
        public string OrderType { get; set; }
        public string Remark { get; set; }
        public int VoucherDate { get; set; }

        public DateTime? BomDate
        {
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
