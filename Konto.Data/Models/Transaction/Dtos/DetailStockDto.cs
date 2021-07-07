using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class DetailStockDto
    {
        public virtual bool IsSelected { get; set; }
        public virtual int Id { get; set; }

        public virtual Guid RowId { get; set; }

        public virtual int? TransId { get; set; }

        public virtual int? SrNo { get; set; }

        public virtual string InwardNo { get; set; }

        public virtual string Weaver { get; set; }

        public virtual int? ProductId { get; set; }

        public virtual string YarnName { get; set; }

        public virtual int? GradeId { get; set; }

        public virtual int? ColorId { get; set; }

        public virtual int? VoucherId { get; set; }

        public virtual int? VoucherDate { get; set; }

        public virtual string VoucherNo { get; set; }

        public virtual int? RefId { get; set; }

        public virtual int? Cops { get; set; }

        public virtual decimal? CopsWt { get; set; }

        public virtual int? Ply { get; set; }

        public virtual int? Tops { get; set; }

        public virtual decimal? CopsRate { get; set; }

        public virtual decimal? BoxWt { get; set; }

        public virtual decimal? CartnWt { get; set; }

        public virtual decimal? GrossWt { get; set; }

        public virtual decimal? TareWt { get; set; }

        public virtual decimal? NetWt { get; set; }

        public virtual decimal? Qty { get; set; }
        public virtual decimal? OrgQty { get; set; }

        public virtual int? DivId { get; set; }

        public virtual decimal? CurrQty { get; set; }

        public virtual decimal? FinQty { get; set; }

        public virtual int? IssueRefId { get; set; }

        public virtual int? IssueRefVoucherId { get; set; }

        public virtual string Remark { get; set; }

        public virtual int? PlyProductId { get; set; }

        public virtual int VTypeId { get; set; }

        public virtual string LotNo { get; set; }

        public virtual string ColorName { get; set; }

        public DateTime? VDate
        {
            get
            {
                if (VoucherDate != null)
                { return (DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }

        public virtual decimal? Rate { get; set; }
        public virtual int? UomId { get; set; }

        public virtual string RefNo { get; set; }

        public virtual string Extra1 { get; set; }
        public virtual string LastVoucher { get; set; }
        public virtual string LastParty { get; set; }
    }
}
