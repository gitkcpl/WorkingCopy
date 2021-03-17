using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Transaction
{
    [Table("RCPUITrans")]
    public class RCPUITransModel : AuditedEntity
    {
        public RCPUITransModel()
        {
            IsActive = true;
            IsDeleted = false;
        }
        public int ProductId { get; set; }
        public decimal? ColorPer { get; set; }
        public decimal? ColorKgs { get; set; }
        public int RCPUIId { get; set; }
        public string Remark { get; set; }
        public string ColorCategory { get; set; }
    }
}