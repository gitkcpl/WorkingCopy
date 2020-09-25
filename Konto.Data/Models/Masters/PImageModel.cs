using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Konto.Data.Models.Masters
{
    [Table("PImage")]
    [Serializable]
    public class PImageModel : AuditedEntity
    {
        public PImageModel()
        {
           
            this.IsActive = true;
        }

        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [MaxLength(1)]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [MaxLength(500)]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [MaxLength(100)]
        [Display(Name = "Extra1")]
        public string Extra1 { get; set; }

        [MaxLength(50)]
        [Display(Name = "Extra2")]
        public string Extra2 { get; set; }

        public byte[] Img
        {
            get
            {
                if (ImagePath == null) return null;
                Image img1 = Image.FromFile(ImagePath);
                return (byte[])(new ImageConverter()).ConvertTo(img1, typeof(byte[]));
            }
        }
    }
}
