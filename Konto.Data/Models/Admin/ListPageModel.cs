using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    [Table("ListPage")]
    public class ListPageModel 
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [MaxLength(200)]
        [Display(Name = "Descr")]
        public string Descr { get; set; }

        [MaxLength(50)]
        [Display(Name = "Sp Name")]
        public string SpName { get; set; }

        [MaxLength(200)]
        [Display(Name = "Layout File")]
        public string LayoutFile { get; set; }

        [Display(Name = "V Type Id")]
        public int? VTypeId { get; set; }

        [MaxLength(200)]
        [Display(Name = "Group Col")]
        public string GroupCol { get; set; }

        [MaxLength(200)]
        [Display(Name = "Sum Col")]
        public string SumCol { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }
    }
}
