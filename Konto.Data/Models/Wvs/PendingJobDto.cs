using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Wvs
{
    public class PendingJobDto
    {
        public PendingJobDto()
        {
            IsClear = false;
        }

        public int Id { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? VouchDate { get; set; }
        public string ProductName { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }

        public bool IsClear { get; set; }
    }
}
