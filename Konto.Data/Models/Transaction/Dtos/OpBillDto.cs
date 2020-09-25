using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class OpBillDto 
    {
        public int Id { get; set; }
        public int CompId { get; set; }
        public int YearId { get; set; }
        public int BranchId { get; set; }
        public int VoucherId { get; set; }
        public int VoucherDate { get; set; }
        public string VoucherNo { get; set; }
        public string BillNo { get; set; }
        public int AccId { get; set; }
        public int? AgentId { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TdsAmt { get; set; }
        public string BillType { get; set; } //against
        public string DocNo { get; set; } //challanno
        public DateTime? DocDate { get; set; } //challandate
        public decimal ExchRate { get; set; } // rate
        public decimal TotalAmount { get; set; } //bill amount
        public decimal GrossAmount { get; set; } //part payment
        public decimal TotalPcs { get; set; } // retun goods
        public int? BookAcId { get; set; }

    }
}
