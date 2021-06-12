using Konto.Data.Models.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("ewbs")]
    public class Ewb : AuditedEntity
    {
        [Index]
        public int RefId { get; set; }
        [Index]
        public int RefVoucherId { get; set; }

        public int DespatchFromId { get; set; }

        [MaxLength(3)]
        public string ModeOfTrans { get; set; }

        [MaxLength(3)]
        public string SubType { get; set; }

        public int ShiptToId { get; set; }

        [MaxLength(10)]
        public string ShipToPin { get; set; }

        [MaxLength(5)]
        public string DocType { get; set; }

        public int Distance { get; set; }

        [MaxLength(10)]
        public string VehicleNo { get; set; }

        [MaxLength(15)]
        public string VehicleType { get; set; }

        [MaxLength(50)]
        public string EwbNo { get; set; }

        public int? TransId { get; set; }

        [MaxLength(50)]
        public string DocNo { get; set; }

        [MaxLength(50)]
        public string DocDate { get; set; }

        [MaxLength(3)]
        public string TransactionType { get; set; }

        [Index]
        public Guid RefRowId { get; set; }



        [ForeignKey("TransId")]
        public AccModel Transport { get; set; }

        [ForeignKey("DespatchFromId")]
        public StateModel DespatchFrom { get; set; }
    }
}
