using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("Challan")]
    public class ChallanModel : AuditedEntity
    {
        public ChallanModel()
        {
            VoucherNo = "New";
            VDate = DateTime.Now.Date;
            RcdDate = DateTime.Now.Date;
            BillType = "Regular";
            Itc = "Inputs";
            Rcm = "NO";
            BookAcId = 0;
            IsActive = true;
        }

        public int? CompId { get; set; }
        public int? StoreId { get; set; }

        [Required(ErrorMessage = "Voucher is required")]
        public int VoucherId { get; set; }

        [MaxLength(25)]
        public string VoucherNo { get; set; }

        [Required]
        public int VoucherDate { get; set; }


        [NotMapped]
        public DateTime? VDate
        {
            get
            {
                if (VoucherDate != null)
                { return (DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
            set { }

        }


        [MaxLength(25)]
        [Required]
        public string ChallanNo { get; set; }

        [MaxLength(25)]
        public string BillNo { get; set; }

        public DateTime? RcdDate { get; set; }

        [Required(ErrorMessage = "Supplier is required")]
        [Index]
        public int AccId { get; set; }

        public int? AgentId { get; set; }

        public int DivId { get; set; }

        public int? YearId { get; set; }

        [MaxLength(25)]
        public string DocNo { get; set; }

        public DateTime? DocDate { get; set; }

        public int? TransId { get; set; }

        public int? ProcessId { get; set; }

        [MaxLength]
        public string Remark { get; set; }


        [MaxLength(200)]
        public string Extra1 { get; set; }

        [MaxLength(200)]
        public string Extra2 { get; set; }

        [MaxLength(200)]
        public string Extra3 { get; set; }

        [MaxLength(200)]
        public string Extra4 { get; set; }

        public int? MasterId { get; set; }

        [Required(ErrorMessage = "ChallanType is required")]
        public int ChallanType { get; set; }

        public int? DelvAccId { get; set; }

        public decimal? TotalQty { get; set; }

        public decimal? TotalPcs { get; set; }

        public decimal TotalAmount { get; set; }

        public int? BillId { get; set; }

        [MaxLength(25)]
        public string VehicleNo { get; set; }

        public int? BranchId { get; set; }

        public int? EmpId { get; set; }

        [MaxLength(250)]
        public string DName { get; set; }

        public int? DelvAdrId { get; set; }

        public int? AddrId { get; set; }

        public int TypeId { get; set; }

        [NotMapped]
        public string DelAddress { get; set; }

        [Display(Name = "Round Off")]
        public decimal? RoundOff { get; set; }

        [Display(Name = "Tds%")]
        public decimal TdsPer { get; set; }

        [Display(Name = "TdsAmt")]
        public decimal TdsAmt { get; set; }


        [NotMapped]
        public decimal PayableAmt { get; set; }

        public int BookAcId { get; set; }

        [MaxLength(25)]
        [Display(Name = "Rcm")]
        public string Rcm { get; set; }

        [MaxLength(25)]
        [Display(Name = "Itc")]
        public string Itc { get; set; }


        [MaxLength(50)]
        [Display(Name = "Bill Type")]
        public string BillType { get; set; }

        [NotMapped]
        [MaxLength(200)]
        public string JobCardNo { get; set; }

        public virtual ICollection<ChallanTransModel> ChallanTrans { get; set; }

    }
}
