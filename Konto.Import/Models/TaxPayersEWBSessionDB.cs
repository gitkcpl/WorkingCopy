using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keysoft.Erp.Data.Models
{
    [Table("TaxPayersEWBSessionDB")]
    public class TaxPayersEWBSessionDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public long CompanyId { get; set; }

        [MaxLength(50)]
        public string EwbGstin { get; set; }

        [MaxLength(50)]
        public string EwbUserID { get; set; }

        [MaxLength(50)]
        public string EwbPassword { get; set; }

        [MaxLength(50)]
        public string EwbAppKey { get; set; }

        [MaxLength(50)]
        public string EwbAuthToken { get; set; }

        [MaxLength(50)]
        public string EwbSEK { get; set; }

        public DateTime? EwbTokenExp { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [MaxLength(50)]
        public string AppKey { get; set; }

        [MaxLength(50)]
        public string AuthToken { get; set; }

        [MaxLength(50)]
        public string SEK { get; set; }

        public DateTime? TokenExp { get; set; }
    }

}
