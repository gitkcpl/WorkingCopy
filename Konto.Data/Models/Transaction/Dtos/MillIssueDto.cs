﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class MillIssueDto : BaseDto
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
        public string DName { get; set; }
        public int? DelvAccId { get; set; }
        public int? DelvAdrId { get; set; }

        public string JobCardNo { get; set; }
        public string Extra1 { get; set; } //panna
        public string Extra2 { get; set; } //folding Size
        public string Extra4 { get; set; } //pvt marka
        public string Extra3 { get; set; } //shortage
        public int? MasterId { get; set; }
    }

   
}
