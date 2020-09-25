using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class AddressLookupDto
    {
        [Key]
        public int Id { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public string Address { get; set; }

        public int? AreaId { get; set; }
        public string AreaName { get; set; }

        public int? StateId { get; set; }
        public string StateName { get; set; }

        public string PinCode { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public string MobileNo { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
    }
}
