using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Keysoft.Erp.Data.Models
{
    public class KeyYear
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Year ID is required")]
        [Display(Name = "Year ID")]
        public int YearID { get; set; }

        [Required(ErrorMessage = "From Date is required")]
        [Display(Name = "From Date")]
        public int FromDate { get; set; }

        [Required(ErrorMessage = "To Date is required")]
        [Display(Name = "To Date")]
        public int ToDate { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Year Code")]
        public string YearCode { get; set; }

    }
}
