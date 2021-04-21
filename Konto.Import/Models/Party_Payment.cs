using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Keysoft.Erp.Data.Models
{
    public class Part_Payment
    {
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartPaymentID { get; set; }

        public int? PaymentDate { get; set; }

        [MaxLength(1)]
        public string TransType { get; set; }

        public long? AccountID { get; set; }

        public long? PayModeID { get; set; }

        [MaxLength(15)]
        public string ModeNo { get; set; }

        [MaxLength(100)]
        public string Remarks { get; set; }

        public decimal? Amount { get; set; }

        [MaxLength(10)]
        public string VoucherNo { get; set; }

        public long? VoucherID { get; set; }

        public long? CompanyID { get; set; }

        public int? VoucherDate { get; set; }

        public Guid TransCode { get; set; }

       
    }

}
