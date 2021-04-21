using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    [Table("AuditLog")]
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

       
        [MaxLength(50)]
        public string EntityName { get; set; }

        [MaxLength(50)]
        public string PropertyName { get; set; }

        public string PrimaryKeyValue { get; set; }

        [MaxLength]
        public string OldValue { get; set; }

        [MaxLength]
        public string NewValue { get; set; }

        public DateTime? DateChanged { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string EntryMode { get; set; }

    }
}
