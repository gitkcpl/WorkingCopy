using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("JobCard")]
    public class JobCardModel : AuditedEntity
    {
        public JobCardModel()
        { 
            this.IsDeleted = false;
            IsActive = true;
        }

        [Display(Name = "Voucher Id")]
        public int? VoucherId { get; set; }
        [MaxLength(50)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Required]
        [Display(Name = "Voucher Date")]
        public int VoucherDate { get; set; }

        [Display(Name = "Order Date")]
        public DateTime? OrdDate { get; set; }
       
        [Display(Name = "Div Id")]
        public int? DivId { get; set; }
        [Display(Name = "Company Id")]
        public int? CompanyId { get; set; }
        [MaxLength(50)]
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "Machine Id")]
        public int? MachineId { get; set; }
        [Display(Name = "RPUI Id")]
        public int? RPUIId { get; set; }
        [MaxLength(50)]
        [Display(Name = "Dyeing Type")]
        public string DyeingType { get; set; }
        [MaxLength(50)]
        [Display(Name = "Carrier No")]
        public string CarrierNo { get; set; }
        [MaxLength(50)]
        [Display(Name = "Program No")]
        public string ProgramNo { get; set; }
        [Display(Name = "Order Id")]
        public int? OrderId { get; set; }
        [Display(Name = "Account Id")]
        public int? AccountId { get; set; }

        [Display(Name = "Color Id")]
        public int? ColorId { get; set; }
        [Display(Name = "Product Id")]
        public int? ProductId { get; set; }
        [Display(Name = "Order Qty")]
        public decimal? OrderQty { get; set; }
        [Display(Name = "Gross Wt")]
        public decimal? GrossWt { get; set; }
        [Display(Name = "Carrier Wt")]
        public decimal? CarrierWt { get; set; }
        [Display(Name = "No Of Cones")]
        public int? NoOfCones { get; set; }
        [Display(Name = "Spring Wt")]
        public decimal? SpringWt { get; set; }
        [Display(Name = "Spring Id")]
        public int? SpringId { get; set; }
        [Display(Name = "Gray Item Id")]
        public int? GrayItemId { get; set; }
        [MaxLength(50)]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }
        [Display(Name = "Grade Id")]
        public int? GradeId { get; set; }
        [Display(Name = "Batch Id")]
        public int? BatchId { get; set; }
        [Display(Name = "Qty")]
        public decimal Qty { get; set; }
        public decimal TolPer { get; set; }

        [MaxLength(50)]
        [Display(Name = "Challan No")]
        public string ChallanNo { get; set; }
        [Display(Name = "Rate")]
        public decimal? Rate { get; set; }
        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }
        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        
    }
}
