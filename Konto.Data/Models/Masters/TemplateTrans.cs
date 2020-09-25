using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("TemplateTrans")]
    public class TemplateTrans : AuditedEntity
    {
        [Display(Name = "Template Id")]
        public virtual int? TemplateId { get; set; }

        [Display(Name = "Temp Field Id")]
        public virtual int? TempFieldId { get; set; }

        [Display(Name = "Col No")]
        public virtual int? ColNo { get; set; }

        [NotMapped]
        public virtual string FieldName { get; set; }

        [ForeignKey("TemplateId")]
        public virtual TemplateModel Template { get; set; }
    }
}