using Store.G04.Core.Entities.Orderr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specification.Orders
{
    public class OrderSpecificationWithPAymentIntent : BaseSpecification<Order, int>

    {
        public OrderSpecificationWithPAymentIntent(string PaymentIntentId):base(O=>O.PaymentIntentId==PaymentIntentId)
        {
            Include.Add(O => O.DeliveryMethod);
            Include.Add(O => O.Items);
        }
        
    }
}
