using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repostories.Contract;
using Talabat.Repository.Data.Contexts;

namespace Talabat.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly TalabatDbContext dbContext;
        private readonly Hashtable repositories;
        public UnitOfWork(TalabatDbContext dbContext)
        {
            this.dbContext = dbContext;
            repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await dbContext.SaveChangesAsync();

        public IGenericRepository<TEntity,TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (!repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity,TKey>(dbContext);
                repositories.Add(type, repository);
            }
            return repositories[type] as IGenericRepository<TEntity,TKey> ;
        }
    }
}
