using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class PurchaseRetTrans
    {
        [Required(ErrorMessage = "Purchase Ret ID is required")]
        [Display(Name = "Purchase Ret ID")]

        public long PurchaseRetID { get; set; }

        [Required(ErrorMessage = "Row ID is required")]
        [Display(Name = "Row ID")]
        public int RowID { get; set; }

        [Display(Name = "Item ID")]
        public long? ItemID { get; set; }

        [Display(Name = "Screen ID")]
        public long? ScreenID { get; set; }

        [Display(Name = "Color ID")]
        public long? ColorID { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Item Remark")]
        public string ItemRemark { get; set; }

        [Required(ErrorMessage = "Cut is required")]
        [Display(Name = "Cut")]
        public decimal Cut { get; set; }

        [Required(ErrorMessage = "Pcs is required")]
        [Display(Name = "Pcs")]
        public long Pcs { get; set; }

        [Required(ErrorMessage = "Qty is required")]
        [Display(Name = "Qty")]
        public decimal Qty { get; set; }

        [Required(ErrorMessage = "Rate is required")]
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Unit ID")]
        public long? UnitID { get; set; }

        [Required(ErrorMessage = "Total is required")]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Disc Per is required")]
        [Display(Name = "Disc Per")]
        public decimal DiscPer { get; set; }

        [Required(ErrorMessage = "Disc Amount is required")]
        [Display(Name = "Disc Amount")]
        public decimal DiscAmount { get; set; }

        [Required(ErrorMessage = "Service Tax Per is required")]
        [Display(Name = "Service Tax Per")]
        public decimal ServiceTaxPer { get; set; }

        [Required(ErrorMessage = "Service Tax Amount is required")]
        [Display(Name = "Service Tax Amount")]
        public decimal ServiceTaxAmount { get; set; }

        [Required(ErrorMessage = "Edu Cess Per is required")]
        [Display(Name = "Edu Cess Per")]
        public decimal EduCessPer { get; set; }

        [Required(ErrorMessage = "Edu Cess Amount is required")]
        [Display(Name = "Edu Cess Amount")]
        public decimal EduCessAmount { get; set; }

        [Required(ErrorMessage = "Hedu Cess Per is required")]
        [Display(Name = "Hedu Cess Per")]
        public decimal HeduCessPer { get; set; }

        [Required(ErrorMessage = "Hedu Cess Amount is required")]
        [Display(Name = "Hedu Cess Amount")]
        public decimal HeduCessAmount { get; set; }

        [Required(ErrorMessage = "Vat Per is required")]
        [Display(Name = "Vat Per")]
        public decimal VatPer { get; set; }

        [Required(ErrorMessage = "Vat Amount is required")]
        [Display(Name = "Vat Amount")]
        public decimal VatAmount { get; set; }

        [Required(ErrorMessage = "Ad Vat Per is required")]
        [Display(Name = "Ad Vat Per")]
        public decimal AdVatPer { get; set; }

        [Required(ErrorMessage = "Ad Vat Amount is required")]
        [Display(Name = "Ad Vat Amount")]
        public decimal AdVatAmount { get; set; }

        [Required(ErrorMessage = "CST Per is required")]
        [Display(Name = "CST Per")]
        public decimal CSTPer { get; set; }

        [Required(ErrorMessage = "CST Amount is required")]
        [Display(Name = "CST Amount")]
        public decimal CSTAmount { get; set; }

        [Required(ErrorMessage = "Net Total is required")]
        [Display(Name = "Net Total")]
        public decimal NetTotal { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Purchase Ret Trans ID is required")]
        [Display(Name = "Purchase Ret Trans ID")]
        public long PurchaseRetTransID { get; set; }

        [Display(Name = "Design ID")]
        public long? DesignID { get; set; }

        [Display(Name = "Grade ID")]
        public int? GradeID { get; set; }

        [Display(Name = "Gross Weight")]
        public decimal? GrossWeight { get; set; }

        [Display(Name = "Tare Weight")]
        public decimal? TareWeight { get; set; }

        [Display(Name = "Lengths")]
        public decimal? Lengths { get; set; }

        [Display(Name = "Ends")]
        public decimal? Ends { get; set; }

        [Display(Name = "Cops")]
        public int? Cops { get; set; }

        [MaxLength(35)]
        [StringLength(35)]
        [Display(Name = "Merge No")]
        public string MergeNo { get; set; }

        [Display(Name = "Excise Per")]
        public decimal ExcisePer { get; set; }

        [Display(Name = "Excise Amount")]
        public decimal ExciseAmount { get; set; }

        [Display(Name = "Excise Cess Per")]
        public decimal ExciseCessPer { get; set; }

        [Display(Name = "Excise Cess Amt")]
        public decimal ExciseCessAmt { get; set; }

        [Display(Name = "Excise Hedu Per")]
        public decimal ExciseHeduPer { get; set; }

        [Display(Name = "Excise Hedu Amt")]
        public decimal ExciseHeduAmt { get; set; }

        [Display(Name = "Taxable Amount")]
        public decimal TaxableAmount { get; set; }

        [Required(ErrorMessage = "Trans Code is required")]
        [Display(Name = "Trans Code")]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransCode { get; set; }

        [ForeignKey("PurchaseRetID")]
        public PurchaseRet PurchaseRet { get; set; }

        //[ForeignKey("item_id")]
        //public item Item { get; set; }

        //[ForeignKey("ScreenID")]
        //public screen Screen { get; set; }

        //[ForeignKey("itemcol_id")]
        //public itemcol Itemcol { get; set; }

        //[ForeignKey("DesignID")]
        //public DesignMaster DesignMaster { get; set; }

       // public List<purchaseRet_trans_d> PurchaseRetTransD { get; set; }

    }
}
