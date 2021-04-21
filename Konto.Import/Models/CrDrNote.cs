using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class CrDrNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "Note Code")]
        public Guid? NoteCode { get; set; }

        [Display(Name = "Voucher ID")]
        public long? VoucherID { get; set; }

        [Display(Name = "Voucher Date")]
        public int? VoucherDate { get; set; }

        [Display(Name = "Company ID")]
        public long? CompanyID { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [MaxLength(50)]
        public string OrderNo { get; set; }

        public int? OrderDate { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [Display(Name = "Bill Date")]
        public int? BillDate { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Doc No")]
        public string DocNo { get; set; }

        [Display(Name = "Doc Date")]
        public DateTime? DocDate { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Reason")]
        public string Reason { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Tax Bill")]
        public string TaxBill { get; set; }

        [Display(Name = "Account ID")]
        public long? AccountID { get; set; }

        [Required(ErrorMessage = "Cgst is required")]
        [Display(Name = "Cgst")]
        public decimal Cgst { get; set; }

        [Required(ErrorMessage = "Sgst is required")]
        [Display(Name = "Sgst")]
        public decimal Sgst { get; set; }

        [Required(ErrorMessage = "Igst is required")]
        [Display(Name = "Igst")]
        public decimal Igst { get; set; }

        [Required(ErrorMessage = "Taxable Value is required")]
        [Display(Name = "Taxable Value")]
        public decimal TaxableValue { get; set; }

        [Required(ErrorMessage = "Bill Amount is required")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmount { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(15)]
        [StringLength(15)]
        [Required(ErrorMessage = "Cr Dr Type is required")]
        [Display(Name = "Cr Dr Type")]
        public string CrDrType { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Required(ErrorMessage = "Trans Type is required")]
        [Display(Name = "Trans Type")]
        public string TransType { get; set; }

        public decimal TdsPer { get; set; }
        public decimal TdsAmount { get; set; }
        public long? TdsAccountID { get; set; }
        public int? DivisionID { get; set; }

        public long? RefBankID { get; set; }

        [MaxLength(50)]
        public string ModeNo { get; set; }

        public int? ModeDate { get; set; }

        public long? AgentID { get; set; }

        //  [ForeignKey("voucher_id")]
        // public voucher Voucher { get; set; }

        // [ForeignKey("account_Id")]
        //  public account Account { get; set; }

        //  public List<crdr_bill> CrdrBills { get; set; }

        public List<CrDrNoteAddon> CrDrNoteAddons { get; set; }

        public List<CrDrNoteTrans> CrDrNoteTrans { get; set; }

    }
}
