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
            (!productSpec.brandId.HasValue || productSpec.brandId == p.BrandId)
            &&
            (!productSpec.categoryId.HasValue || productSpec.categoryId == p.CategoryId)
            )


        {

        }
            
    }
}
