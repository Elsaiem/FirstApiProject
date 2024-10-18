using Store.G04.Core;
using Store.G04.Core.Entities;
using Store.G04.Core.Entities.Orderr;
using Store.G04.Core.RepositoreContract;
using Store.G04.Core.ServicesContract;
using Store.G04.Core.Specification.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Orderrs
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IBusketService _busketService;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork,IBusketService busketService,IPaymentService paymentService)
        {
            _UnitOfWork = unitOfWork;
            this._busketService = busketService;
            this._paymentService = paymentService;
        }

         

        public async Task<Order> CreateOrderAsync(string BuyerEmail, string basketId, int DeliveryMethodId, AddressOrder ShippingAddress)
        {
            var DeliveryMethod = await _UnitOfWork.Repository<DeliveryMethod, int>().GetAsync(DeliveryMethodId);

            var Basket= await _busketService.GetBusketAsync(basketId);
            if (Basket == null) return null;

            var orderItems= new List<OrderItem>();

            if (Basket.Items.Count() > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product=await _UnitOfWork.Repository<Product, int>().GetAsync(item.Id);
                    var ProductOrderItem = new ProductItemOrder(product.Id, product.Name,product.PictureUrl);
                    var orderItem=new OrderItem(ProductOrderItem,product.Price,item.Quantity);

                    orderItems.Add(orderItem);
                }

            }

            var subtotal=orderItems.Sum(I=>I.Price*I.Quantity);

            //Todo
            if (!string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var spec = new OrderSpecificationWithPAymentIntent(Basket.PaymentIntentId);
                var ExOrder =await  _UnitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
                _UnitOfWork.Repository<Order, int>().Delete(ExOrder);
            }


            
            var basketDto=  await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);


            var order = new Order(BuyerEmail,ShippingAddress,DeliveryMethod,orderItems,subtotal,basketDto.PaymentIntentId);

          await  _UnitOfWork.Repository<Order, int>().AddAsync(order);
           var result=  await _UnitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
           
        }

        public async Task<IEnumerable<Order>> GetOrdersForSpecifiUserAsync(string BuyerEmail)
        {
               var spec= new OrderSpecification(BuyerEmail);


             var orders= await _UnitOfWork.Repository<Order, int>().GetAllWirhSpecAsync(spec);
              if (orders == null) return null;
              return orders;
           
        }

        public async Task<Order?> GetOrderIdForSpecifiUserAsync(string BuyerEmail, int OrderId)
        {
            var spec= new OrderSpecification(BuyerEmail, OrderId);


             var order= await _UnitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
            if (order == null) return null;
            return order;
        }
    }
}
