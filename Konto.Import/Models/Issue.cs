using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "issuemasterid is required")]
        public long issuemasterid { get; set; }

        [Required(ErrorMessage = "serialno is required")]
        public long serialno { get; set; }

        [Required(ErrorMessage = "addeddate is required")]
        public int addeddate { get; set; }

        [Required(ErrorMessage = "requirementid is required")]
        public long requirementid { get; set; }

        [Required(ErrorMessage = "companyid is required")]
        public int companyid { get; set; }

        public int? unitid { get; set; }

        public int? DeptID { get; set; }

        [MaxLength(2)]
        public string IssueType { get; set; }

        public long? FinItemID { get; set; }

        public decimal? Qty { get; set; }

        public int? AddUserID { get; set; }

        public int? EditUserID { get; set; }

        [MaxLength(100)]
        public string Remark { get; set; }

        public long? SessionID { get; set; }

        public DateTime? TransDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public int? MasterID { get; set; }

        public long? StoreID { get; set; }

        public long? account_Id { get; set; }

        public byte? Subdeptid { get; set; }

        public int? DivisionID { get; set; }

        public int? ToDivisionID { get; set; }

        public int? JobCardID { get; set; }

        [MaxLength(1)]
        public string JobType { get; set; }

        [Required(ErrorMessage = "Authorized is required")]
        public bool Authorized { get; set; }

        public IEnumerable<IssueTrans> IssueTrans { get; set; }

    }
}