using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    public static class RBAC
    {
        public static RBACUser UserRight;

        public static bool Pending_Details_View;
        public static bool Pending_Party_View;
        public static bool Pending_Product_summary_View;

    }

    public class RBACUser
    {
        public int User_Id { get; set; }
        public bool IsSysAdmin { get; set; }
        public string Username { get; set; }
        public bool bFound { get; set; } = false;
        private List<UserRole> Roles = new List<UserRole>();

        public RBACUser()
        {

        }
        public RBACUser(string _username)
        {
            this.Username = _username;
            this.IsSysAdmin = false;
            GetDatabaseUserRolesPermissions();
        }


        private void GetDatabaseUserRolesPermissions()
        {
            using (KontoContext _data = new KontoContext())
            {
                UserMasterModel _user = _data.UserMasters.Include("Role").Include("Role.PERMISSIONS").Include("Role.PERMISSIONS.Permission").FirstOrDefault(u => u.UserName == this.Username);

                if (_user != null)
                {
                    this.User_Id = _user.Id;
                    RolesModel _role = _user.Role;

                    UserRole _userRole = new UserRole { Role_Id = _role.Id, RoleName = _role.RoleName };
                    foreach (RolePermission _permission in _role.PERMISSIONS)
                    {
                        _userRole.Permissions.Add(new RolePerm
                        {
                            Permission_Id = _permission.PermissionId,
                            PermissionDescription = _permission.Permission.PermissionDescription,
                            PermissionTypeId = _permission.Permission.PermissionTypeId,
                            ModuleId = _permission.Permission.ModuleId
                        });
                    }
                    this.Roles.Add(_userRole);

                    if (!this.IsSysAdmin)
                        this.IsSysAdmin = _role.IsSysAdmin;

                }
            }
        }
        public bool HasPermission(int moduleid, int permissiontypeid)
        {
            //   bool bFound = false;
            foreach (UserRole role in this.Roles)
            {
                // bFound = (role.Permissions.Where(p => p.PermissionDescription.ToLower() == requiredPermission.ToLower()).ToList().Count > 0);
                bFound = role.Permissions.Any(x => x.PermissionTypeId == permissiontypeid && x.ModuleId == moduleid);
                if (bFound)
                    break;
            }
            return bFound;
        }
    }

    public class UserRole
    {
        public int Role_Id { get; set; }
        public string RoleName { get; set; }
        public List<RolePerm> Permissions = new List<RolePerm>();
    }

    public class RolePerm
    {
        public int Permission_Id { get; set; }
        public string PermissionDescription { get; set; }
        public int PermissionTypeId { get; set; }
        public int ModuleId { get; set; }
    }
}
