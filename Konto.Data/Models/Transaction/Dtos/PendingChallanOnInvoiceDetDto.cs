using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingChallanOnInvoiceDetDto
    {
        public virtual string ChallanNo { get; set; }

        public virtual int? OrdId { get; set; }

        public virtual int? ChlnDate { get; set; }

        public virtual string BillNo { get; set; }

        public virtual DateTime? RcdDate { get; set; }

        public virtual int? TransportId { get; set; }

        public virtual decimal? TotalQty { get; set; }

        public virtual string Product { get; set; }

        public virtual string OrderNO { get; set; }

        public virtual int? OrdDate { get; set; }

        public virtual int ProductId { get; set; }

        public virtual int? ColorId { get; set; }

        public virtual int? GradeId { get; set; }

        public virtual decimal Disc { get; set; }

        public virtual decimal DiscAmt { get; set; }

        public virtual decimal Cgst { get; set; }

        public virtual decimal CgstAmt { get; set; }

        public virtual decimal Sgst { get; set; }

        public virtual decimal SgstAmt { get; set; }

        public virtual decimal Igst { get; set; }

        public virtual decimal IgstAmt { get; set; }

        public virtual decimal FreightRate { get; set; }

        public virtual decimal Freight { get; set; }

        public virtual decimal Cess { get; set; }

        public virtual decimal CessAmt { get; set; }

        public virtual decimal OtherAdd { get; set; }

        public virtual decimal OtherLess { get; set; }

        public virtual int? UomId { get; set; }

        public virtual int? Pcs { get; set; }

        public virtual decimal? Total { get; set; }

        public virtual decimal? NetTotal { get; set; }

        public virtual string LotNo { get; set; }

        public virtual decimal? Qty { get; set; }

        public virtual decimal? Rate { get; set; }

        public virtual int? TransId { get; set; }

        public virtual int Id { get; set; }

        public virtual int? VoucherId { get; set; }

        public virtual string DocNo { get; set; }

        public virtual DateTime? DocDate { get; set; }

        public virtual string VehicleNo { get; set; }

        public virtual string ItemRemark { get; set; }

        public virtual string Remark { get; set; }

        public virtual string DesignNo { get; set; }

        public virtual string GradeName { get; set; }

        public virtual string ColorName { get; set; }

        public virtual int? DesignId { get; set; }

        public DateTime? ChallanDate
        {
            get
            {
                if (ChlnDate != null)
                { return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
            set { }
        }
        
        public DateTime? OrderDate
        {
            get
            {
                if (OrdDate != null)
                { return (DateTime.ParseExact(OrdDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
            set { }
        }
    }
}
