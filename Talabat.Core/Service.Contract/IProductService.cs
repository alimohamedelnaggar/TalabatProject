using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.specification.Products;

namespace Talabat.Core.Service.Contract
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductSpecParams @params);
        public Task<ProductDto> GetProductAsync(int id);
        public Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
        public Task<IEnumerable<ProductBrand>> GetBrandsAsync();
    }
}
