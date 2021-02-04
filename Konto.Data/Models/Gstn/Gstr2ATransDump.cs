using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Gstn
{

    [Table("gstr2a_trans_dump")]
    public class Gstr2ATransDump
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MainId { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public decimal Cess { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Taxable { get; set; }

        [ForeignKey("MainId")]
        public Gstr2ADump Gstr2ADump { get; set; }
    }
}
