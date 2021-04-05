using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Gstn
{
    [Table("gstr2a_dump")]
    public class Gstr2ADump
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Index]
        public int Billid { get; set; }
        public int AccId { get; set; }

        [MaxLength(20)]
        public string GstIn { get; set; }

        [MaxLength(50)]
        public string InvoiceNo { get; set; }

        [MaxLength(50)]
        public string Pos { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceValue { get; set; }

        [MaxLength(50)]
        public string FilePeriod { get; set; }
        public DateTime FileDate { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public decimal Cess { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Taxable { get; set; }

        public string TransType { get; set; }

        public string FPrd { get; set; }
        public int CompId { get; set; }
        public int YearId { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Gstr2ATransDump> Gstr2aTrans { get; set; }
    }
}
