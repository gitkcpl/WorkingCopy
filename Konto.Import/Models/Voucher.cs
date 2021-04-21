using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Keysoft.Erp.Data.Models
{
    [Table("voucher")]
    public class Voucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "voucher id is required")]
        public long voucher_id { get; set; } // bigint, not null

        [MaxLength(50)]
        [Required(ErrorMessage = "voucher name is required")]
        public string voucher_name { get; set; } // varchar(50), not null

        [MaxLength(50)]
        public string print_name { get; set; } // varchar(50), null

        [Required(ErrorMessage = "vouchertype id is required")]
        public long vouchertype_id { get; set; } // bigint, not null

        [MaxLength(5)]
        public string sort_name { get; set; } // varchar(5), null

        public long? account_id { get; set; } // bigint, null

        [Required(ErrorMessage = "numbering method is required")]
        public byte numbering_method { get; set; } // tinyint, not null

        [Required(ErrorMessage = "numbering type is required")]
        public byte numbering_type { get; set; } // tinyint, not null

        public long? start_from { get; set; } // bigint, null

        public short? voucher_width { get; set; } // smallint, null

        [Required(ErrorMessage = "prefill with zero is required")]
        public bool prefill_with_zero { get; set; } // bit, not null

        [Required(ErrorMessage = "print after save is required")]
        public bool print_after_save { get; set; } // bit, not null

        [Required(ErrorMessage = "regenrate is required")]
        public byte regenrate { get; set; } // tinyint, not null

        [Required(ErrorMessage = "company id is required")]
        public long company_id { get; set; } // bigint, not null

        [MaxLength(1)]
        public string TaxType { get; set; } // char(1), null

        public VoucherType VoucherType { get; set; }
    }


}
