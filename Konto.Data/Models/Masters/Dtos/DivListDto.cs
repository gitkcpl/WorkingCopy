using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class DivListDto : BaseDto
    {
        [Display(Name = "Division Name")]
        public virtual string DivisionName { get; set; }

        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }
    }
}
