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
        public ProductSpecification(string? sort, int? brandId, int? categoryId, int? pageSize, int? pageIndex) :base(
            p=>
            (!brandId.HasValue || brandId==p.BrandId)
            &&
            (!categoryId.HasValue || categoryId==p.CategoryId)
            )
        {

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
        }
        public void ApplyIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
