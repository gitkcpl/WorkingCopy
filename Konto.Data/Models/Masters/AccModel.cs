using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("Acc")]
    public class AccModel : AuditedEntity
    {
        public AccModel()
        {
            
            IsActive = true;
            VatTds = "NA";
            TdsReq = "No";
            TcsReq = "No";
            IoTax = "NA";
            BToB = "Yes";
            NopId = 1;
            DeducteeId = 1;
            PGroupId = 1;
            EmpId = 1;
            AgentId = 1;
            TransportId = 1;
        }

        [MaxLength(100)]
        [MinLength(2)]
        [Required]
        [Display(Name = "Acc Name")]
        public virtual string AccName { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Print Name")]
        public virtual string PrintName { get; set; }

        [Display(Name = "Group Id")]
        public virtual int? GroupId { get; set; }

        [Display(Name = "Party Group")]
        public virtual int? PGroupId { get; set; }

        [MaxLength(3)]
        [Display(Name = "Tds Req")]
        [Required]
        public virtual string TdsReq { get; set; }

        [MaxLength(3)]
        [Display(Name = "Tcs Req")]
        [Required]
        public virtual string TcsReq { get; set; }

        [MaxLength(5)]
        [Display(Name = "Vat Tds")]
        [Required]
        [DefaultValue("NA")]
        public virtual string VatTds { get; set; }

        [MaxLength(6)]
        [Display(Name = "Io Tax")]
        [Required]
        public virtual string IoTax { get; set; }

        [Display(Name = "Deductee Id")]
        [Required]
        public virtual int DeducteeId { get; set; }

        [Required]
        public virtual int NopId { get; set; }

        [Display(Name = "Cr Days")]
        public virtual int CrDays { get; set; }

        [Display(Name = "Cr Limit")]
        public virtual decimal CrLimit { get; set; }

        [MaxLength(3)]
        [Display(Name = "BToB")]
        [Required]
        public virtual string BToB { get; set; }

        [Display(Name = "Agent Id")]
        public virtual int AgentId { get; set; }

        [Display(Name = "Transport Id")]
        public virtual int TransportId { get; set; }

        [Display(Name = "Emp Id")]
        public virtual int EmpId { get; set; }

        [MaxLength(12)]
        [Display(Name = "Aadhar No")]
        public virtual string AadharNo { get; set; }

        [MaxLength(15)]
        [Display(Name = "Gst In")]
        public virtual string GstIn { get; set; }

        [Display(Name = "GST Date")]
        [Column(TypeName = "datetime")]
        public virtual DateTime? GSTDate { get; set; }

        [MaxLength(10)]
        [Display(Name = "Pan No")]
        public virtual string PanNo { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }

        public virtual string Grade { get; set; }

        public virtual string CollDay { get; set; }

        public virtual int? CollById { get; set; }

        public virtual decimal DiscPer { get; set; }
        public int AddressId
        {
            get; set;
        }

        // dbo.Acc.GroupId -> dbo.AcGroup.Id (FK_Acc_AcGroup)
        [ForeignKey("GroupId")]
        public virtual AcGroupModel AcGroup { get; set; }

        // dbo.Acc.PGroupId -> dbo.PartyGroup.Id (FK_Acc_PartyGroup)
        [ForeignKey("PGroupId")]
        public virtual PartyGroupModel PartyGroup { get; set; }

        // dbo.Acc.DeducteeId -> dbo.Deductee.Id (FK_Acc_Deductee)
        [ForeignKey("DeducteeId")]
        public virtual DeducteeModel Deductee { get; set; }

        // dbo.Acc.NopId -> dbo.Nop.Id (FK_Acc_Nop)
        [ForeignKey("NopId")]
        public virtual NopModule Nop { get; set; }

        public virtual ICollection<AccAddressModel> AccAddresses { get; set; }

        // dbo.AccBank.AccId -> dbo.Acc.Id (FK_AccBank_Acc)
        public virtual ICollection<AccBankModel> AccBanks { get; set; }

        public virtual EmpModel Emp { get; set; }

    }
}
