using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendingLotDto
    {
        public PendingLotDto()
        {
            IsChecked = false;
        }
        public virtual bool IsSelected { get; set; }
        public int Id { get; set; }
        public int TransId { get; set; }
        public int VoucherId { get; set; }
        public string VoucherNo { get; set; }
        public string LotNo { get; set; }
        public string ChallanNo { get; set; }
        public int? VoucherDate { get; set; }
        public DateTime? VouchDate
        {
            get
            {
                if (VoucherDate != null)
                { return (DateTime.ParseExact(VoucherDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }

        public string AccName { get; set; }
        public string ProductName { get; set; }
        public int Pcs { get; set; }
        public int ProductId { get; set; }
        public int AccId { get; set; }
        public int UomId { get; set; }
        public bool? IsChecked { get; set; }
        public decimal? Qty { get; set; }
        public string remark { get; set; }

    }
}
