using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class VoucherNoDto
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public string VoucherNo { get; set; }
    }
}
