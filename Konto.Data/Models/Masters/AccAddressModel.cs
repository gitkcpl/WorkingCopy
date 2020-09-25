using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("AccAddress")]
    public class AccAddressModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id
        {
            get;set;
        } // int

        [Display(Name = "Acc Id")]
        public int AccId
        {
            get; set;
        } // int

        [MaxLength(200)]
        [Display(Name = "Address1")]
        public string Address1
        {
            get; set;
        } // varchar(100)

        [MaxLength(200)]
        [Display(Name = "Address2")]
        public string Address2
        {
            get; set;
        } // varchar(100)

        [Display(Name = "City Id")]
        public int? CityId
        {
            get; set;
        } // int

        [Display(Name = "Area Id")]
        public int? AreaId
        {
            get; set;
        } // int

        [MaxLength(15)]
        [Display(Name = "Pin Code")]
        public string PinCode
        {
            get; set;
        } // varchar(15)

        [MaxLength(75)]
        [Display(Name = "Contact Person")]
        public string ContactPerson
        {
            get; set;
        } // varchar(75)

        [MaxLength(15)]
        [Display(Name = "Mobile No")]
        public string MobileNo
        {
            get; set;
        } // varchar(15)

        [MaxLength(50)]
        [Display(Name = "Phone")]
        public string Phone
        {
            get; set;
        } // varchar(50)

        [MaxLength(200)]
        [Display(Name = "Email")]
        public string Email
        {
            get; set;
        } // varchar(50)

        [MaxLength(50)]
        [Display(Name = "Wesbite")]
        public string Website
        {
            get; set;
        } // varchar(50)

        [Display(Name = "Route Id")]
        public int? RouteId
        {
            get; set;
        } // int

        [Display(Name = "Is Default")]
        public bool IsDefault
        {
            get; set;
        } // bit

        [Display(Name = "Is Deleted")]
        public bool IsDeleted
        {
            get; set;
        } // bit

        [Display(Name = "Modify User")]
        public string ModifyUser
        {
            get; set;
        }

        [Required]
        public string AddressType
        {
            get; set;
        }

        public string Others
        {
            get; set;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Row Id")]
        public Guid RowId
        {
            get; set;
        } // uniqueidentifier

        // dbo.AccAddress.AccId -> dbo.Acc.Id (FK_AccAddress_Acc)
        [ForeignKey("AccId")] public virtual AccModel Acc { get; set; }

        // dbo.AccAddress.CityId -> dbo.City.Id (FK_AccAddress_City)
        [ForeignKey("CityId")] public virtual CityModel City { get; set; }

        // dbo.AccAddress.AreaId -> dbo.Area.Id (FK_AccAddress_Area)
        [ForeignKey("AreaId")] public virtual AreaModel Area { get; set; }

        // dbo.AccAddress.RouteId -> dbo.Route.Id (FK_AccAddress_Route)
        [ForeignKey("RouteId")] public virtual RouteModel Route { get; set; }

    }
}
