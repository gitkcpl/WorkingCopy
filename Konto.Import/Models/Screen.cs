using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysoft.Erp.Data.Models
{
    public class Screen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Screen ID is required")]
        public long ScreenID { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Screen Name is required")]
        public string ScreenName { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        public int Priority { get; set; }


    }
}
