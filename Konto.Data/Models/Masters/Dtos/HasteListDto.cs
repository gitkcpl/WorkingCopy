using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class HasteListDto : BaseDto
    {
        [Display(Name = "Description")]
        public string HasteName
        {
            get; set;
        } // varchar(50)


        [Display(Name = "Address1")]
        public string Address1
        {
            get; set;
        } // varchar(100)


        [Display(Name = "Address2")]
        public string Address2
        {
            get; set;
        } // varchar(100)


        [Display(Name = "Mobile No")]
        public string MobileNo
        {
            get; set;
        } // varchar(15)


        [Display(Name = "Email")]
        public string Email
        {
            get; set;
        } // varchar(50)


        [Display(Name = "Aadhar No")]
        public string AadharNo
        {
            get; set;
        } // varchar(15)


        [Display(Name = "Pan No")]
        public string PanNo
        {
            get; set;
        } // varchar(15)


        [Display(Name = "Remark")]
        public string Remark
        {
            get; set;
        } // varchar(max)
    }
}
