using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("CustomRep")]
    public class CustomRepModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

      
        [Required]
        [Display(Name = "Rep Code")]
        public virtual Guid RepCode { get; set; }


        [MaxLength(50)]
        [Display(Name = "Report Types")]
        public virtual string ReportTypes { get; set; }

        [Display(Name = "Rep Id")]
        public virtual int? RepId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Field Name")]
        public virtual string FieldName { get; set; }

        [Required]
        [Display(Name = "Show")]
        public virtual bool Show { get; set; }

        [Required]
        [Display(Name = "Order Index")]
        public virtual int OrderIndex { get; set; }

        [MaxLength(50)]
        [Display(Name = "Heading")]
        public virtual string Heading { get; set; }

        [Required]
        [Display(Name = "Width")]
        public virtual int Width { get; set; }

        [Required]
        [Display(Name = "Group Index")]
        public virtual int GroupIndex { get; set; }

        [Required]
        [Display(Name = "Sort Index")]
        public virtual int SortIndex { get; set; }

        [Required]
        [Display(Name = "Summary")]
        public virtual bool Summary { get; set; }

        [Required]
        [Display(Name = "Group Summary")]
        public virtual bool GroupSummary { get; set; }

        [MaxLength(50)]
        [Display(Name = "Summary Type")]
        public virtual string SummaryType { get; set; }

        [MaxLength(50)]
        [Display(Name = "Appearance")]
        public virtual string Appearance { get; set; }

        [MaxLength(500)]
        [Display(Name = "Header Text")]
        public virtual string HeaderText { get; set; }

        [MaxLength(500)]
        [Display(Name = "Footer Text")]
        public virtual string FooterText { get; set; }
    }

}
