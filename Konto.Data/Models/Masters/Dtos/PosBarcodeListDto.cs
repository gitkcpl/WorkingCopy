using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class PosBarcodeListDto
    {
        public int  Id { get; set; }
        public string Barcode { get; set; }
        public string ItemCode { get; set; }
        public string ProductName { get; set; }
        public int Pcs { get; set; }
        public decimal SaleRate { get; set; }
        public decimal Mrp { get; set; }
        public string GroupName { get; set; }
        public string SubGroup { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Vendor { get; set; }
    }
}
