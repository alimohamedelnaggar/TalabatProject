using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Core.specification.Order;

namespace Talabat.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;

        public OrderService(IUnitOfWork unitOfWork,IBasketRepository basketRepository)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
        }

        public async Task<Core.Entities.Order.Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket=await basketRepository.GetBasketAsync(basketId);
            if (basket is null) return null;
            var orderItems=new List<OrderItem>();
            if (basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product=await unitOfWork.Repository<Product,int>().GetAsync(item.Id);
                    var productOrderItem = new ProdcutItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productOrderItem, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }
           var deliveryMethod=await unitOfWork.Repository<DeliveryMethod, int>().GetAsync(deliveryMethodId);

            var subTotal=orderItems.Sum(I=>I.Price*I.Quantity);

            var order=new Core.Entities.Order.Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal,"");
            await unitOfWork.Repository<Core.Entities.Order.Order, int>().AddAsync(order);
            var result= await unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;

        }

        public async Task<Core.Entities.Order.Order?> GetOrderbyIdForSpecificUserAsync(string buyerEmail, int orderId)
        {
            var spec=new OrderSpecification(buyerEmail,orderId);
           var order= await unitOfWork.Repository<Core.Entities.Order.Order,int>().GetWithSpecAsync(spec);
            if (order == null) return null;
            return order;
        }

        public async Task<IEnumerable<Core.Entities.Order.Order>?> GetOrderForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
           var order=await unitOfWork.Repository<Core.Entities.Order.Order, int>().GetAllWithSpecAsync(spec);
            if (order == null) return null;
            return order;
        }
    }
}
