using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("ReportPara")]
    public class ReportParaModel : AuditedEntity
    {

        [Index]
        public int ReportId { get; set; }

        [MaxLength(50)]
        [Index]
        public string ParameterName { get; set; }
       
        [Index]
        public int? ParameterValue { get; set; }

    }
}
