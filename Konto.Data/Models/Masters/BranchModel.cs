using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Branch")]
    public class BranchModel : AuditedEntity
    {
        public BranchModel()
        {
            IsActive = true;
            IsDeleted = false;
        }

        [MaxLength(10)]
        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }

        [MaxLength(75)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Display(Name = "Comp Id")]
        public int? CompId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [Display(Name = "City Id")]
        public int? CityId { get; set; }

        [Display(Name = "Area Id")]
        public int? AreaId { get; set; }

        [MaxLength(10)]
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }

        [MaxLength(25)]
        [Display(Name = "Gst In")]
        public string GstIn { get; set; }

        [MaxLength(25)]
        [Display(Name = "Aadhar No")]
        public string AadharNo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [MaxLength(50)]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [MaxLength(200)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        [ForeignKey("CompId")]
        public virtual CompModel Company { get; set; }

        public virtual ICollection<DivisionModel> Divisions { get; set; }


    }
}
