using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Apparel
{
    [Table("BOMTrans")]
    public class BOMTransModel : AuditedEntity
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int AccId { get; set; }
        public decimal BaseQty { get; set; }
        
        public int UomId { get; set; }
        public decimal RequireQty { get; set; }
        public decimal Stock { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal ShortQty { get; set; }

        public decimal RefQty { get; set; }
        public int BOMId { get; set; }
        public int OrderTransId { get; set; }
        public int TransType { get; set; }
        public string Remark1 { get; set; }
        public string Remark2 { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }

        
    }
}
