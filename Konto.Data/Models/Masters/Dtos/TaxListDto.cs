using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class TaxListDto : BaseDto
    {
        [Display(Name = "Tax Name")]
        public string TaxName { get; set; }

      
        [Display(Name = "Tax Type")]
        public string TaxType { get; set; }

        [Display(Name = "Sgst")]
        public decimal Sgst { get; set; }

        [Display(Name = "Cgst")]
        public decimal Cgst { get; set; }

        [Display(Name = "Igst")]
        public decimal Igst { get; set; }

      
        [Display(Name = "Cess Type")]
        public string CessType { get; set; }

        [Display(Name = "Cess")]
        public decimal Cess { get; set; }

        [Display(Name = "Cess Rate")]
        public decimal CessRate { get; set; }

    }
}
