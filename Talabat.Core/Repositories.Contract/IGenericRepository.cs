using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.specification;

namespace Talabat.Core.Repostories.Contract
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity?> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> specification);
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity,TKey> specification);
        Task<int> GetCountAsync(ISpecification<TEntity, TKey> specification);
        void Delete(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
    }
}
