using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("VoucherType")]
    public class VoucherTypeModel : AuditedEntity
    {
        public VoucherTypeModel()
        {
            IsActive = true;
        }

        [MaxLength(50)]
        [Display(Name = "Type Name")]
        public virtual string TypeName { get; set; }

        [MaxLength]
        [Display(Name = "Terms")]
        public virtual string Terms { get; set; }

        [Display(Name = "Send Sms")]
        public virtual bool? SendSms { get; set; }

        [MaxLength(300)]
        [Display(Name = "Sms Templates")]
        public virtual string SmsTemplates { get; set; }

        [Display(Name = "Sms To Party")]
        public virtual bool? SmsToParty { get; set; }

        [Display(Name = "Sms To Agent")]
        public virtual bool? SmsToAgent { get; set; }

        [Display(Name = "Sms To User")]
        public virtual bool? SmsToUser { get; set; }

        [MaxLength(100)]
        [Display(Name = "Other Mobile")]
        public virtual string OtherMobile { get; set; }

        [Display(Name = "Send Mail")]
        public virtual bool? SendMail { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email Sub")]
        public virtual string EmailSub { get; set; }

        [MaxLength]
        [Display(Name = "Email Body")]
        public virtual string EmailBody { get; set; }

        [Display(Name = "Email To Party")]
        public virtual bool? EmailToParty { get; set; }

        [Display(Name = "Email To Agent")]
        public virtual bool? EmailToAgent { get; set; }

        [Display(Name = "Email To User")]
        public virtual bool? EmailToUser { get; set; }

        [MaxLength(100)]
        [Display(Name = "Other Email")]
        public virtual string OtherEmail { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }


    }



    [Table("Voucher_Party")]
    public class VoucherPartyModel : AuditedEntity
    {

        [Display(Name = "Voucher Type Id")]
        public virtual int? VoucherTypeId { get; set; }

        [Display(Name = "Group Id")]
        [Required]
        public virtual int? GroupId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [ForeignKey("VoucherTypeId")]
        public virtual VoucherTypeModel VoucherType { get; set; }

        [ForeignKey("GroupId")]
        public virtual AcGroupModel AcGroup { get; set; }
    }

    [Table("Voucher_Book")]
    public class VoucherBookModel : AuditedEntity
    {
        [Display(Name = "Voucher Type Id")]
        public virtual int? VoucherTypeId { get; set; }

        [Display(Name = "Group Id")]
        [Required]
        public virtual int? GroupId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [ForeignKey("VoucherTypeId")]
        public virtual VoucherTypeModel VoucherType { get; set; }

        // dbo.Vocher_Party.GroupId -> dbo.AcGroup.Id (FK_Vocher_Party_acgroup)
        [ForeignKey("GroupId")]
        public virtual AcGroupModel AcGroup { get; set; }

    }

    [Table("Voucher_Item")]
    public class VoucherItemModel : AuditedEntity
    {
        [Display(Name = "Voucher Type Id")]
        public virtual int? VoucherTypeId { get; set; }

        [Display(Name = "P Type Id")]
        [Required]
        public virtual int? PTypeId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [ForeignKey("VoucherTypeId")]
        public virtual VoucherTypeModel VoucherType { get; set; }

        // dbo.Voucher_Item.PTypeId -> dbo.ProductType.Id (FK_Vocher_Item_ProductType)
        [ForeignKey("PTypeId")]
        public virtual ProductTypeModel ProductType { get; set; }
    }

}