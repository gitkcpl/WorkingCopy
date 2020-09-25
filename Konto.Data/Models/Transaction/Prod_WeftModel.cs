using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("Prod_Weft")]
    public class Prod_WeftModel : AuditedEntity
    {
        public Prod_WeftModel()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        } 
        [Display(Name = "Prod Id")]
        public int? ProdId { get; set; }

        [Required]
        [Display(Name = "Product Id")]
        public int? ProductId { get; set; }

        [Display(Name = "Denier")]
        public decimal? Denier { get; set; }

        [Display(Name = "PI")]
        public decimal? PI { get; set; }

        [Display(Name = "Qty")]
        public decimal? Qty { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public string ProductName { get; set; }

    }
}