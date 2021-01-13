using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Display(Name = "Product Id")]
        
        public int ProductId { get; set; }

        [NotMapped]
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Desc Type")]
        public int DescType { get; set; }

        [NotMapped]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Qty")]
        public decimal Qty { get; set; }

        [Display(Name = "Cut")]
        public decimal Cut { get; set; }

        [Display(Name = "Color Id")]
        public int ColorId { get; set; }

        [NotMapped]
        [Display(Name = "ColorName")]
        public string ColorName { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Total")]
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
