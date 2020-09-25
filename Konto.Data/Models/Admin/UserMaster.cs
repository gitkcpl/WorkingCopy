using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    [Table("UserMaster")]
    public class UserMasterModel : AuditedEntity
    {
        public UserMasterModel()
        {
            IsActive = true;
            IsDeleted = false;
        }
       
        [MaxLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Display(Name = "User Pass")]
        public string UserPass { get; set; }

        
        [Display(Name = "Last Login Date")]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        
        [Display(Name = "Emp Id")]
        public int? EmpId { get; set; }

        
        [ForeignKey("RoleId")]
        public virtual RolesModel Role { get; set; }
    }

}
