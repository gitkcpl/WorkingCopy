using System;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class JobCardDto
    {

        public int Id { get; set; }
        public int? TransId { get; set; }

        public string VoucherNo { get; set; }
        public string RefNo { get; set; }
        public DateTime VouchDate { get; set; }
        public string OrderNo { get; set; }
        public string BeamNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string Party { get; set; }
        public int? PartyId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ColorId { get; set; }
        public string ColorName { get; set; }
        public int? DesignId { get; set; }
        public string Design { get; set; }
        public string IssueBy { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? PendingQty { get; set; }
        public string Remark { get; set; }
    }
}