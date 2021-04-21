using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Company ID")]
        public long CompanyID { get; set; }

        [MaxLength(200)]
       
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [MaxLength(200)]
       
        [Display(Name = "Print Name")]
        public string PrintName { get; set; }

        [Required]
        [Display(Name = "Company Group")]
        public int CompanyGroup { get; set; }

        [MaxLength(500)]
       
        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; }

        [MaxLength(100)]
       
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [MaxLength(100)]
       
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [MaxLength(50)]
       
        [Display(Name = "E Mail")]
        public string EMail { get; set; }

        [MaxLength(50)]
      
        [Display(Name = "Web Site")]
        public string WebSite { get; set; }

        [Required]
        [Display(Name = "Year From")]
        public int YearFrom { get; set; }

        [MaxLength(15)]
      
        [Display(Name = "Company Password")]
        public string CompanyPassword { get; set; }

        [Display(Name = "Business Type")]
        public int? BusinessType { get; set; }

        [MaxLength(100)]
       
        [Display(Name = "Lst No")]
        public string LstNo { get; set; }

        [MaxLength(100)]
        
        [Display(Name = "Cst No")]
        public string CstNo { get; set; }

        [MaxLength(100)]
       
        [Display(Name = "Service Tax No")]
        public string ServiceTaxNo { get; set; }

        [MaxLength(100)]
       
        [Display(Name = "Pan No")]
        public string PanNo { get; set; }

        [MaxLength(50)]
        
        [Display(Name = "Tds Ac No")]
        public string TdsAcNo { get; set; }

        [MaxLength(50)]
      
        [Display(Name = "Company Reg No")]
        public string CompanyRegNo { get; set; }

        [MaxLength(500)]
       
        [Display(Name = "Factory Address")]
        public string FactoryAddress { get; set; }

        [MaxLength(100)]
       
        [Display(Name = "Factory Phone")]
        public string FactoryPhone { get; set; }

        [MaxLength(100)]
       
        [Display(Name = "Factory Fax")]
        public string FactoryFax { get; set; }

        [MaxLength(100)]
      
        [Display(Name = "Company Remark")]
        public string CompanyRemark { get; set; }

        [MaxLength(50)]
         [Display(Name = "Comp Para")]
        public string CompPara { get; set; }

        [MaxLength(50)]
        [Display(Name = "Division")]
        public string Division { get; set; }

        [MaxLength(50)]
        [Display(Name = "Comp Range")]
        public string CompRange { get; set; }

        [Display(Name = "Commission Rate")]
        public decimal? CommissionRate { get; set; }

    }
}
