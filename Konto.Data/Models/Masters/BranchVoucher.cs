using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("branch_voucher")]
    public class BranchVoucher: AuditedEntity
    {
        
       
        public virtual int BranchId { get; set; }
        public virtual int SaleVoucherId { get; set; }
        public virtual int SaleReturnVoucherId { get; set; }
        public virtual int PurchaseVoucherId { get; set; }
        public virtual int StockTransferVoucherId { get; set; }
        public virtual int ReceiptVoucherId { get; set; }
        public virtual int PaymentVoucherId { get; set; }
        public virtual int CrDrNoteVoucherId { get; set; }

        [ForeignKey("BranchId")]
        public virtual BranchModel Branch { get; set; }
    }
}
