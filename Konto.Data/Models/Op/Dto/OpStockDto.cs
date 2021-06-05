using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Op.Dto
{
    public class OpStockDto
    {
        public int Id { get; set; }

        public int BalId { get; set; }

        public  string Barcode { get; set; }
        public string ProductName { get; set; }

        public int? UomId { get; set; }

        public int? GroupId { get; set; }

        public int? PTypeId { get; set; }

        public decimal? OpQty { get; set; }

        public int? OpNos { get; set; }

        public decimal? StockValue { get; set; }

        public string UnitName { get; set; }

        public string GroupName { get; set; }

        public string TypeName { get; set; }
        public string SizeName { get; set; }
    }
}
