using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("TaxMaster")]
    public class TaxModel : AuditedEntity
    {
        
        [MaxLength(50)]
        [Display(Name = "Tax Name")]
        public string TaxName { get; set; }

        [MaxLength(25)]
        [Display(Name = "Tax Type")]
        public string TaxType { get; set; }

        [Display(Name = "Sgst")]
        public decimal Sgst { get; set; }

        [Display(Name = "Cgst")]
        public decimal Cgst { get; set; }

        [Display(Name = "Igst")]
        public decimal Igst { get; set; }

        [MaxLength(25)]
        [Display(Name = "Cess Type")]
        public string CessType { get; set; }

        [Display(Name = "Cess")]
        public decimal Cess { get; set; }

        [Display(Name = "Cess Rate")]
        public decimal CessRate { get; set; }

        [MaxLength(100)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }



    }
}
