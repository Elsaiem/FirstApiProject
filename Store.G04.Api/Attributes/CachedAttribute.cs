using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.G04.Core.ServicesContract;
using System.Text;

namespace Store.G04.Api.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {

        private readonly int _ExpireTime;
        public CachedAttribute(int expireTime)
        {
            _ExpireTime = expireTime;




        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          var cacheService= context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse=await cacheService.GetCacheKeyAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "appliction/json",
                    StatusCode = 200,
                };
                context.Result=contentResult;
                return;
            }
            var executedContext=await next();
            if (executedContext.Result is OkObjectResult response) {
            
            
            await cacheService.SetCacheKeyAsync(cacheKey, response.Value,TimeSpan.FromSeconds(_ExpireTime));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var cacheKey = new StringBuilder();
            cacheKey.Append($"request:{request.Path}");
            foreach (var (key,value)  in request.Query.OrderBy(X=>X.Key)) {
                cacheKey.Append($"|{key}-{value}");
            
            
            }
            return cacheKey.ToString();

        }
    }
}
