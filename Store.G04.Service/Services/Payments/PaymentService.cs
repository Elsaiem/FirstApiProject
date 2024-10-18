using Microsoft.Extensions.Configuration;
using Store.G04.Core;
using Store.G04.Core.Dtos.Busket;
using Store.G04.Core.Entities;
using Store.G04.Core.Entities.Orderr;
using Store.G04.Core.ServicesContract;
using Store.G04.Core.Specification.Orders;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.G04.Core.Entities.Product;


namespace Store.G04.Service.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IBusketService _busketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBusketService busketService,IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            this._busketService = busketService;
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
        }

        public async Task<CustomerBusketDto> CreateOrUpdatePaymentIntentIdAsync(string busketId)
        {

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            //get Busket
            var busket= await _busketService.GetBusketAsync(busketId);
            if (busket is null) { return null; }
            var shippingPrice = 0m;
            var subTotal=0m;
            if (busket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(busket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            }
            if (busket.Items.Count > 0)
            {
                foreach (var item in busket.Items)
                {
                 var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.Id);
                   if(product.Price != item.Price)
                    {
                        item.Price= product.Price;
                    }
                }
                subTotal= busket.Items.Sum(I=>I.Price*I.Quantity);


            }
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(busket.PaymentIntentId)) {
                //create
                var option=new PaymentIntentCreateOptions()
                {
                    Amount=(long)(subTotal *100+shippingPrice*100),
                    PaymentMethodTypes=new List<string>() { "card"},
                    Currency="usd",


                };
              paymentIntent=await service.CreateAsync(option);
                busket.PaymentIntentId = paymentIntent.Id;
                busket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                //Update
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100),
                   
                };
                paymentIntent = await service.UpdateAsync(busket.PaymentIntentId,option);
                busket.PaymentIntentId = paymentIntent.Id;
                busket.ClientSecret = paymentIntent.ClientSecret;
            }

             busket= await  _busketService.UpdateBusketAsync(busket);
            if (busket is null) { return null; }
            return busket;


        }

        public async Task<Order> UpdatePaymentIntentForSucceedOrFailed(string paymentIntentId, bool flag)
        {
            var spec = new OrderSpecificationWithPAymentIntent(paymentIntentId);
            var order= await   _unitOfWork.Repository<Order,int>().GetWithSpecAsync(spec);
            if (order == null)
            {
                // Handle the case when no order is found for the given paymentIntentId
                throw new Exception($"No order found with PaymentIntentId: {paymentIntentId}");
            }
            if (flag)
            {
                order.Status = OrderStatus.PaymentReceived;
            }
            else
            {
                order.Status=OrderStatus.PaymentFailed;
            }

            _unitOfWork.Repository<Order, int>().Update(order);
           
            await _unitOfWork.CompleteAsync();
            
            return order;

        }
    }
}
