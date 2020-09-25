using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class AccListDto : BaseDto
    {

        [Display(Name = "Account Name")]
        public string AccName { get; set; }

        public string GroupName { get; set; }

        public int GroupId { get; set; }

        public int? PGroupId { get; set; }

        public string GSTIN { get; set; }

        public string PanNo
        {
            get; set;
        }

        public string AadharNo
        {
            get; set;
        }
        public string PartyGroup { get; set; }

        public string TdsReq { get; set; }

        public string TcsReq { get; set; }

        public string VatTds { get; set; }

        public int CrDays { get; set; }

        public decimal CrLimit { get; set; }

        public string BToB { get; set; } // 

        public int AgentId { get; set; }

        public string Agent { get; set; }

        public string Transport { get; set; }

        public int TransportId { get; set; }

        public int EmpId { get; set; }

        public int? AddressId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string CityName { get; set; }

        public string Address => Address1 + " " + Address2 + " " + CityName;

        public string SalesMan { get; set; }

        public int? StateId { get; set; }

        public string Email { get; set; }

        public string MobileNo { get; set; }

        public decimal Bal { get; set; }

        public decimal OpBal { get; set; }

        public AcGroupModel AcGroup { get; set; }
    }
}
