using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repostories.Contract;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository
{
   public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TalabatDbContext _dbContext;

        public GenericRepository(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>) await _dbContext.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
           return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
