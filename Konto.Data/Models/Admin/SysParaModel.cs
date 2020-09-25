using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    [Table("SysPara")]
    public class SysParaModel 
    {
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        [Display(Name = "Descr")]
        public string Descr { get; set; }

        [MaxLength(200)]
        [Display(Name = "Default Value")]
        public string DefaultValue { get; set; }

        [MaxLength(200)]
        [Display(Name = "Value Descr")]
        public string ValueDescr { get; set; }

        [MaxLength(50)]
        [Display(Name = "Category")]
        public string Category { get; set; }


        [Required(ErrorMessage = "Row Id is required")]
        [Display(Name = "Row Id")]
        public Guid RowId { get; set; }

        public virtual ICollection<CompParaModel> CompParas { get; set; }

    }


    [Table("CompPara")]
    public class CompParaModel 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Comp Id")]
        public int? CompId { get; set; }

        [Display(Name = "Para Id")]
        public int? ParaId { get; set; }

        [MaxLength(200)]
        [Display(Name = "Para Value")]
        public string ParaValue { get; set; }

        [MaxLength(50)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }


        [Required(ErrorMessage = "Row Id is required")]
        [Display(Name = "Row Id")]
        public Guid RowId { get; set; }

        // dbo.CompPara.CompId -> dbo.Company.Id (FK_CompPara_Company)
        [ForeignKey("CompId")]
        public virtual CompModel Company { get; set; }



        // dbo.CompPara.ParaId -> dbo.SysPara.Id (FK_CompPara_SysPara)
        [ForeignKey("ParaId")]
        public virtual SysParaModel SysPara { get; set; }

    }
}
