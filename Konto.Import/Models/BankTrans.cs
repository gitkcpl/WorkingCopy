using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Keysoft.Erp.Data.Models
{
   public class BankTrans
    {
        [Required(ErrorMessage = "Bank ID is required")]
        [Display(Name = "Bank ID")]
        public long BankID { get; set; }

        [Required(ErrorMessage = "Row ID is required")]
        [Display(Name = "Row ID")]
        public int RowID { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]
        public long AccountID { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Mode Type")]
        public int? ModeType { get; set; }

        [Display(Name = "Mode No")]
        public long? ModeNo { get; set; }

        [Display(Name = "Mode Date")]
        public DateTime? ModeDate { get; set; }

        [Required(ErrorMessage = "Method Of Adjust is required")]
        [Display(Name = "Method Of Adjust")]
        public int MethodOfAdjust { get; set; }

        [Display(Name = "Bank Trans Remark")]
        public string BankTransRemark { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Bank Trans ID is required")]
        [Display(Name = "Bank Trans ID")]
        public long BankTransID { get; set; }

        [Required(ErrorMessage = "Recon Date is required")]
        [Display(Name = "Recon Date")]
        public int ReconDate { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Display(Name = "refbankid")]
        public long? refbankid { get; set; }

        [Display(Name = "Bill Party Account ID")]
        public long? BillPartyAccountID { get; set; }

        [Display(Name = "Duty ID")]
        public int? DutyID { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Ref No")]
        public string RefNo { get; set; }

        [Required(ErrorMessage = "Chq Ret Ref Id is required")]
        [Display(Name = "Chq Ret Ref Id")]
        public long ChqRetRefId { get; set; }

        [Required(ErrorMessage = "Bank Code is required")]
        [Display(Name = "Bank Code")]
       
        public Guid TransCode { get; set; }

        [ForeignKey("BankID")]
        public Bank Bank { get; set; }

        

     //   [ForeignKey("AccountID")]
      //  public account Account { get; set; }

        //[ForeignKey("refbank_id")]
      //  public refbank Refbank { get; set; }

       // [ForeignKey("BillPartyAccountID")]
       // public account Account1 { get; set; }

    }
}
