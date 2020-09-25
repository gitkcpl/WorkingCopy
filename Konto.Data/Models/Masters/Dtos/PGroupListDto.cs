using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class PGroupListDto : BaseDto
    {
        [Display(Name = "Group Code")]
        public string GroupCode { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

      
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}
