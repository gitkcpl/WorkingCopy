using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class ReconsileDTO
    {
        public virtual int Id { get; set; }

        public virtual DateTime VoucherDate { get; set; }

        public virtual string VoucherNo { get; set; }

        public virtual int? RefAccountId { get; set; }

        public virtual string Particular { get; set; }

        public virtual decimal Debit { get; set; }

        public virtual decimal Credit { get; set; }

        public virtual string ChqNo { get; set; }

        public virtual Guid? TransCode { get; set; }
        public virtual Guid RefId { get; set; }
        public virtual DateTime? BankDate { get; set; }

        public virtual decimal? Amount { get; set; }

        public virtual string BankName { get; set; }
    }


}
