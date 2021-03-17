using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class ColorMathingListDto
    {
        public DateTime Date { get; set; }
        
        public string ColorName { get; set; }
        public int? ColorId { get; set; }
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int? ItemId { get; set; }
    }
}
