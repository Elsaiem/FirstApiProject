using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.G04.Core.Dtos.Orders;
using Store.G04.Core.Entities.Orderr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {
         public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrderToReturnDto>()
                      .ForMember(D=>D.DeliveryMethod,options=>options.MapFrom(s=>s.DeliveryMethod.ShortName))
                      .ForMember(D=>D.DeliveryMethodCost,options=>options.MapFrom(s=>s.DeliveryMethod.Cost));

            CreateMap<AddressOrder,AddressOrderDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                     .ForMember(O => O.ProductName, options => options.MapFrom(OI => OI.Product.ProductName))
                     .ForMember(O => O.ProductId , options => options.MapFrom(OI => OI.Product.ProductId))
                     .ForMember(O => O.PictureUrl, options => options.MapFrom(OI => $" {configuration["BaseURL"]}{OI.Product.PictureUrl}"));
 


        }
    }
}
