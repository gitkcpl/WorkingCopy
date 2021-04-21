using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
     public class So
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SoID { get; set; }

        public long CompanyID { get; set; }

        public long? VoucherId { get; set; }

        public int? SoDate { get; set; }

        [MaxLength(25)]
        public string VoucherNo { get; set; }

        public long? AccountID { get; set; }

        public long? AgentID { get; set; }

        public long? TransID { get; set; }

        public long? TotalPcs { get; set; }

        public decimal? TotalQty { get; set; }

        public decimal? TotalAmt { get; set; }

        [MaxLength(100)]
        public string Remark { get; set; }

        public bool SoCancel { get; set; }

        public bool OrderClose { get; set; }

        public decimal? RoundOff { get; set; }

        public decimal? BillAmount { get; set; }

        public int? DivisionID { get; set; }

        public int? UnitID { get; set; }

        public int? DueDays { get; set; }

        public int? DueDate { get; set; }

        [MaxLength(25)]
        public string PartyOrderNo { get; set; }

        public long? CustomerID { get; set; }

        public long? SalesTypeID { get; set; }

        public long? SalesAcID { get; set; }

        public decimal? TotalWithourTax { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? TotalGross { get; set; }

        public decimal? AddLessBeforeTax { get; set; }

        public decimal? VatPer { get; set; }

        public decimal? VatAmount { get; set; }

        public decimal? AdVatPer { get; set; }

        public decimal? AdVatAmount { get; set; }

        public decimal? CSTPer { get; set; }

        public decimal? CSTAmount { get; set; }

        public decimal? AddLedger { get; set; }

        public decimal? LessLedger { get; set; }

        public int? EmpID { get; set; }

        public decimal? RecvAmt { get; set; }

        public short? SalesType { get; set; }

        public byte? Counter { get; set; }

        [MaxLength(50)]
        public string PaymentTerms { get; set; }

        public long? SalesNature { get; set; }

        [MaxLength(10)]
        public string Currancy { get; set; }

        public DateTime? TransDate { get; set; }

        public int? EditUserid { get; set; }

        public decimal? Exchangrate { get; set; }

        [MaxLength(500)]
        public string DeliveryTerms { get; set; }

        [MaxLength(500)]
        public string SpecialNotes { get; set; }

        [MaxLength(500)]
        public string DispatchInstruction { get; set; }

        public int? AddUserid { get; set; }

        public long? PartyID { get; set; }

        public long? itemcat_id { get; set; }

        public long? BookID { get; set; }

        public List<SoDelv> SoDelvs { get; set; }

        public List<SoTrans> SoTrans { get; set; }

    }
}
