﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konto.Data.Models.Masters.Dtos
{
    public class AddressDto
    {
        public virtual int Id { get; set; }

        public virtual int? AccId { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual int? CityId { get; set; }

        public virtual int? AreaId { get; set; }

        public virtual string PinCode { get; set; }

      

        public virtual string MobileNo { get; set; }

       
       
        public virtual string Email { get; set; }

      

        public virtual string AddressType { get; set; }

        

    }
}
