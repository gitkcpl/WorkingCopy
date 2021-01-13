using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Apparel
{
    [Table("BOM")]
    public class BomModel : AuditedEntity
    {
        public int DivisionId { get; set; }
        public int VoucherId { get; set; }
        public string VoucherNo { get; set; }
        public int VoucherDate { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public decimal TargetQty { get; set; }
        public string Remark { get; set; }

        public int CompId { get; set; }
        public int YearId { get; set; }
        public int BranchId { get; set; }

        [ForeignKey("VoucherId")]
        public VoucherModel Voucher { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
    }
}
