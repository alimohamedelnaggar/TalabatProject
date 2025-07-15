using AutoMapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Dtos.Product;
using Talabat.Core.Entities;
using Talabat.Core.Helper;
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

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec)
        {
            var specification = new ProductSpecification(productSpec);
           var result= mapper.Map<IEnumerable<ProductDto>>(await unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(specification));
            var countspec = new ProductCountSpecification(productSpec);
            var count = await unitOfWork.Repository<Product,int>().GetCountAsync(countspec);
            return new PaginationResponse<ProductDto>(productSpec.pageSize, productSpec.pageIndex, count, result);
            
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
