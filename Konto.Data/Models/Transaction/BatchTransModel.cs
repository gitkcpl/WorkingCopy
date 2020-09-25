using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Transaction
{
    [Table("BatchTrans")]
    public class BatchTransModel : AuditedEntity
    {
        [Display(Name = "Batch Id")]
        public int BatchId { get; set; }

        [Display(Name = "Item Id")]
        public int? ItemId { get; set; }

        [Display(Name = "Per")]
        [Range(0, 100)]
        public decimal? Per { get; set; }

        [MaxLength(50)]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Ply")]
        public int? Ply { get; set; }

        [Display(Name = "RefId")]
        public int? RefId { get; set; }

        [Display(Name = "RefTransId")]
        public int? RefTransId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchModel Batch { get; set; }
    }
}
