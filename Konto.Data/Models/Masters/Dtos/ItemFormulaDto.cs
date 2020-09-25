using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class ItemFormulaDto
    {
        public int ProductId { get; set; }

        [Display(Name = "Desc Type")]
        public int? DescType { get; set; }

        
        [Display(Name = "Category")]
        public string category { get; set; }

        [Display(Name = "Qty")]
        public decimal Qty { get; set; }

        [Display(Name = "cut")]
        public decimal cut { get; set; }

        [Display(Name = "Color Id")]
        public int? ColorId { get; set; }


        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [MaxLength(500)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

    }
}
