using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class SerialBatchDto 
    {
        public int Id { get; set; }

        [Required][MaxLength(50)][MinLength(2)]
        public string SerialNo { get; set; }

        public int RefId { get; set; }

        public int RefTransId { get; set; }

        public int RefVoucherId { get; set; }

        public bool IsAcitve { get; set; }
        public bool IsDeleted { get; set; }
    }
}
