using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "item id is required")]
        public long item_id { get; set; }

        [MaxLength(25)]
        public string item_code { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "item name is required")]
        public string item_name { get; set; }

        [MaxLength(100)]
        public string print_name { get; set; }

        public long? unit_id { get; set; }

        public decimal? cut { get; set; }

        public decimal? dealer_price { get; set; }

        public string HsnCode { get; set; }
        public byte? dp_vat_inc { get; set; }

        public decimal? dp_disc_per { get; set; }

        public decimal? cost_price { get; set; }

        public decimal? lst_per { get; set; }

        public decimal? octroi_per { get; set; }

        public decimal? cst_per { get; set; }

        public decimal? freight_per { get; set; }

        public decimal? pvat_per { get; set; }

        public decimal? pad_vat_per { get; set; }

        public decimal? other_per { get; set; }

        public decimal? sale_rate { get; set; }

        public byte? sale_vat_inc { get; set; }

        public decimal? mrp { get; set; }

        public decimal? dp_per { get; set; }

        public decimal? cost_per { get; set; }

        public decimal? profit_per { get; set; }

        public decimal? gp_per { get; set; }

        public decimal? service_tax { get; set; }

        public decimal? edu_cess { get; set; }

        public decimal? hedu_cess { get; set; }

        public decimal? vat_per { get; set; }

        public decimal? ad_vat_per { get; set; }

        public long? itemgroup_id { get; set; }

        public long? subgroup_id { get; set; }

        public long? category_id { get; set; }

        public long? color_id { get; set; }

        public long? brand_id { get; set; }

        public long? size_id { get; set; }

        public decimal? rol { get; set; }

        public decimal? max_lvl { get; set; }

        public decimal? min_lvl { get; set; }

        public int? pcs_per_pack { get; set; }

        public int? prio { get; set; }

        [MaxLength(200)]
        public string remark { get; set; }

        public int? PVatClassID { get; set; }

        public int? VatClassID { get; set; }

        [Required(ErrorMessage = "Filter Type is required")]
        public byte FilterType { get; set; }

        [Required(ErrorMessage = "Check Negative is required")]
        public bool CheckNegative { get; set; }

        public long? ItemTypeID { get; set; }

        public int? RollPerKg { get; set; }

        public decimal? WorkerLaourCost { get; set; }

        public decimal? WarperLabourCost { get; set; }

        public decimal? DrawerLabourCost { get; set; }

        public decimal? FolderLabourCost { get; set; }

        public decimal? MendorLabourCost { get; set; }

        public decimal? MasterLabourCost { get; set; }

        public decimal? StandardWeight { get; set; }

        public decimal? LicenseMtrs { get; set; }

        public decimal? Ends { get; set; }

        public decimal? BeamWeight { get; set; }

        public decimal? BhairanMtrs { get; set; }

        public bool? stockRequired { get; set; }

        public int? PackTypeID { get; set; }

        public long? AccountID { get; set; }

        public decimal? Sale_Disc_Per { get; set; }

        public int? DeptID { get; set; }

        public decimal? WholeSaleRate { get; set; }

        public decimal? SemiWholeSaleRate { get; set; }

        public decimal? MUProfit_Per { get; set; }

        [MaxLength]
        public byte[] ItemImage { get; set; }

        [MaxLength(1)]
        public string StockReq { get; set; }

        [MaxLength(25)]
        public string Deniar { get; set; }

        public short? comodityid { get; set; }

        [MaxLength(50)]
        public string StyleNo { get; set; }

        public virtual VatClass VatClass1 { get; set; }
    }
}
