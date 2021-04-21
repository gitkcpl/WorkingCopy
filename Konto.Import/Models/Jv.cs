using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Jv
    {
        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Jv ID is required")]
        [Display(Name = "Jv ID")]
        public long JvID { get; set; }

        [Required(ErrorMessage = "Jv Trans Type is required")]
        [Display(Name = "Jv Trans Type")]
        public int JvTransType { get; set; }

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

        [Required(ErrorMessage = "Jv Ac ID is required")]
        [Display(Name = "Jv Ac ID")]
        public long JvAcID { get; set; }

        [Required(ErrorMessage = "Jv Amount is required")]
        [Display(Name = "Jv Amount")]
        public decimal JvAmount { get; set; }

        [Display(Name = "Dept ID")]
        public int? DeptID { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Narration")]
        public string Narration { get; set; }

        [Display(Name = "Jv Remark")]
        public string JvRemark { get; set; }

        [Required(ErrorMessage = "Sel is required")]
        [Display(Name = "Sel")]
        public bool Sel { get; set; }

        [Display(Name = "Ref ID")]
        public long? RefID { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Division ID")]
        public int? DivisionID { get; set; }

        [Display(Name = "Ledger Sel")]
        public bool? LedgerSel { get; set; }

        [Display(Name = "Ref Voucher ID")]
        public long? RefVoucherID { get; set; }

        [Display(Name = "Pk Sr")]
        public long? PkSr { get; set; }

        [Display(Name = "Entry Date")]
        public int? EntryDate { get; set; }

        [Required(ErrorMessage = "Authorized is required")]
        [Display(Name = "Authorized")]
        public bool Authorized { get; set; }

        [Required(ErrorMessage = "Jv Code is required")]
        [Display(Name = "Jv Code")]
        public Guid JvCode { get; set; }

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

        //public List<depr_posting> DeprPostings { get; set; }

        //public List<jv_bill> JvBills { get; set; }

        //public List<jv_bill_adjust> JvBillAdjusts { get; set; }

        public List<JvTrans> JvTrans { get; set; }

       // public List<TdsJv> TdsJvs { get; set; }

    }
}
