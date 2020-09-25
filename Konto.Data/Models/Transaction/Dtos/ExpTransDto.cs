using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class ExpTransDto
    {
        public int Id { get; set; }
        public int? BillId { get; set; }

       
        public string HsnCode { get; set; }

        
        public int? ToAccId { get; set; }

        public string Particular { get; set; }

       
        public int? BatchId { get; set; }

        [Display(Name = "Qty")]
     
        public decimal Qty { get; set; }

        [Display(Name = "Unit")]
        public int? UomId { get; set; }

       
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        public decimal Total { get; set; }

        [Display(Name = "Disc %")]
        [Range(-100, 100)]
        public decimal Disc { get; set; }

        [Display(Name = "Disc Amt")]
       // [Range(0.0000, 999999999)]
        public decimal DiscAmt { get; set; }

        [Display(Name = "Freight Rate")]
       // [Range(0.0000, 999999999)]
        public decimal FreightRate { get; set; }

        [Display(Name = "Freight")]
       // [Range(0.0000, 999999999)]
        public decimal Freight { get; set; }

        [Display(Name = "Other Add")]
        [Range(0.0000, 999999999)]
        public decimal OtherAdd { get; set; }

        [Display(Name = "Other Less")]
        [Range(0.0000, 999999999)]
        public decimal OtherLess { get; set; }

        [Display(Name = "Sgst %")]
       // [Range(0.0000, 99)]
        public decimal SgstPer { get; set; }

        [Display(Name = "Sgst Amt")]
       // [Range(0.0000, 999999999)]
        public decimal Sgst { get; set; }

        [Display(Name = "Cgst %")]
      //  [Range(0.0000, 99)]
        public decimal CgstPer { get; set; }

        [Display(Name = "Cgst Amt")]
       // [Range(0.0000, 999999999)]
        public decimal Cgst { get; set; }

        [Display(Name = "Igst %")]
      //  [Range(0.0000, 99)]
        public decimal IgstPer { get; set; }

        [Display(Name = "Igst Amt")]
      //  [Range(0.0000, 999999999)]
        public decimal Igst { get; set; }

        [Display(Name = "Cess Rate")]
       // [Range(0.0000, 99)]
        public decimal CessPer { get; set; }

        [Display(Name = "Cess Amt")]
       // [Range(0.0000, 999999999)]
        public decimal Cess { get; set; }

        public decimal NetTotal { get; set; }
        public string Remark { get; set; }

        public int ProductId { get; set; }
    }
}
