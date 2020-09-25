using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class AcGroupListDto: BaseDto
    {
        [Display(Name = "Group Name")]
        public virtual string GroupName { get; set; }

        [Display(Name = "Nature")]
        public virtual string Nature { get; set; }

        [Display(Name = "Opp Side Name")]
        public virtual string OppSideName { get; set; }

        [Display(Name = "Rec/Pay")]
        public virtual string Extra1 { get; set; }
    }
}
