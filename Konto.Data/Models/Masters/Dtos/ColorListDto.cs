using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class ColorListDto : BaseDto
    {
        [Display(Name = "Color Code")]
        public string ColorCode { get; set; }

        
        [Display(Name = "Color Name")]
        public string ColorName { get; set; }

        [Display(Name = "RGB")]
        public string RGB { get; set; }
    }
}
