using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Op.Dto
{
    public class OpBalDto
    {
        public int BalId { get; set; }
        public string AccName { get; set; }

        public string GroupName { get; set; }

        public decimal OpCredit { get; set; }

        public decimal OpDebit { get; set; }

        public string CurBal { get; set; }

        public decimal OpBal { get; set; }

        public decimal Balance { get; set; }

        [MaxLength(15)]
        [MinLength(15)]
        public string GstIn { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        public string PanNo { get; set; }

        [MaxLength(12)]
        [MinLength(12)]
        public string AadharNo { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public int AccountId { get; set; }

        public int? GroupId { get; set; }

        public int? AddressId { get; set; }

        public short? CompanyId { get; set; }

        public int? YearId { get; set; }
    }
}
