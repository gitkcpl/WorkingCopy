using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class RefBankListDto : BaseDto
    {
        public string BankName { get; set; }
        public string Address { get; set; }
    }
}
