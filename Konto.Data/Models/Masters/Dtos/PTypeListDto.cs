using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class PTypeListDto : BaseDto
    {
       
        [Display(Name = "Type Code")]
        public virtual string TypeCode { get; set; }

       
        [Display(Name = "Type Name")]
        public virtual string TypeName { get; set; }

       
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }
    }
}
