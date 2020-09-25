using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class TempModelDto
    {
        public int? Id { get; set; }  
        public  string TemplateName { get; set; } 
        public  int? StartRowNo { get; set; } 
        public string Voucher { get; set; }
        public string VoucherType { get; set; }
        public string Party { get; set; }
    }
}
