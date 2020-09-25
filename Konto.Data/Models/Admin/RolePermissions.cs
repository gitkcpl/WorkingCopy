using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    [Table("Role_Permissions")]
    public class RolePermission
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        [Display(Name = "Permission Id")]
        public int PermissionId { get; set; }

     
        [Display(Name = "Row Id")]
        public Guid RowId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Permission Type")]
        public string PermissionType { get; set; }

        [ForeignKey("RoleId")]
        public virtual RolesModel Role { get; set; }

        [ForeignKey("PermissionId")]
        public virtual PermissionsModel Permission { get; set; }
    }

}
