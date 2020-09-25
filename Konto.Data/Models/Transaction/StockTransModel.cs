using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("StockTrans")]
    public class StockTransModel : AuditedEntity
    {
        public StockTransModel()
        {
            IsActive = true;
        }

        [Display(Name = "Company Id")]
        public short CompanyId { get; set; }

        [Display(Name = "Branch Id")]
        public int BranchId { get; set; }

        [Display(Name = "Year Id")]
        public int YearId { get; set; }

        [Display(Name = "Div Id")]
        public int? DivId { get; set; }

        [Display(Name = "Godown Id")]
        public int GodownId { get; set; }

        [Display(Name = "Ref Id")]
        public Guid RefId { get; set; }

        [Display(Name = "Master Ref Id")]
        public Guid? MasterRefId { get; set; }

        [Display(Name = "Voucher Date")]
        public int VoucherDate { get; set; }

        [Display(Name = "Trans Date")]
        public DateTime? TransDate { get; set; }

        [Display(Name = "Voucher Id")]
        public int VoucherId { get; set; }

        [MaxLength(25)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [MaxLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Display(Name = "Account Id")]
        public int? AccountId { get; set; }

        [Display(Name = "Item Id")]
        public int ItemId { get; set; }

        [Display(Name = "Rcpt Nos")]
        public int RcptNos { get; set; }

        [Display(Name = "Rcpt Qty")]
        public decimal RcptQty { get; set; }

        [Display(Name = "Issue Nos")]
        public int IssueNos { get; set; }

        [Display(Name = "Issue Qty")]
        public decimal IssueQty { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Agent Id")]
        public int AgentId { get; set; }

        [MaxLength(25)]
        [Display(Name = "Table Name")]
        public string TableName { get; set; }

        [Display(Name = "Key Fld Value")]
        public int? KeyFldValue { get; set; }

        [MaxLength]
        [Display(Name = "Narration")]
        public string Narration { get; set; }

        [Display(Name = "Trans Date Time")]
        public DateTime? TransDateTime { get; set; }

        [Display(Name = "Qty")]
        public decimal Qty { get; set; }


        [Display(Name = "Color Id")]
        public int? ColorId { get; set; }

        [Display(Name = "Batch Id")]
        public int? BatchId { get; set; }

        [Display(Name = "Grade Id")]
        public int? GradeId { get; set; }

        [Display(Name = "Design Id")]
        public int? DesignId { get; set; }

        [Display(Name = "Challan Type")]
        public int? ChallanType { get; set; }

        [MaxLength(50)]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }

        [Display(Name = "Cut")]
        public decimal Cut { get; set; }

        [Display(Name = "Pcs")]
        public int Pcs { get; set; }

        [Display(Name = "UomId")]
        public int? UomId { get; set; }
    }
}
