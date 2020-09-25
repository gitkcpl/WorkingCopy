using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("AccBal")]
    public class AccBalModel
    {

        public AccBalModel()
        {
            DrCr = "Dr";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        [Display(Name = "Acc Id")]
        public int AccId
        {
            get; set;
        } // int

        [Display(Name = "Group Id")]
        [Required]
        [Range(1, 999)]
        public int GroupId
        {
            get; set;
        } // int

        [Display(Name = "Address Id")]
        public int AddressId
        {
            get; set;
        } // nchar(10)

        [Display(Name = "Bal")]
        public decimal Bal
        {
            get; set;
        } // numeric(18,2)

        [Display(Name = "Op Bal")]
        public decimal OpBal
        {
            get; set;
        } // numeric(18,2)

        [Display(Name = "Op Debit")]
        public decimal OpDebit
        {
            get; set;
        } // numeric(18,2)

        [Display(Name = "Op Credit")]
        public decimal OpCredit
        {
            get; set;
        } // numeric(18,2)

        [Display(Name = "Comp Id")]
        public int CompId
        {
            get; set;
        } // int

        [Display(Name = "Year Id")]
        public int YearId
        {
            get; set;
        } // int

        [Display(Name = "Share")]
        public decimal Share
        {
            get; set;
        } // numeric(8,2)


        [Display(Name = "Acc Row Id")]
        public Guid? AccRowId
        {
            get; set;
        } // uniqueidentifier

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

        [MaxLength(50)]
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
        public string Others
        {
            get; set;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RowId
        {
            get; set;
        }

        [NotMapped]
        public string DrCr
        {

            get; set;

        }

        [NotMapped]
        public decimal OpAmt
        {
            get; set;
        }

        [Display(Name = "Modify User")]
        public string ModifyUser
        {
            get; set;
        }

        [Display(Name = "Audit")]
        public bool Audit
        {
            get; set;
        }

        [ForeignKey("GroupId")]
        public virtual AcGroupModel AcGroup { get; set; }

        // dbo.AccBal.AccId -> dbo.Acc.Id (FK_AccBal_Acc)
        [ForeignKey("AccId")]
        public virtual AccModel Acc { get; set; }

        // dbo.AccBal.CompId -> dbo.Company.Id (FK_AccBal_Company)
        [ForeignKey("CompId")]
        public virtual CompModel Company { get; set; }

        // dbo.AccBal.YearId -> dbo.FinYear.Id (FK_AccBal_FinYear)
        [ForeignKey("YearId")]
        public virtual FinYearModel FinYear { get; set; }
    }
}
