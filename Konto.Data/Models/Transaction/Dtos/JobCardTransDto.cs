namespace Konto.Data.Models.Transaction.Dtos
{
    public class JobCardTransDto
    {
        public int? Id { get; set; }
        public int? JobCardId { get; set; }
        public int? ColorId { get; set; }
        public string Description { get; set; }
        public decimal? ColorPer { get; set; }
        public decimal Meter { get; set; }
        public decimal GMeter { get; set; }
        public decimal? ConsumeQty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string ColorCategory { get; set; }
        public int? ItemId { get; set; }
        public decimal? Per { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public int? Ply { get; set; }
        public int? RefId { get; set; }
        public int? RefTransId { get; set; }
        public int? DesignId { get; set; }
        public string LotNo { get; set; }
        public string Remark { get; set; }
        public string DesignName { get; set; } 
    }
}