using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class ProdWeftItemDTO
    {
        public int Id { get; set; }
        public int? WeftId { get; set; } 
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid RowId { get; set; }
        public int? ProdId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Denier { get; set; }
        public decimal? PI { get; set; }
        public decimal? RS { get; set; }
        public decimal? Qty { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string ProductName { get; set; }
    }
}
