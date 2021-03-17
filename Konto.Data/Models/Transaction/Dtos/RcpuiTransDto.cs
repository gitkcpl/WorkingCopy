using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class RcpuiTransDto
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public decimal? ColorPer { get; set; }
        public decimal? ColorKgs { get; set; }
        public int? RCPUIId { get; set; }
        public string ProductName { get; set; }
        public string Remark { get; set; }
        public string ColorCategory { get; set; }
    }
}