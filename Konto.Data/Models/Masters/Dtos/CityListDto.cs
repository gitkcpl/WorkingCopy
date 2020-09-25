using System;
using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class CityListDto
    {
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        public string State { get; set; }

        [Display(Name = "State Id")]
        public int StateId { get; set; }

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
