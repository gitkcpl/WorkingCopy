using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class RpSetLisDto : BaseDto
    {
        public string RecPay { get; set; }
        public string Field { get; set; }
        public string PlusMinus { get; set; }
        public string PerCap { get; set; }
        public string AmtCap { get; set; }
        public string Ledger { get; set; }
        public string Drcr { get; set; }
        public string HsnCode { get; set; }
    }
}
