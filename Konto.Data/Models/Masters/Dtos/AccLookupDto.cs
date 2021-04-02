using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class AccLookupDto
    {
        public int Id { get; set; }

        public string AccName { get; set; }

        public int? GroupId { get; set; }

        public string GroupName { get; set; }

        public string Balance { get; set; }

        public int? AgentId { get; set; }

        public int? TransportId { get; set; }

        public int? PGroupId { get; set; }

        public string Agent { get; set; }

        public string Transport { get; set; }

        public string PartyGroup { get; set; }

        public int? CrDays { get; set; }

        public decimal? CrLimit { get; set; }

        public string FullAddress { get; set; }

        public int AddressId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string AreaName { get; set; }

        public string CityName { get; set; }

        public string PinCode { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

        public int? EmpId { get; set; }

        public string GSTIN { get; set; }

        public string AadharNo { get; set; }
        public string PanNo { get; set; }

        public int? CityId { get; set; }

        public int? AreaId { get; set; }

        public int? StateId { get; set; }

        public string StateName { get; set; }

        public string GSTCode { get; set; }

        public string GstType { get; set; }

        public bool IsIgst { get; set; }

        public bool IsGst { get; set; }

        public string TdsReq { get; set; }

        public string TcsReq { get; set; }

        public string VatTds { get; set; }

        public string BToB { get; set; }

        public int? TdsAccId { get; set; }

        public int? TcsAccId { get; set; }

        public decimal TcsPer { get; set; }

        public decimal TdsPer { get; set; }

        public bool IsSelected { get; set; }

        public decimal DiscPer { get; set; }
        public string   RateType { get; set; }
        
    }
}
