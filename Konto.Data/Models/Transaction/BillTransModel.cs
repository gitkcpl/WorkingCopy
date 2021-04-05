using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Konto.Data.Models.Transaction
{
    [Table("BillTrans")]
    public class BillTransModel : AuditedEntity
    {
        public BillTransModel()
        {
            ChequeDate = DateTime.Now;
            RpType = "On Account";
            IsActive = true;
            Qty = 1;
        }

        [Display(Name = "Bill Id")]
       
        public int? BillId { get; set; }

        [Display(Name = "Product Id")]
        [Index]
        public int? ProductId { get; set; }

        [Display(Name = "HsnCode")]
        public string HsnCode { get; set; }

        [Display(Name = "Color Id")]
        public int? ColorId { get; set; }

        [Display(Name = "Design Id")]
        public int? DesignId { get; set; }

        [Display(Name = "Grade Id")]
        public int? GradeId { get; set; }

        [Display(Name = "Ref Bank Id")]
        public int? RefBankId { get; set; }

        [NotMapped]
        [Display(Name = "Cheque bank")]
        public string RefBank { get; set; }

        [Display(Name = "To Acc Id")]
        [Index]
        public int? ToAccId { get; set; }

        [NotMapped]
        [Display(Name = "Particular")]
        public string Particular { get; set; }


        [MaxLength(50)]
        [Display(Name = "Cheque No")]
        public string ChequeNo { get; set; }


        [Display(Name = "Cheque Date")]
        public DateTime? ChequeDate { get; set; }

        public int? BatchId { get; set; }

        [MaxLength(50)]
        public string LotNo { get; set; }

       

        [Display(Name = "Avg Wt")]
        [Range(0.0000, 999999999)]
        public decimal? AvgWt { get; set; }

        [Display(Name = "Cut")]
        [Range(0.00, 99)]
        public decimal Cut { get; set; }

        [Display(Name = "Width")]
        [Range(0.0000, 999999999)]
        public decimal Width { get; set; }

        [Display(Name = "Pcs")]
        public int Pcs { get; set; }

        [Display(Name = "Qty")]
      //  [Range(0.0000, 999999999)]
        public decimal Qty { get; set; }
        //numeric(18,4)

        [Display(Name = "Uom Id")]
        public int? UomId { get; set; }

        [Required]
        [Range(0, 999999999)]
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Total")]
     //   [Range(0.0000, 999999999)]
        public decimal Total { get; set; }

        [Display(Name = "Disc")]
        [Range(-100, 100)]
        public decimal Disc { get; set; }

        [Display(Name = "Disc Amt")]
       // [Range(0.0000, 999999999)]
        public decimal DiscAmt { get; set; }

        [Display(Name = "Ocean Freight")]
        [Range(0.0000, 999999999)]
        public decimal OceanFrt { get; set; }

        [Display(Name = "Custom Duty")]
        [Range(0.0000, 999999999)]
        public decimal CustomD { get; set; }

        [Display(Name = "Freight Rate")]
      //  [Range(0.0000, 999999999)]
        public decimal FreightRate { get; set; }

        [Display(Name = "Freight")]
      //  [Range(0.0000, 999999999)]
        public decimal Freight { get; set; }

        [Display(Name = "Other Add")]
        [Range(0.0000, 999999999)]
        public decimal OtherAdd { get; set; }

        [Display(Name = "Other Less")]
        [Range(0.0000, 999999999)]
        public decimal OtherLess { get; set; }

        [Display(Name = "Sgst Per")]
      //  [Range(0.0000, 99)]
        public decimal SgstPer { get; set; }

        [Display(Name = "Sgst")]
      //  [Range(0.0000, 999999999)]
        public decimal Sgst { get; set; }

        [Display(Name = "Cgst Per")]
      ///  [Range(0.0000, 99)]
        public decimal CgstPer { get; set; }

        [Display(Name = "Cgst")]
       // [Range(0.0000, 999999999)]
        public decimal Cgst { get; set; }

        [Display(Name = "Igst Per")]
      //  [Range(0.0000, 99)]
        public decimal IgstPer { get; set; }

        [Display(Name = "Igst")]
       // [Range(0.0000, 999999999)]
        public decimal Igst { get; set; }

        [Display(Name = "Cess Per")]
      //  [Range(0.0000, 99)]
        public decimal CessPer { get; set; }

        [Display(Name = "Cess")]
       // [Range(0.0000, 999999999)]
        public decimal Cess { get; set; }

        [Display(Name = "Tds Per")]
        [Range(0.0000, 99)]
        public decimal TdsPer { get; set; }

        [Display(Name = "Tds Amt")]
        [Range(0.0000, 999999999)]
        public decimal TdsAmt { get; set; }

        [Display(Name = "Tcs Per")]
        [Range(0.0000, 99)]
        public decimal TcsPer { get; set; }

        [Display(Name = "Tcs Amt")]
        [Range(0.0000, 999999999)]
        public decimal TcsAmt { get; set; }

        [Display(Name = "Net Total")]
      //  [Range(0.0000, 999999999)]
        public decimal NetTotal { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(200)]
        [Display(Name = "Comm Descr")]
        public string CommDescr { get; set; }

        [Display(Name = "Dept Id")]
        public int? DeptId { get; set; }

        [Display(Name = "Division Id")]
        public int? DivisionId { get; set; }

        [MaxLength(50)]
        [Display(Name = "RPType")]
        public string RpType { get; set; }

        [Display(Name = "Ref Id")]
        public int? RefId { get; set; }

        [Display(Name = "Reftrans Id")]
        public int? RefTransId { get; set; }

        [Display(Name = "Ref Voucher Id")]
        public int? RefVoucherId { get; set; }

        [Display(Name = "Bank Date")]
        public int? BankDate { get; set; }

        public int TdsAcId { get; set; }

        public decimal SaleRate { get; set; }

        [ForeignKey("BillId")]
        public virtual BillModel Bill { get; set; }

    }
}
