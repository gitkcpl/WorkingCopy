using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingReturnableDto
    {
        public virtual string VoucherNo { get; set; }

        public virtual DateTime? IssueDate { get; set; }

        public virtual int VoucherId { get; set; }

        public virtual int? IssuePcs { get; set; }

        public virtual decimal? IssueQty { get; set; }

        public virtual decimal? PendingQty { get; set; }

        public virtual int? PendingPcs { get; set; }

        public virtual string Party { get; set; }

        public virtual int? ColorId { get; set; }

        public virtual decimal? Cut { get; set; }

        public virtual int? DesignId { get; set; }

        public virtual int? GradeId { get; set; }

        public virtual string Product { get; set; }

        public virtual int? ProductId { get; set; }

        public virtual decimal? Total { get; set; }

        public virtual decimal? Sgst { get; set; }

        public virtual decimal? SgstAmt { get; set; }

        public virtual decimal Cgst { get; set; }

        public virtual decimal CgstAmt { get; set; }

        public virtual decimal Igst { get; set; }

        public virtual decimal IgstAmt { get; set; }

        public virtual decimal FreightRate { get; set; }

        public virtual decimal Freight { get; set; }

        public virtual decimal OtherAdd { get; set; }

        public virtual decimal OtherLess { get; set; }

        public virtual decimal Cess { get; set; }

        public virtual decimal CessAmt { get; set; }

        public virtual decimal Disc { get; set; }

        public virtual decimal DiscAmt { get; set; }

        public virtual int? UomId { get; set; }

        public virtual int? Pcs { get; set; }

        public virtual decimal? NetTotal { get; set; }

        public virtual string LotNo { get; set; }

       public virtual string Remark { get; set; }

        public virtual decimal? rate { get; set; }

        public virtual int? TransId { get; set; }

        public virtual int Id { get; set; }

       

        public virtual decimal RcptQty { get; set; }

    }
}
