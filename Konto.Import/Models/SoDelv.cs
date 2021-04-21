using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keysoft.Erp.Data.Models
{
    public class SoDelv
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SoDelvID { get; set; }

        public long? SoID { get; set; }

        public long? PartyID { get; set; }

        [MaxLength(200)]
        public string DelvAddress { get; set; }

        [MaxLength(100)]
        public string Remarks { get; set; }

        public DateTime? DelvDate { get; set; }

        public decimal? Qty { get; set; }

        [ForeignKey("SoID")]
        public So So { get; set; }

    }
}