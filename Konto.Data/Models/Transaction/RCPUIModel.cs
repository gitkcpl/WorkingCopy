using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("RCPUI")]
    public class RCPUIModel : AuditedEntity
    {
        public RCPUIModel()
        {
            IsActive = true;
            IsDeleted = false; 
            Qty = 100;
        }
          
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
