﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Entities
{
    public class BusketItem
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }











    }
}