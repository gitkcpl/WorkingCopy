using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class BranchListDto : BaseDto
    {
        
        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }

        
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        


    }
}
