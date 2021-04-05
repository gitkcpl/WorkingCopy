using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("item_batches")]
    public class ItemBatch : AuditedEntity
    {
        public int ProductId { get; set; }

        [MaxLength(50)]
       
        public string BatchNo { get; set; }

        [MaxLength(50)]
        [Index]
        public string SerialNo { get; set; }

        [MaxLength(10)]
        public string MfgDate { get; set; }

        [MaxLength(10)]
        public string ExpDate { get; set; }

        public decimal DealerPrice { get; set; }
        public decimal SaleRate { get; set; }

        public decimal Mrp { get; set; }

        public decimal Qty { get; set; }

        public decimal AvalQty { get; set; }
        public decimal BulkRate { get; set; }

        public decimal SemiBulkRate { get; set; }

        [Index]
        public int RefId { get; set; }

        [Index]
        public int RefTransId { get; set; }

        [Index]
        public int RefVoucherId { get; set; }

        [MaxLength(50)]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        public string Extra2 { get; set; }

        public int? BranchId { get; set; }

        public decimal IssueQty { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
    }
}
