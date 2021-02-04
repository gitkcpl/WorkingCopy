using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("Voucher")]
    public class VoucherModel : AuditedEntity
    {
        [MaxLength(50)]
        [Display(Name = "Voucher Name")]
        public virtual string VoucherName { get; set; }

        [MaxLength(4)]
        [Display(Name = "Sort Name")]
        public virtual string SortName { get; set; }

        [Display(Name = "V Type Id")]
        public virtual int VTypeId { get; set; }

        [Display(Name = "Ref Voucher Id")]
        public virtual int? RefVoucherId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }

        [Display(Name = "Book Ac Id")]
        public virtual int? BookAcId { get; set; }

        [ForeignKey("VTypeId")]
        public virtual VoucherTypeModel VoucherType { get; set; }


        public virtual ICollection<VchSetupModel> VchSetups { get; set; }
    }

    [Table("VchSetup")]
    public class VchSetupModel : AuditedEntity
    {
        [Display(Name = "Voucher Id")]
        public virtual int VoucherId { get; set; }

        [Display(Name = "Comp Id")]
        public virtual int CompId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Invoice Heading")]
        public virtual string InvoiceHeading { get; set; }

        [Display(Name = "Vch Width")]
        public virtual int VchWidth { get; set; }

        [Display(Name = "Pre Fill Zero")]
        public virtual bool PreFillZero { get; set; }

        [Display(Name = "Start From")]
        public virtual int StartFrom { get; set; }

        [Display(Name = "Increment")]
        public virtual int Increment { get; set; }

        [MaxLength(25)]
        [Display(Name = "Serial Mask")]
        public virtual string Serial_Mask { get; set; }

        [Display(Name = "Max Value")]
        public virtual int? Max_Value { get; set; }

        [Display(Name = "Last Serial")]
        public virtual int Last_Serial { get; set; }

        [Display(Name = "Fy Reset")]
        public virtual bool FyReset { get; set; }

        [Display(Name = "Print After Save")]
        public virtual bool PrintAfterSave { get; set; }

        [Display(Name = "Email After Save")]
        public virtual bool EmailAfterSave { get; set; }

        [Display(Name = "Sms After Save")]
        public virtual bool SmsAfterSave { get; set; }

        [Display(Name = "Book Fix")]
        public virtual bool BookFix { get; set; }

        [Display(Name = "Acc Id")]
        public virtual int? AccId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }

        public virtual bool ManualSeries { get; set; }

        [ForeignKey("VoucherId")]
        public virtual VoucherModel Voucher { get; set; }

        // dbo.VchSetup.CompId -> dbo.Company.Id (FK_VchSetup_Company)
        [ForeignKey("CompId")]
        public virtual CompModel Company { get; set; }
    }
}
