using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Keysoft.Erp.Data.Models
{
    [Table("Ledger")]
    public class Ledger
    {
        public Ledger()
        {
            OpBill = "N";
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Ledger ID is required")]
        public long LedgerID { get; set; } // bigint, not null
        public long ReferenceID { get; set; } // bigint, null
        public long? VoucherID { get; set; } // bigint, null
        [MaxLength(50)]
        public string VoucherNo { get; set; } // varchar(50), null
        public long? AccountID { get; set; } // bigint, null
        public long? ReferenceAccountID { get; set; } // bigint, null
        public decimal? DebitAmt { get; set; } // numeric(18,2), null
        public decimal? CreditAmt { get; set; } // numeric(18,2), null
        [MaxLength(50)]
        public string TableName { get; set; } // varchar(50), null
        [MaxLength(50)]
        public string TableKeyFieldName { get; set; } // varchar(50), null
        public decimal? BillAmt { get; set; } // numeric(18,2), null
        public int? VoucherDate { get; set; } // int, null
        public int? AgentID { get; set; } // int, null
        public long? BookID { get; set; } // bigint, null
      
        public string Narration { get; set; } // varchar(1000), null
        [MaxLength(50)]
        public string BillNo { get; set; } // varchar(50), null
        public decimal? TotalQTY { get; set; } // numeric(10,3), null
        public long? TotalPcs { get; set; } // bigint, null
        public int? MasterID { get; set; } // int, null
        public int? CompanyID { get; set; } // int, null
        public int? UnitID { get; set; } // int, null
        public bool? LedgerSelected { get; set; } // bit, null
       
        public string Remarks { get; set; } // varchar(1000), null
        public long? TransID { get; set; } // bigint, null
        [MaxLength(1)]
        public string flg { get; set; } // varchar(1), null
        public int? ReconDate { get; set; } // int, null
        public short? TransType { get; set; } // smallint, null
        public DateTime? TransDate { get; set; } // datetime, null
        public int? DivisionID { get; set; } // int, null
        [Required(ErrorMessage = "Entry Type is required")]
        public short EntryType { get; set; } // smallint, not null
        [MaxLength(51)]
        public string ChqNo { get; set; } // varchar(51), null
        [Required(ErrorMessage = "Ref Bank Id is required")]
        public long RefBankId { get; set; } // bigint, not null
        public decimal? AdjustAmount { get; set; } // numeric(18,2), null
        [Required(ErrorMessage = "Ret Amount is required")]
        public decimal RetAmount { get; set; } // numeric(18,2), not null
        public decimal? TdsAmount { get; set; } // numeric(18,2), null
        public decimal? AddLess { get; set; } // numeric(18,2), null
        public Guid? RefCode { get; set; } // uniqueidentifier, null
        public Guid? RefTransCode { get; set; } // uniqueidentifier, null
        public string OpBill { get; set; }
        public bool Pending { get; set; }
        public decimal GrossAmt { get; set; }
        public decimal Amount { get; set; }
        public int YearId { get; set; }

    }

}
