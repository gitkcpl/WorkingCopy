using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("MachineMaster")]
    public class MachineMasterModel : AuditedEntity
    {
        public MachineMasterModel()
        {
            this.IsActive = true;
            this.IsDeleted = false;
        }

        [MaxLength(25)]
        
        public string MachineName { get; set; }

        [MaxLength]
        [Display(Name = "remark")]
        public string Remark { get; set; }

        [Display(Name = "Company ID")]
        public int CompanyID { get; set; }

        [Display(Name = "Div Id")]
        public int? DivId { get; set; }
    }
}
