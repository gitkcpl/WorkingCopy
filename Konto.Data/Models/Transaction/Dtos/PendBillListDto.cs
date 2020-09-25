using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class PendBillListDto 
    {
        public PendBillListDto()
        {
            IsChecked = false;
            Adla1 = 0;
            Adla2 = 0;
            Adla3 = 0;
            Adla4 = 0;
            Adla5 = 0;
            Adla6 = 0;
            Adla7 = 0;
            Adla8 = 0;
            Adla9 = 0;
            Adla10 = 0;
        }
        public string ChallanNo { get; set; }
        public DateTime? ChallanDate
        {
            get
            {
                if (ChlnDate != null)
                { return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }

        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string BillNo { get; set; }
        public string Supplier { get; set; }
        public string VoucherName { get; set; }

        public decimal? TotalQty { get; set; }
        public decimal? TotalAmt { get; set; }
        public decimal? Gross { get; set; }

        public string Transport { get; set; }
        public string PayTerm { get; set; }
        public string RefNo { get; set; }
        public int? SupplierID { get; set; }
        public decimal? Total { get; set; }
        public decimal? Disc { get; set; }
        public decimal? DiscAmt { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? SgstAmt { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? CgstAmt { get; set; }
        public decimal? Igst { get; set; }
        public decimal? IgstAmt { get; set; }
        public decimal? Cess { get; set; }
        public decimal? CessAmt { get; set; }
        public decimal? FreightRate { get; set; }
        public decimal? Freight { get; set; }
        public decimal? OtherAdd { get; set; }
        public decimal? OtherLess { get; set; }
        public decimal? Tds { get; set; }
        public decimal? TdsAmt { get; set; }
        public decimal? NetTotal { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? RetAmt { get; set; }
        public decimal? DueAmt { get; set; }
        public string ItemRemark { get; set; }
        public int? RefTransId { get; set; }
        public bool? IsChecked { get; set; }

        public DateTime? RcdDate { get; set; }
        public int? ToAccId { get; set; }
        public int? RefAcId { get; set; }
        public string Remark { get; set; }
        public Guid RefCode { get; set; }
        public int? BillId { get; set; }
        public int? BillVoucherId { get; set; }
        public int? BillTransId { get; set; }
        public decimal? Amount { get; set; }
        public int? Id { get; set; }
        public int? TransId { get; set; }
        public int? VoucherId { get; set; }
        public int? ChlnDate { get; set; }
        public decimal Adlp1 { get; set; }
        public decimal Adla1 { get; set; }
        public decimal Adlp2 { get; set; }
        public decimal Adla2 { get; set; }
        public decimal Adlp3 { get; set; }
        public decimal Adla3 { get; set; }
        public decimal Adlp4 { get; set; }
        public decimal Adla4 { get; set; }
        public decimal Adlp5 { get; set; }
        public decimal Adla5 { get; set; }
        public decimal Adlp6 { get; set; }
        public decimal Adla6 { get; set; }
        public decimal Adlp7 { get; set; }
        public decimal Adla7 { get; set; }
        public decimal Adlp8 { get; set; }
        public decimal Adla8 { get; set; }
        public decimal Adlp9 { get; set; }
        public decimal Adla9 { get; set; }
        public decimal Adlp10 { get; set; }
        public decimal Adla10 { get; set; }

    }
}
