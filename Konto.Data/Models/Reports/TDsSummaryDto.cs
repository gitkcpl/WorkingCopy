using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class TDsSummaryDto
    {
        public string Party { get; set; }
        public int AccountID { get; set; }
        public decimal? BillAmount { get; set; }
        public decimal? TDSAmount { get; set; }
        public decimal? PayRec { get; set; }
        public string TDSAccount { get; set; }
        public string TDSAC { get; set; }
        public string PanNo { get; set; }
        public decimal AcsValue { get; set; }
        public List<TDSDto> TdsList { get; set; }
    }

    public class TDSDto
    {
        public string BillNo { get; set; }
        public int AccountID { get; set; }
        public int BillID { get; set; }
        public DateTime ChallanDate { get; set; }
        public int? HasteId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PanNo { get; set; }
        public decimal? TdsPer { get; set; }
        public decimal? TdsAmt { get; set; }
        public decimal? Payable { get; set; }
        public string TDSAccount { get; set; }
        public string Party { get; set; }
        public string GroupName { get; set; }
        public string VoucherName { get; set; }
        public string Descr { get; set; }
        public decimal AcsValue { get; set; }
    }
}
