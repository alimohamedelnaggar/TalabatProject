using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string id);
        public Task<bool> DeleteBasketAsync(string id);
        public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

    }
}
