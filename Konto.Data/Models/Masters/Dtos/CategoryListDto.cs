using System.ComponentModel.DataAnnotations;

namespace Konto.Data.Models.Masters.Dtos
{
    public class CategoryListDto: BaseDto
    {
        [Display(Name = "Cat Code")]
        public string CatCode { get; set; }

       
        [Display(Name = "Cat Name")]
        public string CatName { get; set; }

        
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}
