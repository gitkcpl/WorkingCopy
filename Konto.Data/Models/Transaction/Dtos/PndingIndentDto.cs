using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingIndentDto
    {
        public virtual string RequestNo { get; set; }

        public virtual int VoucherDate { get; set; }

        public virtual DateTime RequestDate { get; set; }

      

        public virtual string Product { get; set; }

        public virtual int? ProductId { get; set; }

        public virtual int? ColorId { get; set; }

        
        public virtual int? DivisionId { get; set; }

        public virtual int? DesignId { get; set; }

        public virtual int? GradeId { get; set; }

        public virtual int? UomId { get; set; }

        public virtual decimal Qty { get; set; }
        public virtual decimal PendQty { get; set; }
        public virtual decimal Rate { get; set; }
      
        public virtual decimal Total { get; set; }

        public virtual string Unit { get; set; }
        public virtual int? TransId { get; set; }

        public virtual int Id { get; set; }

        public virtual int VoucherId { get; set; }

        public virtual string Remarks { get; set; }

        public virtual string ColorName { get; set; }

        public virtual string DesignNo { get; set; }

        public virtual string GradeName { get; set; }

        public virtual string RefNo { get; set; }
        public virtual string RequestBy { get; set; }

        public virtual DateTime RequireDate { get; set; }

        public virtual string Division { get; set; }

        public   virtual decimal Sgst { get; set; }
        
        public virtual decimal Cgst { get; set; }
        public virtual decimal Igst { get; set; }
    }
}
