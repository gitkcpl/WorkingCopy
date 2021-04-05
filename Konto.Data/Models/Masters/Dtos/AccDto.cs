using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class AccDto
    {
        public int Id { get; set; }
        public virtual string AccName { get; set; }

        public virtual string PrintName { get; set; }

        public virtual int? GroupId { get; set; }

        public virtual int? PGroupId { get; set; }

        public virtual string TdsReq { get; set; }

        public virtual string TcsReq { get; set; }

        public virtual string VatTds { get; set; }

        public virtual string IoTax { get; set; }

        public virtual int? DeducteeId { get; set; }

        public virtual int? NopId { get; set; }

        public virtual int? CrDays { get; set; }

        public virtual decimal? CrLimit { get; set; }

        public virtual string BToB { get; set; }

        public virtual int? AgentId { get; set; }

        public virtual int? TransportId { get; set; }

        public virtual int? EmpId { get; set; }

        public virtual string AadharNo { get; set; }

        public virtual string GstIn { get; set; }

        public virtual DateTime? GSTDate { get; set; }

        public virtual string PanNo { get; set; }

        public virtual string Extra1 { get; set; }

        public virtual string Extra2 { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual string Grade { get; set; }

        public virtual string CollDay { get; set; }

        public virtual int? CollById { get; set; }
        public virtual decimal DiscPer { get; set; }

    }
}
