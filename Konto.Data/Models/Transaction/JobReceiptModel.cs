using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Transaction
{
    [Table("JobReceipt")]
    public class JobReceiptModel : AuditedEntity
    {
        public JobReceiptModel()
        {
            this.IsActive = true;
            this.IsDeleted = false;
            IsClear = false;
        }

        [Display(Name = "Challan Id")]
        public int? ChallanId { get; set; }

        [Display(Name = "Ref Id")]
        public int? RefId { get; set; }

        [Display(Name = "Color Id")]
        public int? ColorId { get; set; }

        [Display(Name = "Product Id")]
        public int? ProductId { get; set; }

        [Display(Name = "Ref Trans Id Id")]
        public int? RefTransId { get; set; }

        [Display(Name = "Voucher Id")]
        public int? VoucherId { get; set; }

        [Display(Name = "Qty")]
        public decimal? Qty { get; set; }

        [Display(Name = "Pcs")]
        public decimal? Pcs { get; set; }

        [Display(Name = "PendingQty")]
        public decimal? PendingQty { get; set; }

        [Display(Name = "PendingPcs")]
        public decimal? PendingPcs { get; set; }

        [Display(Name = "IssueQty")]
        public decimal? IssueQty { get; set; }

        [Display(Name = "IssuePcs")]
        public decimal? IssuePcs { get; set; }

        [Display(Name = "IsClear")]
        public bool IsClear { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [ForeignKey("ChallanId")]
        public virtual ChallanModel Challan { get; set; }
    }
}
