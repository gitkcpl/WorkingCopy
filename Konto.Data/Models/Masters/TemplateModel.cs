using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Template")]
    public class TemplateModel : AuditedEntity
    {
        [Display(Name = "V Type Id")]
        public virtual int? VTypeId { get; set; }

        [MaxLength(500)]
        [Display(Name = "Descriptions")]
        public virtual string Descriptions { get; set; }

        [Display(Name = "Voucher Id")]
        public virtual int? VoucherId { get; set; }

        [Display(Name = "Start Row No")]
        public virtual int? StartRowNo { get; set; }
               
        [Display(Name = "Acc Id")]
        public virtual int? AccId { get; set; }

        public virtual ICollection<TemplateTrans> TemplateTrans { get; set; }
    }

}
