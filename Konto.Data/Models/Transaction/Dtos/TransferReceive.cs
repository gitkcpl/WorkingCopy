using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class TransferReceive
    {
        public DateTime? ReceiveDateTime { get; set; }
        public  string VoucherNo { get; set; }
        public  string BarCode { get; set; }
        public string ProductName { get; set; }
        public  string ColorName { get; set; }
        public  decimal Qty { get; set; }
        public  int Pcs { get; set; }
        public  string ReceivedBy { get; set; }
    }
}
