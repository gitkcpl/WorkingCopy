using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("RecpaySetting")]
    public class RPSetModel : AuditedEntity
    {

        [MaxLength(50)]
        [Required(ErrorMessage = "Rec Pay is required")]
        [Display(Name = "Rec Pay")]
        public string RecPay { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Field Name is required")]
        [Display(Name = "Field Name")]
        public string Field { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Plus Minus is required")]
        [Display(Name = "Plus Minus")]
        public string PlusMinus { get; set; }

        [MaxLength(10)]
        [Display(Name = "Per Cap")]
        public string PerCap { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Amt Cap is required")]
        [Display(Name = "Amt Cap")]
        public string AmtCap { get; set; }

        [Display(Name = "AccountId")]
        public int AccountId { get; set; }


        [Required(ErrorMessage = "Year is required")]
        [Display(Name = "YearId")]
        public int YearId { get; set; }

        [MaxLength(200)]
        [Display(Name = "Calc On")]
        public string CalcOn { get; set; }

        [MaxLength(200)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(200)]
        [Display(Name = "HsnCode")]
        public string HsnCode { get; set; }

        [Display(Name = "Drcr")]
        public string Drcr { get; set; }

        [Display(Name = "TaxId")]
        public int? TaxId { get; set; }

        [Display(Name = "VoucherId")]
        public int? VoucherId { get; set; }

        public int CompId { get; set; }
    }
}
