using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin.Dtos
{
    public class ParaDto
    {
        public int ParaId { get; set; }
        public int Id { get; set; }
        public int CompId { get; set; }
        public string Descr { get; set; }
        public string DefaultValue { get; set; }
        public string ValueDescr { get; set; }
        public string Category { get; set; }
        public string ParaValue { get; set; }
        public string Remark { get; set; }
    }
}
