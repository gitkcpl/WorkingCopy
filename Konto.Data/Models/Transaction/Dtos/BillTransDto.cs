using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class BillTransDto
    {
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string DesignName { get; set; }
        public string GradeName { get; set; }
       
        public string LotNo { get; set; }
        public decimal Qty { get; set; }
        public decimal Cut { get; set; }
        public int Pcs { get; set; }

        [Display(Name = "Unit")]
        public int? UomId { get; set; }

        public decimal Rate { get; set; }

        public decimal Total { get; set; }

        [Display(Name = "Disc %")]
        [Range(0.0000, 99)]
        public decimal Disc { get; set; }

        [Display(Name = "Disc Amt")]
        [Range(0.0000, 999999999)]
        public decimal DiscAmt { get; set; }

        [Display(Name = "Freight Rate")]
        [Range(0.0000, 999999999)]
        public decimal FreightRate { get; set; }

        [Display(Name = "Freight")]
        [Range(0.0000, 999999999)]
        public decimal Freight { get; set; }

        [Display(Name = "Other Add")]
        [Range(0.0000, 999999999)]
        public decimal OtherAdd { get; set; }

        [Display(Name = "Other Less")]
        [Range(0.0000, 999999999)]
        public decimal OtherLess { get; set; }

        [Display(Name = "Sgst %")]
        [Range(0.0000, 99)]
        public decimal SgstPer { get; set; }

        [Display(Name = "Sgst Amt")]
        [Range(0.0000, 999999999)]
        public decimal Sgst { get; set; }

        [Display(Name = "Cgst %")]
        [Range(0.0000, 99)]
        public decimal CgstPer { get; set; }

        [Display(Name = "Cgst Amt")]
        [Range(0.0000, 999999999)]
        public decimal Cgst { get; set; }

        [Display(Name = "Igst %")]
        [Range(0.0000, 99)]
        public decimal IgstPer { get; set; }

        [Display(Name = "Igst Amt")]
        [Range(0.0000, 999999999)]
        public decimal Igst { get; set; }

        [Display(Name = "Cess Rate")]
        [Range(0.0000, 99)]
        public decimal CessPer { get; set; }

        [Display(Name = "Cess Amt")]
        [Range(0.0000, 999999999)]
        public decimal Cess { get; set; }
        public decimal NetTotal { get; set; }
        public string Remark { get; set; }
 
        public string ChallanNo { get; set; }
        public DateTime? ChallanDate {
            get
            {
                if (ChDate != null)
                { return (DateTime.ParseExact(ChDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
            set { }
        }
        public string OrderNo { get; set; }
        public DateTime? OrderDate { get {
                if (OrdDate != null)
                { return (DateTime.ParseExact(OrdDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            } set { }
        }
        public int Id { get; set; }
        public int? BillId { get; set; }
        public int? BatchId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int DesignId { get; set; }
        public int GradeId { get; set; }
        public int? RefId { get; set; }

        public int? RefTransId { get; set; }

        public int? RefVoucherId { get; set; }

        public int? OrdId { get; set; }
        public int? OrdDate { get; set; }
        public int? ChDate { get; set; }
    }
}
