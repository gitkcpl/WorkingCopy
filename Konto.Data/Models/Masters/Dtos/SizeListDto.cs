using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class SizeListDto : BaseDto
    {
        [Display(Name = "Size Code")]
        public string SizeCode { get; set; }

       
        [Display(Name = "Size Name")]
        public string SizeName { get; set; }

       
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}
