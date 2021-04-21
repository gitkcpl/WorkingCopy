using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{

    [Table("sales_pay")]
    public class sales_pay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Sales Pay ID")]
        public virtual long SalesPayID { get; set; }

        [Display(Name = "Sales ID")]
        public virtual long? SalesID { get; set; }

        [Display(Name = "Payment Date")]
        public virtual int? PaymentDate { get; set; }

        [Display(Name = "Cash Ac ID")]
        public virtual long? CashAcID { get; set; }

        [Display(Name = "Bank Ac ID")]
        public virtual long? BankAcID { get; set; }

        [Display(Name = "Cash Amt")]
        public virtual decimal? CashAmt { get; set; }

        [Display(Name = "Card Amt")]
        public virtual decimal? CardAmt { get; set; }

        [MaxLength(20)]
        [Display(Name = "Card No")]
        public virtual string CardNo { get; set; }

        [Display(Name = "Receipt Amt")]
        public virtual decimal? ReceiptAmt { get; set; }

        [Display(Name = "Cash ID")]
        public virtual long? CashID { get; set; }

        [Display(Name = "Bank ID")]
        public virtual long? BankID { get; set; }

        [Display(Name = "Company Id")]
        public virtual long? CompanyId { get; set; }

        [Display(Name = "Disc Ac ID")]
        public virtual long? DiscAcID { get; set; }

        [Display(Name = "Disc Amt")]
        public virtual decimal? DiscAmt { get; set; }

        [Display(Name = "Account ID")]
        public virtual long? AccountID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Pay Mode")]
        public virtual string PayMode { get; set; }

        [Display(Name = "So ID")]
        public virtual long? SoID { get; set; }

        [Display(Name = "Sales Ret ID")]
        public virtual long? SalesRetID { get; set; }

        [ForeignKey("SalesID")]
        public virtual Sale Sale { get; set; }

        [ForeignKey("CashAcID")]
        public virtual account Account { get; set; }

        [ForeignKey("BankAcID")]
        public virtual account Account1 { get; set; }

        [ForeignKey("SoID")]
        public virtual So So { get; set; }

        [ForeignKey("SalesRetID")]
        public virtual SaleRet SalesRet { get; set; }
    }

}
