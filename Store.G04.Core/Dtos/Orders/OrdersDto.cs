using Store.G04.Core.Entities.Orderr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Dtos.Orders
{
    public class OrdersDto
    {
      public string BusketId { get; set; }

        public int DeliveryMethodId { get; set; }

        public AddressOrderDto ShipAddress { get; set; }





    }
}
