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
            this.ProductId = 1;
            this.IsActive = true;
           
        }

        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Desc Type")]
        public int? DescType { get; set; }

        [NotMapped]
        [Display(Name = "Desc Type")]
        public string category { get; set; }

        [Display(Name = "Qty")]
        public decimal? Qty { get; set; }

        [Display(Name = "cut")]
        public decimal? cut { get; set; }

        [Display(Name = "Color Id")]
        public int? ColorId { get; set; }

        [NotMapped]
        [Display(Name = "ColorName")]
        public string ColorName { get; set; }

        [Display(Name = "Rate")]
        public decimal? Rate { get; set; }

        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        [MaxLength(500)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }
    }
}
