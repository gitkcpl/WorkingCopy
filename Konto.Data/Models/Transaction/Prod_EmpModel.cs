using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("Prod_Emp")]
    public class Prod_EmpModel : AuditedEntity
    {
        public Prod_EmpModel()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        }

        [Display(Name = "Prod Id")]
        public int? ProdId { get; set; }

        [Display(Name = "Voucher Id")]
        public int? VoucherId { get; set; }

        [Display(Name = "LoadingTrans Id")]
        public int? LoadingTransId { get; set; }

        [Display(Name = "Prod Date")]
        public DateTime? ProdDate { get; set; }

        [Required]
        [Display(Name = "Emp Id")]
        public int EmpId { get; set; }

        [Display(Name = "Night Mtrs")]
        public decimal NightMtrs { get; set; }

        [Display(Name = "Day Mtrs")]
        public decimal DayMtrs { get; set; }

        [Display(Name = "Total Mtrs")]
        public decimal TotalMtrs { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }
    }
}