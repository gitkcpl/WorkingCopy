using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.TradingDto
{
    public class JobIssueBarcodeDto
    {
        public virtual string Barcode { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal OrgQty { get; set; }
        public virtual string Product { get; set; }
        public virtual string VoucherNo { get; set; }
        public virtual string ChallanNo { get; set; }
        public virtual string Weaver { get; set; }
        public virtual string Color { get; set; }
        public virtual string LotNo { get; set; }

        public DateTime? VDate
        {
            get
            {
                if (VoucherDate > 10101)
                { return (DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }
        public virtual int Id { get; set; }
        public virtual int? ProdOutId { get; set; }
        public virtual int? TransId { get; set; }
        public virtual int? RefId { get; set; }
        public virtual int? ProductId { get; set; }
        public virtual int? ColorId { get; set; }
        public virtual int VoucherDate { get; set; }
        public virtual int? SrNo { get; set; }
    }
}
