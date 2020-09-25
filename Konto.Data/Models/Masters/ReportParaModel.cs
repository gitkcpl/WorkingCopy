using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters
{
    [Table("ReportPara")]
    public class ReportParaModel : AuditedEntity
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required]
        //[Key]
        //[Display(Name = "Id")]
        //public int Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required]
        //[Display(Name = "Row Id")]
        //public Guid RowId { get; set; }

        [Display(Name = "ReportId")]
        public int ReportId { get; set; }

        [MaxLength(50)]
        [Display(Name = "ParameterName")]
        public string ParameterName { get; set; }
        [Display(Name = "ParameterValue")]
        public int? ParameterValue { get; set; }

        //[Display(Name = "IsActive")]
        //public bool IsActive { get; set; }

        //[Display(Name = "Is Deleted")]
        //public bool IsDeleted { get; set; }

        //[Display(Name = "Create User")]
        //public string CreateUser { get; set; }

        //[Display(Name = "IPAddress")]
        //public string IPAddress { get; set; }

        //[Display(Name = "Modify User")]
        //public string ModifyUser { get; set; }

        //[Display(Name = "CreateDate")]
        //public DateTime CreateDate { get; set; }

        //[Display(Name = "ModifyDate")]
        //public DateTime? ModifyDate { get; set; }
    }
}
