using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keysoft.Erp.Data.Models
{
    [Table("VatClass")]
    public class VatClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "VatClass id is required")]
        public int VatClassID { get; set; }
        public string VatClassName { get; set; }
        public decimal VatPer { get; set; }
        public decimal AdVatPer { get; set; }
        public string Remark { get; set; }
        public string TaxType { get; set; }
        public decimal SGST { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal AdGST { get; set; }
    }
}
