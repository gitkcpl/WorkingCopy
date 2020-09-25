using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Nop")]
    public class NopModule : AuditedEntity
    {
        [MaxLength(300)]
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Descr
        {
            get;set;
        } // varchar(100)

        [MaxLength(15)]
        public string SecCode
        {
            get; set;
        } // varcahr(15)

        [MaxLength(15)]
        public string SecNo
        {
            get; set;
        } // varcahr(15)

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1
        {
            get; set;
        } // varchar(50)

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2
        {
            get; set;
        } // varcha
    }
}
