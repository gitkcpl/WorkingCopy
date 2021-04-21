using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    [Table("sales_trans")]
    public class SaleTrans : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public long? SalesID { get; set; }

        public int? RowID { get; set; }

        public long? ItemID { get; set; }

        public long? ScreenID { get; set; }

        public long? ColorID { get; set; }

        [MaxLength(100)]
        public string ItemRemark { get; set; }

        public decimal? Cut { get; set; }

        public long? Pcs { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Rate { get; set; }

        public long? UnitID { get; set; }

        public decimal? Total { get; set; }

        public decimal? DiscPer { get; set; }

        public decimal? DiscAmount { get; set; }

        public decimal? ServiceTaxPer { get; set; }

        public decimal? ServiceTaxAmount { get; set; }

        public decimal? EduCessPer { get; set; }

        public decimal? EduCessAmount { get; set; }

        public decimal? HeduCessPer { get; set; }

        public decimal? HeduCessAmount { get; set; }

        public decimal? VatPer { get; set; }

        public decimal? VatAmount { get; set; }

        public decimal? AdVatPer { get; set; }

        public decimal? AdVatAmount { get; set; }

        public decimal? CSTPer { get; set; }

        public decimal? CSTAmount { get; set; }

        public decimal? NetTotal { get; set; }

        public long? SoTransID { get; set; }

        public long? ScID { get; set; }

        [MaxLength(25)]
        public string LotNo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Sales Trans ID is required")]
        [Key]
        public long SalesTransID { get; set; }

        public long? CompanyID { get; set; }

        public long? QualityID { get; set; }

        public long? ItemCatID { get; set; }

        public long? StyleID { get; set; }

        public long? OutwardID { get; set; }

        public long? BatchID { get; set; }

        public int? GradeID { get; set; }

        public int? Cops { get; set; }

        public decimal? RetailRate { get; set; }

        public decimal? MRP { get; set; }

        public long? DesignID { get; set; }

        public long? ItemStatus { get; set; }

        public long? EmpID { get; set; }

        public long? SoID { get; set; }

        public decimal? GrossWeight { get; set; }

        public decimal? TareWeight { get; set; }

        public decimal? Ends { get; set; }

        public decimal? Meters { get; set; }

        public long? OutwardTransID { get; set; }

        public int? PackTypeID { get; set; }

        public decimal? ExcisePer { get; set; }

        public decimal? ExciseAmount { get; set; }

        public decimal? ExciseCessPer { get; set; }

        public decimal? ExciseCessAmt { get; set; }

        public decimal? ExciseHeduPer { get; set; }

        public decimal? ExciseHeduAmt { get; set; }

        public decimal? TaxableAmount { get; set; }

        [Required(ErrorMessage = "Trans Code is required")]
   
        public Guid TransCode { get; set; }

        [ForeignKey("SalesID")]
        public  Sale Sale { get; set; }

    }
}
