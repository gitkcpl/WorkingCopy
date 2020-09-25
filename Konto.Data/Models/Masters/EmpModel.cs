using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Emp")]
    public class EmpModel : AuditedEntity
    {
        public EmpModel()
        {
            IsActive = true;
            
        }
        [MaxLength(75)]
        [Display(Name = "Emp Name")]
        [Required]
        [MinLength(2)]
        public string EmpName
        {
            get;set;
        } // varchar(50)

        [MaxLength(100)]
        [Display(Name = "Address1")]
        public string Address1
        {
            get; set;
        } // varchar(100)

        [MaxLength(100)]
        [Display(Name = "Address2")]
        public string Address2
        {
            get; set;
        } // varchar(100)

        [MaxLength(15)]
        [Display(Name = "Mobile No")]
        public string MobileNo
        {
            get; set;
        } // varchar(15)

        [MaxLength(50)]
        [Display(Name = "Email")]
        public string Email
        {
            get; set;
        } // varchar(50)

        [MaxLength(15)]
        [Display(Name = "Aadhar No")]
        public string AadharNo
        {
            get; set;
        } // varchar(15)

        [MaxLength(15)]
        [Display(Name = "Pan No")]
        public string PanNo
        {
            get; set;
        } // varchar(15)

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark
        {
            get; set;
        } // varchar(max)

        public int CompId
        {
            get; set;
        }

        [ForeignKey("CompId")]
        public virtual CompModel Company { get; set; }
    }
}
