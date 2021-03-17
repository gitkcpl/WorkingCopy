using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class CuttingTransDto
    {
        public int Id { get; set; }
        public int? ChallanId { get; set; }

        public string ChallanNo{ get; set; }
        public string TakaVNo { get; set; }
        public string RefNo { get; set; }
        public string LotNo { get; set; }

        [Required(ErrorMessage = "Quality Name Required")]
        public string ProductName { get; set; }

        [Range(1, 9999999, ErrorMessage = "Product Name is Required")]
        public int ProductId { get; set; }

         public string ColorName { get; set; }
        public int ColorId { get; set; }
         
        public string DesignNo{ get; set; } 
        public int DesignId { get; set; }

        public int UnitId { get; set; }

        public int? RefId { get; set; }
        public int? RefVoucherId { get; set; }
        public int? MiscId { get; set; }
        public int? BatchId { get; set; }

         public int Pcs { get; set; }
        public decimal IssueQty { get; set; }

        [Required]
        //[Range(1, 9999999, ErrorMessage = "Qty is Required")]
        public decimal Qty { get; set; }
        public decimal Cops { get; set; }
        public decimal Rate { get; set; }
        public decimal? ShQty { get; set; }
        public decimal? ShPer { get; set; }
        public int GradeId { get; set; }
       
        public string Remark { get; set; }
    }
}
