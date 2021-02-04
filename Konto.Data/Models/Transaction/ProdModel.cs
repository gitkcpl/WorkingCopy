using Konto.Data.Models.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Transaction
{
    [Table("Prod")]
    public class ProdModel : AuditedEntity
    {
        public ProdModel()
        {
            this.IsClose = false;
            IsActive = true;
            IsOk = true;
            IsMultiple = false;
        }

        public int? TransId { get; set; }

        public int? SrNo { get; set; }
        public int? ProductId { get; set; }
        public int? GradeId { get; set; }
        public int? BatchId { get; set; }
        public int? ColorId { get; set; }
        public int? PackId { get; set; }
        public int? MacId { get; set; }
        public int? SubGradeId { get; set; }

        [MaxLength(50)]
        public string TwistType { get; set; }
        public int? CompId { get; set; }
        public int? YearId { get; set; }

        public int? VoucherId { get; set; }

        [Display(Name = "Voucher Date")]
        public int VoucherDate { get; set; }

        //[NotMapped]
        //public DateTime VDate { get; set; }

        [MaxLength(25)]
        [Required]
        public string VoucherNo { get; set; }

        public int? RefId { get; set; }

        public int? RefSCId { get; set; }

        public int Ply { get; set; }
        public int Cops { get; set; }
        public decimal CopsWt { get; set; }
        public decimal BoxWt { get; set; }
        public decimal CartnWt { get; set; }
        public decimal GrossWt { get; set; }
        public decimal TareWt { get; set; }
        public decimal NetWt { get; set; }
        public int? DivId { get; set; }
        public int? BranchId { get; set; }
        public int? JobId { get; set; }
        public int? CopsProductId { get; set; }
        public decimal CopsRate { get; set; }
        public int? BoxProductId { get; set; }
        public decimal BoxRate { get; set; }
        public int? PackEmpId { get; set; }
        public int? CheckEmpId { get; set; }
        public int? PalletProductId { get; set; }
        public int? PlyProductId { get; set; }
        public DateTime? DrawingDate { get; set; }
        public DateTime? LoadingDate { get; set; }
        [Display(Name = "Warping Date")]
        public DateTime? WarpingDate { get; set; }

        [Display(Name = "Close Date")]
        public DateTime? CloseDate { get; set; }

        [MaxLength(50)]
        public string ProdStatus { get; set; }
        public int Tops { get; set; }
        public int Pallet { get; set; }
        public decimal CurrQty { get; set; }
        public decimal FinQty { get; set; }
        public int? IssueRefId { get; set; }
        public int? IssueRefVoucherId { get; set; }

        [MaxLength]
        public string Remark { get; set; }

        public string LotNo { get; set; }

        [Display(Name = "Is Close")]
        public bool IsClose { get; set; }

        public bool IsOk { get; set; }

        public bool IsMultiple { get; set; }

        public int VTypeId { get; set; }

        [MaxLength(200)]
        public string Extra1 { get; set; }

        public int? CProductId { get; set; }

        
        //[NotMapped]
        //public string MachineName { get; set; }

        //[NotMapped]
        //public string ProductName { get; set; }

        //[NotMapped]
        //public string YarnName { get; set; }

        //[NotMapped]
        //public string GrayName { get; set; }

        //[NotMapped]
        //public string DrawerName { get; set; }

        //[NotMapped]
        //public string warperName { get; set; }

        //[NotMapped]
        //public string MendorName { get; set; }

        //[MaxLength(50)]
        //[NotMapped]
        //public string InwardNo { get; set; }

        //[NotMapped]
        //public string Weaver { get; set; }

        //[NotMapped]
        //public decimal? AvgWt { get; set; }

        //[NotMapped]
        //public decimal? StdWt { get; set; }

        //[NotMapped]
        //public decimal? ProdWt { get; set; }

        //[NotMapped]
        //public decimal? ProdTakaWt { get; set; }

        [ForeignKey("RefId")]
        public virtual ChallanModel Challan { get; set; }

        [ForeignKey("TransId")]
        public virtual ChallanTransModel ChallanTrans { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductModel product { get; set; }

        [ForeignKey("CProductId")]
        public virtual ProductModel CProduct { get; set; }

        [ForeignKey("ColorId")]
        public virtual ColorModel color { get; set; }


    }
}
