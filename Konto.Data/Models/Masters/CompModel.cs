using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("Company")]
    public class CompModel : AuditedEntity
    {
        public CompModel()
        {
            IsActive = true;
        }

        [MaxLength(75)]
        [Display(Name = "Comp Name")]
        public virtual string CompName { get; set; }

        [MaxLength(75)]
        [Display(Name = "Print Name")]
        public virtual string PrintName { get; set; }

        [MaxLength(15)]
        [Display(Name = "Sort Name")]
        public virtual string SortName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address1")]
        public virtual string Address1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address2")]
        public virtual string Address2 { get; set; }

        [Display(Name = "City Id")]
        public virtual int? CityId { get; set; }

        [MaxLength(15)]
        [Display(Name = "Pincode")]
        public virtual string Pincode { get; set; }

        [Display(Name = "State Id")]
        public virtual int? StateId { get; set; }

        [MaxLength(100)]
        [Display(Name = "F Address1")]
        public virtual string FAddress1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "F Address2")]
        public virtual string FAddress2 { get; set; }

        [Display(Name = "F City Id")]
        public virtual int? FCityId { get; set; }

        [MaxLength(15)]
        [Display(Name = "F Pincode")]
        public virtual string FPincode { get; set; }

        [Display(Name = "F State Id")]
        public virtual int? FStateId { get; set; }

        [MaxLength(10)]
        [Display(Name = "Mobile")]
        public virtual string Mobile { get; set; }

        [MaxLength(50)]
        [Display(Name = "Phone")]
        public virtual string Phone { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email")]
        public virtual string Email { get; set; }

        [MaxLength(50)]
        [Display(Name = "Website")]
        public virtual string Website { get; set; }

        [MaxLength(25)]
        [Display(Name = "Para")]
        public virtual string Para { get; set; }

        [MaxLength(20)]
        [Display(Name = "Gst In")]
        public virtual string GstIn { get; set; }

        [MaxLength(20)]
        [Display(Name = "Pan No")]
        public virtual string PanNo { get; set; }

        [MaxLength(20)]
        [Display(Name = "Aadhar No")]
        public virtual string AadharNo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Tds Ac No")]
        public virtual string TdsAcNo { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [MaxLength(25)]
        [Display(Name = "Ac No")]
        public virtual string AcNo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Bank Name")]
        public virtual string BankName { get; set; }

        [MaxLength(500)]
        [Display(Name = "Holy World")]
        public virtual string HolyWorld { get; set; }

        [MaxLength(50)]
        [Display(Name = "Ifs Code")]
        public virtual string IfsCode { get; set; }

        [MaxLength(100)]
        [Display(Name = "Insurance")]
        public virtual string Insurance { get; set; }

        [MaxLength(50)]
        [Display(Name = "Send From")]
        public virtual string SendFrom { get; set; }

        [MaxLength(50)]
        [Display(Name = "Send Pass")]
        public virtual string SendPass { get; set; }

        [MaxLength]
        [Display(Name = "Logo Path")]
        public virtual string LogoPath { get; set; }

        [MaxLength]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }

        [Display(Name = "Nob Id")]
        public virtual int? NobId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email Pass")]
        public virtual string EmailPass { get; set; }

        [MaxLength(5000)]
        [Display(Name = "Promotional API")]
        public virtual string PromotionalAPI { get; set; }

        [MaxLength(500)]
        [Display(Name = "Transactional API")]
        public virtual string TransactionalAPI { get; set; }

        [MaxLength(50)]
        public virtual string   GstInUserId { get; set; }

        [MaxLength(50)]
        public virtual string EwayBillUserId { get; set; }

        [MaxLength(50)]
        public virtual string EwayBillPassword { get; set; }

        [MaxLength(50)]
        public string AppKey { get; set; }

        [MaxLength(50)]
        public string AuthToken { get; set; }

        [MaxLength(50)]
        public string SEK { get; set; }

        public DateTime? TokenExp { get; set; }

        public virtual ICollection<BranchModel> Branches { get; set; }

    }
}
