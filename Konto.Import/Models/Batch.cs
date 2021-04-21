using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Batch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long BatchID { get; set; }

        public long? VoucherID { get; set; }

        public int? VoucherDate { get; set; }

        [MaxLength(25)]
        public string VoucherNo { get; set; }

        public long? FinItemID { get; set; }

        public long? CompanyID { get; set; }

        [MaxLength(100)]
        public string Remark { get; set; }

        public int? UnitID { get; set; }

        public int? DivisionID { get; set; }

        public long? ColorID { get; set; }

        [MaxLength(50)]
        public string CrossSection { get; set; }


    }
}
