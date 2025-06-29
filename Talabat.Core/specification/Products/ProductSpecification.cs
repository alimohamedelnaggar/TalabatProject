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
        public ProductSpecification(ProductSpecParams specParams) :base(
            p=>
            (!specParams.brandId.HasValue || specParams.brandId ==p.BrandId)
            &&
            (!specParams.categoryId.HasValue || specParams.categoryId == p.CategoryId)
            )
        {

            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch (specParams.sort)
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
            ApplyPagination(specParams.pageSize * (specParams.pageIndex - 1),specParams.pageSize);
        }
        public void ApplyIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
