﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Konto.Data.Models.Masters
{
    [Table("ReportType")]
    public class ReportTypeModel 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Row Id")]
        public Guid RowId { get; set; }

        [MaxLength(100)]
        [Display(Name = "ReportName")]
        public string ReportName { get; set; }

        [MaxLength(500)]
        [Display(Name = "ReportTypes")]
        public string ReportTypes { get; set; }

        [Display(Name = "VoucherTypeId")]
        public int? VoucherTypeId { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [MaxLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [MaxLength(500)]
        [Display(Name = "FileName")]
        public string FileName { get; set; }

        [Display(Name = "Create User")]
        public string CreateUser { get; set; }

        [Display(Name = "Modify User")]
        public string ModifyUser { get; set; }

        [Display(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "ModifyDate")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "SpName")]
        public string SpName { get; set; }

        [NotMapped]
        public string VoucherTypeName { get; set; }
    }
}
