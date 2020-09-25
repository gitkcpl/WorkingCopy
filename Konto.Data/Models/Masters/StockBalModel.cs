using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("ProductBal")]
    public class StockBalModel 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Company Id")]
        public int CompanyId { get; set; }

        [Display(Name = "Year Id")]
        public int YearId { get; set; }

        [Display(Name = "Div Id")]
        public int? DivId { get; set; }

        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Godown Id")]
        public int GodownId { get; set; }

        [Display(Name = "Branch Id")]
        public int BranchId { get; set; }

        [Display(Name = "Item Code")]
        public Guid? ItemCode { get; set; }

        [Display(Name = "Op Nos")]
        public int OpNos { get; set; }

        [Display(Name = "Op Qty")]
        public decimal OpQty { get; set; }

        [Display(Name = "Issue No")]
        public int IssueNo { get; set; }

        [Display(Name = "Issue Qty")]
        public decimal IssueQty { get; set; }

        [Display(Name = "Rcpt Nos")]
        public int RcptNos { get; set; }

        [Display(Name = "Rcpt Qty")]
        public decimal RcptQty { get; set; }

        [Display(Name = "Bal Nos")]
        public int BalNos { get; set; }

        [Display(Name = "Bal Qty")]
        public decimal BalQty { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Stock Value")]
        public decimal StockValue { get; set; }

        [MaxLength(50)]
        [Display(Name = "Lot No")]
        public string LotNo { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Modify Date")]
        public DateTime? ModifyDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Create User")]
        public string CreateUser { get; set; }

        [MaxLength(50)]
        [Display(Name = "Modify User")]
        public string ModifyUser { get; set; }

        [MaxLength(100)]
        [Display(Name = "Ip Address")]
        public string IpAddress { get; set; }


        [Display(Name = "Row Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RowId { get; set; }

        // dbo.ProductBal.ProductId -> dbo.Product.Id (FK_ProductBal_Product)
        [ForeignKey("ProductId")]
        public virtual ProductModel Product { get; set; }

        // dbo.ProductBal.CompId -> dbo.Company.Id (FK_ProductBal_Company)
        [ForeignKey("CompanyId")]
        public virtual CompModel Company { get; set; }
    }
}
