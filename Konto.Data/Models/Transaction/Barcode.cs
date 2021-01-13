using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("barcode")]
    public class BarcodeModel : AuditedEntity
    {
        public virtual int ProductId {get;set;}


        [MaxLength(31)]
        [Index]
        public virtual string BarcodeNo
        {
            get; set;
        }

        public virtual  int CompId { get; set; }

        [Index]
        public virtual int ReportId { get; set; }

        [MaxLength(30)]
        public virtual string RackNo { get; set; }
        public virtual int? EmpId { get; set; }

        public virtual int ComboPcs { get; set; }
       
        [Index]
        public virtual int RefBarcodeId { get; set; }

        [Index]
        public virtual int OrderTransId { get; set; }
        public virtual int PcsNo { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }

        [ForeignKey("EmpId")]
        public virtual EmpModel Emp { get; set; }

    }
}
