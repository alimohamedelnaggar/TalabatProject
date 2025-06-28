using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.specification;

namespace Talabat.Repository
{
    public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDesc != null)
            {
                query = query.OrderByDescending(specification.OrderByDesc);
            }
            if (specification.IsPagination)
            {
                query= query.Skip(specification.Skip).Take(specification.Take);
            }
            query = specification.Includes.Aggregate(query, (currentQuery, includes) => currentQuery.Include(includes));

            return query;
        }


    }
}
