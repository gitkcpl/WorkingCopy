using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Reports
{
    [Table("rep_cols")]
    public class RepColumn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; }
        public  int ReportId { get; set; }
        public string ColName { get; set; }

        public int SysOrder { get; set; }
        public  int UserOrder { get; set; }
        [MaxLength(50)]
        public  string SysHead { get; set; }
        [MaxLength(50)]
        public  string UserHead { get; set; }

        public bool Show { get; set; }

        public  decimal Width { get; set; }

        [MaxLength(200)]
        public string FileName { get; set; }
    }
}
