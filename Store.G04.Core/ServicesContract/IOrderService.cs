using Store.G04.Core.Entities.Orderr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.ServicesContract
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail,string basketId,int DeliveryMethodId,AddressOrder ShippingAddress);

        Task<IEnumerable<Order>?> GetOrdersForSpecifiUserAsync(string BuyerEmail);
        Task<Order?> GetOrderIdForSpecifiUserAsync(string BuyerEmail,int OrderId);



    }
}
