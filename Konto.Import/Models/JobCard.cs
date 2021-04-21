using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class JobCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobCardID { get; set; }

        public int? JobCardDate { get; set; }

        [MaxLength(10)]
        public string JobCardNo { get; set; }

        public int? MachineID { get; set; }

        [MaxLength(1)]
        public string JobCardType { get; set; }

        public long? AccountID { get; set; }

        public long? FinishItemID { get; set; }

        public long? ColorID { get; set; }

        public decimal? OrderQty { get; set; }

        public long? RawItemID { get; set; }

        [MaxLength(50)]
        public string LotNo { get; set; }

        public short? NoOfCones { get; set; }

        public decimal? Qty { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Amount { get; set; }

        public long? OrderTransID { get; set; }

        public long? CompanyID { get; set; }

        public int? AddUserID { get; set; }

        public int? EditUserID { get; set; }

        public int? SessionID { get; set; }

        public DateTime? TransDate { get; set; }

        [MaxLength(30)]
        public string IpAddr { get; set; }

        public long? PcTransID { get; set; }

        public int? UnitID { get; set; }

        public int? DivisionID { get; set; }

        public long? OperatorID { get; set; }

        [MaxLength(1)]
        public string DyeingType { get; set; }

        [MaxLength(10)]
        public string ChallanNo { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        public long? GradeID { get; set; }

        [MaxLength(1)]
        public string JobClose { get; set; }

        [MaxLength(50)]
        public string CarrierNo { get; set; }

        [MaxLength(50)]
        public string ProgramNo { get; set; }

        public decimal GrossWt { get; set; }

        public decimal CarrierWt { get; set; }

        public long? SpringID { get; set; }

        public decimal SpringWt { get; set; }


    }
}
