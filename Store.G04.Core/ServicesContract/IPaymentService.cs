using Store.G04.Core.Dtos.Busket;
using Store.G04.Core.Entities;
using Store.G04.Core.Entities.Orderr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.ServicesContract
{
    public interface IPaymentService
    {
       Task<CustomerBusketDto>  CreateOrUpdatePaymentIntentIdAsync(string busketId);

       Task<Order> UpdatePaymentIntentForSucceedOrFailed(string paymentIntentId,bool flag);

    }
}
