using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class ProductDetailsDto : ProductLookupDto
    {

        public int GroupId { get; set; }
        public int SubGrupId { get; set; }

        public int CategroyId { get; set; }
        public int BrandId { get; set; }

        public int ColorId { get; set; }
        public int SizeId { get; set; }

        public string ColorName { get; set; }
        public string BrandName { get; set; }

        public decimal Mrp { get; set; }
        public decimal Rate1 { get; set; }
        public decimal Rate2 { get; set; }
        public decimal Qty { get; set; }

        public string StyleNo { get; set; }

        public string   Description { get; set; }
        public string StockReq { get; set; }
        public string BatchReq { get; set; }
       
        public decimal ProfitPer { get; set; }
        
    }
}
