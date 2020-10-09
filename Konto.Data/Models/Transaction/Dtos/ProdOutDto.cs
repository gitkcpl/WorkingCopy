using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class ProdOutDto : AuditedEntity
    {
        [Display(Name = "Prod Id")]
        public virtual int? ProdId { get; set; }

        [Display(Name = "Trans Id")]
        public virtual int? TransId { get; set; }

        [Display(Name = "Sr No")]
        public virtual int? SrNo { get; set; }

        [Display(Name = "Product Id")]
        public virtual int? ProductId { get; set; }

        [Display(Name = "Grade Id")]
        public virtual int? GradeId { get; set; }

        [Display(Name = "Ref Id")]
        public virtual int? RefId { get; set; }

        [Display(Name = "Color Id")]
        public virtual int? ColorId { get; set; }

        [Display(Name = "Comp Id")]
        public virtual int? CompId { get; set; }

        [Display(Name = "Year Id")]
        public virtual int? YearId { get; set; }

        [Display(Name = "Voucher Id")]
        public virtual int? VoucherId { get; set; }

        [MaxLength(25)]
        [Display(Name = "Voucher No")]
        public virtual string VoucherNo { get; set; }

        [Display(Name = "Gray Mtr")]
        public virtual decimal? GrayMtr { get; set; }

        [Display(Name = "Gray Pcs")]
        public virtual decimal? GrayPcs { get; set; }

        [Display(Name = "Fin Mrt")]
        public virtual decimal? FinMrt { get; set; }

        [Display(Name = "TP1")]
        public virtual decimal? TP1 { get; set; }

        [Display(Name = "TP2")]
        public virtual decimal? TP2 { get; set; }

        [Display(Name = "TP3")]
        public virtual decimal? TP3 { get; set; }

        [Display(Name = "TP4")]
        public virtual decimal? TP4 { get; set; }

        [Display(Name = "TP5")]
        public virtual decimal? TP5 { get; set; }

        [Display(Name = "Sh Mtr")]
        public virtual decimal? ShMtr { get; set; }

        [Display(Name = "Sh Per")]
        public decimal? ShPer { get; set; }

        [Display(Name = "Qty")]
        public virtual decimal? Qty { get; set; }

        public virtual bool? IsOk { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        public virtual int VTypeId { get; set; }

        [Display(Name = "TakaStatus")]
        public virtual string TakaStatus { get; set; }
        public virtual string LotNo { get; set; }
         public virtual decimal PlainQty { get; set; }
    }
}
