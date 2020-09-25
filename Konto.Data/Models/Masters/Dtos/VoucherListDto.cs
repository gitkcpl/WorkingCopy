using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class VoucherListDto: BaseDto
    {
        public virtual string VoucherName { get; set; }
        public virtual string SortName { get; set; }

        [Display(Name = "Type")]
        public virtual string TypeName { get; set; }
    }
}
