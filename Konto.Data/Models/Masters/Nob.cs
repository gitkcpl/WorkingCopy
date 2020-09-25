using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Nob")]
    public class NobModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "Business Type")]
        public virtual string BusinessType { get; set; }

        [MaxLength]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }
    }

}
