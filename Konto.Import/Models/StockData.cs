using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class StockData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Stock Trans ID is required")]
        [Display(Name = "Stock Trans ID")]
        public long StockTransID { get; set; }

        [Display(Name = "Reference ID")]
        public long? ReferenceID { get; set; }

        [Display(Name = "Trans ID")]
        public long? TransID { get; set; }

        [Display(Name = "Company ID")]
        public int? CompanyID { get; set; }

        [Display(Name = "Account ID")]
        public int? AccountID { get; set; }

        [Display(Name = "Voucher Date")]
        public int? VoucherDate { get; set; }

        [Display(Name = "Trans Date")]
        public DateTime? TransDate { get; set; }

        [Display(Name = "Voucher ID")]
        public int? VoucherID { get; set; }

        [Display(Name = "Voucher Type ID")]
        public int? VoucherTypeID { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        [Display(Name = "Item ID")]
        public int? ItemID { get; set; }

        [Display(Name = "In Pcs")]
        public int? InPcs { get; set; }

        [Display(Name = "Out Pcs")]
        public int? OutPcs { get; set; }

        [Display(Name = "In Qty")]
        public decimal? InQty { get; set; }

        [Display(Name = "Out Qty")]
        public decimal? OutQty { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Table Name")]
        public string TableName { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Table Key Field")]
        public string TableKeyField { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Rate")]
        public decimal? Rate { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Narration")]
        public string Narration { get; set; }

        [Display(Name = "Division ID")]
        public int? DivisionID { get; set; }

        [Display(Name = "Grade ID")]
        public int? GradeID { get; set; }

        [Display(Name = "Sub Grade ID")]
        public int? SubGradeID { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }

        [Display(Name = "Machine ID")]
        public int? MachineID { get; set; }

        [Display(Name = "Batch ID")]
        public long? BatchID { get; set; }

        [MaxLength(2)]
        [StringLength(2)]
        [Display(Name = "issuetype")]
        public string issuetype { get; set; }

        [Display(Name = "Store ID")]
        public int? StoreID { get; set; }

        [Display(Name = "Color ID")]
        public long? ColorID { get; set; }

        [Display(Name = "Trans Code")]
        public Guid? TransCode { get; set; }

        [Display(Name = "Ref Code")]
        public Guid? RefCode { get; set; }


    }
}
