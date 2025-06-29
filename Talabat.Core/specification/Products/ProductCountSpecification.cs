using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification.Products
{
    public class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductSpecParams productSpec)
            :base(
            p =>
            (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search))
            &&
            (!productSpec.brandId.HasValue || productSpec.brandId == p.BrandId)
            &&
            (!productSpec.categoryId.HasValue || productSpec.categoryId == p.CategoryId)
            )


        {

        }
            
    }
}
