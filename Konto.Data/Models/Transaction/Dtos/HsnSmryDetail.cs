using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class HsnSmryDetail
    {
         public string Party { get; set; }
        public string GstIn { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string BillNo { get; set; }
     
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string HsnCode { get; set; }

        public decimal? Rate { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? GrossAmount { get; set; }
              
        public decimal? CgstPer { get; set; }
        public decimal? CgstAmt { get; set; }
        public decimal? SgstPer { get; set; }
        public decimal? SgstAmt { get; set; }        
          public decimal? IgstPer { get; set; }
        public decimal? IgstAmt { get; set; }

        public decimal? GSTRate { get; set; }
        public decimal? TdsAmt { get; set; }
        public decimal? TotalAmount { get; set; }

        public decimal? TaxableValue { get; set; }
        
        public string VoucherName { get; set; }
        public int BillId { get; set; }
    }
}
