using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("AcGroup")]
    public class AcGroupModel : AuditedEntity
    {
        public AcGroupModel()
        {
            IsActive = true;
        }
        [MaxLength(15)]
        [Display(Name = "Group Code")]
        public virtual string GroupCode { get; set; }

        [MaxLength(50)]
        [Display(Name = "Group Name")]
        public virtual string GroupName { get; set; }

        [Display(Name = "Under Group Id")]
        public virtual int UnderGroupId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Opp Side Name")]
        public virtual string OppSideName { get; set; }

        [MaxLength(30)]
        [Display(Name = "Nature")]
        public virtual string Nature { get; set; }

        [MaxLength]
        [Display(Name = "Remark")]
        public virtual string Remark { get; set; }

        [Display(Name = "Bl Sort")]
        public virtual int BlSort { get; set; }

        [Display(Name = "Tb Sort")]
        public virtual int TbSort { get; set; }

        [Display(Name = "Tr Sort")]
        public virtual int TrSort { get; set; }

        [Display(Name = "Only Total")]
        public virtual bool OnlyTotal { get; set; }

        [Display(Name = "Op Balance Req")]
        public virtual bool OpBalanceReq { get; set; }

        [Display(Name = "Agent Req")]
        public virtual bool AgentReq { get; set; }

        [Display(Name = "Transport Req")]
        public virtual bool TransportReq { get; set; }

        [Display(Name = "Address Req")]
        public virtual bool AddressReq { get; set; }

        [Display(Name = "Taxation Req")]
        public virtual bool TaxationReq { get; set; }

        [Display(Name = "Salesman Req")]
        public virtual bool SalesmanReq { get; set; }

        [Display(Name = "Bank Detail Req")]
        public virtual bool BankDetailReq { get; set; }

        [Display(Name = "Party Group Req")]
        public virtual bool PartyGroupReq { get; set; }

        [Display(Name = "Price Level Req")]
        public virtual bool PriceLevelReq { get; set; }

        [Display(Name = "Coll Days Req")]
        public virtual bool CollDaysReq { get; set; }

        [Display(Name = "Int Account Req")]
        public virtual bool IntAccountReq { get; set; }

        [Display(Name = "Depr Account Req")]
        public virtual bool DeprAccountReq { get; set; }

        [Display(Name = "Tcs Req")]
        public virtual bool TcsReq { get; set; }

        [Display(Name = "Tds Req")]
        public virtual bool TdsReq { get; set; }

        [Display(Name = "Tax Type Req")]
        public virtual bool TaxTypeReq { get; set; }

        [Display(Name = "Cr Limit Req")]
        public virtual bool CrLimitReq { get; set; }

        [Display(Name = "Grade Req")]
        public virtual bool GradeReq { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra1")]
        public virtual string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public virtual string Extra2 { get; set; }


    }
}
