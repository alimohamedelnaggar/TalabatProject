using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.Orders;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Mapping.Orders
{
    public class OrderProfile:Profile
    {
        private readonly IConfiguration configuration;

        public OrderProfile(IConfiguration configuration)
        {
         CreateMap<Order,OrderToReturnDto>().ForMember(d=>d.DeliveryMethod,op=>op.MapFrom(s=>s.DeliveryMethod.ShortName)).ForMember(d=>d.DeliveryMethodCost,op=>op.MapFrom(s=>s.DeliveryMethod.Cost));  
            CreateMap<Address,OrderAddressDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().
                ForMember(d => d.ProductId, op => op.MapFrom(s => s.Prodcut.ProductId))
                .ForMember(d => d.ProductName, op => op.MapFrom(s => s.Prodcut.ProductName))
                .ForMember(d => d.PictureUrl, op => op.MapFrom(s => $"{configuration["BaseUrl"]}{s.Prodcut.PictureUrl}"));
this.configuration = configuration;
        }
    }
}
