using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class AcGroup
    {
        public AcGroup()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "acgroup id is required")]
        [Display(Name = "acgroup id")]
        public long acgroup_id { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Required(ErrorMessage = "acgroup is required")]
        [Display(Name = "acgroup")]
        public string acgroup { get; set; }

        [Display(Name = "undergroup id")]
        public long? undergroup_id { get; set; }

        [Required(ErrorMessage = "prio is required")]
        [Display(Name = "prio")]
        public int prio { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "remark")]
        public string remark { get; set; }

        [MaxLength(4)]
        [StringLength(4)]
        [Display(Name = "show in")]
        public string show_in { get; set; }

        [Required(ErrorMessage = "BL Sort is required")]
        [Display(Name = "BL Sort")]
        public int BLSort { get; set; }

        [Required(ErrorMessage = "Tb Sort is required")]
        [Display(Name = "Tb Sort")]
        public int TbSort { get; set; }

        [Required(ErrorMessage = "Only Total is required")]
        [Display(Name = "Only Total")]
        public bool OnlyTotal { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Transport Req")]
        public string TransportReq { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Agent Req")]
        public string AgentReq { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Address Req")]
        public string AddressReq { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Contact Req")]
        public string ContactReq { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Taxation Req")]
        public string TaxationReq { get; set; }

       // public List<account> Accounts { get; set; }

       

        //public List<voucher_group> VoucherGroups { get; set; }

        //public List<voucher_party> VoucherParties { get; set; }

        //public List<voucher_type> VoucherTypes { get; set; }

    }
}
