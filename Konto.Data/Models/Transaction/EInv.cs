using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("einvs")]
    public class EInv : AuditedEntity
    {
        [Index]
        public int RefId { get; set; }
        [Index]
        public int RefVoucherId { get; set; }

        public string AckNo { get; set; }
        public string AckDt { get; set; }
        public string Irn { get; set; }
        public string SignedInvoice { get; set; }
        public string SignedQrCode { get; set; }
        public string Status { get; set; }
        public string EwbNo { get; set; }
        public string EwbDt { get; set; }
        public string EwbValidTill { get; set; }
        public byte[] QrCodeImage { get; set; }
        public string JwtIssuer { get; set; }

        [Index]
        public Guid RefRowId { get; set; }

        public string TransType { get; set; }

    }
}
