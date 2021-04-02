using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Haste")]
    public class HasteModel : AuditedEntity
    {
        public HasteModel()
        {
            Id = 0;
            IsActive = true;
           
        }

        [MaxLength(50)]
        [Display(Name = "Master Type")]
        public string MType { get; set; }

        [MaxLength(200)]
        [Display(Name = "Haste Name")]
        public string HasteName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [MaxLength(15)]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(15)]
        [Display(Name = "Aadhar No")]
        public string AadharNo { get; set; }

        [MaxLength(15)]
        [Display(Name = "Pan No")]
        public string PanNo { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Comp Id")]
        public int? CompId { get; set; }

        public int? AccId { get; set; }

        // dbo.Haste.CompId -> dbo.Company.Id (FK_Haste_Company)
        [ForeignKey("CompId")]
        public virtual CompModel Company { get; set; }

        [ForeignKey("AccId")]
        public virtual AccModel Acc { get; set; }
    }
}
