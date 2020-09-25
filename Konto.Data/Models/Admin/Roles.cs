using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    [Table("Roles")]
    public class RolesModel : AuditedEntity
    {
       

      
        [MaxLength(50)]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Is Sys Admin")]
        public bool IsSysAdmin { get; set; }

        [MaxLength]
        [Display(Name = "Role Description")]
        public string RoleDescription { get; set; }

        public virtual ICollection<UserMasterModel> USERS { get; set; }
        public virtual ICollection<RolePermission> PERMISSIONS { get; set; }

    }

}
