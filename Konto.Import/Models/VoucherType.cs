using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Keysoft.Erp.Data.Models
{
    [Table("voucher_type")]
    public class VoucherType
    {
        [Key]
        [Required(ErrorMessage = "vouchertype id is required")]
        public long vouchertype_id { get; set; } // bigint, not null

        [MaxLength(50)]
        public string vouchertype_name { get; set; } // varchar(50), null

        [Required(ErrorMessage = "prio is required")]
        public int prio { get; set; } // int, not null

        [MaxLength(5)]
        public string SortName { get; set; } // varchar(5), null

        public long? AcGroupID { get; set; } // bigint, null

        public ICollection<Voucher> Voucher { get; set; }
    }

}
