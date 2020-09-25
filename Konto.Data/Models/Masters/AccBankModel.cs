using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("AccBank")]
    public class AccBankModel 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id
        {
            get;set;
        } // int

        [Display(Name = "Acc Id")]
        public int? AccId
        {
            get; set;
        } // int

        [MaxLength(50)]
        [Display(Name = "Bank Name")]
        [Required]
        public string BankName
        {
            get; set;
        } // varchar(50)

        [MaxLength(50)]
        [Display(Name = "Branch Name")]
        public string BranchName
        {
            get; set;
        } // varchar(50)

        [MaxLength(100)]
        [Display(Name = "Address")]
        public string Address
        {
            get; set;
        } // varchar(100)

        [MaxLength(15)]
        [Display(Name = "Ifs Code")]
        
        public string IfsCode
        {
            get; set;
        } // varchar(15)

        [MaxLength(50)]
        [Display(Name = "Account No")]
        [Required]
        public string AccountNo
        {
            get; set;
        } // varchar(50)


        [Required]
        [Display(Name = "Row Id")]
        public Guid RowId
        {
            get; set;
        } // uniqueidentifier

        [Display(Name = "Is Deleted")]
        public bool IsDeleted
        {
            get; set;
        }

        [Display(Name = "Modify User")]
        public string ModifyUser
        {
            get; set;
        }
        // dbo.AccBank.AccId -> dbo.Acc.Id (FK_AccBank_Acc)
        [ForeignKey("AccId")]
        public virtual AccModel Acc { get; set; }

    }
}
