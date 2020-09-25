using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class PartyGroupListDto : BaseDto
    {
        [Display(Name = "Group Name")]
        public string GroupName
        {
            get; set;
        } // varchar(75)

        [Display(Name = "Address")]
        public string Address
        {
            get; set;
        } // varchar(200)

        [Display(Name = "Contact No")]
        public string ContactNo
        {
            get; set;
        } // varchar(25)

        public string Remark
        {
            get; set;
        } // varchar(200)
    }
}
