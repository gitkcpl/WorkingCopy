using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Gstn
{
    public class HsnTransDto
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int TaxMasterId {get;set;}
        public DateTime ApplyDate { get; set; }
    }
}
