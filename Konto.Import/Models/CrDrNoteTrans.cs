using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class CrDrNoteTrans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Trans ID is required")]
        [Display(Name = "Trans ID")]
        public int TransID { get; set; }

        [Display(Name = "Id")]
        public long? Id { get; set; }

        [Display(Name = "Account ID")]
        public long? AccountID { get; set; }

        [MaxLength(500)]
        [StringLength(500)]
        [Display(Name = "Narration")]
        public string Narration { get; set; }

        [Display(Name = "Vat Class ID")]
        public int? VatClassID { get; set; }

        [Required(ErrorMessage = "Sgst Per is required")]
        [Display(Name = "Sgst Per")]
        public decimal SgstPer { get; set; }

        [Required(ErrorMessage = "Sgst Amt is required")]
        [Display(Name = "Sgst Amt")]
        public decimal SgstAmt { get; set; }

        [Required(ErrorMessage = "Cgst Per is required")]
        [Display(Name = "Cgst Per")]
        public decimal CgstPer { get; set; }

        [Required(ErrorMessage = "Cgst Amt is required")]
        [Display(Name = "Cgst Amt")]
        public decimal CgstAmt { get; set; }

        [Required(ErrorMessage = "Igst Per is required")]
        [Display(Name = "Igst Per")]
        public decimal IgstPer { get; set; }

        [Required(ErrorMessage = "Gross is required")]
        [Display(Name = "Gross")]
        public decimal Gross { get; set; }

        [Required(ErrorMessage = "Total is required")]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Igst Amt is required")]
        [Display(Name = "Igst Amt")]
        public decimal IgstAmt { get; set; }

        [Required(ErrorMessage = "Trans Code is required")]
        [Display(Name = "Trans Code")]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransCode { get; set; }

        [ForeignKey("Id")]
        public CrDrNote CrDrNote { get; set; }

        public decimal ExciseCessPer { get; set; }
        public decimal ExciseCessAmt { get; set; }

        public int Pcs { get; set; }

        public decimal Qty { get; set; }

        public decimal Rate { get; set; }

        public decimal Addition { get; set; }

        public decimal Discount { get; set; }


        //[ForeignKey("account_Id")]
        // public account Account { get; set; }

        //[ForeignKey("VatClassID")]
        //  public VatClass VatClass { get; set; }

    }
}
