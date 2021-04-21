using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Keysoft.Erp.Data.Models
{
    [Table("purchase")]
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Purchase ID is required")]
        public long PurchaseID { get; set; } // bigint, not null
        public Guid PurchaseCode { get; set; }
        [Required(ErrorMessage = "Company ID is required")]
        public long CompanyID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Voucher ID is required")]
        public long VoucherID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Voucher Date is required")]
        public int VoucherDate { get; set; } // int, not null
        [MaxLength(25)]
        public string VoucherNo { get; set; } // varchar(25), null
        [MaxLength(25)]
        public string BillNo { get; set; } // varchar(25), null
        [Required(ErrorMessage = "Order Date is required")]
        public int OrderDate { get; set; } // int, not null
        [MaxLength(1)]
        public string OrderNo { get; set; } // varchar(1), null
        [Required(ErrorMessage = "Challan Date is required")]
        public int ChallanDate { get; set; } // int, not null
        [MaxLength(100)]
        public string ChallanNo { get; set; } // varchar(25), null
        public long? PurTypeID { get; set; } // bigint, null
        [Required(ErrorMessage = "Account ID is required")]
        public long AccountID { get; set; } // bigint, not null
        [Required(ErrorMessage = "Purchase Ac ID is required")]
        public long PurchaseAcID { get; set; } // bigint, not null
        public long? AgentID { get; set; } // bigint, null
        public long? GodownID { get; set; } // bigint, null
        public long? TransportID { get; set; } // bigint, null
        public long? CityID { get; set; } // bigint, null
        [MaxLength(25)]
        public string LrNo { get; set; } // varchar(25), null
        [Required(ErrorMessage = "Lr Date is required")]
        public int LrDate { get; set; } // int, not null
        [Required(ErrorMessage = "Weight is required")]
        public decimal Weight { get; set; } // numeric(18,4), not null
        [Required(ErrorMessage = "Freight is required")]
        public decimal Freight { get; set; } // numeric(18,2), not null
        [MaxLength(15)]
        public string BaleNo { get; set; } // varchar(15), null
        public string PurchaseRemark { get; set; } // text, null
        [Required(ErrorMessage = "Total Withour Tax is required")]
        public decimal TotalWithourTax { get; set; } // numeric(26,2), not null
        [Required(ErrorMessage = "Discount Amount is required")]
        public decimal DiscountAmount { get; set; } // numeric(26,2), not null
        [Required(ErrorMessage = "Total Gross is required")]
        public decimal TotalGross { get; set; } // numeric(26,2), not null
        [Required(ErrorMessage = "Add Less Before Tax is required")]
        public decimal AddLessBeforeTax { get; set; } // numeric(26,2), not null
        [Required(ErrorMessage = "Service Tax Per is required")]
        public decimal ServiceTaxPer { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "Service Tax Amount is required")]
        public decimal ServiceTaxAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Edu Cess Per is required")]
        public decimal EduCessPer { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "Edu Cess Amount is required")]
        public decimal EduCessAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Hedu Cess Per is required")]
        public decimal HeduCessPer { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "Hedu Cess Amount is required")]
        public decimal HeduCessAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Vat Per is required")]
        public decimal VatPer { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "Vat Amount is required")]
        public decimal VatAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Ad Vat Per is required")]
        public decimal AdVatPer { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "Ad Vat Amount is required")]
        public decimal AdVatAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "CST Per is required")]
        public decimal CSTPer { get; set; } // numeric(10,2), not null
        [Required(ErrorMessage = "CST Amount is required")]
        public decimal CSTAmount { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Add Less After Tax is required")]
        public decimal AddLessAfterTax { get; set; } // numeric(18,2), not null
        [Required(ErrorMessage = "Total Amount is required")]
        public decimal TotalAmount { get; set; } // numeric(26,2), not null
        [Required(ErrorMessage = "Round Off is required")]
        public decimal RoundOff { get; set; } // numeric(5,2), not null
        [Required(ErrorMessage = "Bill Amount is required")]
        public decimal BillAmount { get; set; } // numeric(26,2), not null
        [Required(ErrorMessage = "Total Pcs is required")]
        public long TotalPcs { get; set; } // bigint, not null
        [Required(ErrorMessage = "Total Qty is required")]
        public decimal TotalQty { get; set; } // numeric(19,4), not null
        [Required(ErrorMessage = "Bill Close is required")]
        public bool BillClose { get; set; } // bit, not null
        [Required(ErrorMessage = "Rate is required")]
        public decimal Rate { get; set; } // numeric(18,2), not null
        public long? ItemID { get; set; } // bigint, null
        [Required(ErrorMessage = "Due Days is required")]
        public short DueDays { get; set; } // smallint, not null
        public int? DueDate { get; set; } // int, null
        [MaxLength(50)]
        public string FromNo { get; set; } // varchar(50), null
        public decimal? AddLedger { get; set; } // money, null
        public decimal? LessLedger { get; set; } // money, null
        [Required(ErrorMessage = "Sel is required")]
        public bool Sel { get; set; } // bit, not null
        public decimal? TDSPer { get; set; } // numeric(8,2), null
        public decimal? TdsAmount { get; set; } // money, null
        public int? UnitID { get; set; } // int, null
        public int? DivisionID { get; set; } // int, null
        public int? SessionID { get; set; } // int, null
        public int? UserID { get; set; } // int, null
        public DateTime TransDate { get; set; } // int, null
        [Required(ErrorMessage = "Currency is required")]
        public bool Currency { get; set; } // bit, not null
        public decimal? InrAmount { get; set; } // money, null
        public decimal? CurrencyRate { get; set; } // numeric(10,2), null
        public bool? LedgerSel { get; set; } // bit, null
        public long? TDSAccountID { get; set; } // bigint, null
        public byte? PurchaseType { get; set; } // tinyint, null
        public int? TdsDate { get; set; } // int, null
        public decimal? ExcisePer { get; set; } // numeric(5,2), null
        public decimal? ExciseAmt { get; set; } // numeric(12,2), null
        public decimal? ExciseCessPer { get; set; } // numeric(5,2), null
        public decimal? ExciseCessAmt { get; set; } // numeric(10,2), null
        public decimal? ExciseHeduCessPer { get; set; } // numeric(5,2), null
        public decimal? ExciseHeduCessAmt { get; set; } // numeric(10,2), null
        public bool? TransferData { get; set; } // bit, null
        public decimal? DiscPer { get; set; } // numeric(5,2), null
        public decimal? DiscAmount { get; set; } // numeric(10,2), null
        public decimal? WtLessQty { get; set; } // numeric(10,2), null
        public decimal? OtherLess { get; set; } // numeric(10,2), null
        public decimal? AddAmount { get; set; } // numeric(10,2), null
        public decimal? RDAmount { get; set; } // numeric(10,2), null
        public decimal? WtLessRate { get; set; } // numeric(10,2), null
        public decimal? WtLessAmount { get; set; } // numeric(10,2), null
        public decimal? SweetAmount { get; set; } // numeric(10,2), null
        public decimal? RGAmount { get; set; } // numeric(10,2), null
        public decimal? AddPer { get; set; } // numeric(5,2), null
        public decimal? AddAmount2 { get; set; } // numeric(10,2), null
        public decimal? DDComAmount { get; set; } // numeric(10,2), null
        public decimal? DisputeAmount { get; set; } // numeric(10,2), null
        public decimal? FoldAmount { get; set; } // numeric(10,2), null
        public decimal? TotalAddLess { get; set; } // numeric(10,2), null
        [MaxLength(40)]
        public string AddLessRmk { get; set; } // varchar(40), null
        public decimal? AddLessBase { get; set; } // numeric(10,2), null
        public decimal? AddLessRate { get; set; } // numeric(10,2), null
        public decimal? AddLessAmt { get; set; } // numeric(10,2), null
        [MaxLength(40)]
        public string AddLessRmk1 { get; set; } // varchar(40), null
        public decimal? AddLessBase1 { get; set; } // numeric(10,2), null
        public decimal? AddLessRate1 { get; set; } // numeric(10,2), null
        public decimal? AddLessAmt1 { get; set; } // numeric(10,2), null
        [MaxLength(40)]
        public string AddLessRmk2 { get; set; } // varchar(40), null
        public decimal? AddLessBase2 { get; set; } // numeric(10,2), null
        public decimal? AddLessRate2 { get; set; } // numeric(10,2), null
        public decimal? AddLessAmt2 { get; set; } // numeric(10,2), null
        public decimal? Interest { get; set; } // numeric(10,2), null
        public int? PayDate { get; set; } // int, null
        public long? ExciseType { get; set; } // bigint, null
        [Required(ErrorMessage = "Authorized is required")]
        public bool Authorized { get; set; } // bit, not null
        public int? FormDate { get; set; } // int, null

        public ICollection<PurchaseTrans> PurchaseTrans { get; set; }
        public ICollection<PurchaseAddOn> PurchaseAddOn { get; set; }

    }

}

