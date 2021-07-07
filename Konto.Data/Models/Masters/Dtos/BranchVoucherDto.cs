using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class BranchVoucherDto
    {
        public  int Id { get; set; }
        public  int BranchId { get; set; }
        public  int SaleVoucherId { get; set; }
        public int SaleReturnVoucherId { get; set; }
        public  int PurchaseVoucherId { get; set; }
        public  int StockTransferVoucherId { get; set; }
        public  int ReceiptVoucherId { get; set; }
        public  int PaymentVoucherId { get; set; }
        public virtual int CrDrNoteVoucherId { get; set; }
    }
}
