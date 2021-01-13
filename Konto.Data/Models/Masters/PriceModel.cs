using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("ProductPrice")]
    public class PriceModel : AuditedEntity
    {
        public PriceModel()
        {
            IsActive = true;
        }
        [Display(Name = "Product Id")]
        public int? ProductId { get; set; }

        [MaxLength(25)]
        [Display(Name = "Batch No")]
        public string BatchNo { get; set; }

        [MaxLength(10)]
        [Display(Name = "Mfg Date")]
        public string MfgDate { get; set; }

        [MaxLength(10)]
        [Display(Name = "Exp Date")]
        public string ExpDate { get; set; }

        [Display(Name = "Dealer Price")]
        public decimal DealerPrice { get; set; }

        [Display(Name = "Sale Rate")]
        public decimal SaleRate { get; set; }

        [Display(Name = "Qty")]
        public decimal Qty { get; set; }

        [Display(Name = "Mrp")]
        public decimal Mrp { get; set; }

        [Display(Name = "Branch Id")]
        public int? BranchId { get; set; }

        [Display(Name = "Issue Qty")]
        public decimal IssueQty { get; set; }

        public decimal Rate1 { get; set; }

        public decimal Rate2 { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }

    }
}
