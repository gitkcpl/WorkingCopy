using Konto.Data.Models.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Gstn
{
    [Table("hsn_master")]
    public class HsnMaster :AuditedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual int Id { get; set; }

        [MaxLength(50)]
        public virtual string HsnCode { get; set; }

        [MaxLength(100)]
        public virtual string HsnDescr { get; set; }

        [MaxLength(50)]
        public virtual string Extra1 { get; set; }
        [MaxLength(50)]
        public virtual string   Extra2 { get; set; }

       
    }


    [Table("hsn_trans")]
    public class HsnTrans
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Range(1,99999)]
        public virtual int HsnMasterId { get; set; }

        public virtual DateTime ApplyDate { get; set; }
        public virtual int TaxMasterId { get; set; }

        [ForeignKey("TaxMasterId")]
        public virtual TaxModel TaxMaster { get; set; }

        [ForeignKey("HsnMasterId")]
        public virtual HsnMaster HsnMaster { get; set; }
    }
}
