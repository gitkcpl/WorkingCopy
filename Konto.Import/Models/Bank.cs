using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Bank
    {
        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Bank ID is required")]
        [Display(Name = "Bank ID")]
        public long BankID { get; set; }

        [Display(Name = "Bank Trans Type")]
        public int? BankTransType { get; set; }

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

        [Required(ErrorMessage = "Bank Ac ID is required")]
        [Display(Name = "Bank Ac ID")]
        public long BankAcID { get; set; }

        [Required(ErrorMessage = "Bank Amount is required")]
        [Display(Name = "Bank Amount")]
        public decimal BankAmount { get; set; }

        [Display(Name = "Dept ID")]
        public int? DeptID { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Narration")]
        public string Narration { get; set; }

        [Display(Name = "Bank Remark")]
        public string BankRemark { get; set; }

        [Required(ErrorMessage = "Sel is required")]
        [Display(Name = "Sel")]
        public bool Sel { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Division ID")]
        public int? DivisionID { get; set; }

        [Display(Name = "Ledger Sel")]
        public bool? LedgerSel { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Challan No")]
        public string ChallanNo { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "BSR Code")]
        public string BSRCode { get; set; }

        [MaxLength(30)]
        [StringLength(30)]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "From Date")]
        public int? FromDate { get; set; }

        [Display(Name = "To Date")]
        public int? ToDate { get; set; }

        [Display(Name = "Challan Date")]
        public int? ChallanDate { get; set; }

        [Required(ErrorMessage = "Authorized is required")]
        [Display(Name = "Authorized")]
        public bool Authorized { get; set; }

        [Required(ErrorMessage = "Bank Code is required")]
        [Display(Name = "Bank Code")]
        public Guid BankCode { get; set; }

        //  [ForeignKey("CompanyID")]
        //  public company Company { get; set; }

        //  [ForeignKey("voucher_id")]
        //  public Voucher Voucher { get; set; }

        //   [ForeignKey("account_Id")]
        //   public account Account { get; set; }

        //[ForeignKey("UnitID")]
        //public Unit_M UnitM { get; set; }

        //[ForeignKey("DivisionID")]
        //public DivisionM DivisionM { get; set; }

        // public List<bank_bill> BankBills { get; set; }

        //  public List<bank_bill_adjust> BankBillAdjusts { get; set; }

        public List<BankTrans> BankTrans { get; set; }

    }
}
