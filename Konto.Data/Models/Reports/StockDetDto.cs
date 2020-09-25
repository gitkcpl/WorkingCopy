using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class StockDetDto
    {

        public string ReportType { get; set; }
        public int ItemId { get; set; }
        public string Product { get; set; }
        public string Particular { get; set; }
        public string VoucherNo { get; set; }
        public string BillNo { get; set; }
        public string VoucherName { get; set; }
        public DateTime? TransDate { get; set; }

        public decimal InwQty { get; set; }
        public decimal OutQty { get; set; }
        public int? InwPcs { get; set; }
        public int? OutPcs { get; set; }
        public decimal StockQty { get; set; }
        public int? StockPcs { get; set; }
        public decimal Qty { get; set; }
        public int? Pcs { get; set; }
        public Guid RefId { get; set; }
        public Guid MasterRefId { get; set; }
        public  int VTypeId { get; set; }
    }
}
