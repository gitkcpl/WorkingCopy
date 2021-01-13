using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class PositionListDto : BaseDto
    {
        [Display(Name = "Position Name")]
        public virtual string PositionName { get; set; }

        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }
    }
}
