using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class GreyPendingOrderDto
    {
        public virtual string orderNo { get; set; }

        public virtual DateTime OrderDate { get; set; }
        public virtual int VoucherDate { get; set; }

        public virtual string Product { get; set; }

        public virtual decimal OrderQty { get; set; }

        public virtual decimal PendingQty { get; set; }

        public virtual int ProductId { get; set; }

        public virtual int? ColorId { get; set; }

        public virtual decimal? Cut { get; set; }

        public virtual int? DesignId { get; set; }

        public virtual int? GradeId { get; set; }

        public virtual int UomId { get; set; }

        public virtual int NoOfLot { get; set; }

        public virtual decimal rate { get; set; }

        public virtual decimal Disc { get; set; }

        public virtual int TransId { get; set; }

        public virtual int Id { get; set; }

        public virtual int VoucherId { get; set; }

        public virtual string Remarks { get; set; }

        public virtual decimal Sgst { get; set; }

        public virtual decimal Cgst { get; set; }

        public virtual decimal Igst { get; set; }

    }
}
