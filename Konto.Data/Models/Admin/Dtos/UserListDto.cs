using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin.Dtos
{
    public class UserListDto : BaseDto
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

    }
}
