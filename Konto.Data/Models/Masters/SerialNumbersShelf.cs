using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("SerialNumbersShelf")]
    public class SerialNumbersShelf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "Voucher Id")]
        public virtual int VoucherId { get; set; }

        [Required]
        [Display(Name = "Serial Value")]
        public virtual int Serial_Value { get; set; }

        [Required]
        [Display(Name = "Is Hold")]
        public virtual bool Is_Hold { get; set; }

        [Required]
        [Display(Name = "Year Id")]
        public virtual int YearId { get; set; }

        [Required]
        [Display(Name = "Branch Id")]
        public virtual int BranchId { get; set; }

        [Required]
        [Display(Name = "Company Id")]
        public virtual int CompanyId { get; set; }
    }

}
