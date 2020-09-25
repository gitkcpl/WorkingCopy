using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("SpPara")]
    public class SpParaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

        [MaxLength(200)]
        [Display(Name = "Sp Name")]
        public virtual string SpName { get; set; }

        [MaxLength(200)]
        [Display(Name = "Para Name")]
        public virtual string ParaName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Para Type")]
        public virtual string ParaType { get; set; }

        [MaxLength(200)]
        [Display(Name = "Default Value")]
        public virtual string DefaultValue { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }
    }

}
