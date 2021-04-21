using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Keysoft.Erp.Data.Models
{
    public class TexProd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Prod ID is required")]
        [Display(Name = "Prod ID")]
        public long ProdID { get; set; }

        [Display(Name = "Voucher ID")]
        public long? VoucherID { get; set; }

        [Display(Name = "Voucher Date")]
        public int? VoucherDate { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Display(Name = "Batch ID")]
        public long? BatchID { get; set; }

        [Display(Name = "Machine ID")]
        public int? MachineID { get; set; }

        [Display(Name = "Grade ID")]
        public int? GradeID { get; set; }

        [Display(Name = "Sub Grade ID")]
        public int? SubGradeID { get; set; }

        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Twist ID")]
        public string TwistID { get; set; }

        [Display(Name = "Pack ID")]
        public int? PackID { get; set; }

        [Required(ErrorMessage = "Ply is required")]
        [Display(Name = "Ply")]
        public int Ply { get; set; }

        [Display(Name = "Cops")]
        public int? Cops { get; set; }

        [Display(Name = "Cops Weight")]
        public decimal? CopsWeight { get; set; }

        [Display(Name = "Carton Weight")]
        public decimal? CartonWeight { get; set; }

        [Display(Name = "Gross Weight")]
        public decimal? GrossWeight { get; set; }

        [Display(Name = "Tare Weight")]
        public decimal? TareWeight { get; set; }

        [Display(Name = "Net Weight")]
        public decimal? NetWeight { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Company ID")]
        public long? CompanyID { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Division ID")]
        public int? DivisionID { get; set; }

        [Display(Name = "Item ID")]
        public long? ItemID { get; set; }

        [Display(Name = "Job Card ID")]
        public int? JobCardID { get; set; }

        [Display(Name = "Color ID")]
        public long? ColorID { get; set; }

        [Display(Name = "Trans Date")]
        public DateTime? TransDate { get; set; }

        [Display(Name = "Adduser Id")]
        public int? AdduserId { get; set; }

        [Display(Name = "Edit User Id")]
        public int? EditUserId { get; set; }

        [Display(Name = "Pack Emp Id")]
        public int? PackEmpId { get; set; }

        [Display(Name = "Chek Emp Id")]
        public int? ChekEmpId { get; set; }

        [Display(Name = "Cops Item Id")]
        public long? CopsItemId { get; set; }

        [Display(Name = "Box Item Id")]
        public long? BoxItemID { get; set; }

        [Display(Name = "Cops Rate")]
        public decimal? CopsRate { get; set; }

        [Display(Name = "Box Rate")]
        public decimal? BoxRate { get; set; }

        [Display(Name = "Pallet Item Id")]
        public long? PalletItemId { get; set; }

        [Display(Name = "Ply Item Id")]
        public long? PlyItemId { get; set; }

        public int? PackTypeID { get; set; }

    }
}
