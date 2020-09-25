using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("TransType")]
    public class TransType : BaseEntity
    {
        public TransType()
        {
            this.IsActive = true;
        }

        [MaxLength(100)]
        [Display(Name = "Type Name")]
        public string TypeName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }
    }
}
