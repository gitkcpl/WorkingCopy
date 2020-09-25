using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Admin
{
    [Table("Permissions")]
    public class PermissionsModel
    {
      

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "Permission Description")]
        public string PermissionDescription { get; set; }

       
        [Display(Name = "Row Id")]
        public Guid RowId { get; set; }

        [Display(Name = "Module Id")]
        public int ModuleId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Permission Type")]
        public string PermissionType { get; set; }

        [Display(Name = "Permission Type Id")]
        public int PermissionTypeId { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

}
