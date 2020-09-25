using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("Batch")]
    public class BatchModel : AuditedEntity
    {
        [Display(Name = "Comp Id")]
        public int? CompId { get; set; }

        [Display(Name = "Div Id")]
        public int? DivId { get; set; }

        [Display(Name = "Voucher Id")]
        public int? VoucherId { get; set; }

        [Display(Name = "Year Id")]
        public int? YearId { get; set; }

        [Display(Name = "Branch Id")]
        public int? BranchId { get; set; }

        [MaxLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Display(Name = "Voucher Date")]
        public int VoucherDate { get; set; }

        [NotMapped]
        [Display(Name = "Voucher Date")]
        public DateTime VDate { get; set; }

        [Display(Name = "Item Id")]
        public int? ItemId { get; set; }

        [Display(Name = "Shade Id")]
        public int? ShadeId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Cross Section")]
        public string Cross_Section { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        public virtual ICollection<BatchTransModel> BatchTrans { get; set; }

    }
}
