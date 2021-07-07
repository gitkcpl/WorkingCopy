using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class GrnProdDto 
    {
        public virtual int Id { get; set; }
        public int? TransId { get; set; }
        public int? RefId { get; set; }

        [Required]
        public int? SrNo { get; set; }
        public int? ProductId { get; set; }
        public int? GradeId { get; set; }
        public int? BatchId { get; set; }
        public int? ColorId { get; set; }
        public int? CompId { get; set; }
        public int? YearId { get; set; }
        public int? VoucherId { get; set; }
        public int VoucherDate { get; set; }
        
        [Required]
        public string VoucherNo { get; set; }
        public int? PalletProductId { get; set; }
        public int Cops { get; set; }
        public decimal GrossWt { get; set; }
        public decimal TareWt { get; set; }

        [Required][Range(0.00001, double.MaxValue,ErrorMessage ="Qty Must Be Greater Than Zero")]
        public decimal NetWt { get; set; }
        public int Ply { get; set; } //length
        public string ProdStatus { get; set; }
        public string LotNo { get; set; }
        public decimal CurrQty { get; set; }
        public decimal FinQty { get; set; }
        public bool IsOk { get; set; }
        public decimal CopsRate { get; set; }
        public int Tops { get; set; }
        public int? PlyProductId { get; set; }
        public int? BranchId { get; set; }

        public int ProdOutId { get; set; }
        public string ChallanNo { get; set; }
        public DateTime? VDate
        {
            get
            {
                if ( VoucherDate > 10101)
                { return (DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }
        public string Weaver { get; set; }

        public string ColorName { get; set; }
        public int RefTransId { get; set; }

        public string Extra1 { get; set; }

        public int IssueRefVoucherId { get; set; }
    }
}
