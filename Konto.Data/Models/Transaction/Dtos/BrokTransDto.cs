using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class BrokTransDto
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string Party { get; set; }
        public decimal Disc { get; set; }
        public decimal TdsAmt { get; set; }
        public decimal NetTotal { get; set; }
        public decimal CessPer { get; set; }
        public decimal Cess { get; set; }
    }
}
