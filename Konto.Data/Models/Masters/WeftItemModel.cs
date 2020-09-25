using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("WeftItem")]
    [Serializable]
    public class WeftItemModel : AuditedEntity
    {
        public WeftItemModel()
        {
            this.IsActive = true;
            
            VDate = DateTime.Now;
        }
        [Required]
        [Display(Name = "Product Id")]
        public int? ProductId { get; set; }

        [Required]
        [Display(Name = "Type Id")]
        public int TypeId { get; set; } //1-Weft,2-Warp

        [Display(Name = "Denier")]
        public decimal? Denier { get; set; }

        [Display(Name = "Voucher Date")]
        public int? VoucherDate { get; set; }

        [Display(Name = "V Date")]
        public DateTime VDate { get; set; }

        //[Display(Name = "Peek")]
        //public decimal? Peek { get; set; }

        [Display(Name = "Change")]
        public decimal? Change { get; set; }

        [Display(Name = "Qty")]
        public decimal? Qty { get; set; }

        [Display(Name = "ColorId")]
        public int? ColorId { get; set; }

        [Display(Name = "MColorId")]
        public int? MColorId { get; set; }

        [Display(Name = "PI")]
        public decimal? PI { get; set; }

        [Display(Name = "RS")]
        public decimal? RS { get; set; }

        [Display(Name = "Ends")]
        public decimal? Ends { get; set; }

        [Display(Name = "Mtr")]
        public decimal? Mtr { get; set; }

        [Display(Name = "Totcard")]
        public decimal Totcard { get; set; }

        [Display(Name = "TotPick")]
        public decimal TotPick { get; set; }

        [Display(Name = "Weight")]
        public decimal Weight { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Tar")]
        public int Tar { get; set; }

        [Display(Name = "Costing")]
        public decimal Costing { get; set; }

        [Display(Name = "JobCharge")]
        public decimal JobCharge { get; set; }

        [Display(Name = "NWeight")]
        public decimal NWeight { get; set; }

        [Display(Name = "Wasteper")]
        public decimal Wasteper { get; set; }

        [Display(Name = "RefId")]
        public int? RefId { get; set; }

        [Display(Name = "AccId")]
        public int? AccId { get; set; }

        [Display(Name = "ItemId")]
        public int? ItemId { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "IType")]
        public string IType { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Card")]
        public decimal Card { get; set; }

        [Display(Name = "Panno")]
        public int Panno { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public string ProductName { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public string AccName { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public string ColorName { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public string MainColor { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public string ItemName { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? LengthInMtr { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? LengthInCm { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? QWeight { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? Wastage { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? Wastageper { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? AvPick { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? YCost { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? JobCharges { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? CostWWaste { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? CostNWaste { get; set; }

        [NotMapped]
        [MaxLength(150)]
        public decimal? OneMtrCost { get; set; }

        [ForeignKey("RefId")]
        public virtual ProductModel product { get; set; }

    }
}
