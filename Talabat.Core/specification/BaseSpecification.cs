using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;

        public List<Expression<Func<TEntity, object>>> Includes { get; set; } =new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDesc { get; set; } = null;
        public int Skip { get ; set; }
        public int Take { get ; set ; }
        public bool IsPagination { get; set; }

        public BaseSpecification(Expression<Func<TEntity,bool>> expression)
        {
            Criteria= expression;
        }
        public BaseSpecification()
        {
            
        }
        public void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }
        public void ApplyPagination(int skip,int take)
        {
            IsPagination = true;
            Skip= skip;
            Take= take;
        }
    }
}
