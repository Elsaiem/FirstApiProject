﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Entities
{
    public  class CustomerBusket
    {
        public string Id { get; set; }

        public List<BusketItem> Items { get; set; }

       public int? DeliveryMethodId { get; set; }

        public string? PaymentIntentId  { get; set; }

        public string? ClientSecret { get; set; }


    }
}
