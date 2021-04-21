using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    [Table("sales")]
    public class Sale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Sales ID is required")]
        [Key]
        public long SalesID { get; set; }

        public long? CompanyID { get; set; }

        public long? VoucherID { get; set; }

        public int? VoucherDate { get; set; }

        [MaxLength(25)]
        public string BillNo { get; set; }

        public int? OrderDate { get; set; }

        [MaxLength(25)]
        public string OrderNo { get; set; }

        public int? ChallanDate { get; set; }

        [MaxLength(150)]
        public string ChallanNo { get; set; }

        public long? SalesTypeID { get; set; }

        public long? AccountID { get; set; }

        public long? SalesAcID { get; set; }

        public long? AgentID { get; set; }

        public long? GodownID { get; set; }

        public long? TransportID { get; set; }

        public long? CityID { get; set; }

        [MaxLength(25)]
        public string LrNo { get; set; }

        public int? LrDate { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Freight { get; set; }

        [MaxLength(15)]
        public string BaleNo { get; set; }

        public string SalesRemark { get; set; }

        public decimal? TotalWithourTax { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? TotalGross { get; set; }

        public decimal? AddLessBeforeTax { get; set; }

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

        public decimal? AddLessAfterTax { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? RoundOff { get; set; }

        public decimal? BillAmount { get; set; }

        public long? TotalPcs { get; set; }

        public decimal? TotalQty { get; set; }

        public bool? BillClose { get; set; }

        public long? MasterID { get; set; }

        public short? Due_Days { get; set; }

        public int? Due_Date { get; set; }

        [MaxLength(50)]
        public string FormNo { get; set; }

        public bool? Sel { get; set; }

        public decimal? AddLedger { get; set; }

        public decimal? LessLedger { get; set; }

        public long? PartyID { get; set; }

        public int? UnitID { get; set; }

        public int? DivisionID { get; set; }

        public int? UserID { get; set; }

        public int? SessionID { get; set; }

        public DateTime? TransDate { get; set; }

        public bool? Currency { get; set; }

        public decimal? CurrencyRate { get; set; }

        public decimal? InrAmount { get; set; }

        [MaxLength(200)]
        public string Terms { get; set; }

        public bool? LedgerSel { get; set; }

        public decimal? TdsPer { get; set; }

        public decimal? TdsAmount { get; set; }

        public long? TdsAccountID { get; set; }

        public int? TdsDate { get; set; }

        public byte? Counter { get; set; }

        public short? TransType { get; set; }

        public long? DeliveryTypeID { get; set; }

        public long? CustomerID { get; set; }

        public long? SalesRetNo { get; set; }

        public int? SalesRetDate { get; set; }

        public decimal? SalesRetAmt { get; set; }

        public short? SalesType { get; set; }

        public int? EmpID { get; set; }

        public decimal? RecvAmt { get; set; }

        public int? SchemeID { get; set; }

        public decimal? Points { get; set; }

        public long? SoID { get; set; }

        public long? SalesRetID { get; set; }

        [Required(ErrorMessage = "Bill Cancel is required")]
        public bool BillCancel { get; set; }

        public decimal? DiscPer { get; set; }

        public decimal? DiscAmount { get; set; }

        public string WtLessQty { get; set; }

        public decimal? OtherLess { get; set; }

        public decimal? AddAmount { get; set; }

        public decimal? RDAmount { get; set; }

        public string WtLessRate { get; set; }

        public string WtLessAmount { get; set; }

        public decimal? SweetAmount { get; set; }

        public decimal? RGAmount { get; set; }

        public decimal? AddPer { get; set; }

        public decimal? AddAmount2 { get; set; }

        public string DDComAmount { get; set; }

        public decimal? DisputeAmount { get; set; }

        public decimal? FoldAmount { get; set; }

        public decimal? TotalAddLess { get; set; }

        [MaxLength(40)]
        public string AddLessRmk { get; set; }

        public decimal? AddLessBase { get; set; }

        public decimal? AddLessRate { get; set; }

        public decimal? AddLessAmt { get; set; }

        [MaxLength(40)]
        public string AddLessRmk1 { get; set; }

        public decimal? AddLessBase1 { get; set; }

        public decimal? AddLessRate1 { get; set; }

        public decimal? AddLessAmt1 { get; set; }

        [MaxLength(40)]
        public string AddLessRmk2 { get; set; }

        public decimal? AddLessBase2 { get; set; }

        public decimal? AddLessRate2 { get; set; }

        public decimal? AddLessAmt2 { get; set; }

        public decimal? Interest { get; set; }

        public int? PayDate { get; set; }

        public decimal? ExcisePer { get; set; }

        public decimal? ExciseAmt { get; set; }

        public decimal? ExciseCessPer { get; set; }

        public decimal? ExciseCessAmt { get; set; }

        public decimal? ExciseHeduCessPer { get; set; }

        public decimal? ExciseHeduCessAmt { get; set; }

        public DateTime? PrepDate { get; set; }

        public DateTime? PrepTime { get; set; }

        public DateTime? RemovalDate { get; set; }

        public DateTime? RemovalTime { get; set; }

        [Required(ErrorMessage = "Authorized is required")]
        public bool Authorized { get; set; }

        public int? FormDate { get; set; }
        public Nullable<int> VehicleID { get; set; }
        [Required(ErrorMessage = "Sales Code is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SalesCode { get; set; }

        public ICollection<SaleTrans> SaleTrans { get; set; }

        public ICollection<sales_pay> SalesPay { get; set; }
        
    }
}