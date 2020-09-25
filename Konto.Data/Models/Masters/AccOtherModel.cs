using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("AccOther")]
    public class AccOtherModel
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }// int

        [Display(Name = "Acc Id")]
        public int? AccId { get; set; }

        [Display(Name = "Op Stock Acc Id")]
        public int? OpStockAccId { get; set; }

        [Display(Name = "Int Acc Id")]
        public int? IntAccId { get; set; }

        [Display(Name = "Int Tds Acc Id")]
        public int? IntTdsAccId { get; set; }

        [Display(Name = "Int Per")]
        public decimal? IntPer { get; set; }

        [Display(Name = "Int Tds Per")]
        public decimal? IntTdsPer { get; set; }

        [MaxLength(2)]
        [Display(Name = "Tds Dr Cr")]
        public string TdsDrCr { get; set; }

        [Display(Name = "Tcs Acc Id")]
        public int? TcsAccId { get; set; }

        [Display(Name = "Tcs Per")]
        public decimal? TcsPer { get; set; }

        [Display(Name = "Tds Acc Id")]
        public int? TdsAccId { get; set; }

        [Display(Name = "Tds Per")]
        public decimal? TdsPer { get; set; }

        [Display(Name = "Dep Acc Id")]
        public int? DepAccId { get; set; }

        [Display(Name = "Dep Per")]
        public decimal? DepPer { get; set; }

        [Required]
        [Display(Name = "Row Id")]
        public Guid RowId { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Modify User")]
        public string ModifyUser { get; set; }

        // dbo.AccOther.AccId -> dbo.Acc.Id (FK_AccOther_Acc)
        [ForeignKey("AccId")]
        public virtual AccModel Acc { get; set; }

        // dbo.AccOther.OpStockAccId -> dbo.Acc.Id (FK_AccOther_OpStcok_Acc)
        [ForeignKey("OpStockAccId")]
        public virtual AccModel OpStockAcc { get; set; }

        // dbo.AccOther.IntAccId -> dbo.Acc.Id (FK_AccOther_Int_Acc)
        [ForeignKey("IntAccId")]
        public virtual AccModel IntAcc { get; set; }

        // dbo.AccOther.IntTdsAccId -> dbo.Acc.Id (FK_AccOther_IntTds_Acc)
        [ForeignKey("IntTdsAccId")]
        public virtual AccModel IntTdsAcc { get; set; }

        // dbo.AccOther.TcsAccId -> dbo.Acc.Id (FK_AccOther_Tcs_Acc)
        [ForeignKey("TcsAccId")]
        public virtual AccModel TcsAcc { get; set; }

        // dbo.AccOther.TdsAccId -> dbo.Acc.Id (FK_AccOther_Tds_Acc)
        [ForeignKey("TdsAccId")]
        public virtual AccModel TdsAcc { get; set; }

        // dbo.AccOther.DepAccId -> dbo.Acc.Id (FK_AccOther_Dep_Acc)
        [ForeignKey("DepAccId")]
        public virtual AccModel DeprAcc { get; set; }

    }
}
