using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class docDetailList
    {
        public string VoucherNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string BillNo { get; set; }
        public string Party { get; set; }
        public string SpecialNotes { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? GSTRate { get; set; }
        public decimal? Igst { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? Sgst { get; set; }

        public decimal? TdsAmt { get; set; }
        public decimal? TotalAmount { get; set; }
       
        public decimal? TaxableValue { get; set; }
      
        public string GstIn { get; set; }
        public int BillId { get; set; }
    }
}
