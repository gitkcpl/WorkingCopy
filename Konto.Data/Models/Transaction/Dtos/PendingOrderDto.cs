using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingOrderDto
    {
        public virtual string VoucherNo { get; set; }

        public virtual int VoucherDate { get; set; }

        public virtual DateTime VouchDate { get; set; }

        public virtual int DelvAccId { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual int DelvAdrId { get; set; }

        public virtual string CityName { get; set; }

        public virtual int TransportId { get; set; }

        public virtual int? TotalPcs { get; set; }

        public virtual decimal? TotalQty { get; set; }

        public virtual decimal? PendingQty { get; set; }

        public virtual string Product { get; set; }

        public virtual int? ProductId { get; set; }

        public virtual int? ColorId { get; set; }

        public virtual decimal? Cut { get; set; }

        public virtual int? DivisionId { get; set; }

        public virtual int? DesignId { get; set; }

        public virtual int? GradeId { get; set; }

        public virtual int? UomId { get; set; }

        public virtual int? LotPcs { get; set; }

        public virtual decimal? NetTotal { get; set; }

        public virtual int? NoOfLot { get; set; }

        public virtual decimal? rate { get; set; }

        public virtual decimal? Sgst { get; set; }

        public virtual decimal? SgstAmt { get; set; }

        public virtual decimal Cgst { get; set; }

        public virtual decimal CgstAmt { get; set; }

        public virtual decimal Igst { get; set; }

        public virtual decimal IgstAmt { get; set; }

        public virtual decimal? Total { get; set; }

        public virtual decimal? Disc { get; set; }

        public virtual decimal? DiscAmt { get; set; }

        public virtual int? TransId { get; set; }

        public virtual int Id { get; set; }

        public virtual int VoucherId { get; set; }

        public virtual string Remarks { get; set; }

        public virtual string ColorName { get; set; }

        public virtual string DesignNo { get; set; }

        public virtual string GradeName { get; set; }

        public virtual string RefNo { get; set; }

        public virtual string Party { get; set; }
    }
}
