using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("BillMain")]
    public class BillModel : AuditedEntity
    {
        public BillModel()
        {
            VoucherNo = "New";
            ModeofTrans = "By Road";
            Currency = "Indian Rupee";
            VDate = DateTime.Now.Date;
            DocDate = DateTime.Now.Date;
            IsActive = true;
            TypeId = 1;
        }


        [Required(ErrorMessage = "Comp Id is required")]
        [Display(Name = "Comp Id")]
        public int CompId { get; set; }

        [Required(ErrorMessage = "Year Id is required")]
        [Display(Name = "Year Id")]
        public int YearId { get; set; }

        [Required(ErrorMessage = "Voucher Id is required")]
        [Display(Name = "Voucher Id")]
        public int VoucherId { get; set; }

        [Required(ErrorMessage = "Voucher Date is required")]
        [Display(Name = "Voucher Date")]
        
        public int VoucherDate { get; set; }

        [MaxLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [MaxLength(500)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [MaxLength(25)]
        [Display(Name = "Rcm")]
        public string Rcm { get; set; }

        [MaxLength(25)]
        [Display(Name = "Itc")]
        public string Itc { get; set; }

        [Display(Name = "Rcd Date")]
        public DateTime? RcdDate { get; set; }


        [Display(Name = "Haste Id")]
        public int? HasteId { get; set; }

        [Display(Name = "State Id")]
        public int? StateId { get; set; }


        [Display(Name = "Type Id")]
        public int TypeId { get; set; }

        [Display(Name = "Duedays")]
        public int? Duedays { get; set; }

        [Display(Name = "Trans Id")]
        public int? TransId { get; set; }

        [Display(Name = "Ref Id")]
        public int? RefId { get; set; }

        public int? RefVoucherId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Vehicle No")]
        public string VehicleNo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Bill Type")]
        public string BillType { get; set; }

        [MaxLength(25)]
        [Display(Name = "Doc No")]
        public string DocNo { get; set; }

        [Display(Name = "Doc Date")]
        public DateTime? DocDate { get; set; }

        [Display(Name = "V Date")]
        public DateTime VDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Port Code")]
        public string PortCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "Modeof Trans")]
        public string ModeofTrans { get; set; }

        [MaxLength(50)]
        [Display(Name = "Eway Bill No")]
        public string EwayBillNo { get; set; }

        [MaxLength(25)]
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }

        [Display(Name = "Book Acc Id")]
        public int? BookAcId { get; set; }

        [Display(Name = "Acc Id")]
        [Index]
        public int AccId { get; set; }

        [Display(Name = "Emp Id")]
        public int? EmpId { get; set; }

        [Display(Name = "Store Id")]
        public int? StoreId { get; set; }


        [Display(Name = "Delv Ac Id")]
        public int? DelvAccId { get; set; }


        [Display(Name = "Delv Adr Id")]
        public int? DelvAdrId { get; set; }

        [Display(Name = "Adress Id")]
        public int? AddrId { get; set; }

        [MaxLength(50)]
        [Display(Name = "DName")]
        public string DName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Exch Rate")]
        public decimal ExchRate { get; set; }

        [Display(Name = "Agent Id")]
        public int? AgentId { get; set; }

        [Display(Name = "Branch Id")]
        public int? BranchId { get; set; }

        [Display(Name = "Require Date")]
        public DateTime? RequireDate { get; set; }

        [Display(Name = "Division Id")]
        public int? DivisionId { get; set; }

        [MaxLength]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [MaxLength(500)]
        [Display(Name = "Special Notes")]
        public string SpecialNotes { get; set; }

        [Display(Name = "Total Pcs")]
        public decimal TotalPcs { get; set; }

        [Display(Name = "Total Qty")]
        public decimal TotalQty { get; set; }

        [Display(Name = "Tds%")]
        public decimal TdsPer { get; set; }

        [Display(Name = "Tds Amount")]
        public decimal TdsAmt { get; set; }

        [Display(Name = "Tcs%")]
        public decimal TcsPer { get; set; }

        [Display(Name = "Tcs Amount")]
        public decimal TcsAmt { get; set; }

        [Display(Name = "Custom Duty %")]
        public decimal CustomP { get; set; }

        [Display(Name = "Custom Duty Amount")]
        public decimal CustomA { get; set; }

        [Display(Name = "Ocean freight %")]
        public decimal OceanFrtP { get; set; }

        [Display(Name = "Ocean freight Amount")]
        public decimal OceanFrtA { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Gross Amount")]
        public decimal GrossAmount { get; set; }

        [NotMapped]
        [Display(Name = "Bill Amount")]
        public decimal BillAmt { get; set; }

        [NotMapped]
        [Display(Name = "Payable Amount")]
        public decimal PayableAmt { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(500)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [Display(Name = "Auth")]
        public bool? Auth { get; set; }

        [Display(Name = "Round Off")]
        public decimal? RoundOff { get; set; }

        public decimal AddLess { get; set; }

        public int CostHeadId { get; set; }

        [NotMapped]
        public string DelAddress { get; set; }

        // dbo.BillMain.VoucherId -> dbo.Voucher.Id (FK_BillMain_Voucher)
        [ForeignKey("VoucherId")]
        public virtual VoucherModel Voucher { get; set; }

        
    }
}
