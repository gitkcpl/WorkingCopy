using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Gstn
{
    public class RecoGstr2BtoBDto
    {
        public int Id { get; set; }
        public int AccId { get; set; }
        public string GstIn { get; set; }
        public string AccName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceValue { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public decimal Cess { get; set; }
        public decimal Taxable { get; set; }


        public string PInvoiceNo { get; set; }
        public DateTime PInvoiceDate { get; set; }
        public decimal PInvoiceValue { get; set; }
        public decimal PCgst { get; set; }
        public decimal PSgst { get; set; }
        public decimal PIgst { get; set; }
        public decimal PCess { get; set; }
        public decimal PTaxable { get; set; }

        public decimal DInvoiceValue { get; set; }
        public decimal DCgst { get; set; }
        public decimal DSgst { get; set; }
        public decimal DIgst { get; set; }
        public decimal DCess { get; set; }
        public decimal DTaxable { get; set; }

        public int VTypeId { get; set; }
        public string VoucherType { get; set; }

    }

    public class RecoGstr2aUnMatch
    {
        public int Id { get; set; }
        public int AccId { get; set; }
        public string GstIn { get; set; }
        public string AccName { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceValue { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public decimal Cess { get; set; }
        public decimal Taxable { get; set; }
        public int VTypeId { get; set; }
        public string VoucherType { get; set; }
    }

}
