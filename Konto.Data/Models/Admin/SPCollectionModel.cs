using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    [Table("SPCollection")]
    public class SPCollectionModel 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Section is required")]
        [Display(Name = "Section")]
        public string Section { get; set; }

        [MaxLength(500)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Row Id is required")]
        [Display(Name = "Row Id")]
        public Guid RowId { get; set; }
    }
}
