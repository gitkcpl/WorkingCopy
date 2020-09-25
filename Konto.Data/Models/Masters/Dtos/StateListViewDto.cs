using System;
using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class StateListViewDto 
    {
       
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        public string Country { get; set; }

        [Display(Name = "Country Id")]
        public int CountryId { get; set; }

        
        [Display(Name = "Gst Code")]
        public string GstCode { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Modify Date")]
        public DateTime? ModifyDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Create User")]
        public string CreateUser { get; set; }

        [MaxLength(50)]
        [Display(Name = "Modify User")]
        public string ModifyUser { get; set; }

        public int Id { get; set; }
    }
}
