using System;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class GrnDto : BaseDto
    {
       
        public int CompId { get; set; }
        public int YearId { get; set; }
        public int BranchId { get; set; }
        public int VoucherId { get; set; }
        public int StoreId { get; set; }
        public int VoucherDate { get; set; }
        public string VoucherNo { get; set; }
        public string ChallanNo { get; set; }
        public string BillNo { get; set; }
        public DateTime? RcdDate { get; set; }
        public int AccId { get; set; }

        public int? AgentId { get; set; }

        public int DivId { get; set; }
        public string DocNo { get; set; }

        public DateTime? DocDate { get; set; }

        public int? TransId { get; set; }
        public string Remark { get; set; }

        public int ChallanType { get; set; }

        public decimal TotalQty { get; set; }
        public int TypeId { get; set; }
        public decimal TotalPcs { get; set; }

        public decimal TotalAmount { get; set; }
        public string VehicleNo { get; set; }
        public int? EmpId { get; set; }

        public int? DelvAccId { get; set; }
        public int? DelvAdrId { get; set; }

        public int? ProcessId { get; set; }

        public int RefId { get; set; }

        public int RefVoucherId { get; set; }

        public string Extra1 { get; set; }

    }
}
