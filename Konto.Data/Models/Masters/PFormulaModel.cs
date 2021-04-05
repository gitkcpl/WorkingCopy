using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("PFormula")]
    public class PFormulaModel : AuditedEntity
    {
        public PFormulaModel()
        {
            this.ProductId = 0;
            this.IsActive = true;
           
        }
        //[NotMapped]
        //[Required]
        //public string ProductName { get; set; }
        public int ProductId { get; set; }

        public int DescType { get; set; }

        public decimal Qty { get; set; }

        public decimal Cut { get; set; }

        public int ColorId { get; set; }

        public decimal Rate { get; set; }

        public decimal Total { get; set; }

        [MaxLength(500)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [Required]
        public int? RefProductId { get; set; }

        [Required]
        public int? UomId { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }

        [ForeignKey("RefProductId")]
        public ProductModel RefProduct { get; set; }
    }
}
