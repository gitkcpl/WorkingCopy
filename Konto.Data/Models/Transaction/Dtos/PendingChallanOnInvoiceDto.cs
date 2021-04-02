using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingChallanOnInvoiceDto
    {
        public string ChallanNo { get; set; } // varchar(25)

        public DateTime ChallanDate { get; set; }

        public int? ChlnDate { get; set; } // int

        public string BillNo { get; set; } // varchar(25)

        public DateTime? RcdDate { get; set; } // datetime2(7)

        public int? TransportId { get; set; } // int

        public decimal? TotalQty { get; set; } // decimal(18,2)

        public decimal? InwPcs { get; set; } // int

        public decimal? InwQty { get; set; } // int

        public decimal? TotalPcs { get; set; } // int

        public decimal? NetTotal { get; set; } // decimal(18,2)

        public int Id { get; set; } // int

        public int? VoucherId { get; set; } // int

        public string DocNo { get; set; } // varchar(25)

        public DateTime? DocDate { get; set; } // datetime2(7)

        public string VehicleNo { get; set; } // varchar(25)

        public string Remark { get; set; } // varchar(max)

        public int AccId { get; set; }
    }
}
