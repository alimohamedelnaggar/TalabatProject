using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.Order
{
    public class OrderService : IOrderService
    {
        public Task<Core.Entities.Order.Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Entities.Order.Order?> GetOrderbyIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Entities.Order.Order>?> GetOrderForSpecificUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
