using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repostories.Contract;

namespace Talabat.Core
{
    public interface IUnitOfWork
    {
        public Task<int> CompleteAsync();

        public IGenericRepository<TEntity,TKey> Repository<TEntity, TKey>() where TEntity :BaseEntity<TKey>;
    }
}
