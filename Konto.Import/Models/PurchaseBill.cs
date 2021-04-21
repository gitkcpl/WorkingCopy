using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class PurchaseBill
    {
        [Key]
        public long PurchaseBillID { get; set; }

        public long? PurchaseID { get; set; }

        public long? SrNo { get; set; }

        public long? BillVoucherID { get; set; }

        public decimal? Amount { get; set; }

        public bool? BillClose { get; set; }

        public long? CompanyID { get; set; }

        public string BillNo { get; set; }

        public decimal? AddLessAmount { get; set; }

        public decimal? PrePaid { get; set; }

        [ForeignKey("PurchaseID")]
        public Purchase Purchase { get; set; }



    }

}
