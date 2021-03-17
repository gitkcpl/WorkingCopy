using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Pos
{
    [Table("customers")]
    public class CustomerModel : AuditedEntity
    {

        [Required][MaxLength(75)][MinLength(2)]
        public string   CustomerName { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public int? AreaId { get; set; }
        public int? CityId { get; set; }

        [MaxLength(10)]
        public string MobileNo { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public DateTime? Dob { get; set; }
        public DateTime? AnniDate { get; set; }

        [MaxLength(20)]
        public string GstNo { get; set; }

        [MaxLength(25)]
        public string MemberNo { get; set; }

        public DateTime? MemberDate { get; set; }

        public decimal OpBillAmt { get; set; }

        public decimal OpPoint { get; set; }
        public decimal OpPointUsed { get; set; }

        [ForeignKey("AreaId")]
        public AreaModel Area { get; set; }

        [ForeignKey("CityId")]
        public CityModel City { get; set; }
    }
}
