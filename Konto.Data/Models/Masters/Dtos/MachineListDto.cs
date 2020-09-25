using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class MachineListDto
    {
        public int Id{ get; set; }
        public string MachineName { get; set; }
        public string Remark { get; set; }
        public int CompanyID { get; set; }
        public int? DivId { get; set; }
        public string DivisionName { get; set; }
        public bool IsActive { get; set; } 
        public bool IsDeleted { get; set; } 
        public DateTime? CreateDate { get; set; } 
        public DateTime? ModifyDate { get; set; } 
        public string CreateUser { get; set; } 
        public string ModifyUser { get; set; } 
    }
}
