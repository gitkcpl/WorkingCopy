using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("Attachment")]
    public class AttachmentModel : AuditedEntity
    {
        public AttachmentModel()
        {
            IsActive = true;
           // Id = 0;
        }
        public int RefVoucherId { get; set; }
        public int VoucherId { get; set; }

        [MaxLength]
        [MinLength(2)]
        [Required(ErrorMessage = "FilePath is required")]
        [Display(Name = "FilePath")]
        public string FilePath { get; set; }

        [MaxLength]
        public string FileDescr { get; set; }

        public int FileCatId { get; set; }

        public int FileSubCatId { get; set; }

        [MaxLength(50)]
        [MinLength(2)]
        public string FileStatus { get; set; }

        public int DeptId { get; set; }

        public int TeamId { get; set; }

        public DateTime? PublishDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        [MaxLength(200)]
        [MinLength(2)]
        public string KeyWords { get; set; }
    }
}
