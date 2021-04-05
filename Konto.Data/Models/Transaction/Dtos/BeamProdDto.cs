using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class BeamProdDto
    {

        public virtual int Id { get; set; }
        public int? TransId { get; set; }
        public int? RefId { get; set; }

        public int? SrNo { get; set; }
        public int? ProductId { get; set; }
        public int? GradeId { get; set; }
        public int? BatchId { get; set; }
        public int? ColorId { get; set; }
        public int? CompId { get; set; }
        public int? YearId { get; set; }
        public int? VoucherId { get; set; }
        public int VoucherDate { get; set; }

        public string VoucherNo { get; set; }
        public int? PalletProductId { get; set; }
        public int? Cops { get; set; }
        public int? Ply { get; set; } //length
        public decimal? GrossWt { get; set; }
        public decimal? TareWt { get; set; }
        public decimal NetWt { get; set; }
        public decimal? AvgWt { get; set; }
        public decimal? CurrQty { get; set; }
        public decimal? FinQty { get; set; }
        public decimal? CopsRate { get; set; }
        public decimal? BoxRate { get; set; }
        public decimal? CopsWt { get; set; }
        public decimal CartnWt { get; set; }
        public string ProdStatus { get; set; }
        public string LotNo { get; set; }

        public bool? IsOk { get; set; }

        public int? Tops { get; set; }
        public int? PlyProductId { get; set; }
        public int? BranchId { get; set; }

        public int? ProdOutId { get; set; }
        public string ChallanNo { get; set; }
        public DateTime? VDate
        {
            get
            {
                if (VoucherDate > 10101)
                { return (DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }

        //used in Taka opening for Voucher date 
        public DateTime? VouDate
        {
            get; set;
        }

        public string Weaver { get; set; }

        public int? RefTransId { get; set; }

        public string Extra1 { get; set; }
        public decimal? Qty { get; set; }

        public int? BoxProductId { get; set; }

        public string ProductName { get; set; }
        public string DesignNo { get; set; }
        public string ColorName { get; set; }
        public int? DivId { get; set; }
        public bool? IsClose { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? IssueRefId { get; set; }
        public int? IssueRefVoucherId { get; set; }
        public DateTime? LoadingDate { get; set; }
        public string MachineName { get; set; }
        public string Remark { get; set; }
        public int? MacId { get; set; }
        public int? PackId { get; set; }
        public int? Pallet { get; set; }
        public int? SubGradeId { get; set; }
        public string TwistType { get; set; }
        public string YarnName { get; set; }
        public string MendorName { get; set; }
        public string BatchName { get; set; }
        public string GradeName { get; set; }
        public string BoxItem { get; set; }
        public string SubGradeName { get; set; }
        public decimal? BoxWt { get; set; }
    }
}