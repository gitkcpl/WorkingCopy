using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class JobReceiptDTO 
    {
        public JobReceiptDTO()
        {
            IsClear = false;
        }

        public int? Id { get; set; }
        public int? ChallanId { get; set; }
        public int? TransId { get; set; }
        public int? RefId { get; set; }
        public int? VoucherId { get; set; }
        public int? ChlnDate { get; set; }
        public DateTime? ChallanDate
        {
            get
            {
                if (ChlnDate != null)
                { return (DateTime.ParseExact(ChlnDate.ToString(), "yyyyMMdd", CultureInfo.CurrentCulture)); }
                else return null;
            }
        }
        public string ChallanNo { get; set; }

        public decimal? IssueQty { get; set; }
        public decimal? IssuePcs { get; set; }
        public decimal? PendingQty { get; set; }
        public decimal? PendingPcs { get; set; }
        public decimal? CurrPendingQty { get; set; }
        public decimal? CurrPendingPcs { get; set; }

        public decimal? Qty { get; set; }
        public decimal? Pcs { get; set; }

        public string Product { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public string Color { get; set; }
        public int? DesignId { get; set; }
        public string Design { get; set; }
        public decimal Rate { get; set; }
        public decimal Cgst { get; set; }
        public decimal CgstPer { get; set; }
        public decimal Sgst { get; set; }
        public decimal SgstPer { get; set; }
        public decimal Igst { get; set; }
        public decimal IgstPer { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsClear { get; set; }
        public string Jobcardno { get; set; }
        public int? JobId { get; set; }
        public int PTypeId { get; set; }

        public string Barcode { get; set; }

        public decimal SaleRate { get; set; }
        public decimal BulkRate { get; set; }
        public decimal SemiBulkRate { get; set; }
    }
}
