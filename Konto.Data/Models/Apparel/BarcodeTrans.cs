using Konto.Data.Models.Masters;
using Konto.Data.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Apparel
{
    [Table("barcode_trans")]
    public class BarcodeTrans : AuditedEntity
    {
        public virtual int CompId { get; set; }
        public virtual int YearId { get; set; }
        public virtual int VoucherDate { get; set; }
        public virtual int? DivId { get; set; }

        [MaxLength(200)]
        public virtual string Remarks { get; set; }

        public virtual int? EmpId { get; set; }

        public virtual int? ProductId { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual int TransType { get; set; } //0 inward/ 1 outward/2 qc
        public virtual bool QcPassed { get; set; }

        [Index][MaxLength(31)]
        public virtual string BarcodeNo { get; set; }
        public virtual int? BarcodeId { get; set; }

        [ForeignKey("EmpId")]
        public virtual EmpModel Emp { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }

        [ForeignKey("BarcodeId")]
        public virtual BarcodeModel Barcode { get; set; }

        [ForeignKey("DivId")]
        public virtual DivisionModel Division { get; set; }
    }
}
