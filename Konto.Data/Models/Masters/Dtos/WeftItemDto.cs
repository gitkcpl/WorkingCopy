using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class WeftItemDto
    {
        [Required]
        public string ProductName { get; set; }
        public int? VoucherDate { get; set; }

        public string AccName { get; set; }
        public string ColorName { get; set; }
        public string MainColor { get; set; }
        public string ItemName { get; set; }
        public decimal? LengthInMtr { get; set; }
        public decimal? LengthInCm { get; set; }
        public decimal? QWeight { get; set; }
        public decimal? Wastage { get; set; }
        public decimal? Wastageper { get; set; }
        public decimal? AvPick { get; set; }
        public decimal? YCost { get; set; }
        public decimal? JobCharges { get; set; }
        public decimal? CostWWaste { get; set; }
        public decimal? CostNWaste { get; set; }
        public decimal? OneMtrCost { get; set; }
        public decimal? Denier { get; set; }
     
        public decimal? Change { get; set; }
        public decimal? Qty { get; set; }
        public decimal? PI { get; set; }
        public decimal? RS { get; set; }
        public decimal? Ends { get; set; }
        public decimal? Mtr { get; set; }
        public decimal Totcard { get; set; }
        public decimal TotPick { get; set; }
        public decimal Weight { get; set; }
        public decimal Total { get; set; }
        public int Tar { get; set; }
        public decimal Costing { get; set; }
        public decimal JobCharge { get; set; }
        public decimal NWeight { get; set; }
        public decimal Wasteper { get; set; }
        public string Remark { get; set; }
        public string IType { get; set; }
        public decimal Rate { get; set; }
        public decimal Card { get; set; }
        public int Panno { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
      

        public int Id { get; set; }

        [Required]
        public int? ProductId { get; set; }
        public int TypeId { get; set; } //1-Weft,2-Warp
        public int? ColorId { get; set; }
        public int? MColorId { get; set; }
        public int? RefId { get; set; }
        public int? AccId { get; set; }
        public int? ItemId { get; set; }

    }
}