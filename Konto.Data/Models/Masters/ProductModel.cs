using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Konto.Data.Models.Masters
{
    [Table("Product")]
    public class ProductModel : AuditedEntity
    {
        public ProductModel()
        {
            IsActive = true;
            StockReq = "Yes";
            BatchReq = "No";
            SerialReq = "No";
            Sales = true;
            Purchase = true;
            HsnCode = "NA";
            DDate = DateTime.Now.Date;
        }

        public int? DesignDate { get; set; }

        [NotMapped]
        public DateTime? DDate {
            get
            {
                if (DesignDate != null && DesignDate!=10101)
                { return (DateTime.ParseExact(DesignDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
            set { }
        }

        [MaxLength(100)]
        [Display(Name = "Product Name")]
        [Required]
        [MinLength(2)]
        public string ProductName { get; set; }

        [MaxLength(500)]
        [Display(Name = "Product Desc")]
        [Required]
        public string ProductDesc { get; set; }

        [MaxLength(25)]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [MaxLength(25)]
        [Display(Name = "Bar Code")]
        public string BarCode { get; set; }

        [MaxLength(15)]
        [Display(Name = "Hsn Code")]
        [Required]
        public string HsnCode { get; set; }

        [Display(Name = "Tax Id")]
        [Required]
        [Range(1, 99999)]
        public int TaxId { get; set; }

        [Display(Name = "P Type Id")]
        [Required]
        [Range(1, 99999)]
        public int PTypeId { get; set; }

        [Display(Name = "Group Id")]
        public int GroupId { get; set; }

        [Display(Name = "Sub Group Id")]
        public int SubGroupId { get; set; }

        [Display(Name = "Size Id")]
        public int SizeId { get; set; }
   

        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [Display(Name = "Style Id")]
        public int StyleId { get; set; }

        [Display(Name = "Color Id")]
        public int ColorId { get; set; }

        [Display(Name = "Brand Id")]
        public int BrandId { get; set; }

        [Display(Name = "Uom Id")]
        [Required]
        [Range(1, 999)]
        public int UomId { get; set; }

        [Display(Name = "Pur Uom Id")]
        [Required]
        [Range(1, 999)]
        public int PurUomId { get; set; }

        [Required]
        [Display(Name = "Pur Disc")]
        public decimal PurDisc { get; set; }

        [Required]
        [Display(Name = "Pur Sp Disc")]
        public decimal PurSpDisc { get; set; }

        [Required]
        [Display(Name = "Sale Disc")]
        public decimal SaleDisc { get; set; }

        [Required]
        [Display(Name = "Sale Sp Disc")]
        public decimal SaleSpDisc { get; set; }

        [Required]
        [Display(Name = "Actual Cost")]
        public decimal ActualCost { get; set; }

        [MaxLength(5)]
        [Display(Name = "Stock Req")]
        [Required]
        public string StockReq { get; set; }

        [MaxLength(5)]
        [Display(Name = "Batch Req")]
        [Required]
        public string BatchReq { get; set; }

        [MaxLength(5)]
        [Display(Name = "Serial Req")]
        [Required]
        public string SerialReq { get; set; }

        [Required]
        [Display(Name = "Min Level")]
        public decimal MinLevel { get; set; }

        [Required]
        [Display(Name = "Max Level")]
        public decimal MaxLevel { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public decimal Rol { get; set; }

        [Required]
        [Display(Name = "Min Ord Qty")]
        public decimal MinOrdQty { get; set; }

        [Required]
        [Display(Name = "Max Ord Qty")]
        public decimal MaxOrdQty { get; set; }

        [Required]
        [Display(Name = "Lead Days")]
        public int LeadDays { get; set; }

        [Required]
        [Display(Name = "Check Negative")]
        public bool CheckNegative { get; set; }

        [MaxLength(500)]
        [Display(Name = "Ingr")]
        public string Ingr { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Required]
        [Display(Name = "Sale Rate Tax Inc")]
        public bool SaleRateTaxInc { get; set; }

        [Display(Name = "Vendor Id")]
        public int? VendorId { get; set; }

        [Display(Name = "Stock Ac Id")]
        public int? StockAcId { get; set; }

        [Required]
        [Display(Name = "Sales")]
        public bool Sales { get; set; }

        [Required]
        [Display(Name = "Purchase")]
        public bool Purchase { get; set; }

        [Required]
        [Display(Name = "Inventory")]
        public bool Inventory { get; set; }

        [Required]
        [Display(Name = "Fixed Asset")]
        public bool FixedAsset { get; set; }

        [Required]
        [Display(Name = "Work Order")]
        public bool WorkOrder { get; set; }



        [Display(Name = "Std Wt")]
        public decimal StdWt { get; set; }

        [Display(Name = "Price1")]
        public decimal Price1 { get; set; }

        [Display(Name = "Price2")]
        public decimal Price2 { get; set; }

        [MaxLength(1)]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Acc Id")]
        public int? AccId { get; set; }

        [Display(Name = "Party Item Id")]
        public int? PartyItemId { get; set; }

        [Display(Name = "Parent Item Id")]
        public int? ParentItemId { get; set; }

        [Display(Name = "Fabric Top Id")]
        public int? FabricTopId { get; set; }

        [Display(Name = "Fabric Bottom Id")]
        public int? FabricBottomId { get; set; }

        [Display(Name = "Fabric Dupatta Id")]
        public int? FabricDupattaId { get; set; }

        [Display(Name = "Catalog Id")]
        public int? CatalogId { get; set; }

        public decimal Cut { get; set; }

        [ForeignKey("PTypeId")]
        public ProductTypeModel PType { get; set; }

        public virtual ICollection<PriceModel> ProductPrices { get; set; }

    }
}
