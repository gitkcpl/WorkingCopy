using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class SmsMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SMSId { get; set; }

        [MaxLength(50)]
        public string UserId { get; set; }

        [MaxLength(50)]
        public string UserPass { get; set; }

        [MaxLength(500)]
        public string PromotionalAPI { get; set; }

        [MaxLength(500)]
        public string TransactionalAPI { get; set; }

        [MaxLength(15)]
        public string MobileNo { get; set; }

        [MaxLength(1000)]
        public string MessageField { get; set; }

        [MaxLength(50)]
        public string UserIdField { get; set; }

        [MaxLength(50)]
        public string PasswordField { get; set; }

        [MaxLength(30)]
        public string SenderCode { get; set; }

        [MaxLength(100)]
        public string EmailId { get; set; }

        public long? CompanyID { get; set; }

        public int? UnitID { get; set; }

        public int? AddUserId { get; set; }

        public int? EditUserId { get; set; }

        public DateTime? TransDate { get; set; }

        [MaxLength(30)]
        public string SenderCodeTrans { get; set; }

    }
}
