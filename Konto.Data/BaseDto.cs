using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data
{
    public class BaseDto
    {
        
        public virtual int Id { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Modify Date")]
        public DateTime? ModifyDate { get; set; }

        
        [Display(Name = "Create User")]
        public string CreateUser { get; set; }

       
        [Display(Name = "Modify User")]
        public string ModifyUser { get; set; }
    }
}
