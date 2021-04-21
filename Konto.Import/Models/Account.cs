using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Keysoft.Erp.Data.Models
{
    
    public class account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "account Id is required")]
        public long account_Id { get; set; } // bigint, not null

        [MaxLength(100)]
        public string account_name { get; set; } // varchar(100), null

        [MaxLength(100)]
        public string print_name { get; set; } // varchar(100), null

        public long? party_group { get; set; } // bigint, null

        public long? group_id { get; set; } // bigint, null

        [MaxLength(50)]
        public string lst_no { get; set; } // varchar(50), null

        [MaxLength(50)]
        public string cst_no { get; set; } // varchar(50), null

        public string remark { get; set; } // text, null

        [Required(ErrorMessage = "sel is required")]
        public bool sel { get; set; } // bit, not null

        [MaxLength(200)]
        public string address { get; set; } // varchar(200), null

        public long? account_city { get; set; } // bigint, null

        public long? account_area { get; set; } // bigint, null

        [MaxLength(50)]
        public string contact_person { get; set; } // varchar(50), null

        [MaxLength(8)]
        public string pin_code { get; set; } // varchar(8), null

        [MaxLength(50)]
        public string telo { get; set; } // varchar(50), null

        [MaxLength(50)]
        public string mobile { get; set; } // varchar(50), null

        [MaxLength(50)]
        public string fax { get; set; } // varchar(50), null

        [MaxLength(50)]
        public string email { get; set; } // varchar(50), null

        [MaxLength(50)]
        public string website { get; set; } // varchar(50), null

        public long? trans_id { get; set; } // bigint, null

        public long? agent_id { get; set; } // bigint, null

        [MaxLength(50)]
        public string pan_no { get; set; } // varchar(50), null

        [Required(ErrorMessage = "cr days is required")]
        public int cr_days { get; set; } // int, not null

        [Required(ErrorMessage = "cr limit is required")]
        public decimal cr_limit { get; set; } // numeric(28,2), not null

        [Required(ErrorMessage = "dr limit is required")]
        public decimal dr_limit { get; set; } // numeric(28,2), not null

        public long? tds_natureid { get; set; } // bigint, null

        public long? deductee_id { get; set; } // bigint, null

        [Required(ErrorMessage = "firm type is required")]
        public long firm_type { get; set; } // bigint, not null

        [MaxLength(5)]
        public string active_ac { get; set; } // varchar(5), null

        [Required(ErrorMessage = "roi is required")]
        public decimal roi { get; set; } // numeric(18,2), not null

        [MaxLength(50)]
        public string srv_tax { get; set; } // varchar(50), null

        [Required(ErrorMessage = "only total is required")]
        public byte only_total { get; set; } // tinyint, not null

        [Required(ErrorMessage = "interest rate is required")]
        public decimal interest_rate { get; set; } // numeric(10,2), not null

        [Required(ErrorMessage = "interest styles is required")]
        public byte interest_styles { get; set; } // tinyint, not null

        [Required(ErrorMessage = "interest balance is required")]
        public byte interest_balance { get; set; } // tinyint, not null

        [Required(ErrorMessage = "prio is required")]
        public int prio { get; set; } // int, not null

        public decimal? dep_Per { get; set; } // numeric(18,2), null

        public short? tb_sort { get; set; } // smallint, null

        public short? tr_sort { get; set; } // smallint, null

        public int? bl_sort { get; set; } // int, null

        [Required(ErrorMessage = "company id is required")]
        public long company_id { get; set; } // bigint, not null

        [Required(ErrorMessage = "account show is required")]
        public byte account_show { get; set; } // tinyint, not null

        [MaxLength(25)]
        public string SortName { get; set; } // varchar(25), null

        public long? AccountTypeID { get; set; } // bigint, null

        [Required(ErrorMessage = "Is TDS is required")]
        public bool IsTDS { get; set; } // bit, not null

        public decimal? TdsPer { get; set; } // numeric(18,2), null

        public decimal? LabourRate { get; set; } // numeric(18,2), null

        [MaxLength(50)]
        public string EccNo { get; set; } // varchar(50), null

        [MaxLength(50)]
        public string RegNo { get; set; } // varchar(50), null

        [MaxLength(50)]
        public string Range { get; set; } // varchar(50), null

        [MaxLength(200)]
        public string RangeAdd { get; set; } // varchar(200), null

        [MaxLength(50)]
        public string Division { get; set; } // varchar(50), null

        public long? MarketID { get; set; } // bigint, null

        [MaxLength(15)]
        public string ShopNo { get; set; } // varchar(15), null

        [MaxLength(2)]
        public string Grade { get; set; } // varchar(2), null

        public decimal? DepreciationRate { get; set; } // numeric(18,2), null

        public long? DepreciationAccountID { get; set; } // bigint, null

        public decimal? ShareRate { get; set; } // numeric(18,2), null

        public byte? Days { get; set; } // tinyint, null

        [MaxLength(10)]
        public string MemberNo { get; set; } // varchar(10), null

        public int? memberDate { get; set; } // int, null

        public int? Ratetype { get; set; } // int, null

        [MaxLength(100)]
        public string BankName { get; set; } // varchar(100), null

        [MaxLength(50)]
        public string Branchname { get; set; } // varchar(50), null

        [MaxLength(25)]
        public string AccountNo { get; set; } // varchar(25), null

        [MaxLength(15)]
        public string Ifscode { get; set; } // varchar(15), null

        [MaxLength(1)]
        public string TexIo { get; set; } // varchar(1), null

        public long? ClosingStockAcID { get; set; } // bigint, null

      //  [ForeignKey("group_id")]
        //public virtual AcGroup Acgroup { get; set; }
    }

}
