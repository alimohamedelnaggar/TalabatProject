using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repostories.Contract;
using Talabat.Core.specification;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly TalabatDbContext _dbContext;

        public GenericRepository(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
                return (IEnumerable<TEntity>)await _dbContext.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator<TEntity,TKey>.GetQuery(_dbContext.Set<TEntity>(),specification).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            if (typeof(TEntity) == typeof(Product))
                return await _dbContext.Products.Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id) as TEntity;


            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetWithSpecAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();
        }
    }
}
