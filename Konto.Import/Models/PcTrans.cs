using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class PcTrans : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        [Required(ErrorMessage = "Pc ID is required")]
        [Display(Name = "Pc ID")]
        public long PcID { get; set; }

        [Display(Name = "Item ID")]
        public long? ItemID { get; set; }

        [Display(Name = "Screen ID")]
        public long? ScreenID { get; set; }

        [Display(Name = "Color ID")]
        public long? ColorID { get; set; }

        [Display(Name = "Item Remark")]
        public string ItemRemark { get; set; }

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

        [Display(Name = "Po ID")]
        public long? PoID { get; set; }

        [Display(Name = "Po Row ID")]
        public long? PoRowID { get; set; }

        [Required(ErrorMessage = "Cut is required")]
        [Display(Name = "Cut")]
        public decimal Cut { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Pc Trans ID is required")]
        [Display(Name = "Pc Trans ID")]
        public long PcTransID { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Merge No")]
        public string MergeNo { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Item Base")]
        public string ItemBase { get; set; }

        [Display(Name = "Cops")]
        public int? Cops { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Po Vucher No")]
        public string PoVucherNo { get; set; }

        [Display(Name = "Issue Pcs")]
        public int? IssuePcs { get; set; }

        [Display(Name = "Issue Qty")]
        public decimal? IssueQty { get; set; }

        [Display(Name = "Sh Per")]
        public decimal? ShPer { get; set; }

        [Display(Name = "Sh Qty")]
        public decimal? ShQty { get; set; }

        [Display(Name = "Pack Type ID")]
        public int? PackTypeID { get; set; }

        [Display(Name = "Grade ID")]
        public int? GradeID { get; set; }

        [Display(Name = "Lengths")]
        public decimal? Lengths { get; set; }

        [Display(Name = "Gross Weight")]
        public decimal? GrossWeight { get; set; }

        [Display(Name = "Tare Weight")]
        public decimal? TareWeight { get; set; }

        [Display(Name = "Ends")]
        public int? Ends { get; set; }

        [Display(Name = "Short Amt")]
        public decimal? ShortAmt { get; set; }

        [Display(Name = "Sec Qty1")]
        public decimal? SecQty1 { get; set; }

        [Display(Name = "Sec Qty2")]
        public decimal? SecQty2 { get; set; }

        [Display(Name = "Lace Qty")]
        public decimal? LaceQty { get; set; }

        [Display(Name = "Lost Qty")]
        public decimal? LostQty { get; set; }

        [Display(Name = "Fresh Qty")]
        public decimal? FreshQty { get; set; }

        [Display(Name = "Item Cat ID")]
        public long? ItemCatID { get; set; }

        [Required(ErrorMessage = "Cess Rate is required")]
        [Display(Name = "Cess Rate")]
        public decimal CessRate { get; set; }

        [Required(ErrorMessage = "Cess Amount is required")]
        [Display(Name = "Cess Amount")]
        public decimal CessAmount { get; set; }

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

        [Display(Name = "Ref ID")]
        public long? RefID { get; set; }

        [Display(Name = "Ref Voucher ID")]
        public long? RefVoucherID { get; set; }

        public Guid TransCode { get; set; }
        public Nullable<int> TopPallet { get; set; }
        public Nullable<int> WoodPallet { get; set; }
        public Nullable<int> PvcPallet { get; set; }
        public Nullable<int> BottomPallet { get; set; }
        public Nullable<long> PurchaseId { get; set; }

        [ForeignKey("PcID")]
        public Pc Pc { get; set; }
    }
}