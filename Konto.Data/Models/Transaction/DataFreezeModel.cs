using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("data_freeze")]
    public class DataFreezeModel
    {
        [Key]
        public int Id { get; set; }
        public int? FromDate { get; set; }

        public int? ToDate { get; set; }

        public long? VoucherTypeID { get; set; }

        public bool? Freeze { get; set; }

        [MaxLength(10)]
        public string Pass { get; set; }

        public int? CompanyID { get; set; }

        public DateTime? ModifyDate { get; set; }

        [MaxLength(50)]
        public string ModifyUser { get; set; }

    }
}
