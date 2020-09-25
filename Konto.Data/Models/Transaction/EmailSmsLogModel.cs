using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Transaction
{
    [Table("EmailSmsLog")]
    public class EmailSmsLogModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public virtual int Id { get; set; }

       
        [Required]
        [Display(Name = "Row Id")]
        public virtual Guid RowId { get; set; }

        [MaxLength(2)]
        [Display(Name = "Trans Type")]
        public virtual string TransType { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Email To")]
        public virtual string EmailTo { get; set; }

        [MaxLength(200)]
        [Display(Name = "Sms To")]
        public virtual string SmsTo { get; set; }

        [MaxLength(200)]
        [Display(Name = "Email Sub")]
        public virtual string EmailSub { get; set; }

        [MaxLength]
        [Display(Name = "Email Body")]
        public virtual string EmailBody { get; set; }

        [MaxLength]
        [Display(Name = "Emal Header")]
        public virtual string EmalHeader { get; set; }

        [MaxLength]
        [Display(Name = "Email Footer")]
        public virtual string EmailFooter { get; set; }

        [MaxLength(500)]
        [Display(Name = "Sms Msg")]
        public virtual string SmsMsg { get; set; }

        [Display(Name = "Schedule Time")]
        public virtual DateTime? ScheduleTime { get; set; }

        [MaxLength(50)]
        [Display(Name = "User Name")]
        public virtual string UserName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Company Name")]
        public virtual string CompanyName { get; set; }

        [Required]
        [Display(Name = "Sended")]
        public virtual bool Sended { get; set; }

        [Display(Name = "Send Time")]
        public virtual DateTime? SendTime { get; set; }

        [MaxLength]
        [Display(Name = "Response Msg")]
        public virtual string ResponseMsg { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Attach File")]
        public virtual string AttachFile { get; set; }

        [MaxLength(100)]
        [Display(Name = "From Mail Id")]
        public virtual string FromMailId { get; set; }

        [MaxLength(50)]
        [Display(Name = "From Mail Pass")]
        public virtual byte[] FromMailPass { get; set; }
    }

  

}
