using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class IssueTrans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "issuetranid is required")]
        public long issuetranid { get; set; }

        [Required(ErrorMessage = "issuemasterid is required")]
        public long issuemasterid { get; set; }

        public long? productionid { get; set; }

        [Required(ErrorMessage = "companyid is required")]
        public short companyid { get; set; }

        [Required(ErrorMessage = "grossweight is required")]
        public decimal grossweight { get; set; }

        [Required(ErrorMessage = "netweight is required")]
        public decimal netweight { get; set; }

        [Required(ErrorMessage = "ends is required")]
        public decimal ends { get; set; }

        [Required(ErrorMessage = "item id is required")]
        public long item_id { get; set; }

        [MaxLength(100)]
        public string remarks { get; set; }

        public decimal? Rate { get; set; }

        public long? UnitID { get; set; }

        public decimal? Amount { get; set; }

        public decimal? formulaPer { get; set; }

        public decimal? FormulaQty { get; set; }

        [MaxLength(25)]
        public string MergeNo { get; set; }

        [MaxLength(25)]
        public string Base { get; set; }

        [ForeignKey("issuemasterid")]
        public Issue Issue { get; set; }
    }
}