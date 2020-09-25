using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Transaction
{
    [Table("LoadingTrans")]
    public class LoadingTranModel : AuditedEntity
    { 
        [Required(ErrorMessage = "Prod Id is required")]
        [Display(Name = "Prod Id")]
        public int ProdId { get; set; }
        [Display(Name = "Loading Date")]
        public DateTime? LoadingDate { get; set; }
        [Display(Name = "Div Id")]
        public int? DivId { get; set; }
        [Display(Name = "Mac Id")]
        public int? MacId { get; set; }
        [MaxLength(50)]
        [Display(Name = "Beam Potion")]
        public string BeamPotion { get; set; }
        [Display(Name = "Product Id")]
        public int? ProductId { get; set; }
        [MaxLength(50)]
        [Display(Name = "ProdStatus")]
        public string ProdStatus { get; set; }
        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }
        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }
    }
}
