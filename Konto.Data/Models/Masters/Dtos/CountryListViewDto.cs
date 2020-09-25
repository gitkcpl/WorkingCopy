using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class CountryListViewDto
    {
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

       

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
