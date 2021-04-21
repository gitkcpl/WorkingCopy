using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class SoTrans
    {
        public long? SoID { get; set; }

        public int? RowID { get; set; }

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
        public long SoTransID { get; set; }

        public long CompanyID { get; set; }

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

        public bool? ItemStatus { get; set; }

        public int? EmpID { get; set; }

        public decimal? cut { get; set; }

        public decimal? MorQty { get; set; }

        public decimal? EveQty { get; set; }

        public long? OrderStatus { get; set; }

        public int? GradeID { get; set; }

        public int? DivisionID { get; set; }

        public int? Deptid { get; set; }

        public long? DesignID { get; set; }

        public long? ScreenID { get; set; }

        public long? ItemCatID { get; set; }

        [ForeignKey("SoID")]
        public So So { get; set; }

      

    }
}
