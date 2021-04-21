
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Keysoft.Erp.Data.Models
{
    public class PcTransD
    {
        [Required(ErrorMessage = "Pc ID is required")]
        [Display(Name = "Pc ID")]
        public long PcID { get; set; }

        [Required(ErrorMessage = "Pc Trans ID is required")]
        [Display(Name = "Pc Trans ID")]
        public long PcTransID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Pc Taka ID is required")]
        [Display(Name = "Pc Taka ID")]
        public long PcTakaID { get; set; }

        [Display(Name = "Meters")]
        public long? Meters { get; set; }

        [Display(Name = "Color ID")]
        public long? ColorID { get; set; }

        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [MaxLength(35)]
        [StringLength(35)]
        [Display(Name = "Box No")]
        public string BoxNo { get; set; }

        [Display(Name = "Spool")]
        public int? Spool { get; set; }

        [Display(Name = "Weight")]
        public decimal? Weight { get; set; }

        [Display(Name = "Top")]
        public int? Top { get; set; }

        [Display(Name = "Ply")]
        public int? Ply { get; set; }

        [Display(Name = "Bottom")]
        public int? Bottom { get; set; }

        [Display(Name = "Gross Weight")]
        public decimal? GrossWeight { get; set; }

        [Display(Name = "Tare Weight")]
        public decimal? TareWeight { get; set; }

        [ForeignKey("PcID")]
        public Pc Pc { get; set; }

        [ForeignKey("PcTransID")]
        public PcTrans PcTrans { get; set; }

    }
}
