using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingGateDto
    {

        public int Id { get; set; }
        public int VoucherId { get; set; }
        public string SrNo { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
