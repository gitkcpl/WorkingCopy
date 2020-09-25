﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class AreaLookupDto
    {
        [Key]
        public int Id { get; set; }
        public string DisplayText { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
