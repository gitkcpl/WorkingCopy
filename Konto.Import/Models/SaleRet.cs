using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class SaleRet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Sales Ret ID is required")]
        [Display(Name = "Sales Ret ID")]
        public long SalesRetID { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Required(ErrorMessage = "Voucher ID is required")]
        [Display(Name = "Voucher ID")]
        public long VoucherID { get; set; }

        [Required(ErrorMessage = "Voucher Date is required")]
        [Display(Name = "Voucher Date")]
        public int VoucherDate { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [Required(ErrorMessage = "Order Date is required")]
        [Display(Name = "Order Date")]
        public int OrderDate { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Order No")]
        public string OrderNo { get; set; }

        [Required(ErrorMessage = "Challan Date is required")]
        [Display(Name = "Challan Date")]
        public int ChallanDate { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Challan No")]
        public string ChallanNo { get; set; }

        [Display(Name = "Sales Type ID")]
        public long? SalesTypeID { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]
        public long AccountID { get; set; }

        [Required(ErrorMessage = "Sales Ac ID is required")]
        [Display(Name = "Sales Ac ID")]
        public long SalesAcID { get; set; }

        [Display(Name = "Agent ID")]
        public long? AgentID { get; set; }

        [Display(Name = "Godown ID")]
        public long? GodownID { get; set; }

        [Display(Name = "Transport ID")]
        public long? TransportID { get; set; }

        [Display(Name = "City ID")]
        public long? CityID { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Lr No")]
        public string LrNo { get; set; }

        [Required(ErrorMessage = "Lr Date is required")]
        [Display(Name = "Lr Date")]
        public int LrDate { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Display(Name = "Weight")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Freight is required")]
        [Display(Name = "Freight")]
        public decimal Freight { get; set; }

        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Bale No")]
        public string BaleNo { get; set; }

        [Display(Name = "Sales Remark")]
        public string SalesRemark { get; set; }

        [Required(ErrorMessage = "Total Withour Tax is required")]
        [Display(Name = "Total Withour Tax")]
        public decimal TotalWithourTax { get; set; }

        [Required(ErrorMessage = "Discount Amount is required")]
        [Display(Name = "Discount Amount")]
        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "Total Gross is required")]
        [Display(Name = "Total Gross")]
        public decimal TotalGross { get; set; }

        [Required(ErrorMessage = "Add Less Before Tax is required")]
        [Display(Name = "Add Less Before Tax")]
        public decimal AddLessBeforeTax { get; set; }

        [Required(ErrorMessage = "Service Tax Per is required")]
        [Display(Name = "Service Tax Per")]
        public decimal ServiceTaxPer { get; set; }

        [Required(ErrorMessage = "Service Tax Amount is required")]
        [Display(Name = "Service Tax Amount")]
        public decimal ServiceTaxAmount { get; set; }

        [Required(ErrorMessage = "Edu Cess Per is required")]
        [Display(Name = "Edu Cess Per")]
        public decimal EduCessPer { get; set; }

        [Required(ErrorMessage = "Edu Cess Amount is required")]
        [Display(Name = "Edu Cess Amount")]
        public decimal EduCessAmount { get; set; }

        [Required(ErrorMessage = "Hedu Cess Per is required")]
        [Display(Name = "Hedu Cess Per")]
        public decimal HeduCessPer { get; set; }

        [Required(ErrorMessage = "Hedu Cess Amount is required")]
        [Display(Name = "Hedu Cess Amount")]
        public decimal HeduCessAmount { get; set; }

        [Required(ErrorMessage = "Vat Per is required")]
        [Display(Name = "Vat Per")]
        public decimal VatPer { get; set; }

        [Required(ErrorMessage = "Vat Amount is required")]
        [Display(Name = "Vat Amount")]
        public decimal VatAmount { get; set; }

        [Required(ErrorMessage = "Ad Vat Per is required")]
        [Display(Name = "Ad Vat Per")]
        public decimal AdVatPer { get; set; }

        [Required(ErrorMessage = "Ad Vat Amount is required")]
        [Display(Name = "Ad Vat Amount")]
        public decimal AdVatAmount { get; set; }

        [Required(ErrorMessage = "CST Per is required")]
        [Display(Name = "CST Per")]
        public decimal CSTPer { get; set; }

        [Required(ErrorMessage = "CST Amount is required")]
        [Display(Name = "CST Amount")]
        public decimal CSTAmount { get; set; }

        [Required(ErrorMessage = "Add Less After Tax is required")]
        [Display(Name = "Add Less After Tax")]
        public decimal AddLessAfterTax { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Round Off is required")]
        [Display(Name = "Round Off")]
        public decimal RoundOff { get; set; }

        [Required(ErrorMessage = "Bill Amount is required")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmount { get; set; }

        [Required(ErrorMessage = "Total Pcs is required")]
        [Display(Name = "Total Pcs")]
        public long TotalPcs { get; set; }

        [Required(ErrorMessage = "Total Qty is required")]
        [Display(Name = "Total Qty")]
        public decimal TotalQty { get; set; }

        [Required(ErrorMessage = "Bill Close is required")]
        [Display(Name = "Bill Close")]
        public bool BillClose { get; set; }

        [Required(ErrorMessage = "Sel is required")]
        [Display(Name = "Sel")]
        public bool Sel { get; set; }

        [Display(Name = "Add Ledger")]
        public decimal? AddLedger { get; set; }

        [Display(Name = "Less Ledger")]
        public decimal? LessLedger { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Division ID")]
        public int? DivisionID { get; set; }

        [Display(Name = "Ledger Sel")]
        public bool? LedgerSel { get; set; }

        [Display(Name = "Customer ID")]
        public long? CustomerID { get; set; }

        [Display(Name = "Emp ID")]
        public int? EmpID { get; set; }

        [Display(Name = "Scheme ID")]
        public int? SchemeID { get; set; }

        [Display(Name = "Recv Amount")]
        public decimal? RecvAmount { get; set; }

        [Display(Name = "Counter")]
        public byte? Counter { get; set; }

        [Display(Name = "Add Less Amt2")]
        public decimal? AddLessAmt2 { get; set; }

        [Display(Name = "Excise Per")]
        public decimal? ExcisePer { get; set; }

        [Display(Name = "Excise Amt")]
        public decimal? ExciseAmt { get; set; }

        [Display(Name = "Excise Cess Per")]
        public decimal? ExciseCessPer { get; set; }

        [Display(Name = "Excise Cess Amt")]
        public decimal? ExciseCessAmt { get; set; }

        [Display(Name = "Excise Hedu Cess Per")]
        public decimal? ExciseHeduCessPer { get; set; }

        [Display(Name = "Excise Hedu Cess Amt")]
        public decimal? ExciseHeduCessAmt { get; set; }

        [Display(Name = "Invoice Type")]
        public long? InvoiceType { get; set; }

        [Display(Name = "Add User ID")]
        public int? AddUserID { get; set; }

        [Display(Name = "Edit User ID")]
        public int? EditUserID { get; set; }

        [Display(Name = "Trans Date")]
        public DateTime? TransDate { get; set; }

        [Required(ErrorMessage = "Authorized is required")]
        [Display(Name = "Authorized")]
        public bool Authorized { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Form No")]
        public string FormNo { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Reason")]
        public string Reason { get; set; }

      //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Ret Code is required")]
        [Display(Name = "Ret Code")]
        public Guid RetCode { get; set; }

        //[ForeignKey("CompanyID")]
        //public company Company { get; set; }

       // [ForeignKey("voucher_id")]
       // public Voucher Voucher { get; set; }

        //[ForeignKey("type_id")]
        //public purtype Purtype { get; set; }

      //  [ForeignKey("AccountID")]
      //  public account Account { get; set; }

        //[ForeignKey("SalesAcID")]
        //public account Account1 { get; set; }

        //[ForeignKey("AgentID")]
        //public account Account2 { get; set; }

        //[ForeignKey("trans_id")]
        //public trans Tran { get; set; }

        //[ForeignKey("city_id")]
        //public city City { get; set; }

        //[ForeignKey("UnitID")]
        //public Unit_M UnitM { get; set; }

        //[ForeignKey("DivisionID")]
        //public DivisionM DivisionM { get; set; }

        //public List<saleret_bill> SaleretBills { get; set; }

        //public List<SaleRet_trans_d> SaleRetTransD { get; set; }

        public List<SaleRetAddon> SalesRetAddons { get; set; }

        public List<SaleRetTrans> SalesRetTrans { get; set; }

    }
}
