using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class CatalogListDto : BaseDto
    {
        [Display(Name = "CatalogNo")]
        public string CatalogNo { get; set; }
        [Display(Name = "Catalog Name")]
        public string CatalogName { get; set; }

       
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}
