using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class OpBillListDto : BaseDto
    {
        public virtual string BillType { get; set; }
        public virtual string VoucherNo { get; set; }
        public virtual DateTime BillDate
        {
            get
            {
                DateTime dt;
                if (DateTime.TryParseExact(VoucherDate.ToString(), "yyyyMMdd",
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out dt))
                    return dt;
                return DateTime.Now;
            }
        }
        public virtual int VoucherDate { get; set; }
        public virtual string BillNo { get; set; }
        public virtual string PartyName { get; set; }
        public string AgentName { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal Rate { get; set; }
        public decimal BillAmt { get; set; }

        public virtual decimal PendingAmt { get; set; }
        public virtual decimal PartPayment { get; set; }
        public virtual decimal Tds { get; set; } 
        public virtual decimal ReturnGoods { get; set; } 
        public virtual string Book { get; set; }

    }
}
