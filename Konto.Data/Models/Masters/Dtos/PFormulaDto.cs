using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class PFormulaDto 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        
        public int DescType { get; set; }

        public string DiscTypeName { get; set; }
        public string Category { get; set; }

      
        public decimal Qty { get; set; }

        
        public decimal Cut { get; set; }

        public decimal ReqQty { get; set; }

        public int ColorId { get; set; }

       
        public string ColorName { get; set; }

       
        public decimal Rate { get; set; }

        public decimal Total { get; set; }

       
        public string Remark { get; set; }

        [Required]
        public int? RefProductId { get; set; }

        [Required]
       public int? UomId { get; set; }
    }

}
