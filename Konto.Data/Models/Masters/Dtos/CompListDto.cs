using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class CompListDto
    {
        [MaxLength(75)]
        [Display(Name = "Comp Name")]
        public virtual string CompName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address1")]
        public virtual string Address1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address2")]
        public virtual string Address2 { get; set; }

        [MaxLength(10)]
        [Display(Name = "Mobile")]
        public virtual string Mobile { get; set; }

        [MaxLength(20)]
        [Display(Name = "Gst In")]
        public virtual string GstIn { get; set; }

        [MaxLength(25)]
        [Display(Name = "Ac No")]
        public virtual string AcNo { get; set; }

        [MaxLength(50)]
        [Display(Name = "Bank Name")]
        public virtual string BankName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Ifs Code")]
        public virtual string IfsCode { get; set; }

        public virtual int Id { get; set; }
    }
}
