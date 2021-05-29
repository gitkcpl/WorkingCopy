using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Konto.Data.Models.Masters;

namespace Konto.Data.Models.Admin
{
    [Table("tds_tcs")]
    public class TdsTcs
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual int NopId { get; set; }
        public virtual int DeducteeId { get; set; }
        public virtual int AppliedDate { get; set; }
        public virtual decimal TaxLimit { get; set; }
        public virtual decimal TaxPer { get; set; }
        public virtual decimal SurChargeLimit { get; set; }
        public virtual decimal SurChargePer { get; set; }
        public virtual decimal EduCessPer { get; set; }
        public virtual decimal SecEduCessPer { get; set; }

        public  virtual decimal TaxRateIfNoPan { get; set; }

        [ForeignKey("NopId")]
        public virtual NopModule Nop { get; set; }

        [ForeignKey("DeducteeId")]
        public virtual DeducteeModel Deductee { get; set; }
    }
}
