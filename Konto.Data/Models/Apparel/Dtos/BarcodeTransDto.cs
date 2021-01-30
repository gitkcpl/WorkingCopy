using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Apparel.Dtos
{
    public class BarcodeTransDto
    {
        public  int VoucherDate { get; set; }
        public  int? DivId { get; set; }

        public  string Remarks { get; set; }

        public  int? EmpId { get; set; }

        public  int? ProductId { get; set; }
        public  decimal Qty { get; set; }
        public  int TrnasType { get; set; } //0 inward/ 1 outward/2 qc
        public  bool QcPassed { get; set; }

        public  string BarcodeNo { get; set; }
        public  int? BarcodeId { get; set; }
        public string EmpName { get; set; }
        public string ProductName { get; set; }
        public string DivName { get; set; }
        public DateTime EntryDate { get
                

            {
                    DateTime dt;

                    if (DateTime.TryParseExact(VoucherDate.ToString(), "yyyyMMdd",
                          CultureInfo.InvariantCulture,
                          DateTimeStyles.None, out dt))
                        return dt;
                    else
                        return DateTime.Now;
                
            } }
        public int Id { get; set; }
    }
}
