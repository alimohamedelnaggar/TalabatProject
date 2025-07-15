using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Attributes
{
    public class CacheAttribute:Attribute //IAsyncActionFilter  
    {
        private readonly int expireTime;

        public CacheAttribute(int expireTime)
        {
            this.expireTime = expireTime;
        }

        //public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    var cacheService= context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
        //    cacheService.GetCacheKeyAsync("");
        //}
        //private string GenerateCacheKeyFromRequest(HttpRequest request)
        //{
        //    var cacheKey = new StringBuilder();
        //    cacheKey.Append($"{request.Path}");
        //    foreach (var item in )
        //    {
                
        //    }
        //}
    }
}
