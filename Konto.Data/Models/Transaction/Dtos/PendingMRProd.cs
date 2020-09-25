using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingMRProd
    {
        public int Id { get; set; }

        public int? SrNo { get; set; }

        public int? ProductId { get; set; }

        public int ProdId { get; set; }

        public string VoucherNo { get; set; }

        public int? ColorId { get; set; }

        public int? CompId { get; set; }

        public int? GradeId { get; set; }

        public int? YearId { get; set; }

        public decimal? GrayMtr { get; set; }

        public decimal FinMrt { get; set; }

        public decimal? ShMtr { get; set; }

        public decimal TP1 { get; set; }

        public decimal TP2 { get; set; }

        public decimal TP3 { get; set; }

        public decimal TP4 { get; set; }

        public decimal TP5 { get; set; }

        public decimal Qty { get; set; }
    }
}
