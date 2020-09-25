using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Route")]
    public class RouteModel :AuditedEntity
    {
        [MaxLength(15)]
        [Display(Name = "Route Code")]
        public string RouteCode
        {
            get;set;
        } // varchar(15)

        [MaxLength(50)]
        [Required]
        [Display(Name = "Route Name")]
        public string RouteName
        {
            get; set;
        } // varchar(50)

        [Display(Name = "City Id")]
        public int CityId
        {
            get; set;
        } // int

        [Display(Name = "Area Id")]
        public int AreaId
        {
            get; set;
        } // int

        [ForeignKey("CityId")]
        public virtual CityModel City { get; set; }

        // dbo.Route.AreaId -> dbo.Area.Id (FK_Route_Area)
        [ForeignKey("AreaId")]
        public virtual AreaModel Area { get; set; }

        // dbo.AccAddress.RouteId -> dbo.Route.Id (FK_AccAddress_Route)
        public virtual ICollection<AccAddressModel> AccAddresses { get; set; }

    }
}
