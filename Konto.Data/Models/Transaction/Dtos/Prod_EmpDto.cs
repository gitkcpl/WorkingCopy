using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class Prod_EmpDto
    {
        public int Id { get; set; }
        public int? ProdId { get; set; }
        public int? VoucherId { get; set; }

        [Required]
        public int EmpId { get; set; }
        public bool? IsDeleted { get; set; }

        public int? LoadingTransId { get; set; }
        public DateTime? ProdDate { get; set; }
        public decimal NightMtrs { get; set; }
        public decimal DayMtrs { get; set; }
        public decimal TotalMtrs { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        [Required]
        public string EmpName { get; set; }

    }
}
