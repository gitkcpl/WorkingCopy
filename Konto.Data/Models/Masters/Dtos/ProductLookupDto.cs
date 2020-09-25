using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class ProductLookupDto 
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string BarCode { get; set; }
        public string HsnCode { get; set; }
        public string TaxName { get; set; }
        public string ProductType { get; set; }
        public string GroupName { get; set; }
        public string SubName { get; set; }
        public string CatName { get; set; }
        public string Vendor { get; set; }
        public string UnitName { get; set; }
        public decimal SaleRate { get; set; }
        public decimal DealerPrice { get; set; }
        public decimal Sgst { get; set; }
        public decimal Cgst { get; set; }
        public decimal Igst { get; set; }
        public decimal Cess { get; set; }
        public int UomId { get; set; }
        public int PurUomId { get; set; }
        public int PTypeId { get; set; }
        public bool CheckNegative { get; set; }
        public int StockPcs { get; set; }
        public decimal? StockQty { get; set; }
        public decimal? BalQty { get; set; }
        public int OpPcs { get; set; }
        public decimal OpQty { get; set; }
        public string SerialReq { get; set; }
        public decimal Cut { get; set; }
        public int TaxId { get; set; }
    }
}
