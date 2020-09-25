using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class GstSummaryCrossDto
    {
        public virtual string TransType { get; set; }

        public virtual string Books { get; set; }

        public virtual string Hsn { get; set; }

        public virtual decimal? TaxPer { get; set; }

        public virtual decimal? Taxable { get; set; }

        public virtual decimal? Cgst { get; set; }

        public virtual decimal? Sgst { get; set; }

        public virtual decimal? Igst { get; set; }

        public virtual decimal? NetTotal { get; set; }

        public virtual string RegType { get; set; }

        public virtual string GstType { get; set; }

        public virtual string Pos { get; set; }
        public virtual int VoucherDate { get; set; }

        public virtual string VoucherNo { get; set; }

        public virtual string BillNo { get; set; }

        public virtual string Party { get; set; }

        public virtual string GstIn { get; set; }

        public virtual string VoucherName { get; set; }

        public virtual string ProductName { get; set; }

        public virtual string Remark { get; set; }

        public virtual string SupplyType { get; set; }
        public virtual int Id { get; set; }

        public virtual int? TranId { get; set; }


    }
}
