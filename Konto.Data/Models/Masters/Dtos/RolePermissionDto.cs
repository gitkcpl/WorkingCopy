using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class RolePermissionDto
    {
        public string Descr { get; set; }
        public int Id { get; set; }
        public string PermissionType { get; set; }
        public int RoleId { get; set; }
        public int TransId { get; set; }

    }
}
