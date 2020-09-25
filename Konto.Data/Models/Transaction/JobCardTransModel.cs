using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("JobCardTrans")]
    public class JobCardTransModel :AuditedEntity
    {
        public JobCardTransModel()
        { 
            this.IsDeleted = false;
            IsActive = true;
        }

        [Display(Name = "Job Card Id")]
        public int? JobCardId { get; set; }
        [MaxLength(50)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Color Per")]
        public decimal? ColorPer { get; set; }
        [Display(Name = "Meter")]
        public decimal Meter { get; set; }
        [Display(Name = "GMeter")]
        public decimal GMeter { get; set; }
        [Display(Name = "Consume Qty")]
        public decimal? ConsumeQty { get; set; }
        [Display(Name = "Rate")]
        public decimal? Rate { get; set; }
        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }
        
        [Display(Name = "Color Id")]
        public int? ColorId  { get; set; }
        [Display(Name = "Item Id")]
        public int? ItemId { get; set; }
        [Display(Name = "Per")]
        public decimal? Per { get; set; }
       
        [Display(Name = "Ply")]
        public int? Ply { get; set; }
        [Display(Name = "RefId")]
        public int? RefId { get; set; }
        [Display(Name = "DesignId")]
        public int? DesignId { get; set; }
        [MaxLength(50)]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }
        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }
       
    }
}
