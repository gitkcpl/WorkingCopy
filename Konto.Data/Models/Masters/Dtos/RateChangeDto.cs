using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class RateChangeDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string BarCode { get; set; }
        public string HsnCode { get; set; }
        public int TaxType { get; set; }
        public int ProductType { get; set; }
        public  int Group { get; set; }
        public int SubGroup { get; set; }
        public int Brand { get; set; }
        public int Category { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public string StyleNo { get; set; }
        public decimal DealerPrice { get; set; }
        public decimal SaleRate { get; set; }
        public decimal Mrp { get; set; }
        public decimal BulkRate { get; set; }
        public decimal SemiBulkRate { get; set; }
        public decimal ProfitPer { get; set; }
        public decimal PackQty { get; set; }
        public decimal PurDisc { get; set; }
        public decimal SaleDisc { get; set; }
        public decimal RatePerQty { get; set; }
        public decimal MinLevel { get; set; }
        public decimal MaxLevel { get; set; }
        public decimal Rol { get; set; }
        public decimal MinOrdQty { get; set; }
        public decimal MaxOrdQty { get; set; }
        public bool SaleRateTaxInc { get; set; }
        public string BatchReq { get; set; }
        public string SerialReq { get; set; }
        public bool ChkNegative { get; set; }

    }
}
