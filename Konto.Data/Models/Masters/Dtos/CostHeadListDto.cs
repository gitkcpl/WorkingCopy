using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class CostHeadListDto : BaseDto
    {
        [Display(Name = "Cost Center")]
        public virtual string HeadName { get; set; }

        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }
    }
}
