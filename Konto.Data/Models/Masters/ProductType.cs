using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("ProductType")]
    public class ProductTypeModel : AuditedEntity
    {
        public ProductTypeModel()
        {
            IsActive = true;
        }

        [MaxLength(15)]
        [Display(Name = "Type Code")]
        public virtual string TypeCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "Type Name")]
        public virtual string TypeName { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "SysType")]
        public string SysType { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }


    }
}
