using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Pos.Dtos
{
    public class CustomerListDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        
        public string Area { get; set; }

        public string City { get; set; }
        public string MobileNo { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? AnniDate { get; set; }
        
        public string GstNo { get; set; }
       
        public string MemberNo { get; set; }

        public DateTime? MemberDate { get; set; }
    }
}
