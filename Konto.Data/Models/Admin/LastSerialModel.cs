using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    [Table("LastSerial")]
    public class LastSerialModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BranchId { get; set; }
        [MaxLength(50)]
        public string BranchCode { get; set; }
        public int VoucherId { get; set; }
        public int YearId { get; set; }
        public int CompId { get; set; }
        [MaxLength(50)]
        public string Last_Serial { get; set; }
    }
}
