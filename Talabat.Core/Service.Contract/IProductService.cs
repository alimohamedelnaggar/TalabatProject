using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos;
using Talabat.Core.Entities;

namespace Talabat.Core.Service.Contract
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort, int? brandId, int? categoryId,int? pageSize,int? pageIndex);
        public Task<ProductDto> GetProductAsync(int id);
        public Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
        public Task<IEnumerable<ProductBrand>> GetBrandsAsync();
    }
}
