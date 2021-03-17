using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class RcpuiDto
    {
        public int? Id { get; set; }
        public string VoucherNo { get; set; }
        public int? VoucherDate { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public decimal? Qty { get; set; }
        public int? VoucherId { get; set; }
        public string Remark { get; set; }
        public int? CompId { get; set; }
        public int? YearId { get; set; }
        public int? BranchId { get; set; }
        public string Description { get; set; }
    }
}