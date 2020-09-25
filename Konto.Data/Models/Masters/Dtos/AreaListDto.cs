using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class AreaListDto
    {
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }

        public string City { get; set; }

        [Display(Name = "City Id")]
        public int CityId { get; set; }

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
