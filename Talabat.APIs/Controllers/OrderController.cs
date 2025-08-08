using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.APIs.Error;
using Talabat.Core;
using Talabat.Core.Dtos.Orders;
using Talabat.Core.Entities.Order;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{
    
    public class OrderController : BaseApiController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IOrderService orderService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var userEmail= User.FindFirstValue(ClaimTypes.Email);
            if(userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var address=mapper.Map<Address>(orderDto.ShippingAddress);
           var order=await orderService.CreateOrderAsync(userEmail, orderDto.BasketId,orderDto.DeliveryMethodId,address);
            if (order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(mapper.Map<OrderToReturnDto>(order));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrderForSpec()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var orders=await orderService.GetOrderForSpecificUserAsync(userEmail);
            if (orders is null) return BadRequest();
            return Ok(mapper.Map<IEnumerable<OrderToReturnDto>>(orders));
        }
        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderForSpec(int orderId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order=await orderService.GetOrderbyIdForSpecificUserAsync(userEmail,orderId);
            if (order is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(mapper.Map<OrderToReturnDto>(order));
        }

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveries=await unitOfWork.Repository<DeliveryMethod,int>().GetAllAsync();

            if (deliveries is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(deliveries);
        }
    }
}
