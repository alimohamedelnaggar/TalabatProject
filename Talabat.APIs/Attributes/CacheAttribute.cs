using Microsoft.AspNetCore.Mvc.Filters;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Attributes
{
    public class CacheAttribute:Attribute   
    {
        private readonly int expireTime;

        public CacheAttribute(int expireTime)
        {
            this.expireTime = expireTime;
        }

        
    }
}
