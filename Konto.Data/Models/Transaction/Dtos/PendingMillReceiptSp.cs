using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingMillReceiptSp
    {
        public string VoucherNo { get; set; }

        public DateTime VoucherDate { get; set; }

        public string RefNo { get; set; }
        public string LotNo { get; set; }
        public string ChallanNo { get; set; }

        public string GreyQuality { get; set; }
        public string FinishQuality { get; set; }
        public string ColorName { get; set; }
        public int? ProductId { get; set; }

        public int? DesignId { get; set; }

        public int? ColorId { get; set; }

        public decimal IssueQty { get; set; }

        public int IssuePcs { get; set; }

        public decimal PendingQty { get; set; }

        public int PendingPcs { get; set; }

        public decimal ReceiptQty { get; set; }

        public decimal ReceiptPcs { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Total { get; set; }

        public decimal Disc { get; set; }

        public decimal DiscAmt { get; set; }

        public decimal FreightRate { get; set; }

        public decimal Freight { get; set; }

        public decimal Cgst { get; set; }

        public decimal CgstAmt { get; set; }

        public decimal IgstAmt { get; set; }

        public decimal Igst { get; set; }

        public decimal Cess { get; set; }

        public decimal CessAmt { get; set; }

        public decimal? Sgst { get; set; }

        public decimal? SgstAmt { get; set; }

        public int? UomId { get; set; }

        public int? TransId { get; set; }

        public int Id { get; set; }

        public int? VoucherId { get; set; }

       
    }
}
