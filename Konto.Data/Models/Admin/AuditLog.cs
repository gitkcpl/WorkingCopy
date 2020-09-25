using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Admin
{
    [Table("AuditLog")]
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long Id { get; set; }

      
        [MaxLength(50)]
        [Display(Name = "Entity Name")]
        public string EntityName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Property Name")]
        public string PropertyName { get; set; }

        [Display(Name = "Primary Key Value")]
        public int? PrimaryKeyValue { get; set; }

        [MaxLength]
        [Display(Name = "Old Value")]
        public string OldValue { get; set; }

        [MaxLength]
        [Display(Name = "New Value")]
        public string NewValue { get; set; }

        [Display(Name = "Date Changed")]
        public DateTime? DateChanged { get; set; }

        [MaxLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Entry Mode")]
        public string EntryMode { get; set; }

        public int MenuId { get; set; }
    }

}
