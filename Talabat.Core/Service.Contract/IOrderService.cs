using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Service.Contract
{
    public interface IOrderService
    {
       Task<Order> CreateOrderAsync(string buyerEmail,string basketId,int deliveryMethodId,Address shippingAddress);
        Task<IEnumerable<Order>?> GetOrderForSpecificUserAsync(string buyerEmail);
        Task<Order?> GetOrderbyIdForSpecificUserAsync(string buyerEmail,int orderId);

    }
}
