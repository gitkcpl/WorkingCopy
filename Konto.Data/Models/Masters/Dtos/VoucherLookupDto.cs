using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class VoucherLookupDto : BaseLookupDto
    {
        public string SortName { get; set; }
        public int? RefVoucherId { get; set; }
        public int VTypeId { get; set; }
        public string InvoiceHeading { get; set; }
        public int Width { get; set; }
        public bool PreFillZero { get; set; }
        public int StartFrom { get; set; }
        public int Increment { get; set; }
        public string Mask { get; set; }
        public int? Max { get; set; }
        public int LastSerial { get; set; }
        public bool Reset { get; set; }
        public bool PrintAfterSave { get; set; }
        public bool Sms { get; set; }
        public bool Email { get; set; }
        public bool FixBook { get; set; }
        public int? AccId { get; set; }
        public bool ManualSeries { get; set; }
    }
}
