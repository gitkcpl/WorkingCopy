using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keysoft.Erp.Data.Models
{
    public class Po
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PoID { get; set; }

        public long CompanyID { get; set; }

        public long? VoucherId { get; set; }

        public int? PoDate { get; set; }

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

        public bool PoCancel { get; set; }

        public decimal? RoundOff { get; set; }

        public decimal? BillAmount { get; set; }

        public int? DivisionID { get; set; }

        public int? UnitID { get; set; }

        public int? DueDays { get; set; }

        public int? DueDate { get; set; }

        public bool? OrderClose { get; set; }

        public long? PurchaseNature { get; set; }

        [MaxLength(10)]
        public string Currancy { get; set; }

        public decimal? Exchangrate { get; set; }

        [MaxLength(25)]
        public string PartyOrderno { get; set; }

        [MaxLength(500)]
        public string DeliveryTerms { get; set; }

        [MaxLength(500)]
        public string PayTrans { get; set; }

        [MaxLength(500)]
        public string SpecialNotes { get; set; }

        [MaxLength(500)]
        public string DispatchInstruction { get; set; }

        public DateTime? TransDate { get; set; }

        public int? EditUserid { get; set; }

        public int? AddUserid { get; set; }

        public long? BookID { get; set; }

        public Guid PoCode { get; set; }

        public List<PoTrans> PoTrans { get; set; }

    }
}
