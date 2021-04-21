using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Sc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ScID { get; set; }

        public long CompanyID { get; set; }

        public long VoucherID { get; set; }

        public int ScDate { get; set; }

        [MaxLength(25)]
        public string VoucherNo { get; set; }

        public int SoDate { get; set; }

        [MaxLength(25)]
        public string SoVoucherNo { get; set; }

        public long AccountID { get; set; }

        public long? AgentID { get; set; }

        public long? TransportID { get; set; }

        public long TotalPcs { get; set; }

        public decimal TotalQty { get; set; }

        public decimal TotalAmt { get; set; }

        [MaxLength]
        public string ScRemark { get; set; }

        public bool BillStatus { get; set; }

        public bool Sel { get; set; }

        public long? ItemID { get; set; }

        public decimal? Rate { get; set; }

        public long? SoID { get; set; }

        public long? SoTransID { get; set; }

        public long? PartyID { get; set; }

        public int? UnitID { get; set; }

        public int? DivisionID { get; set; }

        public decimal? RoundOff { get; set; }

        public decimal? BillAmount { get; set; }

        public byte? SalesType { get; set; }

        public long? ReferenceNo { get; set; }

        [MaxLength(20)]
        public string Module { get; set; }

        public bool ScCancel { get; set; }

        public DateTime? TransDate { get; set; }

        public int? AdduserId { get; set; }

        public int? EditUserId { get; set; }

        [MaxLength(25)]
        public string LrNo { get; set; }

        public int? LrDate { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Freight { get; set; }

        public long? JobCardID { get; set; }

        public long? ColorID { get; set; }

        public long? BookID { get; set; }

        public Guid ScCode { get; set; }

        public string DriverName { get; set; }

        public string DriverlicenceNo { get; set; }

        public long? SessionID { get; set; }
        public long? BatchID { get; set; }
        public int? GradeID { get; set; }
        public int? SubGradeID { get; set; }
        public long? CustomerID { get; set; }
        public long? EmpID { get; set; }
        public byte Counter { get; set; }

    }

    public class ScTrans
    {
        public long ScID { get; set; }

        public int RowID { get; set; }

        public long ItemID { get; set; }

        public long? ScreenID { get; set; }

        public long? ColorID { get; set; }

        public string ItemRemark { get; set; }

        public long Pcs { get; set; }

        public decimal Qty { get; set; }

        public decimal Rate { get; set; }

        public long UnitID { get; set; }

        public decimal Total { get; set; }

        public long SoID { get; set; }

        public long? SoTransID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ScTransID { get; set; }

        public long CompanyID { get; set; }

        public decimal Cut { get; set; }

        public decimal? LengthMtrs { get; set; }

        public long? DesignID { get; set; }

        [MaxLength(35)]
        public string MergeNo { get; set; }

        public decimal? GrossWt { get; set; }

        public decimal? TareWt { get; set; }

        public short? Cops { get; set; }

        public int? Ends { get; set; }

        public long? GradeID { get; set; }

        public long? ItemCatID { get; set; }

        public decimal VatPer { get; set; }

        public decimal VatAmount { get; set; }

        public decimal AdVatPer { get; set; }

        public decimal AdVatAmount { get; set; }

        public decimal CSTPer { get; set; }

        public decimal CSTAmount { get; set; }

        public decimal NetTotal { get; set; }

        public decimal CessRate { get; set; }

        public decimal CessAmount { get; set; }

        public Guid TransCode { get; set; }

        public long? EmpID { get; set; }

        public decimal RetailRate { get; set; }

        public decimal DiscPer { get; set; }

        public decimal DiscAmount { get; set; }

        [ForeignKey("ScID")]
        public Sc Sc { get; set; }
    }
}