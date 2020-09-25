using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("TakaBeam")]
    public class TakaBeamModel : AuditedEntity
    {
        public virtual int? BeamId { get; set; }

        public virtual int? ProdId { get; set; }

        public virtual decimal? Per { get; set; }

        public virtual decimal? Qty { get; set; }

        public virtual decimal? Mtr { get; set; }

    }
}
