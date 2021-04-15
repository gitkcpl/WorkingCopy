using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class BankTransDto
    {
        [Required]
        public string Particular { get; set; }

        [Display(Name ="Amount")]
        public decimal NetTotal { get; set; }

        public decimal Total { get; set; }

        [Display(Name ="Payment Type")]
        public string RpType { get; set; }

        [Display(Name ="Narration")]
        public string Remark { get; set; }

        [MaxLength(50)]
        public string ChequeNo { get; set; }
        
        public string RefBank { get; set; }

        public int ToAccId { get; set; }
        public int? RefBankId { get; set; }

        public int Id { get; set; }
        public int? BillId { get; set; }

        public int TdsAcId { get; set; }
        public decimal TdsAmt { get; set; }
        public decimal TdsPer { get; set; }

        public string Balance { get; set; }
    }
}
