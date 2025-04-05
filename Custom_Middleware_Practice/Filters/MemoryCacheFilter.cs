using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Custom_Middleware_Practice.Filters
{
    public class MemoryCacheFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheFilter(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheKey = context.HttpContext.Request.Path.ToString();

            if (_cache.TryGetValue(cacheKey, out var cachedResponse))
            {
                context.Result = new ObjectResult(cachedResponse);
                return;
            }

            var resultContext = await next();

            if (resultContext.Result is ObjectResult objectResult)
            {
                _cache.Set(cacheKey, objectResult.Value, TimeSpan.FromSeconds(30));
            }
        }
    }
    
    
}
