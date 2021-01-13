using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction.Dtos
{
    public class BeamStatusByMachineDto
    {
        public virtual int Id { get; set; }

        public virtual Guid RowId { get; set; }

        public virtual int? TransId { get; set; }

        public virtual int? SrNo { get; set; }

        public virtual DateTime? LoadingDate { get; set; }

        public virtual string VoucherNo { get; set; }

        public virtual string TwistType { get; set; }

        public virtual int Cops { get; set; }

        public virtual decimal NetWt { get; set; }

        public virtual decimal? ProdWt { get; set; }

        public virtual decimal? BalWt { get; set; }

        public virtual decimal? TakaProd { get; set; }

        public virtual decimal? Mtrs { get; set; }

        public virtual decimal? ProdMtrs { get; set; }

        public virtual decimal? BalMtrs { get; set; }

        public virtual int? GreyProductId { get; set; }

    }
}
