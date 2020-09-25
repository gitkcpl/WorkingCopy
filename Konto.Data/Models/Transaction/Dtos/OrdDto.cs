using System;
using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class OrdDto : BaseDto
    {
        public OrdDto()
        {
            OrderType = "Revenew";
        }
      
        public int CompId { get; set; }
        public int YearId { get; set; }
        public int VoucherId { get; set; }
        public int VoucherDate { get; set; }
        public string VoucherNo { get; set; }
        public string RefNo { get; set; }
        public int AccId { get; set; }
        public int OrderStatusId { get; set; }
        public int EmpId { get; set; }
       
        public int AgentId { get; set; }

        public int TransportId { get; set; }
     
        public int BranchId { get; set; }
        
        public DateTime RequireDate { get; set; }
       
        public string OrderType { get; set; }

        public int? PGroupId { get; set; }
        public int TypeId { get; set; }
        
        public string Remarks { get; set; }

        public string SpecialNotes { get; set; }

        public int PayTermsId { get; set; }
       
        public decimal TotalPcs { get; set; }
       
        public decimal TotalQty { get; set; }
       
        public decimal TotalAmount { get; set; }
        public string Extra1 { get; set; }

    }


    public class OrdTransDto
    {
        public int Id { get; set; }
        public int OrdId { get; set; }

        [Required]
        [Range(1, 9999999,ErrorMessage ="Product Name is Required")]
        public int ProductId { get; set; }
        public int DesignId { get; set; }
        public int GradeId { get; set; }
        public int ColorId { get; set; }

        public string ProductName { get; set; }

        public string ColorName { get; set; }

        public string DesignNo { get; set; }

        public string GradeName { get; set; }

        public int LotPcs { get; set; }

        [Required]
        [Display(Name = "Avg Wt")]
        public decimal AvgWt { get; set; }

        [Required]
        [Display(Name = "Width")]
        public decimal Width { get; set; }

        [Required]
        [Display(Name = "Cut")]
        public decimal Cut { get; set; }

        [Required]
        [Display(Name = "No Of Lot")]
        public int NoOfLot { get; set; }

        [Required]
        [Display(Name = "Qty")]
        [Range(0.1, 999999999,ErrorMessage ="Qty Required")]
        public decimal Qty { get; set; }

        [Required]
        [Display(Name = "Uom Id")]
        [Range(1, 999)]
        public int UomId { get; set; }

        [Required]
        [Display(Name = "Rate")]
        [Range(0.0000, 999999999)]
        public decimal Rate { get; set; }

        [Required]
        [Display(Name = "Total")]
        [Range(0.0000, 99999999999)]
        public decimal Total { get; set; }

        [Required]
        [Display(Name = "Disc")]
        [Range(0.0000, 99)]
        public decimal Disc { get; set; }

        [Required]
        [Display(Name = "Disc Amt")]
        [Range(0.0000, 999999999)]
        public decimal DiscAmt { get; set; }

        [Required]
        [Display(Name = "Sgst")]
        [Range(0.0000, 99)]
        public decimal Sgst { get; set; }

        [Required]
        [Display(Name = "Sgst Amt")]
        [Range(0.0000, 999999999)]
        public decimal SgstAmt { get; set; }

        [Required]
        [Display(Name = "Cgst")]
        [Range(0.0000, 99)]
        public decimal Cgst { get; set; }

        [Required]
        [Display(Name = "Cgst Amt")]
        [Range(0.0000, 999999999)]
        public decimal CgstAmt { get; set; }

        [Required]
        [Display(Name = "Igst")]
        [Range(0.0000, 99)]
        public decimal Igst { get; set; }

        [Required]
        [Display(Name = "Igst Amt")]
        [Range(0.0000, 999999999)]
        public decimal IgstAmt { get; set; }

        [Required]
        [Display(Name = "Cess")]
        [Range(0.0000, 99)]
        public decimal Cess { get; set; }

        [Required]
        [Range(0.0000, 999999999)]
        public decimal CessAmt { get; set; }

        [Required]
        [Range(0.0000, 99999999999)]
        public decimal NetTotal { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        [MaxLength(200)]
        public string CommDescr { get; set; }

        [Required]
        [Display(Name = "Dept Id")]
        public int DeptId { get; set; }

        [Required]
        [Display(Name = "Division Id")]
        public int DivisionId { get; set; }

        [MaxLength(10)]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Priority Required")]
        public string Priority { get; set; }

        [MaxLength(25)]
        public string OrdStatus { get; set; }

        
        public int RefId { get; set; }

        
        public int RefVoucherId { get; set; }

        [Display(Name = "Warp Item Id")]
        public int? WarpItemId { get; set; }

        [MaxLength(200)]
        [Display(Name = "Cancel Reason")]
        public string CancelReason { get; set; }



    }
}
