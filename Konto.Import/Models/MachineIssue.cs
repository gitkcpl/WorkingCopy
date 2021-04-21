using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class MachineIssue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "MacIssueID is required")]
        public long MacIssueID { get; set; }

        [Required(ErrorMessage = "VoucherID is required")]
        public long? VoucherID { get; set; }

        [Required(ErrorMessage = "VoucherDate is required")]
        public int? VoucherDate { get; set; }

        [MaxLength(25)]
        [Required(ErrorMessage = "VoucherNo is required")]
        public string VoucherNo { get; set; }

        [MaxLength(25)]
        public string LotNo { get; set; }

        public int? MacID { get; set; }

        public long? ItemID { get; set; }

        [MaxLength(25)]
        public string MergeNo { get; set; }

        public int? Box { get; set; }

        public decimal? Qty { get; set; }

        [MaxLength(100)]
        public string Remark { get; set; }

        public long? CompanyID { get; set; }

        public int? UserID { get; set; }

        public int? EditUserID { get; set; }

        public int? UnitID { get; set; }

        public int? DivisionID { get; set; }

        public int? TransDate { get; set; }

        public int? TransTime { get; set; }

        public int? Cops { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Amount { get; set; }

        public long? CostHeadID { get; set; }

        public int? GradeID { get; set; }

        public long? SessionID { get; set; }

        public long? PartyID { get; set; }

        public decimal? TotalAmount { get; set; }

        public Int16? TotalBox { get; set; }

        public Int16? TotalCops { get; set; }

        public decimal? TotalQty { get; set; }

        public IEnumerable<MachineIssueTrans> MachineIssueTrans { get; set; }
    }

    public class MachineIssueTrans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "MacIssueItemTransID is required")]
        public long MacIssueItemTransID { get; set; }

        public long? MacIssueID { get; set; }

        [MaxLength(25)]
        public string MergeNo { get; set; }

        public long? ItemID { get; set; }

        public int? GradeID { get; set; }

        public long? CostHeadID { get; set; }

        public long? RowID { get; set; }

        public int? Box { get; set; }

        public int? Cops { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Amount { get; set; }

        public int? UnitID { get; set; }

        [ForeignKey("MacIssueID")]
        public MachineIssue MachineIssue { get; set; }

        public IEnumerable<MachineIssueTransd> MachineIssueTransd { get; set; }
    }

    public class MachineIssueTransd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "MacIssTransID is required")]
        public long MacIssTransID { get; set; }

        public long? MacIssueItemTransID { get; set; }

        public long? MacIssueID { get; set; }

        public long? PcTakaID { get; set; }

        public int? Spool { get; set; }

        public decimal? Weight { get; set; }

        public long? CompanyID { get; set; }

        public decimal? Rate { get; set; }

        public Byte? StockType { get; set; }

        [MaxLength(30)]
        public string BoxNo { get; set; }

        [ForeignKey("MacIssueItemTransID")]
        public MachineIssueTrans MachineIssueTrans { get; set; }
    }
}