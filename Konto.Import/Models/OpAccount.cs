using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class OpAccount
    {
        
        [Column(Order = 1)]
        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }



        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]

       
        public long AccountID { get; set; }

        [Required(ErrorMessage = "Group ID is required")]
        [Display(Name = "Group ID")]
        public long GroupID { get; set; }

        [Required(ErrorMessage = "Op Balance Debit is required")]
        [Display(Name = "Op Balance Debit")]
        public decimal OpBalanceDebit { get; set; }

        [Required(ErrorMessage = "Op Balance Credit is required")]
        [Display(Name = "Op Balance Credit")]
        public decimal OpBalanceCredit { get; set; }

        [Required(ErrorMessage = "Share Per is required")]
        [Display(Name = "Share Per")]
        public decimal SharePer { get; set; }

        [Required(ErrorMessage = "On Date Op Balance is required")]
        [Display(Name = "On Date Op Balance")]
        public decimal OnDateOpBalance { get; set; }

        [Required(ErrorMessage = "Date Range Balance is required")]
        [Display(Name = "Date Range Balance")]
        public decimal DateRangeBalance { get; set; }

        [Display(Name = "Year ID")]
        public long? YearID { get; set; }

        [Display(Name = "Cenvat")]
        public decimal? Cenvat { get; set; }

        [Display(Name = "Edu Cess")]
        public decimal? EduCess { get; set; }

        [Display(Name = "High Edu Cess")]
        public decimal? HighEduCess { get; set; }

        [Key]
        [Required(ErrorMessage = "Op Code is required")]
        [Display(Name = "Op Code")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OpCode { get; set; }

        [Display(Name = "Op Bal")]
        public decimal OpBal { get; set; }

        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

       
       


    }
}
