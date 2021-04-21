using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Cash
    {
        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Cash ID is required")]
        [Display(Name = "Cash ID")]
        public long CashID { get; set; }

        [Required(ErrorMessage = "Cash Trans Type is required")]
        [Display(Name = "Cash Trans Type")]
        public int CashTransType { get; set; }

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

        [Required(ErrorMessage = "Cash Ac Id is required")]
        [Display(Name = "Cash Ac Id")]
        public long CashAcId { get; set; }

        [Required(ErrorMessage = "Cash Amount is required")]
        [Display(Name = "Cash Amount")]
        public decimal CashAmount { get; set; }

        [Display(Name = "Dept ID")]
        public int? DeptID { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Narration")]
        public string Narration { get; set; }

        [Display(Name = "Cash Remark")]
        public string CashRemark { get; set; }

        [Required(ErrorMessage = "Sel is required")]
        [Display(Name = "Sel")]
        public bool Sel { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Division ID")]
        public int? DivisionID { get; set; }

        [Display(Name = "Ledger Sel")]
        public bool? LedgerSel { get; set; }

        [Required(ErrorMessage = "Authorized is required")]
        [Display(Name = "Authorized")]
        public bool Authorized { get; set; }

        [Required(ErrorMessage = "Cash Code is required")]
        [Display(Name = "Cash Code")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CashCode { get; set; }

        //[ForeignKey("CompanyID")]
        //public company Company { get; set; }

        //[ForeignKey("voucher_id")]
        //public voucher Voucher { get; set; }

        //[ForeignKey("account_Id")]
        //public account Account { get; set; }

        //[ForeignKey("UnitID")]
        //public Unit_M UnitM { get; set; }

        //[ForeignKey("DivisionID")]
        //public DivisionM DivisionM { get; set; }

        //public List<cash_bill> CashBills { get; set; }

        //public List<cash_bill_adjust> CashBillAdjusts { get; set; }

        public List<CashTrans> CashTrans { get; set; }

    }
}
