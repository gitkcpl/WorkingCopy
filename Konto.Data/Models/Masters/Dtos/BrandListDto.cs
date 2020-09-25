using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class BrandListDto : BaseDto
    {
      
        [Display(Name = "Brand Code")]
        public string BrandCode { get; set; }

       
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}
