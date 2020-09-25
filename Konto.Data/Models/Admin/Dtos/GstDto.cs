using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin.Dtos
{
    public class GstDto 
    {

        public int Id { get; set; }
        public string NoteType { get; set; }
        public string PortCode { get; set; }
        public string Type { get; set; }
        public string GstIn { get; set; }
        public string Account { get; set; }
        public string VoucherNo { get; set; }
        public string OrgBillNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime VoucherDate { get; set; }
        public DateTime? OrgBillDate { get; set; }
        public string VoucherName { get; set; }
        public string BookName { get; set; }
        public string HsnCode { get; set; }
        public string StateName { get; set; }
        public string OrgStateName { get; set; }
        public string BillType { get; set; }
        public string Reason { get; set; }
        public string BondedWH { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal CGSTPer { get; set; }
        public decimal SGSTPer { get; set; }
        public decimal IGSTPer { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Cess { get; set; }
        public decimal GSTRate { get; set; }
        public int VTypeId { get; set; }
        public int IsRevice { get; set; }

    }
}
