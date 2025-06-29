using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Helper;
using Talabat.Core.Repostories.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Core.specification.Products;

namespace Talabat.APIs.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        
        //private readonly IGenericRepository<Product,int> genericRepository;
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            
            
            this.productService = productService;
        }
        // page size (number of product )
        // page index number of page 
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var products =await productService.GetAllProductsAsync(productSpec);
            return Ok(new PaginationResponse<ProductDto>(productSpec.pageSize,productSpec.pageIndex,0,products));
        }
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<Product>>> GetCategory()
        {
            var categories =await productService.GetCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("brand")]
        public async Task<ActionResult<IEnumerable<Product>>> GetBrand()
        {
            var brands =await productService.GetBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productService.GetProductAsync(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }
    }
}
