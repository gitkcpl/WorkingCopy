using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Keysoft.Erp.Data.Models
{
    public class LmsProd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Prod ID")]
        public long ProdID { get; set; }

        [Display(Name = "Voucher Date")]
        public int? VoucherDate { get; set; }

        [MaxLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Display(Name = "Company ID")]
        public int? CompanyID { get; set; }

        [Display(Name = "Start Date")]
        public int? StartDate { get; set; }

        [Display(Name = "Fold Date")]
        public int? FoldDate { get; set; }

        [Display(Name = "Item ID")]
        public long? ItemID { get; set; }

        [Display(Name = "Taka Meters")]
        public decimal? TakaMeters { get; set; }

        [Display(Name = "Taka Weight")]
        public decimal? TakaWeight { get; set; }

        [Display(Name = "Grade ID")]
        public int? GradeID { get; set; }

        [Display(Name = "Design ID")]
        public int? DesignID { get; set; }

        [Display(Name = "Shade ID")]
        public int? ShadeID { get; set; }

        [MaxLength(25)]
        [Display(Name = "Taka No")]
        public string TakaNo { get; set; }

        [Display(Name = "Job Rate")]
        public decimal? JobRate { get; set; }

        [Display(Name = "Add User ID")]
        public int? AddUserID { get; set; }

        [Display(Name = "Edit User ID")]
        public int? EditUserID { get; set; }

        [Display(Name = "Trans Date")]
        public int? TransDate { get; set; }

        [Display(Name = "Session ID")]
        public long? SessionID { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Voucher ID")]
        public long? VoucherID { get; set; }

        [Display(Name = "Beam Entry ID")]
        public long? BeamEntryID { get; set; }

        [MaxLength(200)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Machine ID")]
        public int? MachineID { get; set; }

        [Display(Name = "Folder Emp ID")]
        public long? FolderEmpID { get; set; }

        [Display(Name = "Mendor Emp ID")]
        public long? MendorEmpID { get; set; }

        [Display(Name = "Checke Emp ID")]
        public long? CheckeEmpID { get; set; }

        [Display(Name = "Mendor Rate")]
        public decimal? MendorRate { get; set; }

        [Display(Name = "Checker Rate")]
        public decimal? CheckerRate { get; set; }

        [Display(Name = "Ref ID")]
        public long? RefID { get; set; }

        [Display(Name = "Ref Voucher ID")]
        public long? RefVoucherID { get; set; }

        [Display(Name = "Ref Code")]
        public Guid? RefCode { get; set; }

        [Display(Name = "Prod Code")]
        public Guid? ProdCode { get; set; }

        [Required]
        [Display(Name = "Current Mtrs")]
        public decimal CurrentMtrs { get; set; }

        [MaxLength(1)]
        [Required]
        [Display(Name = "Stock Status")]
        public string StockStatus { get; set; }

        [MaxLength(30)]
        [Display(Name = "Taka Stage")]
        public string TakaStage { get; set; }


    }
}
