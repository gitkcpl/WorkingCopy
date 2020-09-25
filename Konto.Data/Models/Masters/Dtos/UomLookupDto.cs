using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class UomLookupDto : BaseLookupDto
    {
        public virtual string  RateOn { get; set; }
    }
}
