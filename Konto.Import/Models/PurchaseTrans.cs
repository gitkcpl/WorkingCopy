using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Keysoft.Erp.Data.Models
{
    [Table("purchase_trans")]
    public class PurchaseTrans : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        [Required(ErrorMessage = "Purchase ID is required")]
        public long PurchaseID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Row ID is required")]
        public int RowID { get; set; } // int, not null
        public long? ItemID { get; set; } // bigint, null
        public long? ScreenID { get; set; } // bigint, null
        public long? ColorID { get; set; } // bigint, null
        [MaxLength(100)]
        public string ItemRemark { get; set; } // varchar(100), null
        [Required(ErrorMessage = "Cut is required")]
        public decimal Cut { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "Pcs is required")]
        public long Pcs { get; set; } // bigint, not null
        [Required(ErrorMessage = "Qty is required")]
        public decimal Qty { get; set; } // numeric(26,4), not null
        [Required(ErrorMessage = "Rate is required")]
        public decimal Rate { get; set; } // numeric(18,3), not null
        public long? UnitID { get; set; } // bigint, null
        [Required(ErrorMessage = "Total is required")]
        public decimal Total { get; set; } // numeric(26,2), not null
        [Required(ErrorMessage = "Disc Per is required")]
        public decimal DiscPer { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "Disc Amount is required")]
        public decimal DiscAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Service Tax Per is required")]
        public decimal ServiceTaxPer { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "Service Tax Amount is required")]
        public decimal ServiceTaxAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Edu Cess Per is required")]
        public decimal EduCessPer { get; set; } // numeric(6,2), not null
        [Required(ErrorMessage = "Edu Cess Amount is required")]
        public decimal EduCessAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Hedu Cess Per is required")]
        public decimal HeduCessPer { get; set; } // numeric(6,2), not null
        [Required(ErrorMessage = "Hedu Cess Amount is required")]
        public decimal HeduCessAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Vat Per is required")]
        public decimal VatPer { get; set; } // numeric(6,2), not null
        [Required(ErrorMessage = "Vat Amount is required")]
        public decimal VatAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Ad Vat Per is required")]
        public decimal AdVatPer { get; set; } // numeric(6,2), not null
        [Required(ErrorMessage = "Ad Vat Amount is required")]
        public decimal AdVatAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "CST Per is required")]
        public decimal CSTPer { get; set; } // numeric(6,2), not null
        [Required(ErrorMessage = "CST Amount is required")]
        public decimal CSTAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Net Total is required")]
        public decimal NetTotal { get; set; } // numeric(26,2), not null
        public long? PoTransID { get; set; } // bigint, null
        public long? PcID { get; set; } // bigint, null
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Purchase Trans ID is required")]
        public long PurchaseTransID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Company ID is required")]
        public long CompanyID { get; set; } // bigint, not null
        [MaxLength(35)]
        public string MergeNo { get; set; } // varchar(35), null
        public int? GradeID { get; set; } // int, null
        public int? Cops { get; set; } // int, null
        public long? DesignID { get; set; } // bigint, null
        public decimal? GrossWeight { get; set; } // numeric(10,3), null
        public decimal? TareWeight { get; set; } // numeric(10,3), null
        public decimal? Lengths { get; set; } // numeric(10,3), null
        public short? Ends { get; set; } // smallint, null
        public int? PackTypeID { get; set; } // int, null
        public long? ItemCatID { get; set; } // bigint, null
        public decimal? TaxableAmount { get; set; } // numeric(19,2), null
        public decimal? ExcisePer { get; set; } // numeric(5,2), null
        public decimal? ExciseAmount { get; set; } // numeric(15,3), null
        public decimal? ExciseCessPer { get; set; } // numeric(5,3), null
        public decimal? ExciseCessAmt { get; set; } // numeric(15,3), null
        public decimal? ExciseHeduPer { get; set; } // numeric(5,3), null
        public decimal? ExciseHeduAmt { get; set; } // numeric(15,3), null

      
        public Guid TransCode { get; set; }

        public Purchase Purchase { get; set; }
    }

}
