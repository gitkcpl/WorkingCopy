using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    public class PatiaRegDto
    {
        public virtual int Id { get; set; }

        public virtual DateTime? EntryDate { get; set; }

        public virtual DateTime? ProdDate { get; set; }

        public virtual string TakaNo { get; set; }

        public virtual int? CompId { get; set; }

        public virtual decimal TotalMtrs { get; set; }

        public virtual decimal NightMtrs { get; set; }

        public virtual decimal DayMtrs { get; set; }

        public virtual int? TotalTaka { get; set; }

        public virtual int? Lengths { get; set; }

        public virtual int? Ends { get; set; }

        public virtual string BeamNo { get; set; }

        public virtual int? ProductId { get; set; }

        public virtual string Quality { get; set; }

        public virtual decimal TakaMeters { get; set; }

        public virtual decimal TakaWeight { get; set; }

        public virtual int? GradeId { get; set; }

        public virtual int? ColorId { get; set; }

        public virtual int? MachineID { get; set; }

        public virtual string MachineNo { get; set; }

        public virtual int EmpId { get; set; }

        public virtual string WorkerName { get; set; }

        public virtual decimal JobRate { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual int PeId { get; set; }

    }
}
