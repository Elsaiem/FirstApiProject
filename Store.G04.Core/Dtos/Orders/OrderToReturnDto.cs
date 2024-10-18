using Store.G04.Core.Entities.Orderr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Dtos.Orders
{
    public class OrderToReturnDto
    {
      
        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public string Status { get; set; } 
        public AddressOrderDto ShipingAddress { get; set; }

      //  public int DeliveryMethodId { get; set; }//FK

        public string DeliveryMethod { get; set; }
        public int DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }

        public decimal SubTotal { get; set; }

      public decimal Total { get; set; }    

        public string PaymentIntentId { get; set; }=string.Empty;




    }
}
