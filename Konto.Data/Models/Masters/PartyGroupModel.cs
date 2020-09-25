using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("PartyGroup")]
    public class PartyGroupModel : AuditedEntity
    {
        public PartyGroupModel()
        {
            IsActive = true;
            
        }
        [MaxLength(75)]
        [MinLength(2)]
        [Required(ErrorMessage = "Group Name is required")]
        [Display(Name = "Group Name")]
        public string GroupName
        {
            get;set;
        } // varchar(75)

        [MaxLength(200)]
        [Display(Name = "Address")]
        public string Address
        {
            get; set;
        } // varchar(200)

        [MaxLength(25)]
        [Display(Name = "Contact No")]
        public string ContactNo
        {
            get; set;
        } // varchar(25)


        [Display(Name = "Remark")]
        public string Remark
        {
            get; set;
        } // varchar(200)

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1
        {
            get; set;
        } // varchar(50)

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2
        {
            get; set;
        } // varchar(50)
    }
}
