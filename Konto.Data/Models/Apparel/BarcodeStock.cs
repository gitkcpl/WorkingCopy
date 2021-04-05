using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Apparel
{
    [Table("barcode_stock")]
    public class BarcodeStock : AuditedEntity
    {
        public virtual int CompId { get; set; }
        public virtual int YearId { get; set; }
        public virtual int VoucherDate { get; set; }
        public virtual int? DivId { get; set; }
        public virtual int? EmpId { get; set; }

        public virtual int? ProductId { get; set; }
        public virtual decimal Qty { get; set; }
        
        [MaxLength(50)][Index]
        public virtual string BarcodeNo { get; set; }

        [Index]
        public virtual int BarcodeId { get; set; }
        public virtual int RefId { get; set; }
        [ForeignKey("DivId")]
        public virtual DivisionModel Div { get; set; }

        [ForeignKey("EmpId")]
        public virtual EmpModel Emp { get; set; }
    }
}
