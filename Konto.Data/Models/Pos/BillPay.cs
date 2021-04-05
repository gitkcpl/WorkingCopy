using Konto.Data.Models.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Pos
{
    [Table("bill_pays")]

    public class BillPay : AuditedEntity
    {
        public int PayDate { get; set; }
        public int BillId { get; set; }
        public decimal DiscAmt { get; set; }
        public int? Pay1Id { get; set; }
        public int? Pay2Id { get; set; }
        public int? Pay3Id { get; set; }

        public decimal Pay1Amt { get; set; }
        public decimal Pay2Amt { get; set; }
        public decimal Pay3Amt { get; set; }

        public decimal ChangeAmt { get; set; }
        [MaxLength(50)]
        public string RefNo1 { get; set; }
        [MaxLength(50)]
        public string RefNo2 { get; set; }

        [ForeignKey("BillId")]
        public BillModel BillModel { get; set; }


    }
}
