using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Attributes;
using Talabat.APIs.Error;
using Talabat.Core.Dtos.Product;
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
        [ProducesResponseType(typeof(PaginationResponse<ProductDto>),200)]
        [HttpGet]
        [Cache(100)]
        [Authorize]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var products =await productService.GetAllProductsAsync(productSpec);
            return Ok(products);
        }
        [ProducesResponseType(typeof(IEnumerable<ProductCategory>), 200)]
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<Product>>> GetCategory()
        {
            var categories =await productService.GetCategoriesAsync();
            return Ok(categories);
        }
        [ProducesResponseType(typeof(IEnumerable<ProductBrand>), 200)]
        [HttpGet("brand")]
        public async Task<ActionResult<IEnumerable<Product>>> GetBrand()
        {
            var brands =await productService.GetBrandsAsync();
            return Ok(brands);
        }
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productService.GetProductAsync(id);
            if (product is null)
                return NotFound(new ApiErrorResponse(404));
            return Ok(product);
        }
    }
}
