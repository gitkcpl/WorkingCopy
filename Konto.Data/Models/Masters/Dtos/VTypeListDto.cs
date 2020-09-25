using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class VTypeListDto : BaseDto
    {
        public virtual string TypeName { get; set; }
        public virtual bool? SmsToParty { get; set; }

        public virtual bool? SmsToAgent { get; set; }
        public virtual bool? SmsToUser { get; set; }
    }
}
