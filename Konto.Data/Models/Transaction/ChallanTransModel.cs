using Konto.Data.Models.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Konto.Data.Models.Transaction
{
    [Table("ChallanTrans")]
    public class ChallanTransModel : AuditedEntity
    {
        public ChallanTransModel()
        {
            IsActive = true;
        }
        public int? ChallanId { get; set; }

        public int? ProductId { get; set; }

        public int? NProductId { get; set; }

        public int? ColorId { get; set; }

        public int? BatchId { get; set; }

        [MaxLength(50)]
        public string LotNo { get; set; }

        [MaxLength(50)]
        public string RefNo { get; set; }

        public int? GradeId { get; set; }

        public int? DesignId { get; set; }

        public decimal Cops { get; set; }

        public int Pcs { get; set; }

        [Required]
        [Display(Name = "Qty")]
        [Range(0.0000, 999999999)]
        public decimal Qty { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Rate { get; set; }
        public int? UomId { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Gross { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Weight { get; set; }

        [Range(0.0000, 99)]
        public decimal DiscPer { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Disc { get; set; }

        [Range(0.0000, 99)]
        public decimal FreightRate { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Freight { get; set; }

        [Range(0.0000, 999999999)]
        public decimal OtherAdd { get; set; }

        [Range(0.0000, 999999999)]
        public decimal OtherLess { get; set; }

        [Range(0.0000, 99)]
        public decimal CgstPer { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Cgst { get; set; }

        [Range(0.0000, 99)]
        public decimal SgstPer { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Sgst { get; set; }

        [Range(0.0000, 99)]
        public decimal IgstPer { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Igst { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Total { get; set; }

        [MaxLength]
        public string Remark { get; set; }

        public int? RefId { get; set; }

        public int? RefVoucherId { get; set; }

        public int? MiscId { get; set; }

        public decimal IssueQty { get; set; }

        public decimal IssuePcs { get; set; }

        [Range(0.0000, 99)]
        public decimal CessPer { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Cess { get; set; }

        public int PlainPcs { get; set; }

        public decimal PlainQty { get; set; }

        public  DateTime? ReceiveDateTime { get; set; }
        public  int ReceivedById { get; set; }
        public  bool IsReceived { get; set; }
        public int BranchId { get; set; }

        public int? ScreenId { get; set; }

        //[NotMapped]
        //public string ProductName { get; set; }

        //[NotMapped]
        //public string NewProduct { get; set; }

        //[NotMapped]
        //public string ColorName { get; set; }

        //[NotMapped]
        //public string DesignNo { get; set; }


        //[NotMapped]
        //public int? ODate { get; set; }

        //[NotMapped]
        //public decimal? PendingQty { get; set; }

        //[NotMapped]
        //public decimal? PendingPcs { get; set; }

        //[NotMapped]
        //public decimal? ReceiptQty { get; set; }

        //[NotMapped]
        //public decimal? ReceiptPcs { get; set; }

        //[NotMapped]
        //public string GradeName { get; set; }

        //[NotMapped]
        //public int? vouchrId { get; set; }

        //[NotMapped]
        //public decimal? ShQty { get; set; }

        //[NotMapped]
        //public decimal? ShPer { get; set; }

        //[NotMapped]
        //public string WvrChallan { get; set; }

        //[NotMapped]
        //public string MillName { get; set; }

        //[NotMapped]
        //public string Weaver { get; set; }

        //[NotMapped]
        //public string CuttingDetails { get; set; }

        //[NotMapped]
        //public string TakaVNo { get; set; }

        //[NotMapped]
        //public string ChallanNo { get; set; }

        [ForeignKey("ChallanId")]
        public virtual ChallanModel Challan { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }

    }
}
