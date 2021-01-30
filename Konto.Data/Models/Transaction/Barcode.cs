using Konto.Data.Models.Masters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Transaction
{
    [Table("barcode")]
    public class BarcodeModel : AuditedEntity
    {
        public virtual int ProductId {get;set;}


        [MaxLength(31)]
        [Index(IsUnique =true)]
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

        public virtual int Qty { get; set; }

        public virtual int? AccId { get; set; }
       
        [Index]
        public virtual int RefBarcodeId { get; set; }

        [Index]
        public virtual int OrderTransId { get; set; }
        public virtual int PcsNo { get; set; }

        public virtual int RefId { get; set; } // for storing bom id

        public virtual bool IsLayer { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }

        [ForeignKey("EmpId")]
        public virtual EmpModel Emp { get; set; }


        [ForeignKey("AccId")]
        public virtual AccModel Acc { get; set; }

    }
}
