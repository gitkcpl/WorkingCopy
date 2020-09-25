using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class GradeListDto : BaseDto
    {
        public virtual string GradeName { get; set; }

        public virtual string Remark { get; set; }
    }
}
