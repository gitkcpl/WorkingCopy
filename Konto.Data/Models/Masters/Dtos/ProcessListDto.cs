using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class ProcessListDto : BaseDto
    {
        public string ProcessName { get; set; }
        public string HsnCode { get; set; }
        public int? Priority { get; set; }

        public string GstSlab { get; set; }
    }
}
