using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class PackingTypeListDto : BaseDto
    {
        public virtual string PackingTypeName { get; set; } 
        public virtual string Remark { get; set; }
    }
}
