using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.APIs.Error;
using Talabat.Core.Dtos.Basket;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400, "invalid id !!"));
             var basket=await basketRepository.GetBasketAsync(id);
            if (basket is null) new CustomerBasket() { Id=id};
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto customerBasket)
        {
            var basket=await basketRepository.UpdateBasketAsync(mapper.Map<CustomerBasket>(customerBasket));

            if (basket is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(basket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
             await basketRepository.DeleteBasketAsync(id);
        }
    }
}
