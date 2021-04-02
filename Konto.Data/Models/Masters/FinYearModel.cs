using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("FinYear")]
    public class FinYearModel : AuditedEntity
    {
        public FinYearModel()
        {
            IsActive = true;
        }
        [MaxLength(50)]
        [Display(Name = "Year Code")]
        public string YearCode
        {
            get;set;
        } // varchar(50)

       
        public int? FromDate
        {
            get; set;
        } // int

        
        public int? ToDate
        {
            get; set;
        } // int

        [Display(Name = "From Date")]
        public DateTime FDate
        {
            get; set;
        } // datetime2(7)

        [Display(Name = "To Date")]
        public DateTime TDate
        {
            get; set;
        } // datetime2(7)

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1
        {
            get; set;
        } // varchar(100)

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2
        {
            get; set;
        } // varchar(50)

        public int PrevYearId { get; set; }
      //  public string DbName { get; set; }
       // public string NextYearDbName { get; set; }

    }
}
