using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class TempTransDto : BaseDto
    {
        public   int? TemplateId { get; set; }
        public   int? TempFieldId { get; set; }
        public   int? ColNo { get; set; }

        public   string FieldName { get; set; }
    }
}
