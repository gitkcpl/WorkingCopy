using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class ScTransD
    {
        public long ScID { get; set; }

        public long? ScTransID { get; set; }

        public long? TakaID { get; set; }

        public decimal Meters { get; set; }

        public long? ColorID { get; set; }

        public long CompanyID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ScTalaID { get; set; }

        public long? TexProdID { get; set; }

        public int? GradeID { get; set; }

        public decimal? GrossWeight { get; set; }

        public decimal? TareWeight { get; set; }

        public decimal? NetWeight { get; set; }

        public short? Cops { get; set; }

        public long? BatchID { get; set; }

        public int? MachineID { get; set; }

        public int? PackTypeID { get; set; }

        public long? ItemID { get; set; }

        [MaxLength(25)]
        public string TakaNo { get; set; }

        [MaxLength(25)]
        public string TransType { get; set; }

        public int? SubGradeID { get; set; }

        public ScTrans sctranss { get; set; }
    }
}
