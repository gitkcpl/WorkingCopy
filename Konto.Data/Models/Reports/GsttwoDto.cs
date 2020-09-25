using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class GsttwoDto
    {
     
        public string NoteType { get; set; }
        public string PDescription { get; set; }
        public string UQC { get; set; }
        public string PortCode { get; set; }
        public string Type { get; set; }
        public string GstIn { get; set; }
        public string Account { get; set; }
        public string VNo { get; set; }
        public string BillNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime BillDate { get; set; }
        public string VoucherName { get; set; }
        public string BookName { get; set; }
        public string HsnCode { get; set; }
        public string StateName { get; set; }
        public string BillType { get; set; }
        public string Reason { get; set; }
        public string BondedWH { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal SGSTPer { get; set; }
        public decimal IGSTPer { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal BillAmount { get; set; }
        public decimal UrdTaxable { get; set; }
        public decimal CmpTaxable { get; set; }
        public decimal UrdIgst { get; set; }
        public decimal CmpIgst { get; set; }
        public decimal Cess { get; set; }
        public decimal GSTRate { get; set; }
        public string RevChg { get; set; }
        public string Itc { get; set; }
    }
}
