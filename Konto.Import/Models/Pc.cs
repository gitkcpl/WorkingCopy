using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Pc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Pc ID is required")]
        [Display(Name = "Pc ID")]
        public long PcID { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [Display(Name = "Voucher ID")]
        public long? VoucherID { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Voucher No")]
        public string VoucherNo { get; set; }

        [Required(ErrorMessage = "Pc Date is required")]
        [Display(Name = "Pc Date")]
        public int PcDate { get; set; }

        [Required(ErrorMessage = "Rec Date is required")]
        [Display(Name = "Rec Date")]
        public int? RecDate { get; set; }

        [Required(ErrorMessage = "Po Date is required")]
        [Display(Name = "Po Date")]
        public int PoDate { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Po Voucher No")]
        public string PoVoucherNo { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Display(Name = "Account ID")]
        public long AccountID { get; set; }

        [Display(Name = "Agent ID")]
        public long? AgentID { get; set; }

        [Display(Name = "Transport ID")]
        public long? TransportID { get; set; }

        [Required(ErrorMessage = "Total Pcs is required")]
        [Display(Name = "Total Pcs")]
        public long TotalPcs { get; set; }

        [Required(ErrorMessage = "Total Qty is required")]
        [Display(Name = "Total Qty")]
        public decimal TotalQty { get; set; }

        [Required(ErrorMessage = "Total Amt is required")]
        [Display(Name = "Total Amt")]
        public decimal TotalAmt { get; set; }

        [Display(Name = "Pc Remark")]
        public string PcRemark { get; set; }

        [Required(ErrorMessage = "Bill Status is required")]
        [Display(Name = "Bill Status")]
        public bool BillStatus { get; set; }

        [MaxLength(25)]
        [StringLength(25)]
        [Display(Name = "Challan No")]
        public string ChallanNo { get; set; }

        [MaxLength(30)]
        [StringLength(30)]
        [Display(Name = "Lr No")]
        public string LrNo { get; set; }

        [Display(Name = "Lr Date")]
        public int? LrDate { get; set; }

        [Display(Name = "Unit ID")]
        public int? UnitID { get; set; }

        [Display(Name = "Division ID")]
        public int? DivisionID { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Required(ErrorMessage = "Grn Type is required")]
        [Display(Name = "Grn Type")]
        public string GrnType { get; set; }

        [Display(Name = "Trans Type")]
        public byte? TransType { get; set; }

        [Display(Name = "Store ID")]
        public long? StoreID { get; set; }

        [Display(Name = "Trans Date")]
        public DateTime? TransDate { get; set; }

        [Display(Name = "Adduser Id")]
        public int? AdduserId { get; set; }

        [Display(Name = "Edit User Id")]
        public int? EditUserId { get; set; }

        [Required(ErrorMessage = "Rate is required")]
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "Net Rate is required")]
        [Display(Name = "Net Rate")]
        public decimal NetRate { get; set; }

        [Display(Name = "Book ID")]
        public long? BookID { get; set; }

        public Guid PcCode { get; set; }
        public decimal Freight { get; set; }
        public bool PcCancel { get; set; }
        public Nullable<int> PurchaseId { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        public string DriverMobileNo { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        public string BillNo { get; set; }
    }
}