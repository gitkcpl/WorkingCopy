using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("Ord")]
    public class OrdModel : AuditedEntity
    {

        public OrdModel()
        {
            Currency = "Indian Rupee";
            OrderType = "Revenew";
            VoucherNo = "New";
            VDate = DateTime.Now.Date;
            RequireDate = DateTime.Now.Date;
            IsActive = true;
        }

        [Required]
        [Display(Name = "Comp Id")]
        [Range(1, Int32.MaxValue)]
        public int CompId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Display(Name = "Year Id")]
        public int YearId { get; set; }

        [Required]
        [Display(Name = "Voucher Id")]
        [Range(1, Int32.MaxValue)]
        public int VoucherId { get; set; }

        [Required]
        [Display(Name = "Voucher Date")]
        public int VoucherDate { get; set; }


        [Required]
        [NotMapped]
        public DateTime? VDate
        {
            get
            {

                return DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture);

            }
            set { }

        }

        [MaxLength(25)]
        [Display(Name = "Voucher No")]
        [Required]
        public string VoucherNo { get; set; }

        [MaxLength(25)]
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }

        [Display(Name = "Acc Id")]
        [Required]
        [Range(1, Int32.MaxValue)]
        public int AccId { get; set; }

        [Display(Name = "Order Status")]
        [Required]
        [Range(0, Int32.MaxValue)]
        public int OrderStatusId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int EmpId { get; set; }

        [Display(Name = "Checker Id")]
        public int CheckerId { get; set; }

        [Display(Name = "Check Rate")]
        public decimal CheckRate { get; set; }

        [Display(Name = "Check Uom Id")]
        public int CheckUomId { get; set; }

        [Display(Name = "Emp Rate")]
        public decimal EmpRate { get; set; }

        [Display(Name = "Emp Uom Id")]
        public int EmpUomId { get; set; }

        public int CheckerOutId { get; set; }

        [Display(Name = "Store Id")]
        public int StoreId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Exch Rate")]
        public decimal ExchRate { get; set; }

        [Display(Name = "Agent Id")]
        public int AgentId { get; set; }

        [Display(Name = "Transport Id")]
        public int TransportId { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Branch Id")]
        public int BranchId { get; set; }

        [Display(Name = "Require Date")]
        public DateTime RequireDate { get; set; }

        [MaxLength(25)]
        [Display(Name = "Order Type")]
        public string OrderType { get; set; }

        [Required]
        [Display(Name = "Division Id")]
        public int DivisionId { get; set; }

        [Display(Name = "P Group Id")]
        public int PGroupId { get; set; }

        [Display(Name = "TypeId")]
        public int TypeId { get; set; }

        [MaxLength]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [MaxLength(500)]
        [Display(Name = "Special Notes")]
        public string SpecialNotes { get; set; }

        [Display(Name = "Pay Terms Id")]
        public int PayTermsId { get; set; }

        [Display(Name = "Total Pcs")]
        public decimal TotalPcs { get; set; }

        [Display(Name = "Total Qty")]
        public decimal TotalQty { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [Display(Name = "Auth")]
        public bool Auth { get; set; }

        [Display(Name = "Ref Id")]
        public int RefId { get; set; }

        [Display(Name = "Ref Voucher Id")]
        public int RefVoucherId { get; set; }

        // dbo.Ord.CompId -> dbo.Company.Id (FK_Ord_Company)
        [ForeignKey("CompId")]
        public virtual CompModel Company { get; set; }

        // dbo.Ord.VoucherId -> dbo.Voucher.Id (FK_Ord_Voucher)
        [ForeignKey("VoucherId")]
        public virtual VoucherModel Voucher { get; set; }

        // dbo.Ord.AccId -> dbo.Acc.Id (FK_Ord_Acc)
        [ForeignKey("AccId")]
        public virtual AccModel Acc { get; set; }

        // dbo.Ord.EmpId -> dbo.Emp.Id (FK_Ord_Emp)
        [ForeignKey("EmpId")]
        public virtual EmpModel Emp { get; set; }

        // dbo.OrdTrans.OrdId -> dbo.Ord.Id (FK_OrdTrans_Ord)
        //public virtual ICollection<OrdTransModel> OrdTrans { get; set; }
    }



    [Table("OrdTrans")]
    public class OrdTransModel : AuditedEntity
    {
        public OrdTransModel()
        {
            IsActive = true;
            IsDeleted = false;
        }

        [Display(Name = "Ord Id")]
        public int OrdId { get; set; }

        [Required]
        [Display(Name = "Product Id")]
        [Range(1, 9999999)]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Color Id")]
        public int ColorId { get; set; }

        [Required]
        [Display(Name = "Design Id")]
        public int DesignId { get; set; }

        [Display(Name = "Grade Id")]
        public int GradeId { get; set; }

        [Required]
        [Display(Name = "Avg Wt")]
        public decimal AvgWt { get; set; }

        [Required]
        [Display(Name = "Cut")]
        public decimal Cut { get; set; }

        [Required]
        [Display(Name = "Width")]
        public decimal Width { get; set; }

        [Required]
        [Display(Name = "No Of Lot")]
        public int NoOfLot { get; set; }

        [Required]
        [Display(Name = "Lot Pcs")]
        public int LotPcs { get; set; }

        [Required]
        [Display(Name = "Qty")]
        [Range(0.1, 999999999)]
        public decimal Qty { get; set; }

        [Required]
        [Display(Name = "Uom Id")]
        [Range(1, 999)]
        public int UomId { get; set; }

        [Required]
        [Display(Name = "Rate")]
        [Range(0.0000, 999999999)]
        public decimal Rate { get; set; }

        [Required]
        [Display(Name = "Total")]
        [Range(0.0000, 99999999999)]
        public decimal Total { get; set; }

        [Required]
        [Display(Name = "Disc")]
        [Range(0.0000, 99)]
        public decimal Disc { get; set; }

        [Required]
        [Display(Name = "Disc Amt")]
        [Range(0.0000, 999999999)]
        public decimal DiscAmt { get; set; }

        [Required]
        [Display(Name = "Sgst")]
        [Range(0.0000, 99)]
        public decimal Sgst { get; set; }

        [Required]
        [Display(Name = "Sgst Amt")]
        [Range(0.0000, 999999999)]
        public decimal SgstAmt { get; set; }

        [Required]
        [Display(Name = "Cgst")]
        [Range(0.0000, 99)]
        public decimal Cgst { get; set; }

        [Required]
        [Display(Name = "Cgst Amt")]
        [Range(0.0000, 999999999)]
        public decimal CgstAmt { get; set; }

        [Required]
        [Display(Name = "Igst")]
        [Range(0.0000, 99)]
        public decimal Igst { get; set; }

        [Required]
        [Display(Name = "Igst Amt")]
        [Range(0.0000, 999999999)]
        public decimal IgstAmt { get; set; }

        [Required]
        [Display(Name = "Cess")]
        [Range(0.0000, 99)]
        public decimal Cess { get; set; }

        [Required]
        [Display(Name = "Cess Amt")]
        [Range(0.0000, 999999999)]
        public decimal CessAmt { get; set; }

        [Required]
        [Display(Name = "Net Total")]
        [Range(0.0000, 99999999999)]
        public decimal NetTotal { get; set; }

        [MaxLength(200)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(200)]
        [Display(Name = "Comm Descr")]
        public string CommDescr { get; set; }

        [Required]
        [Display(Name = "Dept Id")]
        public int DeptId { get; set; }

        [Required]
        [Display(Name = "Division Id")]
        public int DivisionId { get; set; }

        [MaxLength(10)]
        [Display(Name = "Priority")]
        public string Priority { get; set; }

        [MaxLength(25)]
        [Display(Name = "Ord Status")]
        public string OrdStatus { get; set; }

        [Display(Name = "Ref Id")]
        public int RefId { get; set; }

        [Display(Name = "Ref Voucher Id")]
        public int RefVoucherId { get; set; }

        [Display(Name = "Warp Item Id")]
        public int? WarpItemId { get; set; }

        [MaxLength(200)]
        [Display(Name = "Cancel Reason")]
        public string CancelReason { get; set; }

        

        // dbo.OrdTrans.OrdId -> dbo.Ord.Id (FK_OrdTrans_Ord)
        [ForeignKey("OrdId")]
        public virtual OrdModel Ord { get; set; }

        // dbo.OrdTrans.ProductId -> dbo.Product.Id (FK_OrdTrans_Product)
        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }
    }
}
