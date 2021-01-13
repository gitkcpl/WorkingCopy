using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("emp_rates")]
    public class EmpRate : AuditedEntity
    {
        public virtual int EmpId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual decimal Rate { get; set; }

        public virtual string Remarks { get; set; }

        [ForeignKey("EmpId")]
        public virtual EmpModel Emp { get; set; }


        [ForeignKey("ProductId")]
        public virtual  ProductModel Product { get; set; }

    }

}
