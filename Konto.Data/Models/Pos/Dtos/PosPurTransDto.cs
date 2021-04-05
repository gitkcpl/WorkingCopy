using Konto.Data.Models.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Pos.Dtos
{
    public class PosPurTransDto : BillTransDto
    {
        public string ItemCode { get; set; }
        public string   StyleNo { get; set; }
        public string Description { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }
        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }

        public int SizeId { get; set; }

        public string Size { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public int PurUomId { get; set; }

        public decimal SaleDisc { get; set; }
       
        public bool SaleRateTaxInc { get; set; }
        public int TaxId { get; set; }

        public decimal ProfitPer { get; set; }

        
        public decimal BulkRate { get; set; }
        public decimal SemiBulkRate { get; set; }
        public decimal BulkQty { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
