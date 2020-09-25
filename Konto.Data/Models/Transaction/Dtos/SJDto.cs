using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class SJDto
    {
        public int Id { get; set; }
        public int? ChallanId { get; set; }

        [Required(ErrorMessage = "Quality Name Required")]
        public string ProductName { get; set; }
          
        [Range(1, 9999999, ErrorMessage = "Product Name is Required")]
        public int ProductId { get; set; }

        public decimal ReceiveQty { get; set; }
        public decimal IssueQty { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Rate { get; set; }
        [Required]
        [Range(1, 9999999, ErrorMessage = "Invalid Unit Name")]
        public int UomId { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Gross { get; set; }

        [Range(0.0000, 999999999)]
        public decimal Total { get; set; }

        [MaxLength]
        public string Remark { get; set; }

    }
}
