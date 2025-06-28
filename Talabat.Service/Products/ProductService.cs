using AutoMapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Service.Contract;
using Talabat.Core.specification.Products;

namespace Talabat.Service.ProductsService
{
    public class ProductService:IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort, int? brandId, int? categoryId, int? pageSize, int? pageIndex)
        {
            var specification = new ProductSpecification(sort, brandId, categoryId, pageSize,pageIndex);
           return mapper.Map<IEnumerable<ProductDto>>(await unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(specification));
            
        }
        public async Task<ProductDto> GetProductAsync(int id)
        {
            var specification = new ProductSpecification(id);
            return mapper.Map<ProductDto>(await unitOfWork.Repository<Product, int>().GetWithSpecAsync(specification));
            
        }
        public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync()
        {
            return await unitOfWork.Repository<ProductCategory, int>().GetAllAsync();
        }
        public Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            return unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
        }
    }
}
