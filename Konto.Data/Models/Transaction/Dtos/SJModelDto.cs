using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class SJModelDto
    {
        public int Id { get; set; }
        public string VoucherNo { get; set; }
        public int VoucherDate { get; set; }
        public DateTime? VDate
        {
            get
            {
                if (VoucherDate != null)
                { return (DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
            set { }

        }

        public int VoucherId { get; set; }
        public int ProcessId { get; set; }

        public string ChallanNo { get; set; }
        public string ProcessName { get; set; }
        public string Remark { get; set; }

        public DateTime? RcdDate { get; set; }
    }
}
