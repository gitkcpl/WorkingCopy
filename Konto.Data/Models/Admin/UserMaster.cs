using Konto.Data.Models.Masters;
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

        public int? BranchId { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public bool AcceptTerms { get; set; }

        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        [ForeignKey("RoleId")]
        public virtual RolesModel Role { get; set; }

        [ForeignKey("BranchId")]
        public virtual BranchModel BranchFk { get; set; }

        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }

}
