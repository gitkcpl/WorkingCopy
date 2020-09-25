using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class GrnTransDto
    {
        public int Id { get; set; }
        public int? ChallanId { get; set; }
        [Required]
        public string ProductName { get; set; }
        
       
        public string ColorName { get; set; }
        public string DesignNo { get; set; }
        public string GradeName { get; set; }

        [Range(1, 9999999, ErrorMessage = "Product Name is Required")]
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int GradeId { get; set; }
        public int DesignId { get; set; }
        public string LotNo { get; set; }
        public decimal Cops { get; set; }

        public int Pcs { get; set; }

        [Required]
        [Display(Name = "Qty")]
        [Range(0.0000, double.MaxValue)]
        public decimal Qty { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Rate { get; set; }

        [Required][Range(1, 9999999,ErrorMessage ="Invalid Unit Name")]
        public int UomId { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Gross { get; set; }

     

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

        [Range(0.0000, 99)]
        public decimal CessPer { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Cess { get; set; }

        
        [Range(0.0000, 999999999)]
        public decimal Total { get; set; }

        [MaxLength]
        public string Remark { get; set; }
        public int? RefId { get; set; }

        public int? RefVoucherId { get; set; }

        public int? MiscId { get; set; }

       
        public string OrdNo { get; set; }

       
       
        public DateTime? OrdDate
        {
            get
            {
                if (ODate != null)
                { return (DateTime.ParseExact(ODate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
            set { }
        }
        public int? ODate { get; set; }

        public string RefNo { get; set; }

        
    }
}
