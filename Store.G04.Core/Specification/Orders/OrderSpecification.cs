using Store.G04.Core.Entities.Orderr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specification.Orders
{
    public class OrderSpecification : BaseSpecification<Order, int>
    {
        public OrderSpecification(string buyerEmail,int orderId)
               :base(O=>O.BuyerEmail==buyerEmail && O.Id==orderId)
        {

            Include.Add(O => O.DeliveryMethod);
            Include.Add(O=>O.Items);


        } public OrderSpecification(string buyerEmail)
               :base(O=>O.BuyerEmail==buyerEmail)
        {

            Include.Add(O => O.DeliveryMethod);
            Include.Add(O=>O.Items);


        }
    }
}
