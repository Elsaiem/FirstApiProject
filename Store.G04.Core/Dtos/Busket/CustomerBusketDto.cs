using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Dtos.Busket
{
    public class CustomerBusketDto
    {
        public string Id { get; set; }

        public List<BusketItem> Items { get; set; }




    }
}
