using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
   
    public class IRN
    {
        [Key]
        public long Id { get; set; }
        public long SalesID { get; set; }
        public string AckNo { get; set; }
        public string AckDt { get; set; }
        public string Irn { get; set; }
        public string SignedInvoice { get; set; }
        public string SignedQRCode { get; set; }
        public string Status { get; set; }
        public string EwbNo { get; set; }
        public string EwbDt { get; set; }
        public string EwbValidTill { get; set; }
        public byte[] QrCodeImage { get; set; }
        public string JwtIssuer { get; set; }
        public long VoucherID { get; set; }

    }
}
