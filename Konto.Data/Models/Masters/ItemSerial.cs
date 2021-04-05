using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("item_serials")]
    public class ItemSerial : AuditedEntity
    {
        public int ProductId { get; set; }
        
        [MaxLength(50)]
        public string SerialNo { get; set; }

        public decimal Qty { get; set; }

        [MaxLength(50)]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        public string Extra2 { get; set; }
        public int? BranchId { get; set; }
        
        [Index]
        public int RefId { get; set; }

        [Index]
        public int RefTransId { get; set; }

        [Index]
        public int RefVoucherId { get; set; }

        public int BatchId { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
    }
}
