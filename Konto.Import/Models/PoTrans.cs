using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keysoft.Erp.Data.Models
{
    public class PoTrans
    {
        public long PoID { get; set; }

        public long? ItemID { get; set; }

        public long? ColorID { get; set; }

        [MaxLength(100)]
        public string Remark { get; set; }

        public int? Pcs { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Rate { get; set; }

        public long? UnitID { get; set; }

        public decimal? Total { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PoTransID { get; set; }

        public long CompanyID { get; set; }

        public long? OrderStatus { get; set; }

        public int? DivisionID { get; set; }

        public int? Deptid { get; set; }

        [MaxLength(500)]
        public string CommercialDesc { get; set; }

        public long? IndentTrans { get; set; }

        public int? GradeID { get; set; }

        public long? ItemCatID { get; set; }

        public long? ScreenID { get; set; }

        public decimal VatPer { get; set; }

        public decimal VatAmount { get; set; }

        public decimal AdVatPer { get; set; }

        public decimal CstPer { get; set; }

        public decimal AdVatAmount { get; set; }

        public decimal CstAmount { get; set; }

        public decimal CessRate { get; set; }

        public decimal CessAmount { get; set; }

        public decimal DiscPer { get; set; }

        public decimal DiscAmount { get; set; }

        [ForeignKey("PoID")]
        public Po Po { get; set; }

    }
}
