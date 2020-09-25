using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class UomListDto : BaseDto
    {
        [Display(Name = "Unit Code")]
        public string UnitCode { get; set; }

        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        public string RateOn { get; set; }

        [Display(Name = "Gst Unit")]
        public string GSTUnit { get; set; }
    }
}
