using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repostories.Contract;

namespace Talabat.APIs.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        
        private readonly IGenericRepository<Product> genericRepository;

        public ProductsController(IGenericRepository<Product> genericRepository)
        {
            
            this.genericRepository = genericRepository;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products =await genericRepository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await genericRepository.GetAsync(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }
    }
}
