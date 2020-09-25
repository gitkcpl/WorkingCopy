using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("BtoB")]
    public class BtoBModel : AuditedEntity
    {
        public BtoBModel()
        {
            IsActive = true;
        }

        [Required(ErrorMessage = "Ref Code is required")]
        [Display(Name = "Ref Code")]
        public Guid RefCode { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [MaxLength(1)]
        [Display(Name = "Bill Clear")]
        public string BillClear { get; set; }

        [MaxLength(15)]
        [Display(Name = "Trans Type")]
        public string TransType { get; set; }


        [Display(Name = "Bill Voucher Id")]
        public int? BillVoucherId { get; set; }

        [Display(Name = "Company Id")]
        public int? CompanyId { get; set; }

        [Display(Name = "Bill Id")]
        public int? BillId { get; set; }

        [Display(Name = "Bill Trans Id")]
        public int? BillTransId { get; set; }

        [Display(Name = "Ref Id")]
        [Index]
        public int? RefId { get; set; }

        [Display(Name = "Ref Trans Id")]
        public int? RefTransId { get; set; }

        [Display(Name = "Ref Voucher Id")]
        public int RefVoucherId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Bill No")]
        public string BillNo { get; set; }

        public decimal? Adlp1 { get; set; }

        public decimal? Adla1 { get; set; }

        public decimal? Adlp2 { get; set; }

        public decimal? Adla2 { get; set; }

        public decimal? Adlp3 { get; set; }

        public decimal? Adla3 { get; set; }

        public decimal? Adlp4 { get; set; }


        public decimal? Adla4 { get; set; }


        public decimal? Adlp5 { get; set; }

        public decimal? Adla5 { get; set; }

        public decimal? Adlp6 { get; set; }

        public decimal? Adla6 { get; set; }

        public decimal? Adlp7 { get; set; }

        public decimal? Adla7 { get; set; }

        public decimal? Adlp8 { get; set; }

        public decimal? Adla8 { get; set; }

        public decimal? Adlp9 { get; set; }

        public decimal? Adla9 { get; set; }

        public decimal? Adlp10 { get; set; }

        public decimal? Adla10 { get; set; }
    }
}
