using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Grade ID is required")]
        public int GradeID { get; set; }

        [MaxLength(50)]
        public string GradeName { get; set; }

        [MaxLength(100)]
        public string Remark { get; set; }

        public int? OrderIndex { get; set; }

    }
}
