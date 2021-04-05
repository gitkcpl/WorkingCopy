using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class EmpRateDto
    {
        public int Id { get; set; }

        public int EmpId { get; set; }
        public int ProductId { get; set; }

        [Required]
        [Range(1, 99999)]
        public decimal Rate { get; set; }

        [Required]
        public string ProductName { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

    }
}
