using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class LedgerGroupLookupDto 
    {

        public virtual string DisplayText { get; set; }
        public virtual string Nature { get; set; }
     
        public virtual bool OpBalanceReq { get; set; }

       
        public virtual bool AgentReq { get; set; }

       
        public virtual bool TransportReq { get; set; }

      
        public virtual bool AddressReq { get; set; }

      
        public virtual bool TaxationReq { get; set; }

       
        public virtual bool SalesmanReq { get; set; }

       
        public virtual bool BankDetailReq { get; set; }

      
        public virtual bool PartyGroupReq { get; set; }

       
        public virtual bool PriceLevelReq { get; set; }

       
        public virtual bool CollDaysReq { get; set; }

       
        public virtual bool IntAccountReq { get; set; }

       
        public virtual bool DeprAccountReq { get; set; }

      
        public virtual bool TcsReq { get; set; }

       
        public virtual bool TdsReq { get; set; }

      
        public virtual bool TaxTypeReq { get; set; }

       
        public virtual bool CrLimitReq { get; set; }

       
        public virtual bool GradeReq { get; set; }

       
        public virtual string Extra1 { get; set; }

        [Key]
        public virtual int Id { get; set; }
    }
}
