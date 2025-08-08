using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.specification.Order
{
    public class OrderSpecification:BaseSpecification<Core.Entities.Order.Order,int>
    {
        public OrderSpecification(string buyerEmail,int orderId):base(o=>o.BuyerEmail==buyerEmail&&o.Id==orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            
        }
        public OrderSpecification(string buyerEmail):base(o=>o.BuyerEmail==buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
            
        }
        
    }
}
