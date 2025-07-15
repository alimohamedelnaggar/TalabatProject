using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification.Products
{
    public class ProductSpecification : BaseSpecification<Product, int>
    {
        public ProductSpecification(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
        public ProductSpecification(ProductSpecParams productSpec) : base(
            p =>
            (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search))
            &&
            (!productSpec.brandId.HasValue || productSpec.brandId == p.BrandId)
            &&
            (!productSpec.categoryId.HasValue || productSpec.categoryId == p.CategoryId)
            )
        {

            if (!string.IsNullOrEmpty(productSpec.sort))
            {
                switch (productSpec.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }

            ApplyIncludes();
            // 900
            // page size 50
            // page index 3
            ApplyPagination(productSpec.pageSize * (productSpec.pageIndex - 1),productSpec.pageSize);
        }
        public void ApplyIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
