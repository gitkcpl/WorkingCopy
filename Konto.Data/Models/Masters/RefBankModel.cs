using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("RefBank")]
    public class RefBankModel : AuditedEntity
    {
        public RefBankModel()
        {
            IsActive = true;
            Id = 0;
        }

        [MaxLength(50)]
        [MinLength(2)]
        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [MaxLength(200)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }
    }
}
