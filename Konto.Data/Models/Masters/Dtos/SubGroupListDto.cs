using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class SubGroupListDto : BaseDto
    {
        [Display(Name = "Sub Code")]
        public string SubCode { get; set; }

        [Display(Name = "Sub Group Name")]
        public string SubName { get; set; }

        public string GroupName { get; set; }
    }
}
